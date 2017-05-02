using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAndPLinq
{
    class TaskInitialize
    {

        public static void InitializeWay1()
        {
            string message = "PassthistoTask";
            var task = Task.Factory.StartNew(DoSomeTask, message);
            task.Wait();
        }

        //Note Signature of DoSomeTask
        public static void DoSomeTask(object message)
        {
            Console.WriteLine(message);
        }

        public static void InitializeWay2lambda()
        {
            string message = "PassthistoTaskViaCaputured Variable.....";
            var task = Task.Factory.StartNew(()=> {
                Console.WriteLine(message);
            } );
            task.Wait();
        }

        public static void InitializeWay2lambda_1()
        {
            string message = "PassthistoTaskViaCaputured Variable.....";
            var task = Task.Factory.StartNew((state) => {
                Console.WriteLine(message);
            }, "messageTask");
            task.Wait();
        }

        /*Cancelling Task
         * Task are cancelled using cancelling token, they shared across many task 
         */
         public static void TestCancelling_1()
        {
            var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            //cts.Cancel()  //cancells now or
            // cts.CancelAfter(3000)

            Task task = Task.Factory.StartNew(() => {
                //do work
                token.ThrowIfCancellationRequested();
                //do work
            }, token);


        }

    }
}
