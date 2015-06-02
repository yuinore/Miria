using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiriaCore.AudioProcesses
{
    class Integral : AudioProcess
    {
        public override float[][] Do(float[][] buffer)
        {
            for (int ch = 0; ch < buffer.Length; ch++)
            {
                double sum = 0;
                for (int i = 0; i < buffer[ch].Length; i++)
                {
                    sum += buffer[ch][i];
                    buffer[ch][i] = (float)sum;
                }
            }

            return buffer;
        }
    }
}
