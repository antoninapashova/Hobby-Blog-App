﻿using Application.Logger;
using Application.Repositories;
using Domain.Entity;
using MediatR;
namespace Application.Hobby.Commands.Delete
{
    public  class DeleteHobbyCommandHandler : IRequestHandler<DeleteHobbyCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _log;

        public DeleteHobbyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _log = SingletonLogger.Instance;
        }

        public async Task<int> Handle(DeleteHobbyCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var hobby = await IsExist(command.Id);

                _unitOfWork.HobbyArticleRepository.Delete(hobby);
                await _unitOfWork.Save();

                return command.Id;
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
                throw;
            }
        }

        private async Task<HobbyEntity> IsExist(int id)
        {
            var hobby = await _unitOfWork.HobbyArticleRepository.GetByIdAsync(id);
            return hobby ?? throw new NullReferenceException($"Hobby with id: {id} does not exist!");
        }
    }
}
