namespace WSC_AI
{
    partial class Main_Form
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
            this.pictureBox_main = new System.Windows.Forms.PictureBox();
            this.button_basler = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_main)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_main
            // 
            this.pictureBox_main.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_main.Location = new System.Drawing.Point(13, 13);
            this.pictureBox_main.Name = "pictureBox_main";
            this.pictureBox_main.Size = new System.Drawing.Size(682, 639);
            this.pictureBox_main.TabIndex = 0;
            this.pictureBox_main.TabStop = false;
            // 
            // button_basler
            // 
            this.button_basler.Location = new System.Drawing.Point(714, 24);
            this.button_basler.Name = "button_basler";
            this.button_basler.Size = new System.Drawing.Size(207, 119);
            this.button_basler.TabIndex = 1;
            this.button_basler.Text = "Запустить";
            this.button_basler.UseVisualStyleBackColor = true;
            this.button_basler.Click += new System.EventHandler(this.button_basler_Click);
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1190, 664);
            this.Controls.Add(this.button_basler);
            this.Controls.Add(this.pictureBox_main);
            this.Name = "Main_Form";
            this.Text = "WSC_AI";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_main)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_main;
        private System.Windows.Forms.Button button_basler;
    }
}

