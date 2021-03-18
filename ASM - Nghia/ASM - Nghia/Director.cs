using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace UniversitySystem
{
    class Director
    {
            /*
           It's needed in the Director:
               set and get for the Builder,
               create both of make new student and lecturer
            */
        public IBuilder Builder { get; set; }

        // Student
        public void makeStudent(string[] StudentInput)
        {
            Builder.setTypes(PersonTypes.Student);
            Builder.setID(StudentInput[0]);
            Builder.setName(StudentInput[1]);

            Builder.setDoB(DateTime.Parse(StudentInput[2], CultureInfo.CreateSpecificCulture("vi-VN")));
            Builder.setEmail(StudentInput[3]);
            Builder.setAddress(StudentInput[4]);
            Builder.setBatchorDept(StudentInput[5]);
        }

        // Lecturer
        public void makeLecturer(string[] LecturerInput)
        {
            Builder.setTypes(PersonTypes.Lecturer);
            Builder.setID(LecturerInput[0]);
            Builder.setName(LecturerInput[1]);
            Builder.setDoB(DateTime.Parse(LecturerInput[2], CultureInfo.CreateSpecificCulture("vi-VN")));
            Builder.setEmail(LecturerInput[3]);
            Builder.setAddress(LecturerInput[4]);
            Builder.setBatchorDept(LecturerInput[5]);
        }
    }
}

