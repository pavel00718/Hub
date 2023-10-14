using Hub.Clases;

namespace Hub.Interfaces
{
    public interface ISpeediaAPI
    {
      List<SPResponse> RecuperarHabitaciones(SPRequest request);
    }
}
