using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using HatoLib;
using MiriaCore.AudioProcesses;

namespace MiriaCore
{
    public partial class MainControl : UserControl
    {
        public MainControl()
        {
            InitializeComponent();
        }

        private void button_go_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello World!");
        }

        private void textBox_inputFile_Enter(object sender, EventArgs e)
        {
            if (textBox_inputFile.Text == "(Drop .wav Here!!)")  // (Drop Here!!)というwavファイルをドラッグする人はいない・・・という想定
            {
                textBox_inputFile.Text = "";
            }
        }

        private void textBox_inputFile_Leave(object sender, EventArgs e)
        {
            if (textBox_inputFile.Text == "")  // (Drop Here!!)というwavファイルをドラッグする人はいない・・・という想定
            {
                textBox_inputFile.Text = "(Drop .wav Here!!)";
            }
        }

        private void textBox_inputFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void textBox_inputFile_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            TextBox target = sender as TextBox;
            TextBox target2 = null;

            if (target == null) return;
            if (target == textBox_inputFile || target == textBox_outputFile)
            {
                target = textBox_inputFile;
                target2 = textBox_outputFile;
            }

            foreach (string filename in files)
            {
                if (File.Exists(filename))
                {
                    target.Text = filename;
                    if (target2 != null)
                    {
                        target2.Text = Path.Combine(
                            Path.GetDirectoryName(filename),
                            Path.GetFileNameWithoutExtension(filename)
                            ) + "_processed.wav";
                    }
                    break;
                }
            }
        }

        private void button_integrate_Click(object sender, EventArgs e)
        {
            DoSingleProcess(new Integral());
        }

        private void button_diff_Click(object sender, EventArgs e)
        {
            DoSingleProcess(new Differential());
        }

        private void button_conv_Click(object sender, EventArgs e)
        {
            if (textBox_ir.Text == "")
            {
                MessageBox.Show("インパルス応答の入力ファイルの欄が空欄になってるよ！");
                textBox_ir.Text = "【ここだよ！！】";
                return;
            }
            if (!AudioFileReader.FileExists(textBox_ir.Text))
            {
                MessageBox.Show("インパルス応答の入力ファイルが無いよー？ : " + textBox_ir.Text);
                return;
            }

            var buf2 = AudioFileReader.ReadAllSamples(textBox_ir.Text);

            DoSingleProcess(new Convolution(buf2));
        }

        private void button_tailcutplus_Click(object sender, EventArgs e)
        {
            TailCutPlus proc = new TailCutPlus();
            // proc.FadeOutTime =
            // proc.Threshold =
            DoSingleProcess(proc);
        }

        private void button_openInput_Click(object sender, EventArgs e)
        {
            string filename = ShowOpenWaveFileDialog("Select Input Audio File");
            if (filename != null)
            {
                textBox_inputFile.Text = filename;
            }
        }

        private void button_openOutput_Click(object sender, EventArgs e)
        {
            string filename = ShowSaveWaveFileDialog("Save Wave File As");
            if (filename != null)
            {
                textBox_outputFile.Text = filename;
            }
        }

        private void button_openIR_Click(object sender, EventArgs e)
        {
            string filename = ShowOpenWaveFileDialog("Select Impulse Response");
            if (filename != null)
            {
                textBox_ir.Text = filename;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox_procs.Items.Add((new TailCutPlus()).ToString());
            listBox_procs.Items.Add((new Integral()).ToString());
            listBox_procs.Items.Add((new Differential()).ToString());
            listBox_procs.Items.Add((new GainDb()).ToString());
        }
    }
}
