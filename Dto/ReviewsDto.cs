namespace Ecm.Dto
{
    public class ReviewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; internal set; }
    }
}
