using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attnd_test.database
{
    class ConnectionDB
    {
        AttendanceEntities att = new AttendanceEntities();

        public List <Class> classes()
        {
            var classes = att.Class.ToList();
            return classes;
        }

        public List <DateTime> days(int num)
        {

            var room = att.Attendance.Where(l => l.class_id == num).ToList();
            var days = room.Where(t => t.time.Value.Month == DateTime.Now.Month).Select(d=>d.time.Value.Date).Distinct().ToList();
            return days;
        }

        public List<Student> show_students()
        {
           


            var students = att.Student.Where(c => c.level == 3).ToList();
           // var attn=students.Join(att.Attendance)
            return  students;
        }
    }
}
