using System.Text.Json.Serialization;

namespace NewsBlazor.Models
{
    public class News
    {
        [JsonPropertyName("newsID")]
        public long? NewsID { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("publicationDate")]
        public DateTime PublicationDate { get; set; }

        [JsonPropertyName("imgDirUrl")]
        public string ImgDirUrl { get; set; }

        [JsonPropertyName("categoryID")]
        public int CategoryID { get; set; }
    }


}
