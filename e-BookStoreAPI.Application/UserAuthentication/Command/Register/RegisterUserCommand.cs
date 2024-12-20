using MediatR;

namespace eBookStoreAPI.Application.UserAuthentication.Command.Register;

public record RegisterUserCommand(string Username, string Password) : IRequest<int>;
