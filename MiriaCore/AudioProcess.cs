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
    }
}
