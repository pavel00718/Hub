using Hub.Interfaces;
using Microsoft.AspNetCore.DataProtection.Repositories;
using System.Xml.Xsl;

namespace Hub.Clases
{
    public class HLRequest : IHotelLegsAPI
    {
        public int hotel;
        public string checkInDate;
        public int numberOfNights;
        public int guests;
        public int rooms;
        public string currency;

        public List<HLResponse> RecuperarHabitaciones(HLRequest request)
        {
            List<HLResponse> hLResponses = new List<HLResponse>();
            hLResponses.Add(new HLResponse() { room = 1, meal = 1, canCancel = false, price = 123.48 });
            hLResponses.Add(new HLResponse() { room = 1, meal = 1, canCancel = true, price = 150.00 });
            hLResponses.Add(new HLResponse() { room = 2, meal = 1, canCancel = false, price = 148.25 });
            hLResponses.Add(new HLResponse() { room = 2, meal = 2, canCancel = false, price = 165.38 });



            return hLResponses;
        }

    }
}
