﻿using MediatR;
using eBookStoreAPI.Application.ApiUtilities.Interfaces;

namespace eBookStoreAPI.Application.UserAuthentication.Command.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IUserService _userService;

    public LoginUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.LoginAsync(request.Username, request.Password);
    }
}
