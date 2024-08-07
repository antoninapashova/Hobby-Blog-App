﻿using Application.Logger;
using Application.Repositories;
using AutoMapper;
using Hobby_Project;
using MediatR;

namespace Application.Comments.Commands.Edit
{
    public class EditCommentCommandHandler : IRequestHandler<EditCommentCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly IMapper _mapper;

        public EditCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _log = SingletonLogger.Instance;
            _mapper = mapper;
        }

        public async Task<int> Handle(EditCommentCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var hobbyComment = _mapper.Map<Comment>(command);
                _unitOfWork.CommentRepository.Update(hobbyComment);
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
