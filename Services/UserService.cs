using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using ContosoPizza.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ContosoPizza.Services;

public class UserService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    public UserService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    // GET USER BY ID
    public async Task<User> GetById(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            throw new KeyNotFoundException($"The user with ID {id} was not found.");
        }
        return user;
    }

    //GET USER BY EMAIL
    public async Task<User> GetUserByEmail(string email)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.Email == email) ?? throw new KeyNotFoundException($"User with email {email} not found.");
    }

    // CREATE A USER
    public async Task CreateUser(User user)
    {
        var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Email == user.Email);
        if(existingUser != null)
        {
            throw new InvalidOperationException("Email not valid!");
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    // VERIFY USER
    public async Task<bool> VerifyUser(string email, string password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        if(user == null)
        {
            return false;
        }
        return BCrypt.Net.BCrypt.Verify(password, user.Password);
    }

    //GENERATE JWT TOKEN
    public string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


}
