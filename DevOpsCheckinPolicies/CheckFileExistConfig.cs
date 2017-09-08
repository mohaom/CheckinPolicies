using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOpsCheckinPolicies
{
    [Serializable]
   public class CheckFileExistConfig
    {
        public string Filename { get; set; }
        public string location { get; set; }
    }
}
