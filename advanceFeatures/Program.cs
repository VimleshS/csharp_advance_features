using System;
using Generics;
using delegates;
using EventAndDelegate;
//using ExtensionMethods;

namespace CSharpAdvanceFeature
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // --------------  GENERICS --------------------
            MyList<int> intlist = new MyList<int>();
            intlist.Add(111);

            //List of all generic collection belongs to
            //System.Collections.Generic

            //Constraints

            var number = new Generics.Nullable<int>(5);
            Console.WriteLine("Has Value {0} ", number.HasAttr);
            Console.WriteLine("and Value {0} ", number.GetValueOrDefault());
            var nullnumber = new Generics.Nullable<int>();
            Console.WriteLine("Has Value {0} ", nullnumber.HasAttr);
            Console.WriteLine("and Value {0} ", nullnumber.GetValueOrDefault());
            //Already there
            //System.Nullable<>
            Console.WriteLine("----case---");



            /*
             //---------------------DELEGATES--------------------
            */
            var photoprocessor = new PhotoProcessor();
            photoprocessor.Process("dummy");
            Console.WriteLine("----case---");

            PhotoFilters filters = new PhotoFilters();
            PhotoFilterHandler filterHandler = filters.ApplyBrightness;
            filterHandler += filters.Resize;
            photoprocessor.Process_1("dummy", filterHandler);
            Console.WriteLine("----case---");


            // Infact we even dont need to create a delegate type we can use Action, Func, Predicate

            //Action<Photo> filters_1 = new Action<Photo>( filters.ApplyBrightness);
            //OR
            Action<Photo> filters_1 = filters.ApplyBrightness;
            photoprocessor.Process_3("dummy", filters_1);

            Console.WriteLine("----case---");
            ExtensionMethods.lambda_expression.TestLambda();
            Console.WriteLine("----case---");
            ExtensionMethods.lambda_expression.TestLambda_1();
            Console.WriteLine("----case---");
            ExtensionMethods.lambda_expression.TestLambda_2();
            Console.WriteLine("----case---");

            VideoEncoder v_encoder = new VideoEncoder();
            Video video = new Video() { Name = "HUM" };

            //Create handlers
            var mailservice = new MailService();
            v_encoder.VideoEncoded += mailservice.OnVideoEncoded;
            v_encoder.Encode(video);

            VideoEncoderWithCLRDelegate v_encoder_new = new VideoEncoderWithCLRDelegate();
            v_encoder_new.VideoEncoded += mailservice.OnVideoEncoded;
            v_encoder_new.VideoEncoded += (new MailServiceWithVideoArgs()).OnVideoEncoded;
            v_encoder_new.Encode(video);

            Console.WriteLine("----case---");
            string blog = "Heffernan v. City of Paterson was a U.S. Supreme Court case concerning the First";
            Console.WriteLine(blog.Shorten(5)); 




        }
    }
}
