namespace WashMyCar.Core.Domain
{
    public class Color : BaseEntity
    {
        public Guid ColorId { get; set; }        
        public string Description { get; set; }
    }
}
