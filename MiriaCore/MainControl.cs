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

            foreach (string filename in files)
            {
                if (File.Exists(filename))
                {
                    textBox_inputFile.Text = filename;
                    textBox_outputFile.Text = Path.Combine(
                        Path.GetDirectoryName(filename),
                        Path.GetFileNameWithoutExtension(filename)
                        ) + "_processed.wav";
                    break;
                }
            }
        }
    }
}
