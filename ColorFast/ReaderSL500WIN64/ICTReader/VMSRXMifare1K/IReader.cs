using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMSRXMifare1K
{
    public interface IReader
    {
        bool Initialize(int port);
        bool IsCardPresent(out ushort cardTagType);
        bool IsInitialized { get; set; }
    }
}
