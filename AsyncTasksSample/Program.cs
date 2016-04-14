using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTasksSample
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                // Start computation.
                Console.WriteLine("program started");
                Example();
                // Handle user input.
                Console.WriteLine("program finisihed");

                Console.ReadKey();
            }
        }

        static async void Example()
        {
            Console.WriteLine("Example() started");
            // This method runs asynchronously.
            var task = AccessTheWebAsync();

            // do another work here 
            Console.WriteLine("DoSomeAnotherWork() started");
            DoSomeAnotherWork();
            Console.WriteLine("DoSomeAnotherWork() ended");

            int x = await task;

            Console.WriteLine("Example() ended");


        }

        static int DoSomeWork()
        {
            // Compute total count of digits in strings.
            int size = 0;
            for (int z = 0; z < 100; z++)
            {
                for (int i = 0; i < 1000000; i++)
                {
                    string value = i.ToString();
                    if (value == null)
                    {
                        return 0;
                    }
                    size += value.Length;
                }
            }
            return size;
        }

        static int DoSomeAnotherWork()
        {
            // Compute total count of digits in strings.
            int size = 0;
            for (int z = 0; z < 100; z++)
            {
                for (int i = 0; i < 1000000; i++)
                {
                    string value = i.ToString();
                    if (value == null)
                    {
                        return 0;
                    }
                    size += value.Length;
                }
            }
            return size;
        }

        static async Task<int> AccessTheWebAsync()
        {
            Console.WriteLine("AccessTheWebAsync() started");
            // You need to add a reference to System.Net.Http to declare client.
            HttpClient client = new HttpClient();

            // GetStringAsync returns a Task<string>. That means that when you await the
            // task you'll get a string (urlContents).
            Task<string> getStringTask = client.GetStringAsync("http://msdn.microsoft.com");

            // You can do work here that doesn't rely on the string from GetStringAsync.
            Console.WriteLine("DoSomeWork() in AccessTheWebAsync started");
            DoSomeWork();
            Console.WriteLine("DoSomeWork() in AccessTheWebAsync ended");

            // The await operator suspends AccessTheWebAsync.
            //  - AccessTheWebAsync can't continue until getStringTask is complete.
            //  - Meanwhile, control returns to the caller of AccessTheWebAsync. [IMPORTANT: if getStringTask task has finisihed, await operator does not suspend AccessTheWebAsync]
            //  - Control resumes here when getStringTask is complete. 
            //  - The await operator then retrieves the string result from getStringTask.
            string urlContents = await getStringTask;

            Console.WriteLine("AccessTheWebAsync() ended");
            // The return statement specifies an integer result.
            // Any methods that are awaiting AccessTheWebAsync retrieve the length value.
            return urlContents.Length;
        }
    }
}
