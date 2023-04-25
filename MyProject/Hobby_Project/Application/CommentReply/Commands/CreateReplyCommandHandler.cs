﻿using Application.Logger;
using Application.Repositories;
using AutoMapper;
using Hobby_Project;
using HobbyProject.Application.Validators;
using HobbyProject.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HobbyProject.Application.CommentReply.Commands
{
    public class CreateReplyCommandHandler : IRequestHandler<CreateReplyCommand, Reply>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly IMapper _mapper;

        public CreateReplyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _log = SingletonLogger.Instance;
            _mapper = mapper;
        }

        public async Task<Reply> Handle(CreateReplyCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (command == null) throw new NullReferenceException("Create reply command is null!");
               
                Reply reply = _mapper.Map<Reply>(command);
                await _unitOfWork.ReplyRepository.Add(reply);
                await _unitOfWork.Save();
                return await Task.FromResult(reply);
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
                throw;
            }
        }
    }
}
