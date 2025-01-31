using System.Numerics;
using BlockGameGL.Client.Input.Button;
using BlockGameGL.Client.Input.Vector;
using Silk.NET.Input;

namespace BlockGameGL.Client.Input;

public static class Input
{
    public static Dictionary<BindActions, Action> BindActions = new();
    public static Dictionary<BindActions, Action> HeldActions = new();

    public static IInputContext InputContext { get; set; }
    public static MouseInput MouseInput = new();
    public static KeyboardEvents KeyboardEvents = new();
    public static bool IsKeyboardMouse = true;
    
}