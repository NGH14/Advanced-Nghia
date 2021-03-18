using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversitySystem
{
   
        internal interface IBuilder
        {
            /*
            It's needed to set in the interface:
                ID, 
                Name, 
                Day of Birth,  
                Email,  
                Address, .. for persons
             */
            public void setTypes(PersonTypes types);

            public void setID(string ID);

            public void setName(string name);

            public void setDoB(DateTime dob);

            public void setEmail(string email);

            public void setAddress(string address);

            public void setBatchorDept(string batchordept);




        }

        internal class PersonBuilder : IBuilder
        {
            /*
            It's needed in the personbuilder to:
                Declare an persons variable, 
                Create reset() function,
                Assign the input argument of persons's information,
                And assign new person to variable 
                , .. 
             */

            private Person persons;

            public void reset() => persons = new Person();

            public PersonBuilder() { reset(); }



            public void setTypes(PersonTypes types) { persons.PersonTypes = types; }

            public void setAddress(string address) { persons.PersonAddress = address; }

            public void setDoB(DateTime dob) { persons.PersonDoB = dob; }

            public void setEmail(string email) { persons.PersonEmail = email; }

            public void setID(string ID) { persons.PersonID = ID; }

            public void setName(string name) { persons.PersonName = name; }

            public void setBatchorDept(string batchordept) { persons.PersonBatchorDept = batchordept; }

            // Initilize new Person
            public Person GetPerson()
            {

                Person ps = persons;

                reset();

                return ps;
            }
        }

        internal enum PersonTypes { Student, Lecturer }

        internal class StudentBuilder : PersonBuilder
        {

            public string StudentID { get; set; }

            public string StudentName { get; set; }

            public DateTime StudentDoB { get; set; }

            public string StudentEmail { get; set; }

            public string StudentAddress { get; set; }

            public string StudentClass { get; set; }

        }
        internal class LecturerBuilder : PersonBuilder
        {
            public string LecturerID { get; set; }

            public string LecturerName { get; set; }

            public DateTime LecturerDoB { get; set; }

            public string LecturerEmail { get; set; }

            public string LecturerAddress { get; set; }

            public string LecturerDept { get; set; }
        }
        internal class Person
        {
            public PersonTypes PersonTypes { get; set; }

            public string PersonBatchorDept { get; set; }

            public string PersonID { get; set; }

            public string PersonName { get; set; }

            public DateTime PersonDoB { get; set; }

            public string PersonEmail { get; set; }

            public string PersonAddress { get; set; }

            
            
            
            

            // Show information func
            public string ShowPerson()
            {
                string types = "";
                // Use dictionary to get the key(Types of person) and value (types)
                    var DifferentTypes = new Dictionary<Func<PersonTypes, bool>, Action>
                {
                    { p => p == PersonTypes.Student,  () => types = "Batch"      },
                    { p => p == PersonTypes.Lecturer, () => types = "Dept" }
                };
                    DifferentTypes.First(p => p.Key(PersonTypes)).Value();

                return 
                    " | ID: " + PersonID + " | Name: " + PersonName +
                    " | DoB: " + PersonDoB.ToString("dd-MM-yy") + " | Email: " + PersonEmail + " " +
                    " | Address: " + PersonAddress + " | " + types + ": " + PersonBatchorDept;
            }
    

        }
    }


