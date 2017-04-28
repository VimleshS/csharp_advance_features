using System.Linq;



/* Extension method is included in ExtensionMethods NamesSpace
 * It will be really a trouble some to add numbers of such namespace in the usasge section "using ...."
 * One solution is to add in System namesSpace  so that these namespace are auto imported
*/

//namespace ExtensionMethods
namespace System
{
    /* Extension Method allows us to add methods to an existing type/class without 
     * - Changing its source code
     * - Creating a new class that inherits from it.
     * 
     * 
     * convention
     *  Static class
     *      [Class]Extension( e.q. StringExtensions)
     *  Static method
     *      public static string Shorten(this String str, int noOfWords)
     *  
     */
    public static class StringExtensions
    {
        public static string Shorten(this String str, int numberOfWords)
        {
            if (numberOfWords == 0)
                return "";

            var words = str.Split(' ');

            if (words.Length <= numberOfWords)
                return str;

            return String.Join(" ", words.Take(numberOfWords));
        }
    }
}
