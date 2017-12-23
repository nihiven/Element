using Microsoft.Xna.Framework.Input;

namespace Element
{
    public interface IInput
    {
        int GetButtonState(Buttons button);
    }
}
