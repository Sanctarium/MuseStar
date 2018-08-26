namespace MuseStar.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AbsoluteUrl { get; set; }
        public string DownloadUrl { get; set; }

        
        public int UserId { get; set; }
        public User User { get; set; }
    }
}