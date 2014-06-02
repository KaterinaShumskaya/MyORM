using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCAbstraction
{
    public interface ILogger
    {
        void Trace(string message);

        void Error(string message);

        void Fatal(string message);

        void Info(string message);
    }
}
