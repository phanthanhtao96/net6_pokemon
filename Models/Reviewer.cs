namespace Ecm.Models
{
    public class Reviewer
    {
        public int Id { get; set; }
        public string FisrtName { get; set; }
        public string LastName { get; set; }

        public ICollection<Review> Reviews { get; set;}
        public string FirstName { get; internal set; }
    }
}
