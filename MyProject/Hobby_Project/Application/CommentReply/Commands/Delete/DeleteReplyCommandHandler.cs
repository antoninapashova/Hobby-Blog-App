﻿using Application.Logger;
using Application.Repositories;
using Hobby_Project;
using HobbyProject.Domain.Entity;
using MediatR;

namespace HobbyProject.Application.CommentReply.Commands.Delete
{
    public class DeleteReplyCommandHandler : IRequestHandler<DeleteReplyCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _log;

        public DeleteReplyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _log = SingletonLogger.Instance;
        }

        public async Task<int> Handle(DeleteReplyCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var reply = await IsExist(command.Id);

                _unitOfWork.ReplyRepository.Delete(reply);
                await _unitOfWork.Save();

                return command.Id;
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
                throw;
            }
        }

        private async Task<Reply> IsExist(int id)
        {
            var reply = await _unitOfWork.ReplyRepository.GetByIdAsync(id);
            return reply ?? throw new NullReferenceException($"Comment with id: {id} does not exist!");
        }
    }
}
