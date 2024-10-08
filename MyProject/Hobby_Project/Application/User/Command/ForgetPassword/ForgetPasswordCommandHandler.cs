﻿using Application.Logger;
using Application.Repositories;
using HobbyProject.Application.Helpers;
using HobbyProject.Domain.Entity;
using MediatR;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace HobbyProject.Application.User.Command.ForgetPassword
{
    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EmailSettings _emailSettings;
        private readonly IEmailService _emailService;
        private readonly ILog _log;

        public ForgetPasswordCommandHandler(IUnitOfWork unitOfWork, IOptions<EmailSettings> emailSettings, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailSettings = emailSettings.Value;
            _emailService = emailService;
            _log = SingletonLogger.Instance; ;
        }

        public EmailSettings Get() => _emailSettings;

        public async Task<string> Handle(ForgetPasswordCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var user = await IsUserExist(command.Email);

                var tokenBytes = RandomNumberGenerator.GetBytes(64);
                var emailToken = Convert.ToBase64String(tokenBytes);

                user.ResetPasswordToken = emailToken;
                user.ResetPasswordExpiry = DateTime.Now.AddMinutes(15);

                string from = _emailSettings.From;
                var emailModel = new EmailModel(command.Email, "Reset password!", EmailBody.EmailStringBody(command.Email, emailToken));
                _emailService.SendEmail(emailModel);

                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.Save();

                return user.Email;
            }
            catch(Exception e)
            {
                _log.LogError(e.Message);
                throw;
            }
        }

         private async Task<UserEntity> IsUserExist(string email)
         {
            var isExists = await _unitOfWork.UserRepository.GetByEmail(email);
            return isExists ?? throw new NullReferenceException("User with that email already exists");
         }
    }
}
