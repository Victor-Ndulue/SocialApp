﻿using System.ComponentModel.DataAnnotations;

namespace SocialAppApi.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
