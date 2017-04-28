using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    public class ExceptionHandling
    {
        //IDisposable
        //to call dispose

        public void CodeUsingTryExceptFinnaly()
        {
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(@"c:\file.zip");
                var content = reader.ReadToEnd();
                //to forcefully demo executioon of finally
                throw new Exception("Opps");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occured");
            }
            finally
            {
                //Handle unmanaged resourses..
                if (reader != null)
                    reader.Dispose();
            }
        }

        public void CodeUsingStatement()
        {
            StreamReader reader = null;
            try
            {
                using(reader = new StreamReader(@"c:\file.zip"))
                {
                    var content = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occured");
            }

        }
    }
}
