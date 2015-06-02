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

        private void DoSingleProcess(AudioProcess proc)
        {
            if (!AudioFileReader.FileExists(textBox_inputFile.Text))
            {
                MessageBox.Show("ファイルが無いよー？ : " + textBox_inputFile.Text);
                return;
            }
            if (File.Exists(textBox_outputFile.Text))
            {
                if (MessageBox.Show(
                    "ファイルを上書きしてもいい？ : " + textBox_outputFile.Text,
                    "Overwrite Confirm",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
            }

            var buf2 = AudioFileReader.ReadAllSamples(textBox_inputFile.Text);

            var buf3 = proc.Do(buf2);

            WaveFileWriter.WriteAllSamples(textBox_outputFile.Text, buf3, buf3.Length, 44100, 32);  // FIXME: サンプリングレートとビット深度
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
            if (!AudioFileReader.FileExists(textBox_ir.Text))
            {
                MessageBox.Show("ファイルが無いよー？ : " + textBox_ir.Text);
                return;
            }

            var buf2 = AudioFileReader.ReadAllSamples(textBox_ir.Text);

            DoSingleProcess(new Convolution(buf2));
        }
    }
}
