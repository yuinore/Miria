using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiriaCore.AudioProcesses
{
    class FadeOut : AudioProcess
    {
        int samplingRate = 44100;

        public double FadeTime { get; set; }  // seconds

        public FadeOut()
        {
            FadeTime = 1.0;
        }

        public override float[][] Do(float[][] buffer)
        {
            int samplesFade = (int)Math.Round(FadeTime * samplingRate);

            for (int ch = 0; ch < buffer.Length; ch++)
            {
                int start = Math.Max(0, buffer[ch].Length - samplesFade);

                for (int i = start; i < buffer[ch].Length; i++)
                {
                    buffer[ch][i] *= 1.0f - (i - (buffer[ch].Length - samplesFade)) / (float)samplesFade;
                }
            }

            return buffer;
        }
    }
}
