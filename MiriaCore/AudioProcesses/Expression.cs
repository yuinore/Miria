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
        }

        public static float[] V8ObjectToFloatArray(dynamic source)
        {
            List<float> found = new List<float>();

            for (int i = 0; i < source.length; i++)
            {
                object next = source[i];

                found.Add(
                    (next is double) ? (float)((double)next) :
                    (next is float) ? (float)next :
                    0.0f
                    );  // なんでや
            }

            return found.ToArray();
        }

        public override float[][] Do(float[][] buffer)
        {
            // TODO: ステレオの両方のチャンネルに対して処理する

            using (var engine = new V8ScriptEngine())
            {
                int BLOCK_SIZE = 8192;

                object aaa = engine.Evaluate("[1,2,3]");
                
                //object bbb = engine.Evaluate("x => 10");

                //object mapper = engine.Evaluate("__mapper = (x => 2 * x)");
                //object mapper = engine.Evaluate("__mapper = (function(x){ return x * x; })");
                engine.Execute("__mapper = (" + ExpressionCode + ")");

                if (false)
                {
                    // TODO: 何らかの方法で高速化

                    engine.AddHostObject("__inputArrayCLR", buffer[0].Select(x => (double)x).ToArray());

                    engine.Execute("inputArray = new Array(__inputArrayCLR.Length); for(i=0;i<__inputArrayCLR.Length;i++){ inputArray[i] = __inputArrayCLR[i]; }");

                    dynamic aaaaaa = engine.Evaluate("inputArray.Length");

                    dynamic bbbbbb = engine.Evaluate("inputArray[0]");

                    //object result = engine.Evaluate("__inputArray.map(function(arr){return arr.map(mapper);})");
                    //object result = engine.Evaluate("result=[]; for(i=0;i<__inputArray.length;i++){result.push(new Array(__inputArray[i].length)); for(j=0;j<__inputArray[i].length;j++){result[i][j]=__mapper(result[i][j]);}} result;");
                    dynamic result = engine.Evaluate("result = new Array(inputArray.length); for(j=0;j<inputArray.length;j++){result[j]=__mapper(inputArray[j]);} result");

                    //return ((double[][])(result)).Select(arr => arr.Select(y => (float)y).ToArray()).ToArray();
                    //double[] result2 = (V8ObjectToArray<double>(result));
                    //float[] result3 = result2.Select((Func<double,float>)(x => (float)x)).ToArray();
                    float[] result2 = (V8ObjectToFloatArray(result));
                    float[] result3 = result2.Select((Func<float, float>)(x => (float)x)).ToArray();

                    return new float[][] { result3, result3 };
                }
                else
                {
                    List<float> wholeResult = new List<float>();

                    var code1 = engine.Compile("result = new Array(inputArray.length); for(j=0;j<inputArray.length;j++){result[j]=__mapper(inputArray[j]);} result.join(',')");

                    for (int blockId = 0; blockId < (buffer[0].Length - 1) / BLOCK_SIZE + 1; blockId++)
                    {
                        int fromInclusive = BLOCK_SIZE * blockId;
                        int toExclusive = Math.Min(BLOCK_SIZE * (blockId + 1), buffer[0].Length);

                        var s = new StringBuilder();

                        s.Append("inputArray = [");
                        
                        s.Append(string.Join(",", Enumerable.Range(fromInclusive, toExclusive - fromInclusive).Select(i => buffer[0][i] + "")));  // こんなアホみたいな方法でも意外と速くて草

                        s.Append("];");

                        engine.Execute(engine.Compile(s.ToString()));
                        //engine.Execute(s.ToString());

                        dynamic result = engine.Evaluate(code1);
                        //dynamic result = engine.Evaluate("result = new Array(inputArray.length); for(j=0;j<inputArray.length;j++){result[j]=__mapper(inputArray[j]);} result");

                        //float[] result2 = (V8ObjectToFloatArray(result));
                        //float[] result3 = result2.Select((Func<float, float>)(x => (float)x)).ToArray();

                        // 配列を渡すのではなく、文字列に変換して渡すことにより大幅に高速化

                        float[] result3 = ((string)result).Split(',').Select(x =>
                        {
                            if (x == "Infinity") return float.PositiveInfinity;
                            if (x == "-Infinity") return float.NegativeInfinity;
                            if (x == "NaN") return float.NaN;

                            return float.Parse(x);
                        }).ToArray();

                        wholeResult.AddRange(result3);
                    }

                    return new float[][] { wholeResult.ToArray(), wholeResult.ToArray() };  // 冷静に考えると前半と後半が同じ参照だとダメだな
                }
            }

            //return buffer;
        }
    }
}
