﻿using Application.Hobby.Queries;
using Hobby_Project;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyProject.Application.Hobby.Queries.GetAllUsers
{
    public class GetHobbyListQuery : IRequest<IEnumerable<HobbyDto>>
    {
    }
}
