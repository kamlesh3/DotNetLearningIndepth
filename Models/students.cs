using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{

    public class students : IStudent
    {
        private List<Student> _studentList;

        public students()
        {
            _studentList = new List<Student>()
            {
                new Student() {id=1,name="Kamlesh Pal",department=Dept.CSE,email="kamlesh@gmail.com" },
                new Student() {id=2,name="Arbind Rauniyar",department=Dept.CSE,email="Arbind@gmail.com" },
                new Student() {id=3,name="Binod kumar sah",department=Dept.BPharmacy,email="Binod@gmail.com" },
                new Student() {id=4,name="Sanjay Kumar Mehta",department=Dept.Mechanical,email="Sanjay@gmail.com" }
            };
        }

        public Student Add(Student stobject)
        {
            stobject.id= _studentList.Max(e => e.id) + 1;
            _studentList.Add(stobject);
            return stobject;

        }

        public Student Delete(int id)
        {
            var student = _studentList.FirstOrDefault(e => e.id == id);
            if (student != null)
            {
                _studentList.Remove(student);
            }
            return student;
        }

        public IEnumerable<Student> GetAllStudent()
        {
            return _studentList;
        }

        public Student getStudent(int id)
        {
            return _studentList.FirstOrDefault(e => e.id == id);
        }

        public Student Update(Student studentChange)
        {
            var student = _studentList.FirstOrDefault(e => e.id == studentChange.id);
            if (student != null)
            {
                student.name = studentChange.name;
                student.email = studentChange.email;
                student.department = studentChange.department;
            }
            return student;
        }
    }
}
