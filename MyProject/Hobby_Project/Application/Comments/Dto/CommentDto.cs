﻿namespace HobbyProject.Application.Comments.Dto
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string CommentContent { get; set; }
        public string Username { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
