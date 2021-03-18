using System;
using System.Collections.Generic;

namespace UniversitySystem

{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleFormat.Format();
            Menu.Start(new List<Person>()).MainSubMenuOption(); 
           
        }
    }
}
