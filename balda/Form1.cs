using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace balda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        char[] first_word = new char[5];

        private void Form1_Load(object sender, EventArgs e)
        {
            greeting f2 = new greeting();
            f2.ShowDialog();
            label5.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label1.Text = greeting.str;
            label2.Text = greeting.str2;
            dataGridView1.ColumnCount = 5;//число столбцов в обьекте 
            dataGridView1.RowTemplate.Height = 50;
            dataGridView1.RowHeadersVisible = false;//убираем заголовок строк 
            dataGridView1.AllowUserToAddRows = false;//убираем параметр добавления строк
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.AllowUserToResizeColumns = false;//запрещаем изменять размер столбцов
            dataGridView1.AllowUserToResizeRows = false;//запрещаем изменять размер строк
            dataGridView1.Rows.Add(5);
            for (int i = 0; i < 5; i++)
                dataGridView1.Columns[i].Width = 50;//размер высоты строки
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    dataGridView1[i, j].Style.Font = new Font(FontFamily.GenericSansSerif, 27);//шрифт
            find_first_word(ref first_word, ref dataGridView1);//заносим слово из пяти букв в третюю строку dataGridView1
        }

        public void find_first_word(ref char[] word, ref DataGridView dataGridView)//Ф-ция записи первого слоа в dataGridView 
        {
            int size = 0;
            int j = 0;
            string line;
            char[][] mas = new char[0][];//временный массив слов из пяти букв
            StreamReader st = new StreamReader("lib.RUS", Encoding.Default);
            while ((line = st.ReadLine()) != null)
            {
                if (line.Length == 5)
                {
                    size++;
                    char[] split_word = new char[line.Length];
                    for (int i = 0; i < line.Length; i++)
                        split_word [i] = line[i];
                    Array.Resize(ref mas, size);
                    Array.Resize(ref mas[j], 5);
                    for (int i = 0; i < line.Length; i++)
                        mas[j][i] = split_word[i];
                    j++;
                }
            }
            st.Close();
            Random r = new Random();//генерируем случайное число
            int t = r.Next(0, size);
            for (int i = 0; i < 5; i++)
                word[i] = mas[t][i];

            for (int i = 0; i < word.Length; i++)
            {
                dataGridView1.Rows[2].Cells[i].Value = word[i].ToString();//заполнение ячейки
            }
        }
     }
}
