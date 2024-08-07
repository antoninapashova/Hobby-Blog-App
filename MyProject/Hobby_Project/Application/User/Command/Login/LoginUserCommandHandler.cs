﻿using Application.Logger;
using Application.Repositories;
using HobbyProject.Application.Helpers;
using HobbyProject.Domain.Entity;
using MediatR;

namespace HobbyProject.Application.User.Command.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand,UserEntity>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly ITokenManager _tokenManager;

        public LoginUserCommandHandler(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
            _log = SingletonLogger.Instance;
        }

        public async Task<UserEntity> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByUsername(command.Username);

                if (!PasswordHasher.VerifyPassword(command.Password, user.Password)) 
                    throw new NullReferenceException("Password is incorect!");

                user.Token = _tokenManager.CreateJwtToken(user);
                var newAccessToken = user.Token;
                var newRefreshToken = _tokenManager.CreateRefreshToken();
                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiredTime = DateTime.Now.AddDays(5);
                await _unitOfWork.Save();

                return user;
            }
            catch(Exception e)
            {
                _log.LogError(e.Message);
                throw;
            }
        } 
    }
}
