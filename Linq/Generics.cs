using System;

namespace Generics
{
    //My generic List.
    class MyList<T>
    {
        public void Add(T value)
        {

        }

        public T this[int index]
        {
            get { throw new NotImplementedException(); }
        }
    }

    //Example of generic constraints
    public class Utilities
    {
        //Non generic method
        public int Max(int a, int b)
        {
            return a > b ? a : b;
        }

        //Generic Method
        public T Max<T>(T a, T b) where T : IComparable
        {
            // a.   gives the method of object class therefore we use contraints.
            // return a > b ? a : b;
            //gives error, so use case is to compare to object 
            //it must have implemented IComparable interface.
            // where T : IComparable  (: == is)
            return a.CompareTo(b) > 0 ? a : b;
            // We have a generic method in a non generic class we can move it to class  as
            // public class Utilities<T> where T : IComparable
        }

    }

    //types of constraints
    // where T : IComparable  //Interface
    // where T : Product      //specific Object
    // where T : struct       //value type
    // where T : class        //ref type
    // where T : new()        //should have default ctor

    public class Product
    {
        public string Title { get; set; }
        public float Price { get; set; }
    }

    public class DisccountCalculator<TProduct> where TProduct : Product
        // accepts products and its subclass
    {
        public float CalculateDiscount(TProduct product)
        {
            return product.Price * 0.5f;
        }
    }

    //Create a Nullable type like int?
    public class Nullable<T> where T : struct //only value type
    {
        private object _value;

        public Nullable()
        {

        }
        public Nullable(T value)
        {
            _value = value;
        }

        public bool HasAttr
        {
            get { return _value != null; }
        }

        public T GetValueOrDefault()
        {
            if (HasAttr)
                return (T)_value;

            return default(T);
        }


    }

    //Example of generic constraints
    public class NewUtilities<T> where T : IComparable, new()
    {
        //Non generic method
        public int Max(int a, int b)
        {
            return a > b ? a : b;
        }

        public void DoSomething(T value)
        {
            //here we wanted to instanciate T but generic do not know about it,
            // therefore we have to apply constraint of default constructor
            // new() along with Icomparable
            var obj = new T();
        }

        //Generic Method
        public T Max(T a, T b)
        {
            return a.CompareTo(b) > 0 ? a : b;
        }

    }

    public class MyDict<TKey, TValue>
    {
        public void Add(TKey key, TValue value)
        {

        }
    }

   
}
