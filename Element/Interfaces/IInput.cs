using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Element.Interfaces
{
    public interface IInput
    {
        int GetButtonState(Buttons button);
        Vector2 GetLeftThumbstickVector();
        Vector2 GetRightThumbstickVector();
        int GetLeftThumbstickCardinal();
        int GetRightThumbstickCardinal();
    }
}
