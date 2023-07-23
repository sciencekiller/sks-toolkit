using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sks_toolkit.DeployENV
{
    internal class Deploy_main
    {
        bool install_gpp = false;
        string gpp_version;
        public void Deploy()
        {
            DeployENV_cpp.Deploy_Cpp(gpp_version);
        }
    }
}
