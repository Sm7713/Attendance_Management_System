using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attnd_test.views
{
    public partial class Edit_atten : Form
    {
        database.ConnectionDB db = new database.ConnectionDB();
        AttendanceEntities att = new AttendanceEntities();
        DataTable dt = new DataTable();
        public Edit_atten()
        {
            InitializeComponent();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("1", typeof(bool));
            classes();
            days(3);
            ComboBox_days.SelectedIndex = 1;
            ComboBox_class.SelectedIndex = 2;

        }
        private void Edit_atten_Load(object sender, EventArgs e)
        {
            dt.Clear();
            ComboBox_class.SelectedIndex = 2;

            // show();
        }
        void classes()
        {
            var clas = db.classes();
            for (int i = 0; i < clas.Count; i++)
            {
                ComboBox_class.Items.Add(clas[i].id);
            }
            //days(int.Parse());
        }

        void days(int num)
        {
            var day = db.days(num);
            for (int i = 0; i < day.Count; i++)
            {
                ComboBox_days.Items.Add(day[i].Day);
            }
        }

        private void ComboBox_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt.Clear();
            show();
            // ComboBox_days.Items.Clear();


        }

        private List<Attendance> show()
        {

            int lvl = int.Parse(ComboBox_class.SelectedItem.ToString());
            var std = att.Student.Where(c => c.level == lvl).ToList();
            var atten = att.Attendance.Where(l => l.class_id == lvl).ToList();
            var month = atten.Where(t => t.time.Value.Month == DateTime.Now.Month);
            days(lvl);
            var day = month.Where(c => c.time.Value.Day == int.Parse(ComboBox_days.SelectedItem.ToString())).ToList();
            for (int i = 0; i < day.Count(); i++)
            {
                dt.Rows.Add(std[i].id, std[i].Name);
                dt.Rows[i][2] = day[i].attendance1.Value;

            }

            guna2DataGridView1.DataSource = dt;
            color(day.Count());
            days(0);
            return day;
        }
        void color(int size)
        {
            for (int i = 0; i < size; i++)
            {
                if (Convert.ToBoolean(dt.Rows[i][2]) == true)
                {
                    guna2DataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Green;
                }
                else
                {
                    guna2DataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Red;
                }

            }

        }

        private void ComboBox_days_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dt.Clear();
            //show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //int lvl = int.Parse(comboBox1.SelectedItem.ToString());
            //var students = att.Student.Where(l => l.level == lvl).ToList();
            //atten_std(students[0].id);
            //if (atten_std(students[0].id))
            //{
            List<Attendance> A = show();
            for (int i = 0; i < A.Count(); i++)
            {
                var a = att.Attendance.Where(o => Convert.ToInt32( o.id) ==Convert.ToInt32(A[i].id)).FirstOrDefault();
                a.attendance1 = Convert.ToBoolean(guna2DataGridView1.Rows[i].Cells["1"].Value);
                att.SaveChanges();

//                {

                    //student_id = students[i].id,
                    //time = DateTime.Now,
                    //attendance1 = Convert.ToBoolean(dataGridView1.Rows[i].Cells[today].Value),
                    //class_id = lvl,
                    //class_time = Convert.ToSingle(time_com.SelectedItem.ToString())
                    //    }
                    //    );

                //}

                //   
                //    MessageBox.Show("Saved Successfully", "Save");

            }
        }

    }
}
