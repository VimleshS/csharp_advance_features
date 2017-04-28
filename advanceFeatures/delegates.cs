using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegates
{
    /*
     * What is delegate?
     * Ans An object that knows how to call a method(or a group of methods)
     * Simply a reference to a function
     * 
     * When to use delegates over interface
     * Event like design or when client doesnt need any other method
     */ 


    public class Photo
    {
        public static Photo Load(string path)
        {
            return new Photo();
        }

        public void Save()
        {
            
        }
    }

    public class PhotoFilters
    {
        public void ApplyBrightness(Photo photo)
        {
            Console.WriteLine("Apply Brightness");
        }
        public void ApplyContrast(Photo photo)
        {
            Console.WriteLine("Apply ApplyContrast");
        }
        public void Resize(Photo photo)
        {
            Console.WriteLine("Apply Resize");
        }
    }

    public delegate void PhotoFilterHandler(Photo photo);

    public class PhotoProcessor
    {

        //Case 1
        
        public void Process(string path)
        {
            var photo = Photo.Load(path);
            
            var filters = new PhotoFilters();
            //this is not flexible or extensible.
            //a new filters cannot be applied with this approch
            //breaks OC principles
            //This can be solved by delegates and interfaces by some polymorphism

            filters.ApplyBrightness(photo);
            filters.ApplyContrast(photo);
            filters.Resize(photo);
            //Above design makes program very rigid and hard to change.
            //use delegate to make it loose
            
            photo.Save();
        }
        

        //Case 2
        public void Process_1(string path, PhotoFilterHandler filterHandler)
        {
            var photo = Photo.Load(path);
            filterHandler(photo);
            photo.Save();
        }

        //Case 3
        public void Process_3(string path, Action<Photo> filterHandler)
        {
            var photo = Photo.Load(path);
            filterHandler(photo);
            photo.Save();
        }


    }
}
