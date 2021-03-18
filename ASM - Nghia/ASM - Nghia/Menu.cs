using System;
using System.Collections.Generic;
using System.Text;

namespace UniversitySystem
{
    class Menu
    {
        /*
        This is the main menu file in university which have:
            -Person list which contain the student and lecturer entities
            -Menu which created for a default class when start application,
            -Start which return new Menu of a person instance 
        */

        //  Menu for person part
        public static List<Person> person;
        public Menu(List<Person> persons) { person = persons; }
        public static Menu Start(List<Person> persons) { return new Menu(persons); }

        // The Menu Part
        public void MainSubMenuOption()
        {
            Console.Clear();
            Console.WriteLine(
                "\n\t\t\t\t_________ University System ________\n\n" +
                "\n\t\t\t\t1.\t  Manage Students\n" +
                "\n\t\t\t\t2.\t  Manage Lecturers\n" +
                "\n\t\t\t\t3.\t  Exit\n" +
                "\n\t\t\t\t____________________________________\n\n");

            Console.Write("\t\t\t\t=>  ");
            int num = int.Parse(Console.ReadLine());
            do
            {
                switch (num)
                {
                    case 1:
                        PersonManagement.StudentManagement.GetStudentManagement(person);
                        break;

                    case 2:
                        PersonManagement.LecturerManagement.GetLecturerManagement(person);
                        break;

                    case 3:
                        Environment.Exit(0);
                        break;
                };

            } while (num > 3 && num < 1);

        }

        // The Sub Menu Part
        public static void SubMenuOption(PersonTypes types)
        {
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t___________ {0} Management ___________\n\n" +
                              "\n\t\t\t\t1.          Add New {0}s\n" +
                              "\n\t\t\t\t2.          View All {0}\n" +
                              "\n\t\t\t\t3.          Search {0}\n" +
                              "\n\t\t\t\t4.          Delete {0}s\n" +
                              "\n\t\t\t\t5           Update {0}\n" +
                              "\n\t\t\t\t6.          Back To Main Menu\n" +
                              "\n\t\t\t\t______________________________________\n",
                              types);
            Console.Write("\t\t\t\t=> ");
        }

    }
}
