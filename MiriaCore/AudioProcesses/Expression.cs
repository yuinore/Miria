#define EXPRESSION_USE_ENCODING_IN
#define EXPRESSION_USE_ENCODING_OUT

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
            //ExpressionCode = "x => x";  // 何も処理せず出力
        }

        public override float[][] Do(float[][] buffer)
        {
            // TODO: ステレオの両方のチャンネルに対して処理する

            using (var engine = new V8ScriptEngine())
            {
                int BLOCK_SIZE = 8192;

                engine.Execute("__mapper = (" + ExpressionCode + ")");

                // アルファベットにエンコードすることにより更に約4倍の高速化
                var code1 = engine.Compile(
                    "(function(){" +
#if EXPRESSION_USE_ENCODING_IN
                    "var __byteArray = new Array(__inputStr.length / 2);\n" +
                    "for(__i = 0; __i < __inputStr.length / 8; __i++) {\n" +
                    "  __byteArray[__i * 4 + 3] = (__inputStr.charCodeAt(__i * 8 + 0) - 65) * 16 + (__inputStr.charCodeAt(__i * 8 + 1) - 65);\n" +
                    "  __byteArray[__i * 4 + 2] = (__inputStr.charCodeAt(__i * 8 + 2) - 65) * 16 + (__inputStr.charCodeAt(__i * 8 + 3) - 65);\n" +
                    "  __byteArray[__i * 4 + 1] = (__inputStr.charCodeAt(__i * 8 + 4) - 65) * 16 + (__inputStr.charCodeAt(__i * 8 + 5) - 65);\n" +
                    "  __byteArray[__i * 4 + 0] = (__inputStr.charCodeAt(__i * 8 + 6) - 65) * 16 + (__inputStr.charCodeAt(__i * 8 + 7) - 65);\n" +
                    "}\n" +
                    "var __inputArray = new Float32Array((new Uint8Array(__byteArray)).buffer);\n" +
#endif
                    "var __result = new Array(__inputArray.length);\n" +
                    "for(__j=0;__j<__inputArray.length;__j++){__result[__j]=__mapper(__inputArray[__j]);}\n" +
#if EXPRESSION_USE_ENCODING_OUT
                    "__byteArray = new Uint8Array((new Float32Array(__result)).buffer);\n" +
                    "var __outputStr = \"\";\n" +
                    "for(__i = 0; __i < __result.length; __i++) {\n" +
                    "  __outputStr += String.fromCharCode(((__byteArray[__i * 4 + 3] >> 4) & 0x0F) + 65);\n" +
                    "  __outputStr += String.fromCharCode(((__byteArray[__i * 4 + 3]     ) & 0x0F) + 65);\n" +
                    "  __outputStr += String.fromCharCode(((__byteArray[__i * 4 + 2] >> 4) & 0x0F) + 65);\n" +
                    "  __outputStr += String.fromCharCode(((__byteArray[__i * 4 + 2]     ) & 0x0F) + 65);\n" +
                    "  __outputStr += String.fromCharCode(((__byteArray[__i * 4 + 1] >> 4) & 0x0F) + 65);\n" +
                    "  __outputStr += String.fromCharCode(((__byteArray[__i * 4 + 1]     ) & 0x0F) + 65);\n" +
                    "  __outputStr += String.fromCharCode(((__byteArray[__i * 4 + 0] >> 4) & 0x0F) + 65);\n" +
                    "  __outputStr += String.fromCharCode(((__byteArray[__i * 4 + 0]     ) & 0x0F) + 65);\n" +
                    "}\n" +
                    "return __outputStr;\n" +
#else
                    "return __result.join(',');\n" +
#endif
                    "})()"
                    );

                List<float[]> ret = new List<float[]>();

                foreach(var channelOfBuffer in buffer)
                {
                    List<float> wholeResult = new List<float>();

                    for (int blockId = 0; blockId < (channelOfBuffer.Length - 1) / BLOCK_SIZE + 1; blockId++)
                    {
                        int fromInclusive = BLOCK_SIZE * blockId;
                        int toExclusive = Math.Min(BLOCK_SIZE * (blockId + 1), channelOfBuffer.Length);

                        var s = new StringBuilder();

#if EXPRESSION_USE_ENCODING_IN
                        s.Append("__inputStr = \"");

                        var charbuf = new byte[(toExclusive - fromInclusive) * 8];

                        for (int i = 0, i4 = 0, i8 = 0; i < toExclusive - fromInclusive; i++, i4 += 4, i8 += 8)
                        {
                            var x = channelOfBuffer[i + fromInclusive];

                            var floatbits = BitConverter.GetBytes(x);

                            charbuf[i8 + 0] = (byte)(((floatbits[3] >> 4) & 0x0F) + 65);
                            charbuf[i8 + 1] = (byte)(((floatbits[3] >> 0) & 0x0F) + 65);
                            charbuf[i8 + 2] = (byte)(((floatbits[2] >> 4) & 0x0F) + 65);
                            charbuf[i8 + 3] = (byte)(((floatbits[2] >> 0) & 0x0F) + 65);
                            charbuf[i8 + 4] = (byte)(((floatbits[1] >> 4) & 0x0F) + 65);
                            charbuf[i8 + 5] = (byte)(((floatbits[1] >> 0) & 0x0F) + 65);
                            charbuf[i8 + 6] = (byte)(((floatbits[0] >> 4) & 0x0F) + 65);
                            charbuf[i8 + 7] = (byte)(((floatbits[0] >> 0) & 0x0F) + 65);
                        }

                        s.Append(Encoding.ASCII.GetString(charbuf));

                        s.Append("\";");
#else
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
#endif

                        // コンパイルしてもコンパイルしなくてもほとんど同じ時間でした
                        engine.Execute(engine.Compile(s.ToString()));
                        
                        dynamic result = engine.Evaluate(code1);

#if EXPRESSION_USE_ENCODING_OUT
                        string result_s = (string)result;
                        
                        float[] result3 = new float[toExclusive - fromInclusive];
                        
                        for (int i = 0; i < toExclusive - fromInclusive; i++)
                        {
                            result3[i] = BitConverter.ToSingle(new byte[] {
                                (byte)((result_s[i * 8 + 6] - 65) * 16 + (result_s[i * 8 + 7] - 65)),
                                (byte)((result_s[i * 8 + 4] - 65) * 16 + (result_s[i * 8 + 5] - 65)),
                                (byte)((result_s[i * 8 + 2] - 65) * 16 + (result_s[i * 8 + 3] - 65)),
                                (byte)((result_s[i * 8 + 0] - 65) * 16 + (result_s[i * 8 + 1] - 65)),
                            }, 0);
                        }
#else
                        float[] result3 = ((string)result).Split(',').Select(x =>
                        {
                            if (x == "Infinity") return float.PositiveInfinity;
                            if (x == "-Infinity") return float.NegativeInfinity;
                            if (x == "NaN") return float.NaN;
                            // NaNやInfinityは残さない方が良い気もします・・・

                            return float.Parse(x);
                        }).ToArray();
#endif

                        wholeResult.AddRange(result3);
                    }

                    ret.Add(wholeResult.ToArray());
                }

                return ret.ToArray();
            }
        }
    }
}
