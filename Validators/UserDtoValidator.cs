using FeedsWebApi.Data;
using FeedsWebApi.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FeedsWebApi.Validators;

public interface IUserDtoValidator
{
    Task ValidateCreate(UserCreateDto dto);
    Task ValidateUpdate(int id, UserUpdateDto dto);
}

public class UserDtoValidator : IUserDtoValidator
{
    private readonly AppDbContext _context;

    public UserDtoValidator(AppDbContext context)
    {
        _context = context;
    }

    public async Task ValidateCreate(UserCreateDto dto)
    {
        await ValidateCommon(dto);

        var isExistingUsername =
            await _context.Users.AnyAsync(u => u.Username == dto.Username);

        if (isExistingUsername)
            throw new ArgumentException("Username is taken.");
    }

    public async Task ValidateUpdate(int id, UserUpdateDto dto)
    {
        await ValidateCommon(dto);

        var isExistingUsername =
            await _context.Users.AnyAsync(u => u.Id != id && u.Username == dto.Username);

        if (isExistingUsername)
            throw new ArgumentException("Username is taken.");
    }

    private async Task ValidateCommon(UserBaseDto dto)
    {
        if (dto == null)
            throw new ArgumentException("Request cannot be null.");

        if (string.IsNullOrEmpty(dto.Username))
            throw new ArgumentException("Username is required.");
    }
}
