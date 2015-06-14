using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiriaCore
{
    abstract class ParameterType
    {
        public abstract double MaxValue { get; }
        public abstract double MinValue { get; }
        public abstract double DefaultValue { get; }
    }
}
