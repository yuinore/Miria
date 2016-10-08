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

        AddEffectMethods addEffectMethods = new AddEffectMethods();

        public string SelectedAudioProcess
        {
            get
            {
                if (addEffectMethods.inst == null) return null;

                return addEffectMethods.inst.ToString();
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
            addEffectMethods.EffectSelector_SelectedIndexChanged(
                flowLayoutPanel1, label1, comboBox1, sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
