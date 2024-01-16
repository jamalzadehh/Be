using Be.Models;
using Be.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Be.Models
{
    public class Team : BaseEntity
    {
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Facebooklink { get; set; }
        public string Twitterlink { get; set; }
        public string Googlelink { get; set;}
        public string ImgUrl { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }

    }
}
