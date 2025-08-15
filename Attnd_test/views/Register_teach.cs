using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attnd_test.views
{
    public partial class Register_teach : Form
    {
        string image_path = string.Empty;
        AttendanceEntities att = new AttendanceEntities();
        public Register_teach()
        {
            InitializeComponent();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            if (check_feiles())
            {
                MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("False");
            }
        }

        bool check_feiles()
        {
            if (string.IsNullOrEmpty(TextBox1.Text))
            {
                TextBox1.BorderColor = Color.Red;
                return false;
            }
            else if (string.IsNullOrEmpty(TextBox2.Text))
            {
                TextBox2.BorderColor = Color.Red;
                return false;
            }
            else if (string.IsNullOrEmpty(TextBox3.Text))
            {
                TextBox3.BorderColor = Color.Red;
                return false;
            }
            else
            {
                return true;
            }

        }
        private void gunaSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            //if (gunaSwitch1.Checked)
            //{
            //    Button1.Visible = false;
            //}
            //else
            //    Button1.Visible = true;
        }

        private void Register_teach_Load(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //Random r = new Random();
            //string current_path = Environment.CurrentDirectory + "\\images\\" + r.Next() + ".jpg";
            //File.Copy(image_path, current_path);
            //byte[] img = File.ReadAllBytes(current_path);
            DialogResult result = MessageBox.Show("Are You Sure For Add New Teacher/Doctor ?", "Conif", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                att.Teacher.Add(new Teacher
                {
                    // id = int.Parse(textBox2.Text),
                    Name_teach = TextBox1.Text,
                    password = TextBox3.Text,
                    user_name = TextBox2.Text,

                });

                att.SaveChanges();
                MessageBox.Show("Saved Successfully", "Save");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                image_path = file.FileName;
                pictureBox1.ImageLocation = image_path;
            }
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox1.Checked)
            {
                TextBox3.UseSystemPasswordChar = false;
            }
            else
            {
                TextBox3.UseSystemPasswordChar = true;
            }
        }
    }
}
