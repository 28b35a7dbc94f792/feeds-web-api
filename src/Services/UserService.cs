using FeedsWebApi.Data;
using FeedsWebApi.Dtos;
using FeedsWebApi.Factories;
using FeedsWebApi.Models;
using FeedsWebApi.Validators;
using Microsoft.EntityFrameworkCore;

namespace FeedsWebApi.Services;

public interface IUserService
{
    Task<UserResponseDto?> GetAsync(int id);
    Task<UserResponseDto?> CreateAsync(UserCreateDto dto);
    Task<UserResponseDto?> UpdateAsync(int id, UserUpdateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> LikeFeedAsync(int userId, int feedId);
    Task<bool> UnlikeFeedAsync(int userId, int feedId);
}

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IUserResponseDtoFactory _userResponseDtoFactory;
    private readonly IUserDtoValidator _userDtoValidator;
    private readonly ILikeValidator _likeValidator;

    public UserService(
        AppDbContext context,
        IUserResponseDtoFactory userResponseDtoFactory,
        IUserDtoValidator userDtoValidator,
        ILikeValidator likeValidator)
    {
        _context = context;
        _userResponseDtoFactory = userResponseDtoFactory;
        _userDtoValidator = userDtoValidator;
        _likeValidator = likeValidator;
    }

    public async Task<UserResponseDto?> GetAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return null;
        
        return _userResponseDtoFactory.Create(user);
    }

    public async Task<UserResponseDto?> CreateAsync(UserCreateDto dto)
    {
        await _userDtoValidator.ValidateCreate(dto);

        var user = new User
        {
            Username = dto.Username,
            Name = dto.Name
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return _userResponseDtoFactory.Create(user);
    }

    public async Task<UserResponseDto?> UpdateAsync(int id, UserUpdateDto dto)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return null;

        await _userDtoValidator.ValidateUpdate(id, dto);

        user.Username = dto.Username;
        user.Name = dto.Name;

        await _context.SaveChangesAsync();
    
        return _userResponseDtoFactory.Create(user);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return false;

        _context.Users.Remove(user);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> LikeFeedAsync(int userId, int feedId)
    {
        var likeExists = await _likeValidator.Validate(userId, feedId);

        if (likeExists)
            return false;

        var like = new Like
        {
            UserId = userId,
            FeedId = feedId
        };

        _context.Likes.Add(like);
        
        await _context.SaveChangesAsync();

        return true;
    }

        public async Task<bool> UnlikeFeedAsync(int userId, int feedId)
    {
        var like = await _context.Likes.FindAsync(userId, feedId);

        if (like == null)
            return false;

        _context.Likes.Remove(like);

        await _context.SaveChangesAsync();

        return true;
    }
}
