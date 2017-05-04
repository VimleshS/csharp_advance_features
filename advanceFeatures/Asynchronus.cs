using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Linq
{

    /*
     * Synchronous Program execution
     *      Program is executed line by line
     * Asynchronous Program execution
     *      When a func is called prgram execution continues to next line without waiting.
     *      Improves responsiveness of programm.
     *      When to do,
     *              Accessing the web
     *              Working with files / databases
     *              Working with images
     *      How do we it
     *              - Mutlithread & Callbacks
     *              - New Approch in .NET 4.5   
     *                  task based asynchronus model implemented using async/await
     */

    /* 
     * Wait will synchronously block until the task completes. So the current thread is literally blocked 
     * waiting for the task to complete. As a general rule, you should use "async all the way down"; 
     * that is, don't block on async code. On my blog, I go into the details of how blocking in asynchronous code 
     * causes deadlock.
      
     * await will asynchronously wait until the task completes. This means the current method is "paused" 
     * (its state is captured) and the method returns an incomplete task to its caller. Later, when the await
     * expression completes, the remainder of the method is scheduled as a continuation.
     * 
     */

    //http://stackoverflow.com/questions/9519414/whats-the-difference-between-task-start-wait-and-async-await
    //http://stackoverflow.com/questions/13140523/await-vs-task-wait-deadlock
    //http://blog.stephencleary.com/2012/02/async-and-await.html


    public class Asynchronus
    {
        static void DoAsTask()
        {
            WriteOutput("1 - Starting");
            var t = Task.Factory.StartNew<int>(DoSomethingThatTakesTime);
            WriteOutput("2 - Task started");
            t.Wait();
            WriteOutput("3 - Task completed with result: " + t.Result);
        }

        static async Task DoAsAsync()
        {
            WriteOutput("1 - Starting");
            var t = Task.Factory.StartNew<int>(DoSomethingThatTakesTime);
            WriteOutput("2 - Task started");
            var result = await t;
            WriteOutput("3 - Task completed with result: " + result);
        }

        /*
         * DoAsTask Output:
                [1] Program Begin
                [1] 1 - Starting
                [1] 2 - Task started
                [3] A - Started something
                [3] B - Completed something
                [1] 3 - Task completed with result: 123
                [1] Program End
           
            DoAsAsync Output:
                [1] Program Begin
                [1] 1 - Starting
                [1] 2 - Task started
                [3] A - Started something
                [1] Program End
                [3] B - Completed something
                [3] 3 - Task completed with result: 123
        */

        static int DoSomethingThatTakesTime()
        {
            WriteOutput("A - Started something");
            Thread.Sleep(1000);
            WriteOutput("B - Completed something");
            return 123;
        }

        static void WriteOutput(string message)
        {
            Console.WriteLine("[{0}] {1}", Thread.CurrentThread.ManagedThreadId, message);
        }

        public static void TestAsyncAwaitAndTask()
        {
            WriteOutput("Program Begin");
            DoAsTask();
            //DoAsAsync();
            WriteOutput("Program End");
            Console.ReadLine();
        }
    }
}
