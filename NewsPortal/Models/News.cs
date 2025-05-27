using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Models
{
    public class News
    {
        public long? NewsID { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(5000)]
        public string Text { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ImgDirUrl { get; set; }
        [Required]
        public int CategoryID { get; set; }
        //public Category Category { get; set; }

        public int ReactionCount { get; set; } = 0;
    }
}
