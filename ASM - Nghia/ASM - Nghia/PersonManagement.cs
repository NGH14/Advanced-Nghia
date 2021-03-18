    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    

namespace UniversitySystem
{
    class PersonManagement
    {
        /*
       It's needed to create:
          List of person,
          Function part:
                          Add, 
                          View,
                          Search,
                          Delete,
                          Update,...
       */

        // The list of input person:
        protected List<Person> ListPerson;


        // Function Part: 

        // Add
        public void Add(PersonTypes types)
        {



            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;

            Console.WriteLine("\n\t\t\t\t\t    Add {0} Form:\n" + " \t\t\t     **Please enter all the requirement information**"
                              + "\n\t\t\t\t______________________________________\n", types);
            Console.ForegroundColor = ConsoleColor.DarkBlue;

            string[] InputData;
            if (types == 0)
            {
                string[] InputF = {
                                        "\n\t\t\t\tID: ",
                                        "\n\t\t\t\tName: ",
                                        "\n\t\t\t\tDoB: ",
                                        "\n\t\t\t\tEmail: ",
                                        "\n\t\t\t\tAddress: ",
                                        "\n\t\t\t\tBatch:  ",
                                      };
                InputData = InputF;
            } else {
                string[] InputF = {
                                        "\n\t\t\t\tID: ",
                                        "\n\t\t\t\tName: ",
                                        "\n\t\t\t\tDoB: ",
                                        "\n\t\t\t\tEmail: ",
                                        "\n\t\t\t\tAddress: ",
                                        "\n\t\t\t\tDept:  ",
                                      };
                InputData = InputF;
            }

            // New list which stored the input data
            var AddData = new List<string>();

            for (int i = 0; i < 6; i++)
            {
                Console.Write(InputData[i]);
                AddData.Add(Console.ReadLine());
            }



            // Check the ID data and Email data which should not null or stored
            string[,] CheckData =  {
                                       { "0", "ID"    },
                                       { "3", "Email" }
                                                      };

            for (int i = 0; i < CheckData.Length * 0.5; i++)
            {


                // Create tempA which equal the add data
                var tempA = AddData[int.Parse(CheckData[i, 0])];

                // create temp B to check by built-in Where by LinQ
                var tempB = ListPerson.Where(p => p.PersonID == tempA);


                // It will return if it's not a standard data
                if (
                    CheckValidate.IsNullorSpace(tempA) ||
                    CheckValidate.IsStored(tempB, CheckData[i, 1])
                   )
                    return;
            }



            /*Check Is the ID input are standard:
                - The student ID of the form like GTxxxxx or GCxxxxx(x: is a digit)
                - Lecturer ID with 8 digits (fixed)*/

            if (types == 0)
            {
                if (
                    CheckValidate.Is7ID(AddData[0], types) ||
                    CheckValidate.IsFormatStudent(AddData[0], types)
                    ) return;


            }
            else
            {
                if (
                    CheckValidate.Is8ID(AddData[0], types) ||
                    CheckValidate.IsFormatLecturer(AddData[0], types)
                    ) return;
            }



            // Check Is the Email input are standard 
            var CheckEmail = AddData[3];
            if (!CheckValidate.IsValidEmail(CheckEmail)) return;


            // Create director and builders to add data 
            var directors = new Director();
            var builders = new PersonBuilder();
            directors.Builder = builders;

            var cases = new Dictionary<Func<PersonTypes, bool>, Action>
                {
                    { p => p == PersonTypes.Student,
                                () => directors.makeStudent(AddData.ToArray())  },
                    { p => p == PersonTypes.Lecturer,
                                () => directors.makeLecturer(AddData.ToArray()) },
                };
            cases.First(c => c.Key(types)).Value();


            // add to list person
            ListPerson.Add(builders.GetPerson());

            //Show successful message
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n\t\t\t\t!Adding {0} Successfully!", types);

            //Show guide
                ShowGuide(types);
        }

        // View
        public void View(PersonTypes types)
        {
            Console.Clear();

            // Check if list is null and display error message.
            if (CheckValidate.IsListNull(ListPerson, types)) return;

            // Get and display a list of Person entities that have the correspond
            // types.
            var ViewList = ListPerson.Where(p => p.PersonTypes == types);

            Console.WriteLine("\n\t\t\t\t\tView All {0} List:\n"
                               + "\t\t\t\t______________________________________\n", types);

            foreach (var a in ViewList) { Console.WriteLine(" " + a.ShowPerson()); }

            //Show guide
            ShowGuide(types);
        }

        // Search
        public void Search(PersonTypes types)
        {
            Console.Clear();

            // Check if list is null and display error message.
            if (CheckValidate.IsListNull(ListPerson, types)) return;

            // Show the guide
            Console.Write("\n\n\n\n\t\t\t__Search {0} by Name: ", types);
            var SearchData = Console.ReadLine();

            // Check is the input are standard.
            if (CheckValidate.IsNullorSpace(SearchData)) return;

            // Convert a list of results which contain the search name's to Array
            var SearchList = ListPerson.Where(p => p.PersonName.Contains(SearchData) && p.PersonTypes == types).ToArray();

            if ((SearchList.Count()) == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\t\t\t\t--------------------------\n" +
                                  "\t\t\t\tThere are No {0} Name: {1} !\n" +
                                  "\t\t\t\t--------------------------", types, SearchData);
                Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                return;
            }

            // Show the success message
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n\t\t          Search Successfully {0} {1} Name: {2}!\n", SearchList.Count(),
                                                                                           types,
                                                                                           SearchData);

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\t\t\t _______________________________________________________\n");

            // Age of person

            // Create table which show 4 information such as No, Name, ID, Batch or Department
            if (types == 0)
            {
                for (int i = 0; i < SearchList.Length; i++)
                {
                    // Age of person
                    int age = (int)((DateTime.Now - SearchList[i].PersonDoB).TotalDays / 365.242199);
                    Console.WriteLine("\t\t\t No #{0} | ID: {1} | Name: {2} | Batch: {3} | Age: {4} |", i,
                                                                                                        SearchList[i].PersonID,
                                                                                                        SearchList[i].PersonName,
                                                                                                        SearchList[i].PersonBatchorDept,
                                                                                                        age);
                }

            }
            else
            {
                for (int i = 0; i < SearchList.Length; i++)
                {
                    int age = (int)((DateTime.Now - SearchList[i].PersonDoB).TotalDays / 365.242199);
                    Console.WriteLine("\t\t\t No #{0} | ID: {1} | Name: {2} | Dept: {3} | Age: {4} |", i,
                                                                                                        SearchList[i].PersonID,
                                                                                                        SearchList[i].PersonName,
                                                                                                        SearchList[i].PersonBatchorDept,
                                                                                                        age);
                }
            }

            // Show guide for user
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("\n\t\t\t__Please Enter No. of {0} to View all Information: #", types);
            

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            // Check and show the message about the choice number:
            var num = Console.ReadLine();
            var checkNum = true ? !num.All(char.IsDigit) || num is null || num.All(char.IsWhiteSpace) : false;
            if (checkNum)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\t\t\t\t--------------------------\n" +
                                  "\t\t\t\t  The Choice {0} is Invalid!\n" +
                                  "\t\t\t\t--------------------------", num);
            
                //Show guide
                ShowGuide(types);
                return;
            }
            Console.Clear();
            Console.WriteLine("\n\t\t        __The Full Information of {0} No.{1}: \n", types, num);

            Console.WriteLine("\t\t\t ______________________________________________");

            //Show full information 
            if (types == 0) { Console.WriteLine(SearchList[int.Parse(num)].ShowPerson()); }
            else { Console.WriteLine(SearchList[int.Parse(num)].ShowPerson()); }

            //Show guide
            ShowGuide(types);
        }

        // Delete 
        public void Delete(PersonTypes types)
        {

            Console.Clear();


            /*
                Call isListnull to check the data in list.
                If have data stored, using buidt-in SingleOrDefault Linq to returns the specific element in listperson
                Using lambda eppresstion delete person have same ID with input.
            */


            if (CheckValidate.IsListNull(ListPerson, types)) return;
            Console.Write("\n\n\n\n\t\t\t__Delete {0} with ID: ", types);
            var Data = Console.ReadLine();
            var DeleteData = ListPerson.SingleOrDefault(p => p.PersonID == Data && p.PersonTypes == types);


            // Check is the ID are stored and is it null or just spacing 
            if (CheckValidate.IsNotFoundlPersonbyID(DeleteData, Data) ||
                CheckValidate.IsNullorSpace(DeleteData.PersonID))
                return;

            

            int age = (int)((DateTime.Now - DeleteData.PersonDoB).TotalDays / 365.242199);
            // make sure user want to delete this element
            
            Console.Write("\n\t\t\t__This {0} Have: \n", types);
            if (types == 0)
            {

                Console.WriteLine("\t\t\t  | ID: {0} | Name: {1} | Batch: {2} | Age: {3} |",
                                                                     DeleteData.PersonID,
                                                                     DeleteData.PersonName,
                                                                     DeleteData.PersonBatchorDept,
                                                                     age);


            }
            else
            {

                Console.WriteLine("\t\t\t  | ID: {0} | Name: {1} | Dept: {2} | Age: {3} |",
                                                                 DeleteData.PersonID,
                                                                 DeleteData.PersonName,
                                                                 DeleteData.PersonBatchorDept,
                                                                 age);

            }

            Console.WriteLine("\n\t\t\t ______________________________________________");

            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("\n\t\t          Are You Sure You Want Update This {0}? [Y/N]:  ", types);

            var num = Console.ReadLine();
            var checkNum = true ? !(num.ToString() == "Y" || num.ToString() == "Yes" || num.ToString() == "YES" || num.ToString() == "yes" ) &&
                                  !(num.ToString() == "N" || num.ToString() == "No" || num.ToString() == "NO" || num.ToString() == "no")
                          : false;
            if (checkNum)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\t\t\t\t--------------------------\n" +
                                  "\t\t\t\t  The Choice {0} is Invalid!\n" +
                                  "\t\t\t\t--------------------------", num);

                //Show guide
                ShowGuide(types);
                return;
            }

            if (num.ToString() == "Y" || num.ToString () == "Yes" || num.ToString() == "YES" || num.ToString() == "yes" )
            {
                ListPerson.Remove(DeleteData);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\n\t\t          Delete {2} with {0} ID: {1} Successfully!\n", types,
                                                                                                 DeleteData.PersonID,
                                                                                                 DeleteData.PersonName);
                ShowGuide(types);
            }
            else { ShowGuide(types); return; }
            
        }

        // Update
        public void Update(PersonTypes types)
        {
            Console.Clear();

            // Check  the list is null
            if (CheckValidate.IsListNull(ListPerson, types)) return;

            // Show guide 
            Console.Write("\n\n\n\n\t\t\t__Update {0} with ID: ", types);
            var Input = Console.ReadLine();
            var UpdateData = ListPerson.SingleOrDefault(a => a.PersonID == Input
                                                            && a.PersonTypes == types);

            // Check is input are standard
            if (
                CheckValidate.IsNotFoundlPersonbyID(UpdateData, Input)
                || CheckValidate.IsNullorSpace(UpdateData.PersonID)
               ) return;

            // Age of person
            int age = (int)((DateTime.Now - UpdateData.PersonDoB).TotalDays / 365.242199);

            Console.Write("\n\t\t\t__This {0} Have: \n", types);
            if (types == 0)
            {

                Console.WriteLine("\t\t\t  | ID: {0} | Name: {1} | Batch: {2} | Age: {3} |",
                                                                     UpdateData.PersonID,
                                                                     UpdateData.PersonName,
                                                                     UpdateData.PersonBatchorDept,
                                                                     age);


            }
            else
            {

                Console.WriteLine("\t\t\t  | ID: {0} | Name: {1} | Dept: {2} | Age: {3} |",
                                                                 UpdateData.PersonID,
                                                                 UpdateData.PersonName,
                                                                 UpdateData.PersonBatchorDept,
                                                                 age);

            }

            Console.WriteLine("\n\t\t\t ______________________________________________");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("\n\t\t          Are You Sure You Want Update This {0}? [Y/N]:  ", types);
            
            var num = Console.ReadLine();
            var checkNum = true ? !(num.ToString() == "Y" || num.ToString() == "Yes" || num.ToString() == "YES" || num.ToString() == "yes") &&
                      !(num.ToString() == "N" || num.ToString() == "No" || num.ToString() == "NO" || num.ToString() == "no")
              : false;
            if (checkNum)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\t\t\t\t--------------------------\n" +
                                  "\t\t\t\t  The Choice {0} is Invalid!\n" +
                                  "\t\t\t\t--------------------------", num);

                //Show guide
                ShowGuide(types);
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            if (num.ToString() == "Y" || num.ToString() == "Yes" || num.ToString() == "YES" || num.ToString() == "yes") 
            {
                // Create array which stored the input same which Add() Funciton
                string[] InputData;
                if (types == 0)
                {
                    string[] InputF = {
                                        "\n\t\t\t\tID: ",
                                        "\n\t\t\t\tName: ",
                                        "\n\t\t\t\tDoB: ",
                                        "\n\t\t\t\tEmail: ",
                                        "\n\t\t\t\tAddress: ",
                                        "\n\t\t\t\tBatch:  ",
                                      };
                    InputData = InputF;
                }
                else
                {
                    string[] InputF = {
                                        "\n\t\t\t\tID: ",
                                        "\n\t\t\t\tName: ",
                                        "\n\t\t\t\tDoB: ",
                                        "\n\t\t\t\tEmail: ",
                                        "\n\t\t\t\tAddress: ",
                                        "\n\t\t\t\tDept:  ",
                                      };
                    InputData = InputF;
                }

                var UpdateList = new List<string>();

                for (int i = 0; i < 6; i++)
                {
                    Console.Write(InputData[i]);
                    UpdateList.Add(Console.ReadLine());
                }
                
                // Check is the input Data ID is stored 
                var CheckData = new int[] { 0, 3 };
                for (int i = 0; i < CheckData.Length; i++)
                {
                    var Templist = ListPerson.Where(p => p.PersonID == UpdateList[CheckData[i]]);

                    if (CheckValidate.IsStoredWhenUpdate(Templist, InputData[CheckData[i]])) return;
                }

                /*Check Is the ID input are standard (""):
                - The student ID of the form like GTxxxxx or GCxxxxx(x: is a digit)
                - Lecturer ID with 8 digits (fixed)*/

                if (types == 0)
                {
                    if (
                        CheckValidate.Is7ID(UpdateList[0], types) ||
                        CheckValidate.IsFormatStudent(UpdateList[0], types)
                        ) return;


                }
                else
                {
                    if (
                        CheckValidate.Is8ID(UpdateList[0], types) ||
                        CheckValidate.IsFormatLecturer(UpdateList[0], types)
                        ) return;
                }



                // Check Is the Email input are standard 
                var CheckEmail = UpdateList[3];
                if (!CheckValidate.IsValidEmail(CheckEmail)) return;

                // Create Dictionary to change the ListPerson Data to the UpdateData 

                var UpdatetoList = new Dictionary<Func<int, bool>, Action>
                {
                    { p => p == 0, () => UpdateData.PersonID  = UpdateList[0] },
                    { p => p == 1, () => UpdateData.PersonName        = UpdateList[1] },
                    { p => p == 2, () => UpdateData.PersonDoB = DateTime.Parse(UpdateList[2],
                                                                CultureInfo.CreateSpecificCulture("vi-VN"))},
                    { p => p == 3, () => UpdateData.PersonEmail       = UpdateList[3] },
                    { p => p == 4, () => UpdateData.PersonAddress     = UpdateList[4] },
                    { p => p == 5, () => UpdateData.PersonBatchorDept    = UpdateList[5] }
                };

                for (int i = 0; i < UpdateList.Count; i++)

                    if (!string.IsNullOrWhiteSpace(UpdateList[i]))
                        UpdatetoList.First(p => p.Key(i)).Value();

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\n\t\t          Update Successfully {2} with {0} ID: {1} !\n", types,
                                                                                                 UpdateData.PersonID,
                                                                                                 UpdateData.PersonName);


                //Show guide
                ShowGuide(types);
            } else {
                ShowGuide(types);
                return;
            }
          
        }

        public void ShowGuide(PersonTypes types) 
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n\n\t\t\t<--Please Enter to Go Back {0} Management", types);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.ReadLine();
        }

       
        internal class StudentManagement : PersonManagement
        {
            internal StudentManagement(List<Person> persons)
            {
                ListPerson = persons;
                var types = PersonTypes.Student;
                int num;

                do
                {
                    Menu.SubMenuOption(types);
                    num = int.Parse(Console.ReadLine());
                    switch (num)
                    {
                        case 1:
                            Add(types);
                            break;

                        case 2:
                            View(types);
                            break;

                        case 3:
                            Search(types);
                            break;

                        case 4:
                            Delete(types);
                            break;

                        case 5:
                            Update(types);
                            break;

                        case 6:
                            Menu.Start(ListPerson).MainSubMenuOption();
                            break;



                    }


                } while (num < 7 && num > 0);
            }

            public static StudentManagement GetStudentManagement(List<Person> persons)
            {
                return new StudentManagement(persons);
            }
        }

        internal class LecturerManagement : PersonManagement
        {
            internal LecturerManagement(List<Person> persons)
            {
                ListPerson = persons;
                var types = PersonTypes.Lecturer;
                int num;

                do
                {
                    Menu.SubMenuOption(types);
                    num = int.Parse(Console.ReadLine());
                    switch (num)
                    {
                        case 1:
                            Add(types);
                            break;

                        case 2:
                            View(types);
                            break;

                        case 3:
                            Search(types);
                            break;

                        case 4:
                            Delete(types);
                            break;

                        case 5:
                            Update(types);
                            break;

                        case 6:
                            Menu.Start(ListPerson).MainSubMenuOption();
                            break;
                    }


                } while (num < 7 && num > 0);
            }

            public static LecturerManagement GetLecturerManagement(List<Person> persons)
            {
                return new LecturerManagement(persons);
            }
        }
    }
}
    

