namespace codecampster.Models
{
    public class Announcement
    {
        public int ID {get;set;}
        public string Message { get; set; }
        public int Rank { get; set; }
        public System.DateTime PublishOn { get; set; }
        public System.DateTime ExpiresOn { get; set; }
    }
}