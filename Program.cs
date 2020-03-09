using Demo.AsyncAwaitExample.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.AsyncAwaitExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Select method: Async Paralel(1), Async(2) or Sync(3)");
            var r = Console.ReadLine();


            Console.WriteLine("Starting...");
            switch (r)
            {
                case "1" /*Async Paralel*/:
                    List<Task<List<User>>> tasks = new List<Task<List<User>>>();

                    //async paralel
                    for (int i = 0; i < 50; i++)
                    {
                        tasks.Add(Task.Run(() => GetUsersAsync()));
                    }
                    var results = await Task.WhenAll(tasks);
                    break;

                case "2" /*Async*/:
                    for (int i = 0; i < 50; i++)
                    {
                        await GetUsersAsync();
                    }
                    break;

                case "3" /*Sync*/:
                    for (int i = 0; i < 50; i++)
                    {
                        GetUsers();
                    }
                    break;

                default:
                    break;
            }

            stopwatch.Stop();
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
            Console.ReadLine();
        }

        static List<User> GetUsers()
        {
            string url = "https://restcore20200226095119.azurewebsites.net";
            string resource = "/api/users/";

            try
            {
                var client = new RestClient(url);
                var request = new RestRequest(resource, Method.GET);
                var response = client.Execute<List<User>>(request);

                if (response.IsSuccessful)
                {
                    var queryResult = response.Data;
                    Console.WriteLine($" records={queryResult.Count()}");
                    return queryResult;
                }
                else
                {
                    throw new Exception(response.ErrorMessage);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        static async Task<List<User>> GetUsersAsync()
        {
            string url = "https://restcore20200226095119.azurewebsites.net";
            string resource = "/api/users/";

            try
            {
                var client = new RestClient(url);
                var request = new RestRequest(resource, Method.GET);
                var response = await client.ExecuteAsync<List<User>>(request);

                if (response.IsSuccessful)
                {
                    var queryResult = response.Data;
                    Console.WriteLine($"loaded {queryResult.Count()} records");
                    return queryResult;
                }
                else
                {
                    throw new Exception(response.ErrorMessage);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
