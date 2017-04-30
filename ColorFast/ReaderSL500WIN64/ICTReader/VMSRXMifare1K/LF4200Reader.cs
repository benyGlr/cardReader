using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMSRXMifare1K
{
    public class LF4200Reader : IReader
    {
        public bool IsInitialized
        {
            get
            {
                return false;
            }

            set
            {
            }
        }

        public bool Initialize(int port)
        {
            return false;
        }

        public bool IsCardPresent(out ushort cardTagType)
        {
            cardTagType = 0;
            return false;
        }
    }
}
