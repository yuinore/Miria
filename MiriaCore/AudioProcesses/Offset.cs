using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiriaCore.AudioProcesses
{
    class Offset : AudioProcess
    {
        public double OffsetAmount { get; set; }

        public override float[][] Do(float[][] buffer)
        {
            float ofs = (float)OffsetAmount;
            for (int ch = 0; ch < buffer.Length; ch++)
            {
                for (int i = 0; i < buffer[ch].Length; i++)
                {
                    buffer[ch][i] += ofs;
                }
            }

            return buffer;
        }
    }
}
