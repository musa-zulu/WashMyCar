using WashMyCar.Core.Domain;

namespace WashMyCar.Core.DataContracts
{
    public interface IWashingBayBookingRepository
    {
        IEnumerable<WashingBayBooking> GetAll();
    }
}
