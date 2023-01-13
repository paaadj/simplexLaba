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
            functionDataGrid.RowHeadersVisible = true;

            resultDataGrid.RowHeadersVisible = false;
            resultDataGrid.AllowUserToResizeRows = false;
            resultDataGrid.AllowUserToResizeColumns = false;
            resultDataGrid.ReadOnly = true;
            resultDataGrid.EnableHeadersVisualStyles = false;
            resultDataGrid.RowHeadersWidth = 120;
            resultDataGrid.RowHeadersVisible = true;

            functionDataGrid.ColumnCount = 3;
            functionDataGrid.RowCount = 3;

            variablesUpDown.Value = 3;
            variablesUpDown.Maximum = 16;
            variablesUpDown.Minimum = 1;
            equationsUpDown.Maximum = 16;
            equationsUpDown.Minimum = 1;
            equationsUpDown.Value = 1;
        }

        private void swap(int basisInd, int swapInd)
        {
            if (basisInd == swapInd)
                return;
            foreach (DataGridViewRow row in functionDataGrid.Rows)
            {
                if (row.Index == 0)
                    continue;
                string tmp = row.Cells[swapInd].Value.ToString();

                row.Cells[swapInd].Value = row.Cells[basisInd].Value;
                row.Cells[basisInd].Value = tmp;
            }
        }
        private void swapToNormal(DataGridView dataGrid)
        {
            int index = 0;
            for (int i = 0; i < basis.Length; i++)
            {
                if (basis[i] != 0)
                {
                    index++;
                }
            }
            index--;
            for (int i = basis.Length - 1; i > 0; i--)
            {
                if (basis[i] != 0)
                {
                    if (i == index)
                        continue;
                    foreach (DataGridViewRow row in dataGrid.Rows)
                    {
                        if (row.Index == 0)
                            continue;
                        string tmp = row.Cells[i].Value.ToString();

                        row.Cells[i].Value = row.Cells[index].Value;
                        row.Cells[index].Value = tmp;
                    }
                    index--;
                }
            }
        }

        private void dataGridToArray(DataGridView datagrid)
        {
            arr = new Fraction[datagrid.RowCount - 1, datagrid.ColumnCount];
            funArr = new Fraction[datagrid.ColumnCount];
            basis = Array.ConvertAll<string, int>(textBox1.Text.Split(','), int.Parse);
            startArr = new Fraction[datagrid.RowCount, datagrid.ColumnCount];
            //Сохраняем стартовые значения задачи
            foreach (DataGridViewRow row in datagrid.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.FormattedValue.ToString().IndexOf("/") == -1)
                        startArr[cell.RowIndex, cell.ColumnIndex] = new Fraction(Convert.ToInt64(cell.FormattedValue.ToString()), 1);
                    else
                        startArr[cell.RowIndex, cell.ColumnIndex] = new Fraction(Convert.ToInt64(cell.FormattedValue.ToString().Split('/')[0]), Convert.ToInt64(cell.FormattedValue.ToString().Split('/')[1]));
                }
            }
            int index = 0;
            for (int i = 0; i < basis.Length; i++)
            {
                if (basis[i] != 0)
                {
                    swap(i, index);
                    index++;
                }
            }
            //Заполняем массив функции и массив ограничений значениями
            foreach (DataGridViewRow row in datagrid.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.RowIndex == 0)
                    {
                        if (cell.FormattedValue.ToString().IndexOf("/") == -1)
                            funArr[cell.ColumnIndex] = new Fraction(Convert.ToInt64(cell.FormattedValue.ToString()), 1);
                        else
                            funArr[cell.ColumnIndex] = new Fraction(Convert.ToInt64(cell.FormattedValue.ToString().Split('/')[0]), Convert.ToInt64(cell.FormattedValue.ToString().Split('/')[1]));
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
            //Возвращаем порядок functionDataGrid в начальное состояние
            swapToNormal(functionDataGrid);
        }

        //Функция заполнения resulDataGrid значениями массивов
        private void arrayToDataGrid()
        {
            if (resultDataGrid.ColumnCount != functionDataGrid.ColumnCount)
            {
                resultDataGrid.ColumnCount = functionDataGrid.ColumnCount;
                resultDataGrid.RowCount = functionDataGrid.RowCount;
                foreach (DataGridViewColumn column in resultDataGrid.Columns)
                {
                    column.HeaderText = "x" + (column.Index + 1);
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                resultDataGrid.Columns[resultDataGrid.ColumnCount - 1].HeaderText = "";
            }
            foreach (DataGridViewRow row in resultDataGrid.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (row.Index == 0)
                    {
                        cell.Value = funArr[cell.ColumnIndex].ToString();
                        continue;
                    }
                    cell.Value = arr[cell.RowIndex - 1, cell.ColumnIndex].ToString();
                }
            }
        }

        //Функция прибавления строки с ненулевым элементом в столбце index к строке index
        private bool addStr(int index)
        {
            int foundIndex = -1;
            if (index == arr.GetLength(0))
                return false;
            for (int i = index + 1; i < arr.GetLength(0); i++)
            {
                if (arr[i, index].numerator != 0)
                {
                    foundIndex = i;
                    break;
                }
            }
            if (foundIndex == -1)
                return false;
            for (int i = 0; i < arr.GetLength(1); i++)
                arr[index, i] += arr[foundIndex, i];
            return true;
        }

        private void Haussian()
        {
            int strs = arr.GetLength(0);
            int cols = arr.GetLength(1);
            for (int i = 0; i < strs - 1; i++)
            {
                if (arr[i, i].numerator == 0)
                    if (!addStr(i))
                        continue;
                for (int j = i + 1; j < strs; j++)
                {
                    Fraction koef = arr[j, i] / arr[i, i];
                    for (int k = 0; k < cols; k++)
                        arr[j, k] -= arr[i, k] * koef;
                }
            }

            for (int i = strs - 1; i > 0; i--)
            {
                if (arr[i, i].numerator == 0) continue;
                for (int j = i - 1; j >= 0; j--)
                {
                    Fraction koef = arr[j, i] / arr[i, i];
                    for (int k = i; k < cols; k++)
                    {
                        arr[j, k] -= arr[i, k] * koef;
                    }
                }
            }
            for (int i = 0; i < strs; i++)
            {
                if (arr[i, i].numerator == 0) continue;
                Fraction koef = arr[i, i];
                for (int j = i; j < cols; j++)
                    arr[i, j] /= koef;
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
            return f;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!validateData())
                return;
            dataGridToArray(functionDataGrid);
            Haussian();
            arrayToDataGrid();
            swapToNormal(resultDataGrid);
            dataGridToArray(resultDataGrid);
            solveGoalFunction();
            arrayToDataGrid();
            step();
            startBtn.Enabled = false;
            nextStepBtn.Visible = true;
        }

        private bool checkForSolution()
        {
            foreach (Fraction item in funArr)
                if (item.Value() < 0 && item != funArr[funArr.Length - 1])
                {
                    Debug.Write(item + " ");
                    return true;
                }
            return false;
        }
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void solveGoalFunction()
        {
            //Сохраняем коэффициенты переменных в функции
            var koeffs = new List<Fraction>();
            for (int i = 0; i < funArr.Length; i++)
            {
                if (i == funArr.Length - 1 || basis[i] != 0)
                    koeffs.Add(funArr[i]);
            }
            //Подставляем в уравнение
            for (int i = 0; i < arr.GetLength(1); i++)//столбец
            {
                if (i != (arr.GetLength(1) - 1) && basis[i] != 0)
                    continue;
                for (int j = 0; j < arr.GetLength(0); j++)//строка
                {
                    funArr[i] = funArr[i] + koeffs[j] * (-arr[j, i]);
                }
            }
            int index = 1;
            for (int i = 0; i < basis.Length; i++)
            {
                if (basis[i] != 0 && i <= (variablesUpDown.Value - 1))
                {
                    resultDataGrid.Rows[index].HeaderCell.Value = "x" + (i + 1);
                    resultDataGrid.Columns[i].Visible = false;
                    index++;
                }
            }
        }

        private void solveNewCoeffs(int col, int row)
        {
            arr[row, col] = arr[row, col].turnOver();
            //Пересчитываем значения в строке опорного элемента
            for (int i = 0; i < arr.GetLength(1); i++)//столцы
            {
                if (i == col)
                    continue;
                arr[row, i] = arr[row, i] * arr[row, col];
            }
            for(int i = 0; i < funArr.GetLength(0); i++)
            {
                if (i == col)
                    continue;
                //Debug.WriteLine(funArr[i] + " = " + funArr[i] + " - " + (funArr[col] + " * " + arr[row, i]));
                funArr[i] = funArr[i] - (funArr[col] * arr[row, i]);
            }
            for (int i = 0; i < arr.GetLength(0); i++)//строки
            {
                if (i == row)
                    continue;
                //Пересчитываем значения вне строк и столбца опорного элемента
                for (int j = 0; j < arr.GetLength(1); j++)//столбцы
                {
                    if (j == col)
                        continue;
                    arr[i, j] = arr[i, j] - (arr[i, col] * arr[row, j]);
                }
                //Пересчитываем значения в столбце опорного элемента
                arr[i, col] = -(arr[i, col] * arr[row, col]);

            }
            funArr[col] = -(funArr[col] * arr[row, col]);
            string tmp = resultDataGrid.Columns[col].HeaderText;
            resultDataGrid.Columns[col].HeaderText = resultDataGrid.Rows[row + 1].HeaderCell.Value.ToString();
            resultDataGrid.Rows[row + 1].HeaderCell.Value = tmp;
            resultDataGrid.Refresh();
        }

        private void clearElems()
        {
            foreach (DataGridViewRow row in resultDataGrid.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                    cell.Style.BackColor = Color.White;
            }
        }

        //Подсвечивание возможных опроных элементов
        private void step()
        {
            int lastCol = arr.GetLength(1) - 1;
            int lastRow = arr.GetLength(0) - 1;

            for (int i = 0; i < lastCol; i++)
            {
                double min = double.MaxValue;
                int minRow = -1;
                if (funArr[i].Value() >= 0)
                    continue;
                for (int j = 0; j <= lastRow; j++)
                {
                    //Debug.WriteLine(arr[j, i] + " / " + arr[j, lastCol] + " = " + (arr[j, i] / arr[j, lastCol]).Value());
                    if ((arr[j, i]).Value() > 0 && (arr[j, lastCol] / arr[j, i]).Value() < min)
                    {
                        min = (arr[j, lastCol] / arr[j, i]).Value();
                        minRow = j;
                    }
                }
                if (minRow != -1)
                    resultDataGrid.Rows[minRow + 1].Cells[i].Style.BackColor = Color.Cyan;
            }
        }

        private void nextStepBtn_Click(object sender, EventArgs e)
        {
            int col = resultDataGrid.CurrentCell.ColumnIndex;
            int row = resultDataGrid.CurrentCell.RowIndex - 1;
            if (resultDataGrid.Rows[row + 1].Cells[col].Style.BackColor != Color.Cyan)
            {
                MessageBox.Show("Выберите элемент, подсвеченный голубым цветом");
                return;
            }
            solveNewCoeffs(col, row);
            arrayToDataGrid();
            clearElems();
            if (!checkForSolution())
            {
                getSolution();
                return;
            }
            step();
        }

        private void getSolution()
        {
            label5.Text = "x = (";
            Fraction[] solution = new Fraction[(int)variablesUpDown.Value];
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                //Debug.WriteLine(i + " " + resultDataGrid.Rows[i + 1].HeaderCell.Value.ToString().Split('x')[1]);
                solution[int.Parse((resultDataGrid.Rows[i + 1].HeaderCell.Value.ToString().Split('x')[1])) - 1] = arr[i, arr.GetLength(1) - 1];
            }
            for (int i = 0; i < solution.Length; i++)
            {
                if (solution[i] == null)
                    solution[i] = new Fraction(0, 1);
                if (i != solution.Length - 1)
                    label5.Text += solution[i] + ", ";
                else
                    label5.Text += solution[i];
            }
            label5.Text += ")\n";
            label5.Text += "f(x) = " + (-funArr[funArr.GetLength(0) - 1]);
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