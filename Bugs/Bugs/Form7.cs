﻿using Bugs.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bugs
{
    public partial class Form7 : Form
    {
        public UserContext DB { get; set; }
        
        public Form7()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form9 addForm = new Form9();
            addForm.DB = this.DB;
            addForm.ShowDialog();
            dataGridView1.DataSource = DB.Workers.ToList();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {

            string connectionString = @"data source=(LocalDB)\v11.0; Initial Catalog = userstore; Integrated Security=True;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand com = new SqlCommand("DELETE FROM workers WHERE Login=@id", con);
                com.Parameters.AddWithValue("@id", textBox1.Text);
                con.Open(); //Открываем подключение
                try
                {
                    com.ExecuteNonQuery();
                    MessageBox.Show("Пользователь удален");
                }
                catch
                {
                    MessageBox.Show("Удалить не удалось!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) //Редактировать
        {
            var login = dataGridView1.CurrentRow.Cells[0].Value;
            WORKER w = (from worker in DB.Workers
                        where worker.Login == login
                        select worker).FirstOrDefault<WORKER>();
            Form10 addForm = new Form10();
            addForm.Workerq = w;
            addForm.DB = this.DB;
            addForm.ShowDialog();
            dataGridView1.DataSource = DB.Workers.ToList();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            DB = new UserContext();
            dataGridView1.DataSource = DB.Workers.ToList();
            dataGridView1.Columns[0].HeaderText = "Логин";
            dataGridView1.Columns[1].HeaderText = "Роль админа";
            dataGridView1.Columns[2].HeaderText = "Пароль";
        }
    }
}
