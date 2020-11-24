namespace CurrencyApp
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.CurrenciesListBox = new System.Windows.Forms.ListBox();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.ProgressLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Calendar = new System.Windows.Forms.MonthCalendar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.CurrencyValueTextBox = new System.Windows.Forms.TextBox();
            this.IdentificatorValueTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.IdentificatorComboBox = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.StatusStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CurrenciesListBox
            // 
            this.CurrenciesListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CurrenciesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CurrenciesListBox.FormattingEnabled = true;
            this.CurrenciesListBox.HorizontalScrollbar = true;
            this.CurrenciesListBox.ItemHeight = 25;
            this.CurrenciesListBox.Location = new System.Drawing.Point(4, 28);
            this.CurrenciesListBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CurrenciesListBox.Name = "CurrenciesListBox";
            this.CurrenciesListBox.Size = new System.Drawing.Size(159, 369);
            this.CurrenciesListBox.TabIndex = 0;
            this.CurrenciesListBox.SelectedIndexChanged += new System.EventHandler(this.CurrenciesListBox_SelectedIndexChanged);
            // 
            // StatusStrip
            // 
            this.StatusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProgressBar,
            this.ProgressLabel});
            this.StatusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.StatusStrip.Location = new System.Drawing.Point(0, 402);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.StatusStrip.Size = new System.Drawing.Size(754, 34);
            this.StatusStrip.TabIndex = 4;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // ProgressBar
            // 
            this.ProgressBar.AutoSize = false;
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(300, 28);
            this.ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(286, 29);
            this.ProgressLabel.Text = "Обновление курсов валют: 100%";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CurrenciesListBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(167, 402);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Валюты";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Calendar);
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(581, 402);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Курс";
            // 
            // Calendar
            // 
            this.Calendar.CalendarDimensions = new System.Drawing.Size(2, 1);
            this.Calendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Calendar.Location = new System.Drawing.Point(4, 122);
            this.Calendar.Margin = new System.Windows.Forms.Padding(14);
            this.Calendar.MaxSelectionCount = 1;
            this.Calendar.Name = "Calendar";
            this.Calendar.TabIndex = 30;
            this.Calendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.Calendar_DateSelected);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.CurrencyValueTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.IdentificatorValueTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.IdentificatorComboBox, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 28);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(573, 94);
            this.tableLayoutPanel1.TabIndex = 29;
            // 
            // CurrencyValueTextBox
            // 
            this.CurrencyValueTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.CurrencyValueTextBox.Location = new System.Drawing.Point(306, 5);
            this.CurrencyValueTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CurrencyValueTextBox.Multiline = true;
            this.CurrencyValueTextBox.Name = "CurrencyValueTextBox";
            this.CurrencyValueTextBox.Size = new System.Drawing.Size(175, 37);
            this.CurrencyValueTextBox.TabIndex = 23;
            // 
            // IdentificatorValueTextBox
            // 
            this.IdentificatorValueTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.IdentificatorValueTextBox.Location = new System.Drawing.Point(306, 52);
            this.IdentificatorValueTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.IdentificatorValueTextBox.Multiline = true;
            this.IdentificatorValueTextBox.Name = "IdentificatorValueTextBox";
            this.IdentificatorValueTextBox.Size = new System.Drawing.Size(175, 37);
            this.IdentificatorValueTextBox.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(4, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(294, 47);
            this.label3.TabIndex = 24;
            this.label3.Text = "Курс валюты";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // IdentificatorComboBox
            // 
            this.IdentificatorComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IdentificatorComboBox.FormattingEnabled = true;
            this.IdentificatorComboBox.Items.AddRange(new object[] {
            "Названию",
            "Название на английском",
            "Числовой код",
            "Символьный код",
            "Id"});
            this.IdentificatorComboBox.Location = new System.Drawing.Point(4, 52);
            this.IdentificatorComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.IdentificatorComboBox.Name = "IdentificatorComboBox";
            this.IdentificatorComboBox.Size = new System.Drawing.Size(294, 33);
            this.IdentificatorComboBox.TabIndex = 28;
            this.IdentificatorComboBox.Text = "Определяющий параметр";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(754, 402);
            this.splitContainer1.SplitterDistance = 167;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(754, 436);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.StatusStrip);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Курсы валют";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox CurrenciesListBox;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel ProgressLabel;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox CurrencyValueTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox IdentificatorValueTextBox;
        private System.Windows.Forms.MonthCalendar Calendar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox IdentificatorComboBox;
    }
}

