using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    /* in .Net 4 they added, Dynamics, to improve interoperability with
     *  COM
     *  Dynamic language like IronPython
     */

    public class Dynamics
    {

        public void TestDyanmic()
        {
            object obj = "test";
            //call directlty
            obj.GetHashCode();


            //use reflection to get the value(a dynamic approch)
            //UGLY to read
            var methodInfo = obj.GetType().GetMethod("GetHashCode");
            methodInfo.Invoke(null, null);

            //With dynamic
            dynamic excelObject = "someexcel...";
            excelObject.Optimize();
            //works perfectly resolves at runtime.
            //like delphi's variant


            /*
             *    +------+     +------+     +------+
                  |  DLR | ==> |  CLR | ==> |  IL  |
                  +------+     +------+     +------+
                  A new component DLR Dynamic language runtime sits on top of CLR and gives
                  dynamic capabilty
            */


            dynamic dyn = "String";
            //works perfectly
            dyn = 10;



        }
    }
}
