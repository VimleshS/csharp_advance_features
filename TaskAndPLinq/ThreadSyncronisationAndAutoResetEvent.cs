using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAndPLinq
{
    /*
     * What is Thread Synchronisation
     * Ans:
     * It is the act of suspending one thread until a certain condition is met in another thread.
     * 
     * Why we need?
     * (TO SAFELY EXCHANGE DATA)
     * Main thread/program spawns a new thread to do complex calcultion in background 
     * the thread loops and produces result after every milisecond, main thread wait for
     * result and picks one by one.
     * This kind of workflow is implemented using AutoResetEvent
     * 
     * AutoResetEvent is very handy and efficient in case of operation.
     * It has methods like WaitOne, Set..
     * 
     * AutoResetEvent is analogus to channel concept in golang.
     * Call to WaitOne suspends a thread and call to Set resumes a thread.
     * 
     * 
     * For a robust communication we need two autoresetevents with call to WaitOne and Set at both ends
     * 
     * MISC: 
     *  When an autoresetevent is set only one thread are let through
     *  An AutoResetEvent is called "auto-reset" because it automatically 
     *  resets after letting one waiting thread through.
     */
    class ThreadSyncronisationAndAutoResetEvent
    {
        // shared field for work result
        public static int result = 0;

        // lock handle for shared result
        private static object lockHandle = new object();

        // event wait handles
        public static EventWaitHandle readyForResult = new AutoResetEvent(false);
        public static EventWaitHandle setResult = new AutoResetEvent(false);

        public static void DoWork()
        {
            while (true)
            {
                int i = result;

                // simulate long calculation
                Thread.Sleep(1);

                // wait until main loop is ready to receive result
                readyForResult.WaitOne();

                // return result
                lock (lockHandle)
                {
                    result = i + 1;
                }

                // tell main loop that we set the result
                setResult.Set();
            }
        }

        public static void Test()
        {
            // start the thread
            Thread t = new Thread(DoWork);
            t.Start();

            // collect result every 10 milliseconds
            for (int i = 0; i < 100; i++)
            {
                // tell thread that we're ready to receive the result
                readyForResult.Set();

                // wait until thread has set the result
                setResult.WaitOne();

                lock (lockHandle)
                {
                    Console.WriteLine(result);
                }

                // simulate other work
                Thread.Sleep(10);
            }

            // messy abort
            t.Abort();
        }
    }

}
