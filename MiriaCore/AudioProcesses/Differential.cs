using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiriaCore.AudioProcesses
{
    class Differential : AudioProcess
    {
        public override float[][] Do(float[][] buffer)
        {
            for (int ch = 0; ch < buffer.Length; ch++)
            {
                float lastval = 0;
                for (int i = 0; i < buffer[ch].Length; i++)
                {
                    float temp = buffer[ch][i];
                    buffer[ch][i] = buffer[ch][i] - lastval;
                    lastval = temp;
                }
            }

            return buffer;
        }
    }
}
