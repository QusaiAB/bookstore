using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;

namespace WebApplication5.ViewsModels
{
    public class AutherBookRepo
    {
        public int BookId { get; set; }
        [Required]
        [StringLength (20 ,MinimumLength =5)]
        public string Titel { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 5)]
        public string Description { get; set; }

        public int AutherId { get; set; }
        public IFormFile File { get; set; }
        public string ImageUrl { get; set; }
        public List <Auther> auther { get; set; }
    }
}
