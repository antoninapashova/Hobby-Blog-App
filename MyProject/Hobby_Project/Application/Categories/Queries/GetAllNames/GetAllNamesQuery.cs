﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyProject.Application.Categories.Queries.GetAllNames
{
     public class GetAllNamesQuery : IRequest<List<CategoryNameDto>>
    {
    }
}