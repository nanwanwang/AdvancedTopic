using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSample
{
    internal class TaskExample
    {
        public static async Task<string> DownloadStringWithRetries(string uri)
        {
            using var httpclient = new HttpClient();
            var nextDelay = TimeSpan.FromSeconds(1);

            for (int i = 0; i != 3; i++)
            {
                try
                {
                    return await httpclient.GetStringAsync(uri);
                }
                catch
                {
                }
                Console.WriteLine($"第{i}次请求...");
                await Task.Delay(nextDelay);
                nextDelay = nextDelay + nextDelay;
            }

            return await httpclient.GetStringAsync(uri);
        }


        public static async Task<string> DownloadStringWithTimeout(string uri)
        {
            using var   httpclient = new HttpClient();
            var downloadTask= httpclient.GetStringAsync(uri);

            var timeoutTask = Task.Delay(100);
            var completedTask = await Task.WhenAny(downloadTask, timeoutTask);
            if (completedTask == timeoutTask) return null;

            return await downloadTask;
        }

        public static async Task<string> DownloadAllAsync(IEnumerable<string> urls)
        {
            var httpClient = new HttpClient();
            var downloads = urls.Select(async x => await httpClient.GetStringAsync(x));

            Task<string>[] downloadTasks = downloads.ToArray();

            string[] htmlPages = await Task.WhenAll(downloadTasks);

            return string.Concat(htmlPages);

        }

        public static async Task ThrowNotImplementedExceptionAsync()
        {
            throw new NotImplementedException();
        }

        public static async Task ThrowInvalidOperationExceptionAsync()
        {
            throw new InvalidOperationException();
        }


        public static async Task ObserveOneExceptionAsync()
        {
            var task1 = ThrowInvalidOperationExceptionAsync();
            var task2 = ThrowNotImplementedExceptionAsync();

            try
            {
                await Task.WhenAll(task1, task2);
            }
            catch (Exception ex)
            {

              //  throw;
            }
           
        }

        public static async Task ObserveTwoExceptionAsync()
        {
            var task1 = ThrowInvalidOperationExceptionAsync();
            var task2 = ThrowNotImplementedExceptionAsync();

            Task allTasks= Task.WhenAll(task1, task2);
            try
            {
                await allTasks;
            }
            catch 
            {
                AggregateException allexceptions = allTasks.Exception;
                //throw;
            }
        }


        public static async Task<int> DelayAndReturnAsync(int value)
        {
            await Task.Delay(TimeSpan.FromSeconds(value));
            return value;
        }

        public static async Task ProcessTaskAsync()
        {
            Task<int> taskA = DelayAndReturnAsync(2);
            Task<int> taskB = DelayAndReturnAsync(3);
            Task<int> taskC = DelayAndReturnAsync(1);
            var tasks = new [] {taskA,taskB,taskC };
            foreach (var task in tasks)
            {
                var result = await task;
                Console.WriteLine(result);
            }
        }

        public static async Task ProcessTaskAsync2()
        {
            Task<int> taskA = DelayAndReturnAsync(2);
            Task<int> taskB = DelayAndReturnAsync(3);
            Task<int> taskC = DelayAndReturnAsync(1);
            var tasks = new[] { taskA, taskB, taskC };
            var processTasks = tasks.Select(async t => 
            {
                var result = await t;
                Console.WriteLine(result);
            }).ToArray();

            await Task.WhenAll(processTasks);
        }


        static async Task ThrowExceptionAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            throw new InvalidOperationException("Test");
        }

        public static async Task TestAsync()
        {
            
                await ThrowExceptionAsync();
           
        }
    }
}
