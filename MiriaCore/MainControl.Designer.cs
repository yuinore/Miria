namespace MiriaCore
{
    partial class MainControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainControl));
            this.button_go = new System.Windows.Forms.Button();
            this.textBox_inputFile = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button_openOutput = new System.Windows.Forms.Button();
            this.button_openInput = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_outputFile = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.button_openIR = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button_tailcutplus = new System.Windows.Forms.Button();
            this.button_conv = new System.Windows.Forms.Button();
            this.textBox_ir = new System.Windows.Forms.TextBox();
            this.button_diff = new System.Windows.Forms.Button();
            this.button_integrate = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_go
            // 
            this.button_go.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_go.Location = new System.Drawing.Point(578, 247);
            this.button_go.Name = "button_go";
            this.button_go.Size = new System.Drawing.Size(75, 23);
            this.button_go.TabIndex = 0;
            this.button_go.Text = "Go!";
            this.button_go.UseVisualStyleBackColor = true;
            this.button_go.Click += new System.EventHandler(this.button_go_Click);
            // 
            // textBox_inputFile
            // 
            this.textBox_inputFile.AllowDrop = true;
            this.textBox_inputFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_inputFile.Location = new System.Drawing.Point(65, 23);
            this.textBox_inputFile.Name = "textBox_inputFile";
            this.textBox_inputFile.Size = new System.Drawing.Size(567, 19);
            this.textBox_inputFile.TabIndex = 1;
            this.textBox_inputFile.Text = "(Drop .wav Here!!)";
            this.textBox_inputFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_inputFile_DragDrop);
            this.textBox_inputFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox_inputFile_DragEnter);
            this.textBox_inputFile.Enter += new System.EventHandler(this.textBox_inputFile_Enter);
            this.textBox_inputFile.Leave += new System.EventHandler(this.textBox_inputFile_Leave);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer1.Size = new System.Drawing.Size(687, 489);
            this.splitContainer1.SplitterDistance = 221;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(683, 217);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button_openOutput);
            this.tabPage1.Controls.Add(this.button_openInput);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.textBox_outputFile);
            this.tabPage1.Controls.Add(this.textBox_inputFile);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(675, 191);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Single Input";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button_openOutput
            // 
            this.button_openOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_openOutput.Location = new System.Drawing.Point(638, 55);
            this.button_openOutput.Name = "button_openOutput";
            this.button_openOutput.Size = new System.Drawing.Size(21, 23);
            this.button_openOutput.TabIndex = 3;
            this.button_openOutput.Text = "...";
            this.button_openOutput.UseVisualStyleBackColor = true;
            this.button_openOutput.Click += new System.EventHandler(this.button_openOutput_Click);
            // 
            // button_openInput
            // 
            this.button_openInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_openInput.Location = new System.Drawing.Point(638, 21);
            this.button_openInput.Name = "button_openInput";
            this.button_openInput.Size = new System.Drawing.Size(21, 23);
            this.button_openInput.TabIndex = 3;
            this.button_openInput.Text = "...";
            this.button_openInput.UseVisualStyleBackColor = true;
            this.button_openInput.Click += new System.EventHandler(this.button_openInput_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Output";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Input";
            // 
            // textBox_outputFile
            // 
            this.textBox_outputFile.AllowDrop = true;
            this.textBox_outputFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_outputFile.Location = new System.Drawing.Point(65, 57);
            this.textBox_outputFile.Name = "textBox_outputFile";
            this.textBox_outputFile.Size = new System.Drawing.Size(567, 19);
            this.textBox_outputFile.TabIndex = 1;
            this.textBox_outputFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_inputFile_DragDrop);
            this.textBox_outputFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox_inputFile_DragEnter);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(675, 132);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Batch Input";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(675, 132);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "About";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 120);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(683, 256);
            this.tabControl2.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.button_openIR);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.button_tailcutplus);
            this.tabPage4.Controls.Add(this.button_conv);
            this.tabPage4.Controls.Add(this.textBox_ir);
            this.tabPage4.Controls.Add(this.button_diff);
            this.tabPage4.Controls.Add(this.button_integrate);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(675, 230);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Single Process";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // button_openIR
            // 
            this.button_openIR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_openIR.Location = new System.Drawing.Point(638, 81);
            this.button_openIR.Name = "button_openIR";
            this.button_openIR.Size = new System.Drawing.Size(21, 23);
            this.button_openIR.TabIndex = 3;
            this.button_openIR.Text = "...";
            this.button_openIR.UseVisualStyleBackColor = true;
            this.button_openIR.Click += new System.EventHandler(this.button_openIR_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(154, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "Impulse Response Input";
            // 
            // button_tailcutplus
            // 
            this.button_tailcutplus.Location = new System.Drawing.Point(22, 110);
            this.button_tailcutplus.Name = "button_tailcutplus";
            this.button_tailcutplus.Size = new System.Drawing.Size(126, 23);
            this.button_tailcutplus.TabIndex = 1;
            this.button_tailcutplus.Text = "TailCutPlus";
            this.button_tailcutplus.UseVisualStyleBackColor = true;
            this.button_tailcutplus.Click += new System.EventHandler(this.button_tailcutplus_Click);
            // 
            // button_conv
            // 
            this.button_conv.Location = new System.Drawing.Point(22, 81);
            this.button_conv.Name = "button_conv";
            this.button_conv.Size = new System.Drawing.Size(126, 23);
            this.button_conv.TabIndex = 1;
            this.button_conv.Text = "Convolve";
            this.button_conv.UseVisualStyleBackColor = true;
            this.button_conv.Click += new System.EventHandler(this.button_conv_Click);
            // 
            // textBox_ir
            // 
            this.textBox_ir.AllowDrop = true;
            this.textBox_ir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ir.Location = new System.Drawing.Point(281, 83);
            this.textBox_ir.Name = "textBox_ir";
            this.textBox_ir.Size = new System.Drawing.Size(351, 19);
            this.textBox_ir.TabIndex = 1;
            this.textBox_ir.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_inputFile_DragDrop);
            this.textBox_ir.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox_inputFile_DragEnter);
            // 
            // button_diff
            // 
            this.button_diff.Location = new System.Drawing.Point(22, 52);
            this.button_diff.Name = "button_diff";
            this.button_diff.Size = new System.Drawing.Size(126, 23);
            this.button_diff.TabIndex = 1;
            this.button_diff.Text = "Differentiate";
            this.button_diff.UseVisualStyleBackColor = true;
            this.button_diff.Click += new System.EventHandler(this.button_diff_Click);
            // 
            // button_integrate
            // 
            this.button_integrate.Location = new System.Drawing.Point(22, 22);
            this.button_integrate.Name = "button_integrate";
            this.button_integrate.Size = new System.Drawing.Size(126, 23);
            this.button_integrate.TabIndex = 0;
            this.button_integrate.Text = "Integrate";
            this.button_integrate.UseVisualStyleBackColor = true;
            this.button_integrate.Click += new System.EventHandler(this.button_integrate_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.button_go);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(675, 289);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Batch Process";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(687, 489);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_go;
        private System.Windows.Forms.TextBox textBox_inputFile;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_outputFile;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button button_conv;
        private System.Windows.Forms.Button button_diff;
        private System.Windows.Forms.Button button_integrate;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_ir;
        private System.Windows.Forms.Button button_tailcutplus;
        private System.Windows.Forms.Button button_openOutput;
        private System.Windows.Forms.Button button_openInput;
        private System.Windows.Forms.Button button_openIR;
    }
}
