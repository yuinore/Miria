using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiriaCore
{
    public partial class AddEffectForm : Form
    {
        public AddEffectForm()
        {
            InitializeComponent();
        }

        AudioProcess inst;

        public string SelectedAudioProcess
        {
            get
            {
                return inst.ToString();
            }
        }

        private void AddEffectForm_Load(object sender, EventArgs e)
        {
            var asm = Assembly.GetExecutingAssembly();  // MiriaCore

            foreach (var type in asm.GetTypes())
            {
                if (type.IsSubclassOf(typeof(AudioProcess)) && !type.IsAbstract)
                {
                    Debug.Assert(type.Namespace == "MiriaCore.AudioProcesses", "名前空間が・・・違う・・・");

                    comboBox1.Items.Add(type.Name);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            label1.Text = "Explanation here. Explanation here. Explanation here. Explanation here. Explanation here.";

            Type procType = Type.GetType("MiriaCore.AudioProcesses." + comboBox1.SelectedItem.ToString());

            inst = (AudioProcess) Activator.CreateInstance(procType);

            foreach (var prop in procType.GetProperties())
            {
                // パブリックかどうかをチェックする必要はないね？

                var p = new FlowLayoutPanel();
                p.FlowDirection = FlowDirection.LeftToRight;
                p.BorderStyle = BorderStyle.None;
                p.AutoScroll = false;
                p.WrapContents = false;
                p.AutoSize = true;
                p.Controls.Add(new Label() { Text = prop.Name, Margin = new System.Windows.Forms.Padding(7) });
                p.Controls.Add(new TextBox() { Text = prop.GetValue(inst).ToString() });

                flowLayoutPanel1.Controls.Add(p);
            }
        }
    }
}
