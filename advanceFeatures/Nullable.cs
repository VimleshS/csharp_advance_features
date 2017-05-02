using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    public class Nullable
    {
        public void TestNullables()
        {

            Nullable<int> index = null;
            //ShortHand for above syntax
            int? indexAgain = null;

            Nullable<DateTime> date1 = new Nullable<DateTime>(new DateTime(2001, 1, 1));
            DateTime? date2 = new DateTime(2001, 1, 1);
            DateTime dt;
            
            //DateTime date2 = date1.GetValueOrDefault();

            //Null Coalsing operator.
            dt = date2 ?? DateTime.Today;

            //Ternary Operator
            var dt2 = date2 != null ? date2.GetValueOrDefault() : DateTime.Today;
        }


    }
}
