using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Attnd_test.views
{
    public partial class Atten_stud : Form
    {
        AttendanceEntities att = new AttendanceEntities();
        Student student;
        int id ;
        public Atten_stud(Student std)
        {
            InitializeComponent();
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            student = std;
            id = std.id;
            Image_std();

        }
        private void Atten_stud_Load(object sender, EventArgs e)
        {
            chart();

        }
        private void chart1_Click(object sender, EventArgs e)
        {

        }
        void chart()
        {
            //chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            chart1.Series[0].Points.AddXY("Present", att.Attendance.Where(i=>i.student_id== id&& i.attendance1==true).Count());
            chart1.Series[0].Points.AddXY("Absent", att.Attendance.Where(i => i.student_id == id && i.attendance1 == false).Count());
            label4.Text = Convert.ToString(Math.Round(att.Attendance.Where(f => f.student_id == id && f.attendance1 == true).Sum(t => t.class_time).Value));
            try
            {
                label3.Text = Convert.ToString(Math.Round(att.Attendance.Where(i => i.student_id == id && i.attendance1 == false).Sum(t => t.class_time).Value));

            }catch(Exception e)
            {
                label3.Text = Convert.ToString("0");

            }


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //void months()
        //{
        //    var months = att.Attendance.Select(m => m.time.Value.Month).Distinct();
        //    foreach (var month in months)
        //    {
        //        comboBox1.Items.Add(month);
        //    }
        //}

        private void button1_Click(object sender, EventArgs e)
        {
           

            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void Image_std()
        {
            try {
                var student = att.Student.SingleOrDefault(i => i.id == id);
                var ms = new MemoryStream(student.image);
                pictureBox1.Image = Image.FromStream(ms);
            }
            catch (Exception e)
            {

            }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!IsFormOpen(typeof(Update_std)))
            {
                Update_std form = new Update_std(student);
                form.Show();
            }
        }

        private bool IsFormOpen(Type formType)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == formType)
                {
                    return true;
                }
            }
            return false;
        }

        void open()
        {
            //Application.Run(new Update_std());
        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //loginFrm f = new loginFrm();
            //f.Show();
            //this.Hide();
        }
    }
}
