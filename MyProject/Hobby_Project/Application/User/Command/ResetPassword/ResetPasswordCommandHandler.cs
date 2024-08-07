﻿using Application.Logger;
using Application.Repositories;
using HobbyProject.Application.Helpers;
using MediatR;

namespace HobbyProject.Application.User.Command.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _log;

        public ResetPasswordCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _log = SingletonLogger.Instance;
        }

        public async Task<string> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            try
            {
               var newToken = command.EmailToken.Replace(" ", "+");

               var user = await _unitOfWork.UserRepository.GetByEmail(command.Email);
               if (user == null) throw new NullReferenceException("User with that email does not exists!");

                var token = user.ResetPasswordToken;
                var emailTokenExpiry = user.ResetPasswordExpiry;

                if(token != command.EmailToken || emailTokenExpiry < DateTime.Now) throw new Exception("Token is expired");

                user.Password = PasswordHasher.HashPassword(command.NewPassword);

                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.Save();

                return token;
            }
            catch(Exception e)
            {
                _log.LogError(e.Message);
                throw;
            }
        }
    }
}
