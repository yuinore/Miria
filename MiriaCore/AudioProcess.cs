using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiriaCore.AudioProcesses
{
    abstract class AudioProcess
    {
        // 派生クラスの名称は、すべて名詞形にすることにしました。
        // なぜか Differentiation は一般的な単語ではなくて、 Differential が名詞形です。

        // 静的メソッドは抽象化する意味が無いことに気付いてしまった・・・
        public abstract float[][] Do(float[][] buffer);

        public override string ToString()
        {
            Type type = this.GetType();

            string derivedClassName = type.Name;  // クラス名を取得。派生クラスの名前が取得できるらしい。

            StringBuilder ret = new StringBuilder(derivedClassName);  // returnする文字列

            foreach (var prop in type.GetProperties())
            {
                string propValue = prop.GetValue(this).ToString();
                if (propValue.IndexOfAny(new[] { '=', '"', ' ', ',' }) != -1)
                {
                    propValue = "\"" + propValue.Replace("\"", "\"\"") + "\"";
                }

                // ↓改行や'\0'は含まないと仮定
                ret.Append(", " + prop.Name + "=" + propValue);
            }

            return ret.ToString();
        }
    }
}
