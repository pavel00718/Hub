using Hub.Clases;

namespace Hub.Interfaces
{
    public interface IHotelLegsAPI
    {
      List<HLResponse> RecuperarHabitaciones(HLRequest request);
    }
}
