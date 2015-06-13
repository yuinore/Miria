using HatoLib;
using MiriaCore.AudioProcesses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiriaCore
{
    partial class MainControl
    {
        private bool GetSingleInputFile(out string inputfile, out string outputfile)
        {
            inputfile = outputfile = "";

            try
            {
                if (textBox_inputFile.Text == "")
                {
                    MessageBox.Show("入力ファイルの欄が空欄になってるよ！");
                    textBox_inputFile.Text = "【ここだよ！！】";
                    return false;
                }
                if (textBox_outputFile.Text == "")
                {
                    MessageBox.Show("出力ファイルの欄が空欄になってるよ！");
                    textBox_outputFile.Text = "【ここだよ！！】";
                    return false;
                }

                if (!AudioFileReader.FileExists(textBox_inputFile.Text))
                {
                    MessageBox.Show("ファイルが無いよー？ : " + textBox_inputFile.Text);
                    return false;
                }
                if (File.Exists(textBox_outputFile.Text))
                {
                    if (MessageBox.Show(
                        "ファイルを上書きしちゃってもいい？ : " + textBox_outputFile.Text,
                        "Overwrite Confirm",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return false;
                    }
                }

                inputfile = textBox_inputFile.Text;
                outputfile = textBox_outputFile.Text;

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("エラーが起きちゃった・・・ごめんね・・・。\r\n\r\nstack trace: \r\n" + e.ToString(), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        private void DoSingleProcess(AudioProcess proc)
        {
            try
            {
                string inputfile, outputfile;

                if (GetSingleInputFile(out inputfile, out outputfile))
                {
                    var buf2 = AudioFileReader.ReadAllSamples(inputfile);

                    var buf3 = proc.Do(buf2);

                    WaveFileWriter.WriteAllSamples(outputfile, buf3, buf3.Length, 44100, 32);  // FIXME: サンプリングレートとビット深度
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("エラーが起きちゃった・・・ごめんね・・・。\r\n\r\nstack trace: \r\n" + e.ToString(), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DoBatchProcess()
        {
            try
            {
                string inputfile, outputfile;

                if (GetSingleInputFile(out inputfile, out outputfile))
                {
                    var buf2 = AudioFileReader.ReadAllSamples(inputfile);

                    for (int k = 0; k < listBox_procs.Items.Count; k++)
                    {
                        string query = (string)listBox_procs.Items[k];
                        var proc = AudioProcessFactory.FromString(query);
                        buf2 = proc.Do(buf2);
                    }

                    WaveFileWriter.WriteAllSamples(outputfile, buf2, buf2.Length, 44100, 32);  // FIXME: サンプリングレートとビット深度
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("エラーが起きちゃった・・・ごめんね・・・。\r\n\r\nstack trace: \r\n" + e.ToString(), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // [CanBeNull]
        private string ShowOpenWaveFileDialog(string caption)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = caption;
            ofd.FileName = "";
            ofd.Filter = "Wave File, Ogg File (*.wav;*.ogg)|*.wav;*.ogg";
            ofd.FilterIndex = 0;
            //ofd.InitialDirectory = HatoPath.FromAppDir("");  // TODO: カレントディレクトリを記憶するようにする。

            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return null;
            }
            else
            {
                return ofd.FileName;
            }
        }

        // [CanBeNull]
        private string ShowSaveWaveFileDialog(string caption)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = caption;
            sfd.FileName = "";
            sfd.Filter = "Wave File (*.wav)|*.wav";
            sfd.FilterIndex = 1;
            // ＞＞このインデックスは、0 からではなく 1 から始まります
            // な、なんだってー！？
            //sfd.InitialDirectory = HatoPath.FromAppDir("");  // TODO: カレントディレクトリを記憶するようにする。

            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;

            //ダイアログを表示する
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return null;
            }
            return sfd.FileName;
        }
    }
}
