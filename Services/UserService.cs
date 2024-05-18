using BCrypt.Net;
using ContosoPizza.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services;

public class UserService
{
    private readonly ApplicationDbContext _context;
    public UserService(ApplicationDbContext context)
    {
        _context = context;
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


}
