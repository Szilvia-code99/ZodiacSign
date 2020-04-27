using Generated;
using Grpc.Core;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System;

namespace Server
{
    internal class ZodiacService : Generated.ZodiacService.ZodiacServiceBase
    {

        private string getHoroscope(string date)
        {
            FileStream file = File.OpenRead("../../../input.txt");
            StreamReader streamReader = new StreamReader(file);

            List<string[]> horoscopeList = new List<string[]>();
            string line, result = "";

            while ((line = streamReader.ReadLine()) != null)
            {
                horoscopeList.Add(line.Split(' '));
            }

            string[] inputDate = date.Split('/');

            for (int i= 0; i < horoscopeList.Count; ++i)
            {
                if ((horoscopeList[i][0].Split('/')[0] == inputDate[0] && Int32.Parse(inputDate[1]) >= Int32.Parse(horoscopeList[i][0].Split('/')[1])) 
                    ||
                    (horoscopeList[i][1].Split('/')[0] == inputDate[0] && Int32.Parse(inputDate[1]) <= Int32.Parse(horoscopeList[i][1].Split('/')[1]))) 
                {
                    result = horoscopeList[i][2];
                    break;
                }
            }
            return result;
        }

        public override Task<ZodiacResponse> Zodiac(ZodiacRequest request, ServerCallContext context)
        {
            var result = getHoroscope(request.Message);

            System.Console.WriteLine("Date requested:", request.Message);

            return Task.FromResult(new ZodiacResponse() { Response = result });
        }
    }
}
