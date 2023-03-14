using System.ComponentModel.DataAnnotations;

namespace Ecm.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; internal set; }

        public Reviewer Reviewer { get; set; }
        public Pokemon Pokemon { get; set; }
    }
}
