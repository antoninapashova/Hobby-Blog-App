﻿using Hobby_Project;
using HobbyProject.Application.Hobby.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Hobby.Queries
{
    public class HobbyDto 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public string HobbySubCategory { get; set; }
        public List<HobbyCommentDTO> HobbyComments { get; set; }
        public List<HobbyTagDto> Tags { get; set; }
    }
}
