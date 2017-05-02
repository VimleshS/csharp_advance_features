using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAndPLinq
{
    public class Plinq
    {
        /*  Ways to switch between LINQ and PLINQ
         *  
         *  Method                 Description
         *  
         *  AsParallel              Switches from LINQ to PLINQ
         *  AsSequence              Switches from PLINQ to LINQ
        */

        public static void LinqReversal()
        {
            string sentence = "A Quick brown fox jumps over a lazy dog";
            //Simple Linq
            var reversedWords = sentence.Split()
                .Select(word => new string(word.Reverse().ToArray()));
            Console.WriteLine(string.Join(" ",reversedWords));
        }

        public static void PLinqReversal()
        {
            string sentence = "A Quick brown fox jumps over a lazy dog";
            var reversedWords = sentence.Split()
                //converts it to parallel linq one gotcha.. 
                //*** LINQ decides itself depending upon overhead that will it run on parallel or sequence ***
                .AsParallel()  

                .Select(word => new string(word.Reverse().ToArray()));
            Console.WriteLine(string.Join(" ", reversedWords));
        }

        public static void PLinqForceFullyExecuteReversal()
        {
            string sentence = "A Quick brown fox jumps over a lazy dog";
            var reversedWords = sentence.Split()
                //converts it to parallel linq one gotcha.. 
                //LINQ decides itself depending upon overhead that will it run on parallel or sequence
                .AsParallel()
                .WithExecutionMode(ParallelExecutionMode.ForceParallelism) /* <<<----*/

                .Select(word => new string(word.Reverse().ToArray()));
            Console.WriteLine(string.Join(" ", reversedWords));
        }


        public static void PLinqForceFullyExecuteOrderedReversal()
        {
            string sentence = "A Quick brown fox jumps over a lazy dog";
            var reversedWords = sentence.Split()
                //converts it to parallel linq one gotcha.. 
                //LINQ decides itself depending upon overhead that will it run on parallel or sequence
                .AsParallel()
                .AsOrdered()                                                  /* <<<----*/
                .WithExecutionMode(ParallelExecutionMode.ForceParallelism) 

                .Select(word => new string(word.Reverse().ToArray()));
            Console.WriteLine(string.Join(" ", reversedWords));
        }
    }
}
