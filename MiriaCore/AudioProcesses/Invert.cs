using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiriaCore.AudioProcesses
{
    class Invert : AudioProcess
    {
        public override float[][] Do(float[][] buffer)
        {
            for (int ch = 0; ch < buffer.Length; ch++)
            {
                for (int i = 0; i < buffer[ch].Length; i++)
                {
                    buffer[ch][i] = -buffer[ch][i];
                }
            }

            return buffer;
        }
    }
}
