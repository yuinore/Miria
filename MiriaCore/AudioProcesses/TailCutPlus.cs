using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiriaCore.AudioProcesses
{
    class TailCutPlus : AudioProcess
    {
        /// <summary>
        /// 無音と判定する振幅です。(単位：dB)
        /// </summary>
        public double Threshold = -36;  // dB

        /// <summary>
        /// フェードアウトさせる最長の時間です。（単位：秒）
        /// </summary>
        public double FadeOutTime = 0.002;  // second
        // ゲッター/セッターを書くと自動プロパティが使えないのがつらい（めんどい）

        /// <summary>
        /// もし、入力がThresholdを下回る小さな音量だった場合は、
        /// 空の.wavを書き出す代わりに、入力をそのまま書き出します。
        /// なお、.wavのタイムスタンプは更新されます。
        /// </summary>
        public bool WriteOriginalIfNoSound = false;

        public override float[][] Do(float[][] buffer)
        {
            double threshold2 = Math.Pow(10.0, Threshold / 20.0);
            int fadeoutsamples = (int)Math.Round(FadeOutTime * 44100);  // FIXME: サンプリングレート
            int i;

            for (i = buffer[0].Length - 1; i >= 0; i--)
            {
                for (int ch = 0; ch < buffer.Length; ch++)
                {
                    if (Math.Abs(buffer[ch][i]) >= threshold2)
                    {
                        goto loopout;
                    }
                }
            }

        loopout:
            if (i < 0)
            {
                // 音が存在しなかった
                if (WriteOriginalIfNoSound)
                {
                    return buffer;  // As it is
                }
                else
                {
                    float[][] buf2 = new float[buffer.Length][];

                    for (int ch = 0; ch < buffer.Length; ch++)
                    {
                        buf2[ch] = new float[1] { 0 };  // なんとなく出力が0サンプルというのはバグと誤解されそうだなと思いました。
                    }

                    return buf2;
                }
            }
            else
            {
                // i 番目のサンプルは大きな音なので、その次のサンプルからフェードアウトを開始する。
                //
                // |￣|＿|￣|＿|￣|＿|￣|＿～～～～～～～～～----------------------　↑振幅
                // 　　　　　　　　　　　　　　　　　↑　　　　　↑
                // 　　　　　　　　　　　　　　fadeoutFrom　　fadeoutTo　　　　　　　→時間（単位：サンプル）
                //
                // 注1) 最後のサンプルの時点でthreshold以上だった場合は、
                //      i + 1 == fadeoutFrom == fadeoutTo == buffer[0].Length となり、何も行いません。
                // 注2) 最後のサンプルからFadeOutTime以下の位置でthreshold以上だった場合でも、
                //      フェードアウトのスロープ（傾き）は急激には**なりません**。
                // 注3) すべてのサンプルがthresholdを下回っていた場合は、
                //      i == -1 となり、上のコードで例外処理が行われます。

                // フェードアウトの開始位置
                int fadeoutFrom = i + 1;  // 単位：サンプル

                // フェードアウトの仮想的な終了位置
                int fadeoutTo = fadeoutFrom + (int)Math.Round(FadeOutTime * 44100);
                if (fadeoutTo < fadeoutFrom) fadeoutTo = fadeoutFrom;

                // フェードアウトが打ち切られて音源が終了する位置。出力される.wavの長さ。
                int fadeoutEnd = Math.Min(fadeoutTo, buffer[0].Length);

                float[][] buf2 = new float[buffer.Length][];

                for (int ch = 0; ch < buffer.Length; ch++)
                {
                    buf2[ch] = new float[fadeoutEnd];

                    for (int i2 = 0; i2 < fadeoutEnd; i2++)
                    {
                        if (i2 < fadeoutFrom)
                        {
                            buf2[ch][i2] = buffer[ch][i2];
                        }
                        else
                        {
                            buf2[ch][i2] = buffer[ch][i2] * (fadeoutTo - i2) / (float)(fadeoutTo - fadeoutFrom);  // linear
                        }
                    }
                }

                return buf2;
            }
        }
    }
}
