namespace WashMyCar.Core.Domain
{
    public class WashingBayBooking : WashingBayBookingBase
    {
        public Guid WashingBayBookingId { get; set; }
        public Guid WashingBayId{ get; set; }
    }
}
