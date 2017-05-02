using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAndPLinq
{
    /*
        In C# 4.0 TPL(task parallel library) was introduced which 
        lets write and share data very easily
        unlike using AutoResetEvent and synchronising..


        Defines Task & Task<T> classes to execute asynchronous works 
        and return a result to another thread
    */
    public class SimpleTask
    {
        public static void TestSimpleTask()
        {
            //This unit of work will return string and hence Task<string>
            Task<string> task = Task<string>.Factory.StartNew(() => {
                Thread.Sleep(100);
                return "Done";
            });


            Console.WriteLine("Res ");
            //waits for result to complete poll/check for response or error if any.
            Console.WriteLine(task.Result);
         }

        public static void TestSimpleTaskNoReturn()
        {
            Task task = Task.Factory.StartNew(
                () => {
                    Thread.Sleep(100);
                    Console.WriteLine(Thread.CurrentThread.IsThreadPoolThread);
                    Console.WriteLine("No Return");
                },
                TaskCreationOptions.LongRunning
                /*Marks the task as long running and thead is not spawned from ThreadPool thus saving 
                 * the performance of threadpool
                 * Since Threadpool threads are only for shorter lived thread execution time.
                 */
            );


            Console.WriteLine("Res ");
            //waits for result to complete poll/check for response or error if any.
            task.Wait();
            Console.WriteLine("dd");
        }
    }
}
             