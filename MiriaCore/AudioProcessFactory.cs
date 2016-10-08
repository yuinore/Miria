using CsvHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiriaCore
{
    static class AudioProcessFactory
    {
        public static AudioProcess FromString(string query)
        {
            try
            {
                // メモ： Unicodeはダメ、shift_jisならOK.
                var parser = new CsvParser(new StreamReader(new MemoryStream(System.Text.Encoding.GetEncoding("shift_jis").GetBytes(query))));  // !?!?!?!?

                while (true)
                {
                    string[] row = parser.Read();

                    if (row == null)
                    {
                        break;
                    }

                    if (row.Length <= 0) throw new Exception();

                    // まず、AudioProcessの派生クラスのインスタンスを作成する。
                    var procType = Type.GetType("MiriaCore.AudioProcesses." + row[0]);
                    var proc = (AudioProcess)Activator.CreateInstance(procType);

                    // 次に、パラメータを設定する
                    for (int k = 1; k < row.Length; k++)
                    {
                        int idx = row[k].IndexOf("=");
                        if (idx < 0) throw new Exception();
                        
                        string propertyName = row[k].Substring(0, idx).Trim();
                        var targetProperty = procType.GetProperty(propertyName);  // 先頭に空白文字が入ってたしそれに気付かなかったしきれそう
                        var targetPropertyType = targetProperty.PropertyType;

                            string propertyValueString = row[k].Substring(idx + 1).Trim();

                        if (propertyValueString.StartsWith("\"")&& propertyValueString.EndsWith("\""))
                        {
                            propertyValueString = propertyValueString.Substring(1, propertyValueString.Length - 2).Replace("\"\"", "\"");
                        }

                        if(targetPropertyType == typeof(double))
                        {
                            double propertyValueDouble = Convert.ToDouble(propertyValueString);

                            targetProperty.SetValue(proc, propertyValueDouble);
                        }
                        else if(targetPropertyType == typeof(string))
                        {
                            targetProperty.SetValue(proc, propertyValueString);
                        }
                        else
                        {
                            Debug.Assert(false);
                        }
                    }

                    return proc;
                }

                throw new Exception();
            }
            //catch { }  // 何らかの処理
            finally
            {
            }
        }
    }
}
