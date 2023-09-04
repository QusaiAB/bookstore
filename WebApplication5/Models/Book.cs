using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }
        public Auther auther { get; set; }



    }
}
