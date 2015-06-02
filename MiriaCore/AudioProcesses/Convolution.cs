using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiriaCore.AudioProcesses
{
    class Convolution : AudioProcess
    {
        float[][] ImpulseResponse;

        public Convolution(float[][] impulseResponse)
        {
            ImpulseResponse = impulseResponse;

            if (ImpulseResponse.Length <= 0) throw new Exception("そんなはずは・・・ない・・・！");
        }

        public override float[][] Do(float[][] buffer)
        {
            var outbuf = new float[buffer.Length][];
            var irSampleCountMax = ImpulseResponse.Select(x => x.Length).Max();

            for (int ch = 0; ch < buffer.Length; ch++)
            {
                int irCh = ImpulseResponse.Length == 1 ? 0 : ch;
                if (irCh >= ImpulseResponse.Length) break;

                outbuf[ch] = new float[buffer[ch].Length + irSampleCountMax - 1];

                for (int i = 0; i < buffer[ch].Length; i++)
                {
                    for (int k = 0; k < ImpulseResponse[irCh].Length; k++)
                    {
                        outbuf[ch][i + k] += buffer[ch][i] * ImpulseResponse[irCh][ImpulseResponse[irCh].Length - k - 1];
                    }
                }
            }

            return outbuf;
        }
    }
}
