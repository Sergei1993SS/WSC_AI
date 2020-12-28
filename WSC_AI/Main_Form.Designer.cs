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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Form));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_api_cam = new System.Windows.Forms.Label();
            this.label_sn_cam = new System.Windows.Forms.Label();
            this.label_cam_model = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox_cam = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label_opc = new System.Windows.Forms.Label();
            this.pictureBox_opc = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_cam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_opc)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(27, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Состояние камеры:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pictureBox_opc);
            this.groupBox1.Controls.Add(this.label_opc);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.pictureBox_cam);
            this.groupBox1.Controls.Add(this.label_api_cam);
            this.groupBox1.Controls.Add(this.label_sn_cam);
            this.groupBox1.Controls.Add(this.label_cam_model);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(789, 435);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Состояние загрузки системы";
            // 
            // label_api_cam
            // 
            this.label_api_cam.AutoSize = true;
            this.label_api_cam.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_api_cam.Location = new System.Drawing.Point(89, 113);
            this.label_api_cam.Name = "label_api_cam";
            this.label_api_cam.Size = new System.Drawing.Size(0, 16);
            this.label_api_cam.TabIndex = 8;
            // 
            // label_sn_cam
            // 
            this.label_sn_cam.AutoSize = true;
            this.label_sn_cam.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_sn_cam.Location = new System.Drawing.Point(178, 86);
            this.label_sn_cam.Name = "label_sn_cam";
            this.label_sn_cam.Size = new System.Drawing.Size(0, 16);
            this.label_sn_cam.TabIndex = 7;
            // 
            // label_cam_model
            // 
            this.label_cam_model.AutoSize = true;
            this.label_cam_model.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_cam_model.Location = new System.Drawing.Point(119, 60);
            this.label_cam_model.Name = "label_cam_model";
            this.label_cam_model.Size = new System.Drawing.Size(0, 16);
            this.label_cam_model.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(51, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "API:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(51, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Серийный номер:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(51, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Модель:";
            // 
            // pictureBox_cam
            // 
            this.pictureBox_cam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_cam.InitialImage = null;
            this.pictureBox_cam.Location = new System.Drawing.Point(181, 31);
            this.pictureBox_cam.Name = "pictureBox_cam";
            this.pictureBox_cam.Size = new System.Drawing.Size(19, 16);
            this.pictureBox_cam.TabIndex = 9;
            this.pictureBox_cam.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(27, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Состояние  OPC:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(51, 192);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Адрес:";
            // 
            // label_opc
            // 
            this.label_opc.AutoSize = true;
            this.label_opc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_opc.Location = new System.Drawing.Point(108, 192);
            this.label_opc.Name = "label_opc";
            this.label_opc.Size = new System.Drawing.Size(0, 16);
            this.label_opc.TabIndex = 12;
            // 
            // pictureBox_opc
            // 
            this.pictureBox_opc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_opc.InitialImage = null;
            this.pictureBox_opc.Location = new System.Drawing.Point(159, 161);
            this.pictureBox_opc.Name = "pictureBox_opc";
            this.pictureBox_opc.Size = new System.Drawing.Size(19, 16);
            this.pictureBox_opc.TabIndex = 13;
            this.pictureBox_opc.TabStop = false;
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 506);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main_Form";
            this.Text = "WSC_AI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_Form_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_cam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_opc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_api_cam;
        private System.Windows.Forms.Label label_sn_cam;
        private System.Windows.Forms.Label label_cam_model;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.PictureBox pictureBox_cam;
        private System.Windows.Forms.Label label_opc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.PictureBox pictureBox_opc;
    }
}

