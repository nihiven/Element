using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Element.Interfaces
{
    public interface IInput : IUpdate
    {
        int GetButtonState(Buttons button);
        Vector2 LeftThumbstickVector { get; }
        Vector2 RightThumbstickVector { get; }
        int LeftThumbstickCardinal { get; }
        int RightThumbstickCardinal { get; }
        float LeftThumbstickAngle { get; }
        float RightThumbstickAngle { get; }

        void SetVibration(float leftMotor, float rightMotor, float duration);
    }
}
