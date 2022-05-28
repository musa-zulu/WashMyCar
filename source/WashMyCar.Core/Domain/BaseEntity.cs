namespace WashMyCar.Core.Domain
{
    public class BaseEntity
    {
        public string AddedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
