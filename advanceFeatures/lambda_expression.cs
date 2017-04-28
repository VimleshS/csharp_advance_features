using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    /* An anaomymous method
         * No access modifier
         * No name
         * No return statement
     */
    class lambda_expression
    {
       
        public static void TestLambda()
        {
            Console.WriteLine(Square(5));
        }

        public static void TestLambda_1()
        {
            Func<int, int> lfunc =  n => n* n;
            Console.WriteLine(lfunc(2));
        }

        public static int Square(int num)
        {
            return num * num;
        }


        public class Book
        {
            public string Title;
            public float Price;
        }

        public static void TestLambda_2()
        {
            List<Book> books = new List<Book>()
            {
                new Book() {Title = "Title 1", Price= 9.23f },
                new Book() {Title = "Title 1", Price= 14.23f },
                new Book() {Title = "Title 1", Price= 5.2f }
            };

            var cheapbooks = books.FindAll(b => b.Price < 10);
            foreach (var item in cheapbooks)
            {
                Console.WriteLine("{0}", item.Title);
            }

        }


    }

}
