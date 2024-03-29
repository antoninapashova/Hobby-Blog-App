﻿using Application.Logger;
using Application.Repositories;
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
                if (command == null) throw new NullReferenceException("Delete reply command is null");

                await _unitOfWork.ReplyRepository.DeleteAsync(command.Id);
                await _unitOfWork.Save();

                return command.Id;
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
                throw;
            }
        }
    }
}
