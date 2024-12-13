using Silk.NET.Input;

namespace BlockGameGL.Client.Input;

public class Input
{
    public static Dictionary<BindActions, Action> BindActions { get; set; } = new();
    public static IInputContext InputContext { get; set; }

    public static bool IsKeyboardMouse = true;
    
}