﻿using Application.Logger;
using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Hobby.Commands.Edit
{
    public class EditHobbyCommandHandler : IRequestHandler<EditHobbyCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private ILog _log;
        private IMapper _mapper;

        public EditHobbyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _log = SingletonLogger.Instance;
            _mapper = mapper;
        }

        public async Task<int> Handle(EditHobbyCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (command == null) throw new NullReferenceException("Edit hobby command is null");
                Domain.Entity.HobbyEntity hobbyArticle = _mapper.Map<Domain.Entity.HobbyEntity>(command);
                await _unitOfWork.HobbyArticleRepository.Update(hobbyArticle);
                await _unitOfWork.Save();
                return await Task.FromResult(command.Id);
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
                return await Task.FromResult(0);
            }
        }
    }
}
