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
    public partial class Update_std : Form
    {
        string image_path = string.Empty;
        AttendanceEntities att = new AttendanceEntities();
        int id;
        public Update_std(Student std)
        {
            InitializeComponent();
            id = std.id;
            textBox1.Text = std.Name;
            textBox2.Text = std.user_name;
            password_box.Text = std.password;
            comboBox1.SelectedText = Convert.ToString(std.level);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (check())
            {
                DialogResult result = MessageBox.Show("Are You Sure For Update Your Information ?", "Require", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Random r = new Random();
                    string current_path = Environment.CurrentDirectory + "\\images\\" + r.Next() + ".jpg";
                    try
                    {
                        File.Copy(image_path, current_path);
                        byte[] img = File.ReadAllBytes(current_path);
                        var std = att.Student.Where(i => i.id == id).FirstOrDefault();
                        std.Name = textBox1.Text;
                        std.user_name = textBox2.Text;
                        std.password = password_box.Text;
                        std.gender = gender();
                        std.image = img;
                        std.level = Convert.ToByte(comboBox1.SelectedItem);
                        att.SaveChanges();
                        MessageBox.Show("Updated Successfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "Upload The Photo");
                    }
                }
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
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
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
    }
}
