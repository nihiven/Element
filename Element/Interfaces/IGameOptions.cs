using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Element.Interfaces
{
    public interface IGameOptions
    {
        bool GetBoolOption(string option, bool defaultValue);
        void SetBoolOption(string option, bool value);
    }
}
