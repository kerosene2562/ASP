namespace NewsPortal.Models
{
    public class News
    {
        public long? NewsID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ImgDirUrl { get; set; }
        public string Category { get; set; }
    }
}
