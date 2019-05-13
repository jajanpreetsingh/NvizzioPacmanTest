using UnityEngine;

public class InputControllor
{
    public static KeyCode GetKeyDownCode()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            return KeyCode.UpArrow;

        if (Input.GetKeyDown(KeyCode.DownArrow))
            return KeyCode.DownArrow;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            return KeyCode.LeftArrow;

        if (Input.GetKeyDown(KeyCode.RightArrow))
            return KeyCode.RightArrow;

        return KeyCode.DoubleQuote;
    }
}