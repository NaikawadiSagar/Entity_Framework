using Student_Management_System.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System
{
    public partial class frm_Collage_Form : Form
    {
        CollageEntities DB = new CollageEntities();
        Student Std = new Student();

        int N_ID = 0;

        public frm_Collage_Form()
        {
            InitializeComponent();
            Data_Show();
        }

        void Clear_Controls()
        {
            tb_Name.Clear();
            tb_Age.Clear();
            tb_Standard.Clear();
            cmb_Gender.SelectedIndex = -1;
        }
        void Data_Show()
        {
            dataGridView1.DataSource = DB.Students.ToList<Student>();
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            Std.Name = tb_Name.Text;
            Std.Age = Convert.ToInt32( tb_Age.Text);
            Std.Gender = cmb_Gender.SelectedItem.ToString();
            Std.Standard = Convert.ToInt32( tb_Standard.Text);

            DB.Students.Add(Std);

           int Res =  DB.SaveChanges();

            if(Res > 0)
            {
                MessageBox.Show("Data Inserted Successfully!!!", "", MessageBoxButtons.OK);
                Data_Show();
            }
            else
            {
                MessageBox.Show("Data Inserted Failed");
            }
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            Clear_Controls();
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            Std.Name = tb_Name.Text;
            Std.Age = Convert.ToInt32(tb_Age.Text);
            Std.Gender = cmb_Gender.SelectedItem.ToString();
            Std.Standard = Convert.ToInt32(tb_Standard.Text);

            DB.Entry(Std).State = System.Data.Entity.EntityState.Modified;

            int Res = DB.SaveChanges();

            if (Res > 0)
            {
                MessageBox.Show("Data Updated Successfully");
                Data_Show();
            }
            else
            {
                MessageBox.Show("Data Updated Failed");
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            DialogResult Dr = MessageBox.Show("Are Your Sure To Delete Data ", "", MessageBoxButtons.YesNo);
            
            if(Dr == DialogResult.Yes)
            {
                Std = DB.Students.Where(X => X.ID == N_ID).FirstOrDefault();
                DB.Entry(Std).State = System.Data.Entity.EntityState.Deleted;

                int Res = DB.SaveChanges();

                if (Res > 0)
                {
                    MessageBox.Show("Data Deleted Successfully");
                    Data_Show();
                }
                else
                {
                    MessageBox.Show("Data Deleltd Failed");
                }

            }
        }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            N_ID = Convert.ToInt32( dataGridView1.CurrentRow.Cells[0].Value);

            Std = DB.Students.Where(X => X.ID == N_ID).FirstOrDefault();

            tb_Name.Text = Std.Name;
            tb_Age.Text = Convert.ToString( Std.Age);
            cmb_Gender.SelectedItem = Std.Gender;
            tb_Standard.Text = Convert.ToString ( Std.Standard);
            
        }

        private void frm_Collage_Form_Load(object sender, EventArgs e)
        {

        }
    }
}
