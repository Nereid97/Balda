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
        string first_word1 = "";
        bool[,] Priznak = new bool[5, 5];
        int [][] PArr = new int [0][];//координаты букв выделенного слова
        bool WasInitializated = false;
        bool Bukva = false;
        bool p = true;
        bool pr = false;
        bool first = true;
        bool prr = true;//признак координат первой буквы
        bool con = false;//признак существования введенной буквы в выделенном слове
        bool exists = false;//признак существования слова в листбоксе
        int f = 0;//длинна массива PArr
        int x = 0;
        int y = 0;
        int x1 = 0;
        int y1 = 0;
        int x_t = 0;
        int y_t = 0;
        int frag_p1;
        int frag_p2;
        int count = 0;
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
            for (int i = 0; i < 5; i++)
                first_word1+= first_word[i];
            dataGridView1.Rows[4].ReadOnly = true;
            dataGridView1.Rows[2].ReadOnly = true;
            dataGridView1.Rows[0].ReadOnly = true;
            WasInitializated = true;
        }
        public void exists_the_word(ListBox listbox1, ListBox listbox2, Label label, ref bool b)//Ф-ция проверки существования слова в листбоксе
        {
            if (label5.Text == first_word1)
            {
                b = true;
                MessageBox.Show("Нельзя использовать первое слово!!!");
            }
            else
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    if (listbox1.Items[i].ToString() == label5.Text && b != true)
                    {
                        b = true;
                        MessageBox.Show("Такое слово уже было использовано!!!");
                        break;
                    }
                }
                for (int i = 0; i < listBox2.Items.Count; i++)
                    if (listbox2.Items[i].ToString() == label5.Text && b != true)
                    {
                        b = true;
                        MessageBox.Show("Такое слово уже было использовано!!!");
                        break;
                    }
            }
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
                Priznak[2, i] = true;
            }
        }
        public void SearchString(string str1, ref bool found)//ф-ция поиска слова в библиотеке
        {
            string line;
            found = false;
            StreamReader st = new StreamReader("lib.RUS", Encoding.Default);
            while ((line = st.ReadLine()) != null)
            {
                if (line == str1)
                {
                    found = true;
                    break;
                }
            }
            st.Close();
            if (found != true)
                MessageBox.Show("Нет такого слова!");
        }
        public void check_letters_in_word(int[][] mas, int x_z, int y_z, ref bool pr)//ф-ция проверки вписаной буквы в слове
        {
            pr = false;
            int x1 = 0;
            int y1 = 0;
            for (int i = 0; i < mas.Length; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (j == 0) x1 = mas[i][j];
                    if (j == 1) y1 = mas[i][j];
                }
                if (x_z == x1 && y_z == y1)
                {
                    pr = true;
                    break;
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (WasInitializated)
            {
                string temp;
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    temp = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                     if (temp.Length > 1)
                    {
                        temp = temp[0].ToString();
                    }
                        if (char.IsDigit(temp[0]))
                        {
                            dataGridView1[e.ColumnIndex, e.RowIndex].Value = null;
                            return;
                        }
                    temp = temp.ToString().ToUpper();
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = temp.ToString();
                    Priznak[e.RowIndex, e.ColumnIndex] = true;
                    if (prr == true)
                    {
                        x_t = e.RowIndex;
                        y_t = e.ColumnIndex;
                        prr = false;
                    }
                }
                
            }
        }
        public void Open_cell(int x_o, int y_o)//Ф-ция открытия ячеек
        {

            if (x_o == 1)
                dataGridView1.Rows[x_o - 1].Cells[y_o].ReadOnly = false;
            if (x_o == 3)
                dataGridView1.Rows[x_o + 1].Cells[y_o].ReadOnly = false;
            if ((x_o == 0 && y_o == 0) || (x_o == 4 && y_o == 0))
                dataGridView1.Rows[x_o].Cells[y_o + 1].ReadOnly = false;
            if ((x_o == 0 && y_o == 4) || (x_o == 4 && y_o == 4))
                dataGridView1.Rows[x_o].Cells[y_o - 1].ReadOnly = false;
            if (y_o == 1 || y_o == 2 || y_o == 3)
            {
                dataGridView1.Rows[x_o].Cells[y_o - 1].ReadOnly = false;
                dataGridView1.Rows[x_o].Cells[y_o + 1].ReadOnly = false;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null &&
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
            {
                x1 = e.RowIndex;
                y1 = e.ColumnIndex;
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                button1.Enabled = true;
                dataGridView1.ReadOnly = true;
                return;
            }
            
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Point coordinate = new Point();
            if (Bukva == true && dataGridView1[e.X/50,e.Y/50].Value!=null)
            {
                coordinate.X = e.X / 50;
                coordinate.Y = e.Y / 50;
                if (p)
                {
                    f++;
                    Array.Resize(ref PArr, f);
                    f--;
                    for (int i = 0; i < 1; i++)
                        Array.Resize(ref PArr[f], 2);
                    PArr[f][0] = e.Y / 50;
                    PArr[f][1] = e.X / 50;
                    f++;
                    dataGridView1.Rows[coordinate.Y].Cells[coordinate.X].Style.BackColor = Color.Aqua;
                    label5.Text += dataGridView1.Rows[coordinate.Y].Cells[coordinate.X].Value.ToString();
                    x = coordinate.X;
                    y = coordinate.Y;
                    p = false;
                }
                else
                {
                    if (((Math.Abs(coordinate.X - x)) == 1 && (Math.Abs(coordinate.Y - y) == 0)) || ((Math.Abs(coordinate.X - x)) == 0 && (Math.Abs(coordinate.Y - y) == 1)))
                    {
                        f++;
                        Array.Resize(ref PArr, f);
                        f--;
                        for (int i = 0; i < 1; i++)
                            Array.Resize(ref PArr[f], 2);
                        PArr[f][0] = e.Y / 50;
                        PArr[f][1] = e.X / 50;
                        f++;
                        if (first == true)
                            button3.Enabled = true;//первый игрок
                        else
                            button2.Enabled = true;//второй игрок
                        x = coordinate.X;
                        y = coordinate.Y;
                        dataGridView1.Rows[coordinate.Y].Cells[coordinate.X].Style.BackColor = Color.Aqua;
                        label5.Text += dataGridView1.Rows[coordinate.Y].Cells[coordinate.X].Value.ToString();

                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)//выделить слово
        {
            Bukva = true;
            dataGridView1.ReadOnly = true;
            button1.Enabled = false;
            button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)//отмена
        {
            button1.Enabled = true;
            button4.Enabled = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.ClearSelection();
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
            p = true;//признак выделения первой буквы
            Bukva = false;
            label5.Text = null;
            prr = false;
            PArr = new int[0][];
            f = 0;
            con = false;
        }
        private void button2_Click(object sender, EventArgs e)//второй игрок добавляет слово
        {
            string temp;
            temp = label5.Text;
            check_letters_in_word(PArr, x_t, y_t, ref con);//провряем существование буквы в слове
            if (con == true)
            {
                    SearchString(temp, ref pr);
                    if (pr == true)
                    {
                         exists_the_word(listBox1, listBox2, label5, ref exists);
                         if (exists == false)
                         {
                             listBox2.Items.Add(label5.Text);
                             frag_p2 += temp.Length;
                             Frags2.Text = frag_p2.ToString();
                             label5.Text = null;
                             for (int i = 0; i < 5; i++)
                                 for (int j = 0; j < 5; j++)
                                     dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                             first = true;
                             button2.Enabled = false;
                             button4.Enabled = false;
                             dataGridView1.ReadOnly = false;
                             count++;
                         }
                         else
                         {
                             label5.Text = null;
                             label5.Text = null;
                             button1.Enabled = false;//Выделить слово
                             button3.Enabled = false;
                             button4.Enabled = false;//отмена
                             dataGridView1.ReadOnly = false;
                             dataGridView1.Rows[x1].Cells[y1].Value = null;
                             for (int i = 0; i < 5; i++)
                                 for (int j = 0; j < 5; j++)
                                     dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                             button2.Enabled = false;
                         }
                    }
                    else
                    {
                        label5.Text = null;
                        button1.Enabled = false;//Выделить слово
                        button2.Enabled = false;
                        button4.Enabled = false;//отмена
                        dataGridView1.ReadOnly = false;
                        dataGridView1.Rows[x1].Cells[y1].Value = null;
                        dataGridView1.ClearSelection();
                        for (int i = 0; i < 5; i++)
                            for (int j = 0; j < 5; j++)
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                        Priznak[x_t, y_t] = false;
                    }
            }
            else
            {
                button1.Enabled = true;
                button4.Enabled = false;
                dataGridView1.ReadOnly = false;
                dataGridView1.ClearSelection();
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                label5.Text = null;
                MessageBox.Show("Выделяемое cлово должно содержать введенную букву!");
                dataGridView1.Rows[x_t].Cells[y_t].Value = null;
                button2.Enabled = false;
                button1.Enabled = false;
            }
            p = true;
            prr = true;
            PArr = new int[0][];
            f = 0;
            con = false;
            Bukva = false;
            exists = false;
            dataGridView1.Rows[4].ReadOnly = true;
            dataGridView1.Rows[2].ReadOnly = true;
            dataGridView1.Rows[0].ReadOnly = true;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (Priznak[i, j] == true)
                    {
                        Open_cell(i, j);
                    }
            dataGridView1.Rows[2].ReadOnly = true;
            end_game(count, label2);
        }
        private void button3_Click(object sender, EventArgs e)//первый игрок добавляет слово
        {
            string temp;
            temp = label5.Text;
            check_letters_in_word(PArr, x_t, y_t,ref con);//провряем существование новой буквы в слове
            if (con == true)
            {
                    SearchString(temp, ref pr);
                    if (pr == true)
                    {
                        exists_the_word(listBox1, listBox2, label5, ref exists);
                        if (exists == false)
                        {
                            listBox1.Items.Add(label5.Text);
                            frag_p1 += temp.Length;
                            Frags1.Text = frag_p1.ToString();
                            label5.Text = null;
                            for (int i = 0; i < 5; i++)
                                for (int j = 0; j < 5; j++)
                                    dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                            first = false;
                            button3.Enabled = false;
                            button4.Enabled = false;
                            dataGridView1.ReadOnly = false;
                            count++;
                        }
                        else
                        {
                            label5.Text = null;
                            button1.Enabled = false;//Выделить слово
                            button2.Enabled = false;
                            button4.Enabled = false;//отмена
                            dataGridView1.ReadOnly = false;
                            dataGridView1.Rows[x1].Cells[y1].Value = null;
                            dataGridView1.ClearSelection();
                            for (int i = 0; i < 5; i++)
                                for (int j = 0; j < 5; j++)
                                    dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                            button3.Enabled = false;
                        }
                    }
                    else
                    {
                        label5.Text = null;
                        button1.Enabled = false;//Выделить слово
                        button3.Enabled = false;
                        button4.Enabled = false;//отмена
                        dataGridView1.ReadOnly = false;
                        dataGridView1.Rows[x1].Cells[y1].Value = null;
                        for (int i = 0; i < 5; i++)
                            for (int j = 0; j < 5; j++)
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                        Priznak[x_t, y_t] = false;
                    }
            }
            else
            {
                button1.Enabled = true;
                button4.Enabled = false;
                dataGridView1.ReadOnly = false;
                dataGridView1.ClearSelection();
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;

                label5.Text = null;
                MessageBox.Show("Выделяемое слово должно содержать введенную букву!");
                dataGridView1.Rows[x_t].Cells[y_t].Value = null;
                button3.Enabled = false;
                button1.Enabled = false;
            }
            p = true;
            prr = true;
            PArr = new int[0][];
            f = 0;
            con = false;
            Bukva = false;
            exists = false;
            dataGridView1.Rows[4].ReadOnly = true;
            dataGridView1.Rows[2].ReadOnly = true;
            dataGridView1.Rows[0].ReadOnly = true;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (Priznak[i, j] == true)
                    {
                        Open_cell(i, j);
                    }
            dataGridView1.Rows[2].ReadOnly = true;
            end_game(count, label1);
        }
        public void end_game(int count,Label label)
        {
            if (count == 25)
            {
                MessageBox.Show("Выиграл " + label.Text);
                dataGridView1.ReadOnly = true;
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1[e.ColumnIndex, e.RowIndex].Value != null)
                dataGridView1[e.ColumnIndex, e.RowIndex].ReadOnly = true;
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1[e.ColumnIndex, e.RowIndex].Value != null)
                dataGridView1[e.ColumnIndex, e.RowIndex].ReadOnly = true;
        }
        private void dataGridView1_CellErrorTextChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1[e.ColumnIndex, e.RowIndex].Value != null)
                dataGridView1[e.ColumnIndex, e.RowIndex].ReadOnly = true;
        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1[e.ColumnIndex, e.RowIndex].Value != null)
                dataGridView1[e.ColumnIndex, e.RowIndex].ReadOnly = true;
        }
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1[e.ColumnIndex, e.RowIndex].Value != null)
                dataGridView1[e.ColumnIndex, e.RowIndex].ReadOnly = true;
        }
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1[e.ColumnIndex, e.RowIndex].Value != null)
                dataGridView1[e.ColumnIndex, e.RowIndex].ReadOnly = true;
        }
     }
}
