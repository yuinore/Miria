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
        private void DoSingleProcess(AudioProcess proc)
        {
            try
            {
                if (textBox_inputFile.Text == "")
                {
                    MessageBox.Show("入力ファイルの欄が空欄になってるよ！");
                    textBox_inputFile.Text = "【ここだよ！！】";
                    return;
                }
                if (textBox_outputFile.Text == "")
                {
                    MessageBox.Show("出力ファイルの欄が空欄になってるよ！");
                    textBox_outputFile.Text = "【ここだよ！！】";
                    return;
                }

                if (!AudioFileReader.FileExists(textBox_inputFile.Text))
                {
                    MessageBox.Show("ファイルが無いよー？ : " + textBox_inputFile.Text);
                    return;
                }
                if (File.Exists(textBox_outputFile.Text))
                {
                    if (MessageBox.Show(
                        "ファイルを上書きしちゃってもいい？ : " + textBox_outputFile.Text,
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
