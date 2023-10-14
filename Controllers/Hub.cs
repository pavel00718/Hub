using Hub.Clases;
using Hub.Clases.HUB;
using Hub.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace Hub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Hub : ControllerBase
    {
        private readonly IHotelLegsAPI _hotelLegsApi;
        private readonly ISpeediaAPI _speediaApi;

        public Hub(IHotelLegsAPI hotelLegsAPI, ISpeediaAPI speediaAPI)
        {
            _hotelLegsApi = hotelLegsAPI;
            _speediaApi = speediaAPI;
        }
       

        [HttpGet("Consultar")]
        public string Consultar(Request request)
        {
            return Callall(request);
        }

        [HttpGet("Consultarxjsontext")]
        public string Consultarxjsontext(string jsontext)
        {           
            try
            {
                Request request = JsonConvert.DeserializeObject<Request>(jsontext);
                return Callall(request);
               
            }
            catch (Exception ex) { return ex.Message; }            
        }

        [HttpGet("ConsultarPrueba")]
        public string ConsultarPrueba(int hotelId, string checkIn, string checkOut,int numberOfGuests, int numberOfRooms, string currency)
        {            
            Request request = new Request { checkIn = checkIn, checkOut = checkOut, numberOfGuests = numberOfGuests, currency = currency, hotelId = hotelId, numberOfRooms = numberOfRooms };

            return Callall(request); 
        }

        private string Callall(Request request)
        {
            List<rooms> rooms = new List<rooms>();
            Object newrequest = Util.ConvertRequest(request, new HLRequest());                        
            List<HLResponse> hlreponse = _hotelLegsApi.RecuperarHabitaciones((HLRequest)newrequest);
            Object newsprequest = Util.ConvertRequest(request, new SPRequest());
            List<SPResponse> spreponse = _speediaApi.RecuperarHabitaciones((SPRequest)newsprequest);

            List<rooms> hlrooms = Util.ConvertResponse(hlreponse);
           List<rooms> sprooms = Util.ConvertResponse(spreponse);

            rooms.AddRange(hlrooms);
            rooms.AddRange(sprooms);
            
            string json = JsonConvert.SerializeObject(new
           {
               rooms
           });

           return json;
        }

    }
}