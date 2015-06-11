using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiriaCore.AudioProcesses
{
    class GainDb : AudioProcess
    {
        public double GainInDecibel { get; set; }

        public GainDb()
        {
            GainInDecibel = 3.0;
        }

        public override float[][] Do(float[][] buffer)
        {
            float gainRaw = (float)Math.Pow(10, GainInDecibel * 0.05);

            for (int ch = 0; ch < buffer.Length; ch++)
            {
                for (int i = 0; i < buffer[ch].Length; i++)
                {
                    buffer[ch][i] *= gainRaw;
                }
            }

            return buffer;
        }
    }
}
