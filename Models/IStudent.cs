using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public interface IStudent
    {
        public Student getStudent(int id);
        IEnumerable<Student> GetAllStudent();
        Student Add(Student stobject);
        Student Update(Student studentChange);
        Student Delete(int id);
    }
}
