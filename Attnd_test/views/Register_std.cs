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

namespace Attnd_test.views
{
    public partial class Register_std : Form
    {

        AttendanceEntities att = new AttendanceEntities();
        string image_path = string.Empty;
        public Register_std()
        {
            InitializeComponent();
            comboBox1.Items.Add(1);
            comboBox1.Items.Add(2);
            comboBox1.Items.Add(3);
            comboBox1.Items.Add(4);
            dateTimePicker1.MaxDate = DateTime.Now;
           // MessageBox.Show(Environment.CurrentDirectory);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        bool check()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Fill All The Boxes");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (check())
            {
                DialogResult result = MessageBox.Show("Are You Sure For Add New Student ?", "Add", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Random r = new Random();
                    string current_path = Environment.CurrentDirectory + "\\images\\" + r.Next() + ".jpg";
                    try
                    {
                        File.Copy(image_path, current_path);
                        byte[] img = File.ReadAllBytes(current_path);
                        att.Student.Add(new Student
                        {
                            // id = int.Parse(textBox2.Text),
                            Name = textBox1.Text,
                            user_name = textBox3.Text,
                            password = password_box.Text,
                            gender = gender(),
                            image = img,
                            level = Convert.ToByte(comboBox1.SelectedItem.ToString())
                        });
                        att.SaveChanges();
                        MessageBox.Show("Saved Successfully", "Save");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "Upload The Photo");
                    }
                }
              
              
            }
        }

        bool gender()
        {
            if (radioButton1.Checked)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           // int year = dateTimePicker1.Value.Day - DateTime.Now.Day;

           // label2.Text = year.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                password_box.UseSystemPasswordChar = false;
            }
            else
            {
                password_box.UseSystemPasswordChar = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog()==DialogResult.OK)
            {
                image_path = file.FileName;
                pictureBox1.ImageLocation = image_path;
            }
        }
    }
}
