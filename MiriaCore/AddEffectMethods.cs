using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiriaCore
{
    class AddEffectMethods
    {
        public AudioProcess inst { get; private set; }

        public void EffectSelector_SelectedIndexChanged(
            FlowLayoutPanel myFlowLayoutPanel,
            Label explanationLabel,
            ComboBox myComboBox,
            object sender,
            EventArgs e)
        {
            myFlowLayoutPanel.Controls.Clear();

            explanationLabel.Text = "Explanation here. Explanation here. Explanation here. Explanation here. Explanation here.";

            Type procType = Type.GetType("MiriaCore.AudioProcesses." + myComboBox.SelectedItem.ToString());

            inst = (AudioProcess)Activator.CreateInstance(procType);
            
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
                var tbox = new TextBox() { Text = prop.GetValue(inst).ToString(), Name = prop.Name };
                tbox.TextChanged += tbox_TextChanged;
                p.Controls.Add(tbox);

                myFlowLayoutPanel.Controls.Add(p);
            }
        }
        
        void tbox_TextChanged(object sender, EventArgs e)
        {
            var targetProperty = inst.GetType().GetProperty(((TextBox)sender).Name);

            var targetType = targetProperty.PropertyType;  // DeclareingTypeではない！！！ （これは宣言されたクラスの型を返すっぽい）

            // targetProperty is double はダメ
            if (targetType == typeof(double))
            {
                double val;
                if (Double.TryParse(((TextBox)sender).Text, out val))
                {
                    targetProperty.SetValue(inst, val);
                    ((TextBox)sender).ForeColor = SystemColors.ControlText;
                }
                else
                {
                    ((TextBox)sender).ForeColor = Color.Red;
                }
            }
            else if (targetType == typeof(string))
            {
                string val = ((TextBox)sender).Text;

                targetProperty.SetValue(inst, val);
                ((TextBox)sender).ForeColor = SystemColors.ControlText;
            }
            else
            {
                Debug.Assert(false);
            }
        }
    }
}
