using FeedsWebApi.Data;
using FeedsWebApi.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FeedsWebApi.Validators;

public interface IUserValidator
{
    Task ValidateCreate(UserCreateDto dto);
    Task ValidateUpdate(int id, UserUpdateDto dto);
}

public class UserValidator : IUserValidator
{
    private readonly AppDbContext _context;

    public UserValidator(AppDbContext context)
    {
        _context = context;
    }

    public async Task ValidateCreate(UserCreateDto dto)
    {
        var isExistingUsername =
            await _context.Users.AnyAsync(u => u.Username == dto.Username);

        if (isExistingUsername)
            throw new ArgumentException("Username is taken.");
    }

    public async Task ValidateUpdate(int id, UserUpdateDto dto)
    {
        var isExistingUsername =
            await _context.Users.AnyAsync(u => u.Id != id && u.Username == dto.Username);

        if (isExistingUsername)
            throw new ArgumentException("Username is taken.");
    }
}
