using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Element
{
    interface IInputHandler
    {
        void Input(ref XB1Pad input);
    }
}
