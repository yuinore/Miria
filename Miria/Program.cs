using System;
using System.Windows.Forms;

namespace Miria
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form = new Form1();
            MiriaCore.MainControl mainctrl = new MiriaCore.MainControl();
            mainctrl.Dock = DockStyle.Fill;
            form.Controls.Add(mainctrl);
            Application.Run(form);
        }
    }
}
