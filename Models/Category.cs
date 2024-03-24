namespace WebApplication1.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayOrder { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
    }
}
