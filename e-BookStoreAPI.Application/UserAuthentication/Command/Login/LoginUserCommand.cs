using MediatR;

namespace eBookStoreAPI.Application.UserAuthentication.Command.Login;

public record LoginUserCommand(string Username, string Password) : IRequest<string>;
