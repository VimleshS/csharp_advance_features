using System;
using System.Threading;

namespace TaskAndPLinq
{
    public class BasicThread
    {
        private static int _value_n = 1;
        private static int _value_d = 1;
        
        /*
         * Locking object should alays be private, so it is not global and any other thread cannot uses it to lock.
         * 
         * We can use nesting lock/Monitor.Enter
         * 
         */

        private static Object _obj = new object();

        public void DoWorkAndIncrement()
        {
            //Shortcut for Monitor.Enter()
            lock (_obj)
            {

                if (_value_d > 0)
                {
                    Console.WriteLine(_value_n/_value_d);
                    _value_d = 0;
                }
            }
        }

        public void DoWorkAndIncrementWithMonitor()
        {
            bool lockTaken = false;
            try
            {
                Monitor.Enter(_obj, ref lockTaken);
                if (_value_d > 0)
                {
                    Console.WriteLine(_value_n / _value_d);
                    _value_d = 0;
                }

            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(_obj);
            }
        }

        public void DoWorkAndIncrementWithMonitorTimeout()
        {
            bool lockTaken = false;
            try
            {
                //Timeout for breaking a loop if unresponsive.
                Monitor.TryEnter(_obj, TimeSpan.FromMilliseconds(500), ref lockTaken);
                if (_value_d > 0)
                {
                    Console.WriteLine(_value_n / _value_d);
                    _value_d = 0;
                    Thread.Sleep(1000);
                }

            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(_obj);
            }
        }

    }

    public class Test
    {
        public static void TestBasicThraed()
        {
            var threadclass = new BasicThread();
            //Ways to call Thread 1
            var th1 = new Thread(threadclass.DoWorkAndIncrement);

            //Ways to call Thread 3 Pass lambda
            // Advantage we can pass multiple parameters, not bound to [void ThreadStart()] 
            // delegate signature of new Thread(ThreadStart())
            var th2 = new Thread( () => {
                threadclass.DoWorkAndIncrement(); 
                });

            th1.Start();
            th2.Start();
        }

        public static void TestBasicThraedWithMonitor()
        {
            var threadclass = new BasicThread();
            var th1 = new Thread(threadclass.DoWorkAndIncrementWithMonitor);
            var th2 = new Thread(threadclass.DoWorkAndIncrementWithMonitor);
            th1.Start();
            th2.Start();
        }

        public static void TestBasicThraedWithMonitorTimeOut()
        {
            var threadclass = new BasicThread();
            var th1 = new Thread(threadclass.DoWorkAndIncrementWithMonitorTimeout);
            var th2 = new Thread(threadclass.DoWorkAndIncrementWithMonitorTimeout);
            th1.Start();
            th2.Start();
        }
    }
}
