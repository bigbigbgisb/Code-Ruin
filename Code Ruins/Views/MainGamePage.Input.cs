using Avalonia.Input;
using System;
using System.Collections.Generic;
using System.Text;


namespace Code_Ruins
{
    public partial class MainGamePage
    {
        void UpdateMovementKeyDownState(KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                isUp = true;
            }
            if (e.Key == Key.S)
            {
                isDown = true;
            }
            if (e.Key == Key.A)
            {
                isLeft = true;
            }
            if (e.Key == Key.D)
            {
                isRight = true;
            }
        }
        void UpdateMovementKeyUpState(KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                isUp = false;
            }
            if (e.Key == Key.S)
            {
                isDown = false;
            }
            if (e.Key == Key.A)
            {
                isLeft = false;
            }
            if (e.Key == Key.D)
            {
                isRight = false;
            }
        }
        void UpdateTaskKeyState(KeyEventArgs e)
        {
            if (!isInTaskZone)
            {
                return;
            }
            if (e.Key == Enum.Parse<Key>(interactKey) && isInTaskZone)
            {
                isInteractPressed = true;
            }
        }
    }
}
