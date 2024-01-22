using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Hourglass
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Tokens tokens = new Tokens();
            sqlMapQueries SQL = new sqlMapQueries();
            timeManager timeManager = new timeManager();
            stringConstructor stringCon = new stringConstructor();

            DateTime LeavingHouse;
            DateTime ArrivingGym;
            DateTime LeavingGym;
            DateTime ArrivingHouse;

            DateTime TodayDate = DateTime.Now;

            DateTime today = DateTime.Today;
            DayOfWeek dayOfWeek = today.DayOfWeek;

            if (dayOfWeek == DayOfWeek.Monday || dayOfWeek == DayOfWeek.Tuesday || dayOfWeek == DayOfWeek.Thursday || dayOfWeek == DayOfWeek.Friday)
            {

                Console.WriteLine("---------------- GYM PLANNER ----------------");

                try
                {
                    // Today's Date + 4:55 PM
                    DateTimeOffset leavingHouseTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 55, 0);
                    long leavingHouseTimeUnixTime = leavingHouseTime.ToUnixTimeSeconds();
                    string urlHomeToGym = $"https://maps.googleapis.com/maps/api/directions/json?origin={tokens.home}&destination={tokens.gym}&mode=transit&departure_time={leavingHouseTimeUnixTime}&key={tokens.apiKey}";

                    // Today's Date + 7:00 PM
                    DateTimeOffset arrivingHouseTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 19, 0, 0);
                    long arrivingHouseTimeUnixTime = arrivingHouseTime.ToUnixTimeSeconds();
                    string urlGymToHome = $"https://maps.googleapis.com/maps/api/directions/json?origin={tokens.gym}&destination={tokens.home}&mode=transit&arrival_time={arrivingHouseTimeUnixTime}&key={tokens.apiKey}";

                    // Create an instance of HttpClient
                    using (HttpClient client = new HttpClient())
                    {
                        Console.WriteLine("Asking Google...");

                        // Send a GET request to the specified URL
                        HttpResponseMessage response1 = await client.GetAsync(urlHomeToGym);

                        // Ensure the response is successful
                        response1.EnsureSuccessStatusCode();

                        // Read the response content as a string
                        string responseBody1 = await response1.Content.ReadAsStringAsync();

                        // Assign web data to sql class
                        SQL.data = JsonConvert.DeserializeObject<MapData>(responseBody1);

                        // SHOULD ADD CHECK< IF FAILS DONT DO ANYTHING
                        //if (SQL.GetTransitLine() == Constants.requiredTransitLine)

                        Console.WriteLine("Correct Line Found");

                        Console.WriteLine("Time you need to leave the house by...");
                        LeavingHouse = timeManager.ConvertUnixToDateTime(SQL.GetDepartureTime());
                        Console.WriteLine(LeavingHouse.ToString());

                        Console.WriteLine("Time you will arrive at the gym...");
                        ArrivingGym = timeManager.ConvertUnixToDateTime(SQL.GetArrivalTime());
                        Console.WriteLine(ArrivingGym.ToString());


                        // Send a GET request to the specified URL
                        HttpResponseMessage response2 = await client.GetAsync(urlGymToHome);

                        // Ensure the response is successful
                        response2.EnsureSuccessStatusCode();

                        // Read the response content as a string
                        string responseBody2 = await response2.Content.ReadAsStringAsync();

                        // Assign web data to sql class
                        SQL.data = JsonConvert.DeserializeObject<MapData>(responseBody2);

                        Console.WriteLine("Time you need to leave the gym by...");
                        LeavingGym = timeManager.ConvertUnixToDateTime(SQL.GetDepartureTime());
                        Console.WriteLine(LeavingGym.ToString());

                        Console.WriteLine("Time you will arrive Home...");
                        ArrivingHouse = timeManager.ConvertUnixToDateTime(SQL.GetArrivalTime());
                        Console.WriteLine(ArrivingHouse.ToString());

                        Console.WriteLine("Workout time : " + timeManager.GetWorkoutTime(ArrivingGym, LeavingGym));
                        Console.WriteLine("Change time : " + timeManager.StripDateTime(timeManager.GetChangeTime(LeavingGym)));

                    }

                    // Time to send texts

                    TwilioClient.Init(tokens.accountSid, tokens.authToken);

                    foreach (var number in tokens.numbers)
                    {
                        var message = MessageResource.Create(
                            body: stringCon.CreateTextString(
                                TodayDate.ToString("dd MMMM yyyy"),
                                timeManager.StripDateTime(LeavingHouse),
                                timeManager.StripDateTime(ArrivingGym),
                                timeManager.StripDateTime(ArrivingHouse),
                                SQL.GetIONDepartureTime(),
                                timeManager.GetWorkoutTime(ArrivingGym, LeavingGym),
                                timeManager.StripDateTime(timeManager.GetChangeTime(LeavingGym))),
                            from: new Twilio.Types.PhoneNumber(tokens.myTwilioNum),
                            to: new Twilio.Types.PhoneNumber(number)
                        );

                        Console.WriteLine($"Message to {number} has been {message.Status}.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Not Going to Gym Today...");
            }
        }
    }
}
