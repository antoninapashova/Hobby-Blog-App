﻿using Application.Logger;
using Application.Repositories;
using AutoMapper;
using Hobby_Project;
using HobbyProject.Application.Categories.Dto;
using MediatR;

namespace HobbyProject.Application.Categories.Queries.GetAllNames
{
    public class GetAllNamesQueryHandler : IRequestHandler<GetAllNamesQuery, IList<CategoryNameDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILog _log;

        public GetAllNamesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _log = SingletonLogger.Instance;
        }

        public async Task<IList<CategoryNameDto>> Handle(GetAllNamesQuery request, CancellationToken cancellationToken)
        {
            try
            {
               var categories = await _unitOfWork.CategoryRepository.GetAllNamesAsync();
               var categoryListVms = _mapper.Map<IList<CategoryNameDto>>(categories);
            
               return categoryListVms.ToList();
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
                throw;
            }
        }
    }
}
