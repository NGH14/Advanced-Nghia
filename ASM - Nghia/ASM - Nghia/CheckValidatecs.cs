using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversitySystem
{
    class CheckValidate
    {
        /* This class created to checking the CheckValidate of user input about:
       Is list about student and lecture null by built in function of list "Any()",
       Is the data are already store by built in function of list "Any()",
       Is the search ID null by is null fuction.
       Is the input are null or just whitespace by IsNullOrWhiteSpace().
       .... 
       */
        public static bool IsListNull(List<Person> list, PersonTypes types)
        {

            var temp = !list.Where(a => a.PersonTypes == types).Any();
            if (temp)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\t\t\t\t   --------------------------\n" +
                                  "\t\t\t\t   There Are No Data in List!\n" +
                                  "\t\t\t\t   --------------------------"
                                    );

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.ReadLine();
            }
            return temp;
        }

        public static bool IsStored(IEnumerable<Person> checklist, string input)
        {
            var temp = checklist.Any();
            if (temp)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\t\t\t\t   --------------------------\n" +
                                 " \t\t\t\t  {0} Input is Already Stored!\n" +
                                 "\t\t\t\t   --------------------------", input);
                Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }
            return temp;
        }

        public static bool IsStoredWhenUpdate(IEnumerable<Person> checklist, string input)
        {
            var temp = checklist.Any();
            if (temp)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\t\t\t\t   --------------------------" +
                                 " {0} Input is Already Stored!\n" +
                                 "\t\t\t\t   --------------------------", input);
                Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }
            return temp;
        }
        public static bool IsNotFoundlPersonbyID(Person person, string ID)
        {
            var temp = person is null;
            if (temp)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\t\t\t\t--------------------------\n" +
                                  "\t\t\t\t  ID {0} is Not Exist!\n" +
                                  "\t\t\t\t--------------------------", ID);
                Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }
            return temp;
        }

        
        public static bool IsNullorSpace(string input)
        {
            var temp = string.IsNullOrWhiteSpace(input);
            if (temp)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\t\t\t\t   -------------------------------------\n" +
                       "\t\t\t\t Please Enter All Require Infomation \n" +
                       "\t\t\t\t   ------------------------------------- "
                       );


                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.ReadLine();

            }
            return temp;
        }

        // check is email input valid by built-in System.Net.Mail.MailAddress
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\t\t\t\t   -------------------------------------\n" +
                       "\t\t\t\t        The Email {0} is Not Valid \n" +
                       "\t\t\t\t   ------------------------------------- ", email
                       );
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.ReadLine();
                return false;
            }
        }

        // Check is the lenght of input ID are long enough
        public static bool Is8ID(string ID, PersonTypes types)
        {

            bool resutl = ID.Length != 8;
            if (resutl)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\t\t\t\t   -------------------------------------\n" +
                       "\t\t\t\t The Input {0} ID Must Long 8 Characters\n" +
                       "\t\t\t\t   ------------------------------------- ", types
                       );

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.ReadLine();

            }
            return resutl;
        }
        public static bool Is7ID(string ID, PersonTypes types)

        {

            bool resutl = ID.Length != 7;
            if (resutl)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\t\t\t\t   -------------------------------------\n" +
                       "\t\t\t\t  The {0} ID Must Long 7 Characters\n" +
                       "\t\t\t\t   ------------------------------------- ", types
                       );

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.ReadLine();

            }
            return resutl;
        }

        // Check is the  input ID are format
        public static bool IsFormatStudent(string ID, PersonTypes types)
        {

            bool CheckFormat = (ID.Substring(0, 2) != "GT" && ID.Substring(0, 2) != "GC") ||
                             !int.TryParse(ID.Substring(2, 5), out int num);


            if (CheckFormat)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\t\t\t\t   -------------------------------------\n" +
                       "\t\t\tThe {0} ID Must Form Like GTxxxxx or GCxxxxx (x: is a digit)\n" +
                       "\t\t\t\t   ------------------------------------- ", types
                       );

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.ReadLine();
            }

            return CheckFormat;
        }

        public static bool IsFormatLecturer(string ID, PersonTypes types)
        {

            bool CheckFormat = !int.TryParse(ID, out int num);


            if (CheckFormat)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\t\t\t\t   -------------------------------------\n" +
                      "\t\t\tThe {0} ID Must Form Like xxxxxxxx (x: is a digit)\n" +
                      "\t\t\t\t   ------------------------------------- ", types
                      );

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.ReadLine();
            }

            return CheckFormat;
        }

        

      
        }

    }

