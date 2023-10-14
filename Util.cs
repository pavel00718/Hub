using Hub.Clases;
using Hub.Clases.HUB;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Reflection;

namespace Hub
{
    public static class Util
    {
        static Dictionary<string, string> CastRequest = new Dictionary<string, string>()
        {
           { "hotel", "hotelId" },
           { "checkInDate", "checkIn" },
           { "numberOfNights", "checkOut"},
           { "guests", "numberOfGuests"},
           { "rooms", "numberOfRooms"},
           { "currency", "currency"},
           { "checkDate","checkIn" },
           { "nights","checkOut" },
        };
        static Dictionary<string, string> CastResponse = new Dictionary<string, string>()
        { 
            { "room", "roomId" },
            { "meal", "mealPlanId" },
            { "canCancel", "isCancellable" },
            { "price", "price"},
            { "breakfast","mealPlanId" },
            { "Cancellable", "isCancellable" },
        };
        public static List<rooms> ConvertResponse(Object response)
        {
                        
            List<rooms> result = new List<rooms>();
            IEnumerable<Object> respuestas = (IEnumerable<object>)response;

            foreach (var respuesta in respuestas)
            {
                rooms room = new rooms();
                List<int> ids = result.Select(x => x.roomId).ToList();
                int respuestaid = (Convert.ToInt32(respuesta.GetType().GetFields().First().GetValue(respuesta).ToString()));
                if (ids.Contains(respuestaid))
                { 
                    room = (rooms)result.Where(x => x.roomId == respuestaid).First();    
                }
                
                foreach (var prop in room.GetType().GetFields())
                {
                    if(!prop.FieldType.IsPrimitive)
                    {
                        List<rates> rates = (List<rates>)prop.GetValue(room);
                        rates rate = new rates();
                        foreach (var prop2 in rate.GetType().GetFields())
                        {
                            KeyValuePair<string, string> dic2 = CastResponse
                                                        .Select(x => new KeyValuePair<string, string>(x.Key, x.Value))
                                                        .Where(x => x.Value == prop2.Name)
                                                        .First();

                            if (prop2.Name == dic2.Value)
                            {
                                try
                                {
                                    var field = respuesta.GetType().GetField(dic2.Key);
                                    if (field != null)
                                    {
                                        prop2.SetValue(rate, field.GetValue(respuesta));
                                    }
                                    
                                }           
                                catch { }
                                                              
                            }
                        }
                        rates.Add(rate);
                        continue;
                    }
                    KeyValuePair<string, string> dic = CastResponse
                                                        .Select(x => new KeyValuePair<string, string>(x.Key, x.Value))
                                                        .Where(x => x.Value == prop.Name)
                                                        .First();
                    if (prop.Name == dic.Value)
                    {
                        
                        prop.SetValue(room, respuesta.GetType().GetField(dic.Key).GetValue(respuesta));
                    }
                }
                if (!ids.Contains(respuestaid))
                {
                    result.Add(room);
                }
                
            }

            return result;
        }

        public static Object ConvertRequest(Request request, object Result)
        {
            

            //SPRequest hLRequest = new SPRequest();
            dynamic data = request;

            foreach (var prop in Result.GetType().GetFields())
            {
                KeyValuePair<string, string> dic = CastRequest
                                                    .Select(x => new KeyValuePair<string, string>(x.Key, x.Value))
                                                    .Where(x => x.Key == prop.Name)
                                                    .FirstOrDefault();
                if (prop.Name == dic.Key)
                {
                    if(dic.Key.ToUpper().Contains("NIGHTS"))
                    {
                        DateTime hasta = Convert.ToDateTime(request.GetType().GetField(dic.Value).GetValue(request));
                        DateTime desde = Convert.ToDateTime(request.GetType().GetField(CastRequest
                                                    .Select(x => new KeyValuePair<string, string>(x.Key, x.Value))
                                                    .Where(x => x.Key == "checkInDate")
                                                    .FirstOrDefault().Value).GetValue(request));
                        int dif = (hasta-desde).Days;
                        prop.SetValue(Result, dif);
                        continue;
                    }
                    prop.SetValue(Result, request.GetType().GetField(dic.Value).GetValue(request));
                }
            }
            return Result;

        }

    }
}
