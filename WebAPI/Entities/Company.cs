namespace WebAPI.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string NameCompany { get; set; } = null!;
        public ICollection<User>? Users { get; set; }
    }
}
