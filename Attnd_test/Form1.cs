using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;

namespace Attnd_test
{
    public partial class Form1 : Form
    {

        AttendanceEntities att = new AttendanceEntities();
       
        DataTable dt = new DataTable();
        List<int> month;
        CurrencyManager cm;
         

        public Form1()
        {
            InitializeComponent();
            grid(DateTime.Now.Month);
            dataGridView1.ReadOnly = false;
            timer1.Start();
            levels();
            comboBox1.SelectedIndex = 1;
            time_com.Text = "Time Of Class";
            months();
            cm = (CurrencyManager)this.BindingContext[month];
            //subject_titles();
        }

        private void months()
        {
            month = new List<int>();
            var months = att.Attendance.Select(m => m.time.Value.Month).Distinct().ToList();
            for (int i = 0; i < months.Count; i++)
            {
                month.Add(months[i]);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //int lvl = int.Parse(comboBox1.SelectedItem.ToString());
            dt.Clear();
            attendance(5);
        }

        DateTimePicker d = new DateTimePicker();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        void subject_titles()
        {
            // var subjects = att.Subject.Select(s => s.title).ToList();
            subjects_com.Items.Clear();
            int subject = int.Parse(comboBox1.SelectedItem.ToString());
            var subjects = from s in att.Subject where s.class_id == subject select s.title;
            foreach (var sub in subjects)
            {
                subjects_com.Items.Add(sub);
            }
        }
        void levels()
        {
            var level = att.Student.Select(l => l.level).Distinct();
            foreach (var lvl in level)
            {
                comboBox1.Items.Add(lvl);  
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
          
        
        }

        void grid(int month)
        {
            dt.Reset();

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            // dt.Columns.Add("Date", typeof(string));

            for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year, month)/*(d.Value.Day + 10)*/; i++)
            {
                DateTime day = new DateTime(DateTime.Now.Year, month, i);
                if (day.DayOfWeek != DayOfWeek.Friday)
                {
                    dt.Columns.Add(+i + "", typeof(bool));
                }


            }
            /*  dt.Rows.Add(1, "Mohammed Saeed","1998-1-19",true);
              dt.Rows.Add(2, "Salim", "2000-3-11",false);
              dt.Rows.Add(3, "Saeed", "2010-10-31",true);*/

            //attendance(month);
            dataGridView1.DataSource = dt;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            DateTime t = new DateTime();
            label1.Text = d.Value.ToShortTimeString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            string today = DateTime.Now.Day.ToString();
            if (checkBox1.Checked)
            {
                //code for check all column in one day
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[today];
                    chk.Value = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[today];
                    chk.Value = false;
                }
            }
        }

        private void savbtn_Click(object sender, EventArgs e)
        {
                string today = DateTime.Now.Day.ToString();
            if (combo_null(time_com)&&combo_null(comboBox1))
            {
                int lvl = int.Parse(comboBox1.SelectedItem.ToString());
                var students = att.Student.Where(l => l.level == lvl).ToList();
                 atten_std(students[0].id);
                DialogResult result = MessageBox.Show("Are You Sure For Store The Current Attendance ?", "Conif", MessageBoxButtons.YesNo);
                if (result==DialogResult.Yes)
                {
                    if (atten_std(students[0].id))
                    {
                        for (int i = 0; i < students.Count(); i++)
                        {
                            att.Attendance.Add(new Attendance
                            {
                                student_id = students[i].id,
                                time = DateTime.Now,
                                attendance1 = Convert.ToBoolean(dataGridView1.Rows[i].Cells[today].Value),
                                class_id = lvl,
                                class_time = Convert.ToSingle(time_com.SelectedItem.ToString())
                            }
                            );

                        }

                        att.SaveChanges();
                        MessageBox.Show("Saved Successfully", "Save");
                    }
                    else
                        MessageBox.Show("The Student Alredy Attendance to Day", "Alarem");

                }


            }
            else
            {
                MessageBox.Show("Choose the Time from Bottom Combobox", "Require");
            }
          
        }

        bool atten_std(int id)
        {
            bool? a = att.Attendance.Where(b => b.student_id == id && b.time.Value.ToString()
            .Contains(DateTime.Today.ToString()))
                .Select(b => b.attendance1.Value).FirstOrDefault();
            if (a.Value)
            {
                return false;
            }
            else
                return true;
        }

        bool combo_null(ComboBox com)
        {
            if (com.SelectedIndex<0)
            {
                return false;
                //MessageBox.Show("Fill the blanck");
            }
            else
            {
                return true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToShortTimeString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            dt.Clear();
            attendance(DateTime.Now.Month);
            subject_titles();
        }

        private void attendance(int month)
        {
           // dt.Clear();
            
            int lvl = int.Parse(comboBox1.SelectedItem.ToString());
            var std = att.Student.Where(l => l.level == lvl).ToList();
            var check = att.Attendance.Where(s => s.class_id == lvl).ToList();
            for (int i = 0; i < std.Count; i++)
            {
                int f = 0;
                dt.Rows.Add(std[i].id, std[i].Name);
                var c = check.Where(q => q.time.Value.Month == month).ToList();
                var r = c.Where(v => v.student_id == std[i].id).ToList();
                if (r.Count > 0)
                {
                    for (int j = 1; j <= r.Count; j++)
                    {
                        DateTime z = r[f].time.Value;
                        dt.Rows[i][Convert.ToString(z.Day)] = r[f].attendance1.Value;
                        if (Convert.ToBoolean(dt.Rows[i][Convert.ToString(z.Day)]) == true)
                        {
                            dataGridView1.Rows[i].Cells[Convert.ToString(z.Day)].Style.BackColor = Color.Green;
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells[Convert.ToString(z.Day)].Style.BackColor = Color.Red;
                        }
                        f++;
                    }
                }
                //dataGridView1.DataSource = dt;
            }
        }
        private void time_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            cm.Position = 0;
            // MessageBox.Show(cm.Current.ToString());
            int num = int.Parse(cm.Current.ToString());
            //DataView v = new DataView(dt);
            //v.RowFilter("");
            grid(num);
            attendance(num);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            cm.Position = month.Count - 1;
            int num = int.Parse(cm.Current.ToString());

            grid(num);
            //attendance(num);

        }
    }
}
