using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace simplexLaba
{
    public partial class Form1 : Form
    {
        Fraction[,] arr;
        Fraction[] funArr;
        int[] basis;
        Fraction[,] startArr;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            functionDataGrid.RowHeadersVisible = false;
            functionDataGrid.AllowUserToResizeRows = false;
            functionDataGrid.AllowUserToResizeColumns = false;
            functionDataGrid.AllowUserToAddRows = false;
            functionDataGrid.EnableHeadersVisualStyles = false;
            functionDataGrid.RowHeadersWidth = 120;
            functionDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            functionDataGrid.RowHeadersVisible = true;
            functionDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            functionDataGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            functionDataGrid.AutoSize = true;

            functionDataGrid.ColumnCount = 3;
            functionDataGrid.RowCount = 3;

            variablesUpDown.Value = 3;
            variablesUpDown.Maximum = 16;
            variablesUpDown.Minimum = 1;
            equationsUpDown.Maximum = 16;
            equationsUpDown.Minimum = 1;
            equationsUpDown.Value = 1;

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!validateData())
                return;
            dataGridToArray();

            simplexMethodForm SMF = new simplexMethodForm(arr, funArr, basis, (int)variablesUpDown.Value, (int)equationsUpDown.Value);
            SMF.Show();

            
            //startBtn.Enabled = false;
            //nextStepBtn.Visible = true;
        }

        private void dataGridToArray()
        {
            arr = new Fraction[functionDataGrid.RowCount - 1, functionDataGrid.ColumnCount];
            funArr = new Fraction[functionDataGrid.ColumnCount];
            startArr = new Fraction[functionDataGrid.RowCount, functionDataGrid.ColumnCount];
            //Сохраняем стартовые значения задачи
            foreach (DataGridViewRow row in functionDataGrid.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.FormattedValue.ToString().IndexOf("/") == -1)
                        startArr[cell.RowIndex, cell.ColumnIndex] = new Fraction(Convert.ToInt64(cell.FormattedValue.ToString()), 1);
                    else
                        startArr[cell.RowIndex, cell.ColumnIndex] = new Fraction(Convert.ToInt64(cell.FormattedValue.ToString().Split('/')[0]), Convert.ToInt64(cell.FormattedValue.ToString().Split('/')[1]));
                }
            }
            //Заполняем массив функции и массив ограничений значениями
            foreach (DataGridViewRow row in functionDataGrid.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.RowIndex == 0)
                    {
                        if (comboBox1.SelectedIndex == 0)
                        {
                            if (cell.FormattedValue.ToString().IndexOf("/") == -1)
                                funArr[cell.ColumnIndex] = new Fraction(Convert.ToInt64(cell.FormattedValue.ToString()), 1);
                            else
                                funArr[cell.ColumnIndex] = new Fraction(Convert.ToInt64(cell.FormattedValue.ToString().Split('/')[0]), Convert.ToInt64(cell.FormattedValue.ToString().Split('/')[1]));
                        }
                        else
                        {
                            if (cell.FormattedValue.ToString().IndexOf("/") == -1)
                                funArr[cell.ColumnIndex] = new Fraction(-Convert.ToInt64(cell.FormattedValue.ToString()), 1);
                            else
                                funArr[cell.ColumnIndex] = new Fraction(-Convert.ToInt64(cell.FormattedValue.ToString().Split('/')[0]), Convert.ToInt64(cell.FormattedValue.ToString().Split('/')[1]));
                        }
                    }
                    else
                    {
                        if (cell.FormattedValue.ToString().IndexOf("/") == -1)
                            arr[cell.RowIndex - 1, cell.ColumnIndex] = new Fraction(Convert.ToInt64(cell.FormattedValue.ToString()), 1);
                        else
                            arr[cell.RowIndex - 1, cell.ColumnIndex] = new Fraction(Convert.ToInt64(cell.FormattedValue.ToString().Split('/')[0]), Convert.ToInt64(cell.FormattedValue.ToString().Split('/')[1]));
                    }
                }
            }
        }

        private void variablesUpDown_ValueChanged(object sender, EventArgs e)
        {
            functionDataGrid.ColumnCount = Convert.ToInt32(variablesUpDown.Value) + 1;
            foreach (DataGridViewColumn column in functionDataGrid.Columns)
            {
                column.HeaderText = "x" + (column.Index + 1);
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            functionDataGrid.Columns[functionDataGrid.ColumnCount - 1].HeaderText = "";
        }

        private void equationsUpDown_ValueChanged(object sender, EventArgs e)
        {
            functionDataGrid.RowCount = Convert.ToInt32(equationsUpDown.Value) + 1;
            functionDataGrid.Rows[1].HeaderCell.Value = "Ограничения";
        }

        private bool validateData()
        {
            bool f = true;
            foreach (DataGridViewRow row in functionDataGrid.Rows)
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.FormattedValue.ToString() == "")
                    {
                        cell.ErrorText = "Эта ячейка пуста";
                        f = false;
                    }
                    else if (cell.FormattedValue.ToString().Contains(".") || cell.FormattedValue.ToString().Contains(","))
                    {
                        cell.ErrorText = "Число должно быть представлено, как целое, либо в формате x/y";
                        f = false;
                    }
                    else if (cell.FormattedValue.ToString().IndexOf("/") != cell.FormattedValue.ToString().LastIndexOf("/"))
                    {
                        cell.ErrorText = "Дробь должна быть \"двухэтажная\"";
                        f = false;
                    }
                    else if (!Regex.Match(cell.FormattedValue.ToString(), "(^\\d+/\\d+$)|(^\\d+$)|(^-\\d+/\\d+$)|(^-\\d+$)").Success)
                    {
                        cell.ErrorText = "Число должно быть представлено, как целое, либо в формате x/y";
                        f = false;
                    }
                    else
                    {
                        cell.ErrorText = null;
                    }
                }
            if (!Regex.Match(textBox1.Text.ToString(), "(^(\\d,)+\\d$)|(^\\d$)").Success)
            {
                label4.Visible = true;
                f = false;
            }
            int col = 0;
            basis = Array.ConvertAll<string, int>(textBox1.Text.Split(','), int.Parse);
            for (int i = 0; i < basis.Length; i++)
            {
                if (basis[i] != 0)
                    col++;
            }
            if (col != equationsUpDown.Value)
            {
                MessageBox.Show("Количество базисных переменных должно быть равно количеству ограничений");
                f = false;
            }
            return f;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label4.Visible = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string file = "d:\\mygrid.bin";
            using (BinaryWriter bw = new BinaryWriter(File.Open(file, FileMode.Create)))
            {
                bw.Write(functionDataGrid.Columns.Count);
                bw.Write(functionDataGrid.Rows.Count);
                foreach (DataGridViewRow dgvR in functionDataGrid.Rows)
                {
                    for (int j = 0; j < functionDataGrid.Columns.Count; ++j)
                    {
                        object val = dgvR.Cells[j].Value;
                        if (val == null)
                        {
                            bw.Write(false);
                            bw.Write(false);
                        }
                        else
                        {
                            bw.Write(true);
                            bw.Write(val.ToString());
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            functionDataGrid.Rows.Clear();
            string file = "d:\\mygrid.bin";
            using (BinaryReader bw = new BinaryReader(File.Open(file, FileMode.Open)))
            {
                int n = bw.ReadInt32();
                int m = bw.ReadInt32();
                for (int i = 0; i < m; ++i)
                {
                    functionDataGrid.Rows.Add();
                    for (int j = 0; j < n; ++j)
                    {
                        if (bw.ReadBoolean())
                        {
                            functionDataGrid.Rows[i].Cells[j].Value = bw.ReadString();
                        }
                        else bw.ReadBoolean();
                    }
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                textBox1.Visible = true;
                label3.Visible = true;
            }
            else
            {
                textBox1.Visible = false;
                label3.Visible = false;
            }
        }
    }
}





/*for (int i = 0; i < arr.GetLength(0); i++)
{
    for (int j = 0; j < arr.GetLength(1); j++)
    {
        Debug.Write(arr[i, j] + " ");
    }
    Debug.Write("\n");
}*/