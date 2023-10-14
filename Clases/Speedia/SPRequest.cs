using Hub.Interfaces;
using Microsoft.AspNetCore.DataProtection.Repositories;
using System.Xml.Xsl;

namespace Hub.Clases
{
    public class SPRequest : ISpeediaAPI
    {
        public int hotel;
        public string checkDate;
        public int nights;
        public int guests;
        public int rooms;
        public string currency;

        public List<SPResponse> RecuperarHabitaciones(SPRequest request)
        {
            List<SPResponse> spResponses = new List<SPResponse>();
            spResponses.Add(new SPResponse() { room = 3, breakfast = 0, Cancellable = false, totalprice = 90.22 });
            spResponses.Add(new SPResponse() { room = 4, breakfast = 2, Cancellable = true, totalprice = 112.09 });
            spResponses.Add(new SPResponse() { room = 4, breakfast = 1, Cancellable = true, totalprice = 98.25 });
            spResponses.Add(new SPResponse() { room = 4, breakfast = 2, Cancellable = false, totalprice = 99.99 });



            return spResponses;
        }

    }
}
