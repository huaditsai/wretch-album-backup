namespace WretchAlbumBackupDownload
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtDetailHtmlPath = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(14, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(330, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "album 資料夾中的 detail.html 路徑：";
            // 
            // txtDetailHtmlPath
            // 
            this.txtDetailHtmlPath.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtDetailHtmlPath.Location = new System.Drawing.Point(19, 57);
            this.txtDetailHtmlPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDetailHtmlPath.Name = "txtDetailHtmlPath";
            this.txtDetailHtmlPath.Size = new System.Drawing.Size(443, 30);
            this.txtDetailHtmlPath.TabIndex = 1;
            // 
            // btnGo
            // 
            this.btnGo.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnGo.Location = new System.Drawing.Point(19, 95);
            this.btnGo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(100, 34);
            this.btnGo.TabIndex = 2;
            this.btnGo.Text = "下載";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // txtLog
            // 
            this.txtLog.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtLog.Location = new System.Drawing.Point(19, 137);
            this.txtLog.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(536, 404);
            this.txtLog.TabIndex = 3;
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnStop.Location = new System.Drawing.Point(125, 95);
            this.btnStop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(103, 34);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSelectFile.Location = new System.Drawing.Point(468, 57);
            this.btnSelectFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(87, 30);
            this.btnSelectFile.TabIndex = 5;
            this.btnSelectFile.Text = "瀏覽";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(18, 548);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(536, 40);
            this.progressBar1.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 603);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.txtDetailHtmlPath);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "無名相簿影音備份下載";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDetailHtmlPath;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

