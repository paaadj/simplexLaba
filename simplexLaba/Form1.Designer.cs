namespace simplexLaba
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.variablesUpDown = new System.Windows.Forms.NumericUpDown();
            this.equationsUpDown = new System.Windows.Forms.NumericUpDown();
            this.functionDataGrid = new System.Windows.Forms.DataGridView();
            this.startBtn = new System.Windows.Forms.Button();
            this.resultDataGrid = new System.Windows.Forms.DataGridView();
            this.nextStepBtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.variablesUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.equationsUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.functionDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Количество переменных:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Количество ограничений:";
            // 
            // variablesUpDown
            // 
            this.variablesUpDown.Location = new System.Drawing.Point(15, 46);
            this.variablesUpDown.Name = "variablesUpDown";
            this.variablesUpDown.Size = new System.Drawing.Size(35, 20);
            this.variablesUpDown.TabIndex = 3;
            this.variablesUpDown.ValueChanged += new System.EventHandler(this.variablesUpDown_ValueChanged);
            // 
            // equationsUpDown
            // 
            this.equationsUpDown.Location = new System.Drawing.Point(15, 85);
            this.equationsUpDown.Name = "equationsUpDown";
            this.equationsUpDown.Size = new System.Drawing.Size(35, 20);
            this.equationsUpDown.TabIndex = 4;
            this.equationsUpDown.ValueChanged += new System.EventHandler(this.equationsUpDown_ValueChanged);
            // 
            // functionDataGrid
            // 
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Format = "1/1";
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.functionDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.functionDataGrid.BackgroundColor = System.Drawing.SystemColors.Menu;
            this.functionDataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.functionDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.functionDataGrid.Location = new System.Drawing.Point(245, 30);
            this.functionDataGrid.Name = "functionDataGrid";
            this.functionDataGrid.Size = new System.Drawing.Size(843, 129);
            this.functionDataGrid.TabIndex = 5;
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(245, 403);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 6;
            this.startBtn.Text = "Начать";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // resultDataGrid
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.NullValue = null;
            this.resultDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.resultDataGrid.BackgroundColor = System.Drawing.SystemColors.Menu;
            this.resultDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultDataGrid.Location = new System.Drawing.Point(245, 165);
            this.resultDataGrid.Name = "resultDataGrid";
            this.resultDataGrid.Size = new System.Drawing.Size(843, 232);
            this.resultDataGrid.TabIndex = 7;
            // 
            // nextStepBtn
            // 
            this.nextStepBtn.Location = new System.Drawing.Point(326, 403);
            this.nextStepBtn.Name = "nextStepBtn";
            this.nextStepBtn.Size = new System.Drawing.Size(106, 23);
            this.nextStepBtn.TabIndex = 8;
            this.nextStepBtn.Text = "Следующий шаг";
            this.nextStepBtn.UseVisualStyleBackColor = true;
            this.nextStepBtn.Visible = false;
            this.nextStepBtn.Click += new System.EventHandler(this.nextStepBtn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 127);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 9;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Базис:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(15, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 26);
            this.label4.TabIndex = 11;
            this.label4.Text = "Базис должен быть записан \r\nчерез запятую";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(245, 443);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "label5";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(866, 492);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(948, 491);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "load";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 549);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.nextStepBtn);
            this.Controls.Add(this.resultDataGrid);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.functionDataGrid);
            this.Controls.Add(this.equationsUpDown);
            this.Controls.Add(this.variablesUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.variablesUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.equationsUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.functionDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown variablesUpDown;
        private System.Windows.Forms.NumericUpDown equationsUpDown;
        private System.Windows.Forms.DataGridView functionDataGrid;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.DataGridView resultDataGrid;
        private System.Windows.Forms.Button nextStepBtn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

