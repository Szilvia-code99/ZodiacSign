using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Client
{
    class Program
    {
        public static bool isDateValid(string date)
        {
            var dateRegex = new Regex(@"\b(((0?[469]|11)/(0?[1-9]|[12]\d|30)|(0?[13578]|1[02])/(0?[1-9]|[12]\d|3[01])|0?2/(0?[1-9]|1\d|2[0-8]))/([1-9]\d{3}|\d{2})|0?2/29/([1-9]\d)?([02468][048]|[13579][26]))\b", RegexOptions.IgnorePatternWhitespace);
            return (dateRegex.Match(date).Success) ? true : false;
        }

        public static void shortenDate(ref string date)
        {
            if (date.Split('/')[0][0] == '0')
            {
                string[] input = date.Split('/');
                date = input[0].Substring(input[0].Length - 1) + "/" + input[1] + "/" + input[2];
            }

            if (date.Split('/')[1][0] == '0')
            {
                string[] input = date.Split('/');
                date = input[0] + "/" + input[1].Substring(input[1].Length - 1) + "/" + input[2];
            }

            return;
        }


        static void Main(string[] args)
        {

           /* 


            const string Host = "localhost";
            const int Port = 16842;

            var channel = new Channel($"{Host}:{Port}", ChannelCredentials.Insecure);


            shortenDate(ref date);

            var client = new Generated.ZodiacService.ZodiacServiceClient(channel);

            var response = client.Zodiac(new Generated.ZodiacRequest
            {
                Message = date
            });

            Console.WriteLine("\nReceived response:", response.Response);*/

            const string Host = "localhost";
            const int Port = 16842;

            var channel = new Channel($"{Host}:{Port}", ChannelCredentials.Insecure);

            string date;
            do
            {
                Console.WriteLine("Enter a date: [format: mm/dd/yyyy]");
                date = Console.ReadLine();
            } while (!isDateValid(date)); 

            shortenDate(ref date);

            var client = new Generated.ZodiacService.ZodiacServiceClient(channel);

            var response = client.Zodiac(new Generated.ZodiacRequest
            {
                Message = date
            });

            Console.WriteLine("\n Your zodiac sign is: {0}", response.Response);


            Console.ReadLine();

        }
    }
}

   

