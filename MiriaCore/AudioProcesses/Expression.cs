using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.ClearScript;
//using Microsoft.ClearScript.JavaScript;
using Microsoft.ClearScript.V8;
using Microsoft.ClearScript;

namespace MiriaCore.AudioProcesses
{
    class Expression : AudioProcess
    {
        public string ExpressionCode { get; set; }

        public Expression()
        {
            ExpressionCode = "x => Math.max(Math.log(x * x) * 0.1 / Math.log(10) + 1.0, 0.0)";  // 0dB～-100dBを1.0～0.0にマップ
            //ExpressionCode = "x => (x >= Math.pow(10, -70.0 * 0.05) ? 1 : 0)";  // 与えられたdB以上の音だけ抜き出し
        }

        public override float[][] Do(float[][] buffer)
        {
            // TODO: ステレオの両方のチャンネルに対して処理する

            using (var engine = new V8ScriptEngine())
            {
                int BLOCK_SIZE = 8192;

                engine.Execute("__mapper = (" + ExpressionCode + ")");

                var code1 = engine.Compile(
                    "(function(){" +
                    "var __result = new Array(__inputArray.length); " +
                    "for(__j=0;__j<__inputArray.length;__j++){__result[__j]=__mapper(__inputArray[__j]);} " +
                    "return __result.join(',');})()"
                    );

                return buffer.Select(channelOfBuffer =>
                {
                    List<float> wholeResult = new List<float>();

                    for (int blockId = 0; blockId < (channelOfBuffer.Length - 1) / BLOCK_SIZE + 1; blockId++)
                    {
                        int fromInclusive = BLOCK_SIZE * blockId;
                        int toExclusive = Math.Min(BLOCK_SIZE * (blockId + 1), channelOfBuffer.Length);

                        var s = new StringBuilder();
                        s.Append("__inputArray = [");
                        
                        // 配列ではなく文字列に変換して渡すことにより大幅に高速化
                        s.Append(string.Join(",", Enumerable.Range(fromInclusive, toExclusive - fromInclusive).Select(i =>
                        {
                            var x = channelOfBuffer[i];

                            if (float.IsNaN(x)) { return "NaN"; }  // ここいる？？
                            else if (float.IsNegativeInfinity(x)) { return "-Infinity"; }
                            else if (float.IsPositiveInfinity(x)) { return "Infinity"; }

                            return x.ToString();
                        })));

                        s.Append("];");

                        engine.Execute(engine.Compile(s.ToString()));

                        dynamic result = engine.Evaluate(code1);

                        float[] result3 = ((string)result).Split(',').Select(x =>
                        {
                            if (x == "Infinity") return float.PositiveInfinity;
                            if (x == "-Infinity") return float.NegativeInfinity;
                            if (x == "NaN") return float.NaN;
                            // NaNやInfinityは残さない方が良い気もします・・・

                            return float.Parse(x);
                        }).ToArray();

                        wholeResult.AddRange(result3);
                    }

                    return wholeResult.ToArray();
                }).ToArray();
            }
        }
    }
}
