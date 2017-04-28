using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TaskAndPLinq
{
    public class excersise_1
    {
        public static string[] Map(string sentence)
        {
            return sentence.Split(' ');
        }

        public static string LogicReverse(string word)
        {
            var _word = word.ToLower();
            var result = _word.Substring(1, _word.Length-1) + _word[0] + "ay";
            return result;
        } 

        public static string[] Process(string[] words)
        {
            var newWords = new string[words.Length];
            /*
            for (var i = 0; i < words.Length; i++)
            {
                var idx = i;
                Task.Factory.StartNew(() => { newWords[idx] = LogicReverse(words[idx]); },
                    TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning);
            }
            */
            
            for (int i = 0; i < words.Length; i++)
            {
                var idx = i;
                newWords[idx] = Task<string>.Factory.StartNew(() => LogicReverse(words[idx]),
                    TaskCreationOptions.AttachedToParent ).Result;
            }
            
            return newWords;
        }

        public static string Reduce(string[] words)
        {
            return String.Join(" ", words);
        }

        public static string PigLatin(string sentence)
        {
            var sp = new Stopwatch();
            sp.Start();
            var task = Task<string[]>.Factory.StartNew(() => Map(sentence))
                .ContinueWith<string[]>(t => Process(t.Result))
                .ContinueWith<string>(t => Reduce(t.Result));
            sp.Stop();
            Console.WriteLine(sp.Elapsed);
            return task.Result;
        }
    }
}
