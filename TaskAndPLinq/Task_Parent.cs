using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAndPLinq
{
    /* Complex Task can be broken into
     *      Hirarchcal Task
     *      Sequential 
     * 
     * Task can be connected hierarchically or sequencially,
     * Sequentially connectd tasks are called continuations
     * 
     * Eack task in continuations are called after previous task is completed.
     * The lambda expression in each continuation can access the result of previous task.
     * 
     * Continuations are the perfect for setting up multi-threaded MapReduce process in C#
     * 
     */

    //Layout of Library
    /*    
     *      Parallel   PLINQ    Async/Await
     *         
     *               T  A  S  K      
     *      
     *      .NET Framework Threadpool
     *      
     *             T H R E A D S
     */


    /* Steps of an asynchronus process
     *      1. Map a problem into independent units of work and assign each unit to a task
     *      2. Perform a series of operation on each unit
     *      3. Reduce the units into result
     *    Since Task it the fundametal block for many of other library in .net therefore we 
     *    have to do manually all 3 operation.
     *    
     *    Below Chart summaries the operations
     *    
     *    Library                   Automatic Features
     *    Tasks                 No mapping, no reducing
     *    Parallel Class        Automatic mapping, no reducing
     *    PLINQ                 Automatic mapping and reducing
     *    
     *    When to use?
     *    Task : When we have imperative mapping of ten thousand units or so and dont need 
     *              automatic mapping or reducing otherwise check Parallel and PLINQ
     *    Parallel : For a fairly large numbers to mapp use Parallel
     *    
     */

    /* Usasge Scenario
     *      Scenario                                                Library
     *      
     *      Complex process + small shared data                     Task
     *      Complex process + large shared data                     Parallel class
     *      Sequence of task on items + small dataset               Task or PLINQ
     *      Sequence of task on items + large dataset               PLINQ
     */

    /* 
     * PLINQ does not preserve the order of items.
     * 
     * There are methods do that
     * 
     *  Method                   Description
     *  -------------------------------------------------------------------
     * AsOrdered                Preserve the order of the item in dataset
     * AsUnordered              Does not
     */


    /* Limitations of PLINQ
     *         -   does not preserve the order(sum, count ..)
     *         -   can be by AsOrdered
     *         -   Cannot be parallelised
     *                  till .Net 4.0 | Concat, First, FirstOrDefault, Last, LastOrDefault,Skip, SkipWhile, Take,takeWhile, Zip 
     *                  all version   | Select, SelectMany, SkipWhile, TakeWhile, Where
     *         
     */

    public class MapReduce
    {
        public static string[] Map(string sentence)
        {
            return sentence.Split(' ');
        }

        public static string Reduce(string[] reversedString)
        {
            return String.Join(" ", reversedString);
        }

        public static string Reverse(string word)
        {
            Thread.Sleep(100);
            StringBuilder sb = new StringBuilder();
            for (var len = word.Length-1; len >=0 ; len--)
            {
                sb.Append(word[len]);
            }
            return sb.ToString();
        }

        public static string[] Process(string[] words)
        {
            for (int i = 0; i < words.Length-1; i++)
            {
                Task.Factory.StartNew(() => { words[i] = Reverse(words[i]); }, 
                    TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning);
            }
            //This is not returned untill all task are finished executing since it is 
            //TaskCreationOptions.AttachedToParent
            return words;
        }

        public static void TestMapReduce()
        {
            //Combination of Hirarchcal Task and Sequential task
            string sentence = "A Quick brown fox jumps over a lazy dog";
            var task = Task<string[]>.Factory.StartNew(() => Map(sentence))
                .ContinueWith<String[]>(t => Process(t.Result))
                .ContinueWith<string>(t => Reduce(t.Result));
            Console.WriteLine(task.Result); 
        }
    }

    public class SillyReducer
    {
        public static List<Task<string>> tasks = new List<Task<string>>();

        public static string Reverse(string word)
        {
            Thread.Sleep(100);
            StringBuilder sb = new StringBuilder();
            for (var len = word.Length - 1; len >= 0; len--)
            {
                sb.Append(word[len]);
            }
            //Console.WriteLine("Here");
            return sb.ToString();
        }

        public static void TestSillyReducer()
        {
            string sentence = "A Quick brown fox jumps over a lazy dog";
            var words =sentence.Split(' ');
            //An example of Hirarchcal Task
            foreach (var word in words)
            {
                tasks.Add(Task<string>.Factory.StartNew(
                    () => {
                        return Reverse(word);
                    }, TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning ));
            }
            foreach (var task in tasks)
            {
                Console.WriteLine(task.Result); 
            }

        }
    }

}
