﻿using Microsoft.AspNetCore.Http;

namespace HobbyProject.Application.Hobby.Commands
{
    public class PhotoDTO
    {
        public string Url { get; set; }
        //public IFormFile File { get; set; }
        public string Description { get; set; }
        public string PublicId { get; set; }
    }
}