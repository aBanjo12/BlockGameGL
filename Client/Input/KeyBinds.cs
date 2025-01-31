using Silk.NET.Input;

namespace BlockGameGL.Client.Input;

public static class KeybindList
{
    public static Dictionary<(Key, bool), BindActions> Keybinds = new()
    {
        { (Key.W, true), BindActions.MoveForward },
        { (Key.S, true), BindActions.MoveBackward },
        { (Key.A, true), BindActions.MoveLeft },
        { (Key.D, true), BindActions.MoveRight }
    };
}