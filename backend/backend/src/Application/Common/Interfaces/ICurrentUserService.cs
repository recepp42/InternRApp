namespace backend.Application.Common.Interfaces;

public interface ICurrentUserService
{
    string? UserId { get; }
    string Role { get; }
}
