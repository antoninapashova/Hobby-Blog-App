﻿using Application.Comments.Queries;
using Application.Logger;
using Application.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyProject.Application.Comments.Queries.GetCommentsByHobbyId
{
    public class GetCommentsByHobbyIdQueryHandler : IRequestHandler<GetCommentsByHobbyIdQuery, List<CommentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILog _log;

        public GetCommentsByHobbyIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILog log)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _log = log;
        }

        public async Task<List<CommentDto>> Handle(GetCommentsByHobbyIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.CommentRepository.GetCommentsByHobbyId(request.HobbyId);
                List<CommentDto> comments = _mapper.Map<List<CommentDto>>(result);
                return await Task.FromResult(comments);

            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
                throw;
            }
        }
    }
}