using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiriaCore.AudioProcesses
{
    class Reverse : AudioProcess
    {
        public override float[][] Do(float[][] buffer)
        {
            for (int ch = 0; ch < buffer.Length; ch++)
            {
                for (int i = 0, j = buffer[ch].Length - 1; i < j; i++, j--)
                {
                    float tmp = buffer[ch][i];
                    buffer[ch][i] = buffer[ch][j];
                    buffer[ch][j] = tmp;
                }
            }

            return buffer;
        }
    }
}
