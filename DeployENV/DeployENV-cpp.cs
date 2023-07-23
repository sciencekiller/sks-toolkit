using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace sks_toolkit.DeployENV
{
    internal class DeployENV_cpp
    {
        public static void Deploy_Cpp(string version)
        {
            for(int i = 0; i < 5; i++)
            {
                Trace.WriteLine("Thread!");
                Thread.Sleep(1000);
            }
        }
    }
}
