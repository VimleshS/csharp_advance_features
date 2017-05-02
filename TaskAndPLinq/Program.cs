using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAndPLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test.TestBasicThraed();
            //Test.TestBasicThraedWithMonitor();
            //Test.TestBasicThraedWithMonitorTimeOut();


            //ThreadSyncronisationAndAutoResetEvent.Test();
            //SimpleTask.TestSimpleTask();
            //SimpleTask.TestSimpleTaskNoReturn();



            //TaskInitialize.InitializeWay1();
            //TaskInitialize.InitializeWay2lambda();
            //TaskInitialize.InitializeWay2lambda_1();


            //MapReduce.TestMapReduce();
            //SillyReducer.TestSillyReducer();
            //Console.WriteLine(excersise_1.PigLatin("Mark Farragher"));




            Plinq.LinqReversal();
            Plinq.PLinqReversal();

            Plinq.PLinqForceFullyExecuteReversal();
            Plinq.PLinqForceFullyExecuteOrderedReversal();
        }
}
}
