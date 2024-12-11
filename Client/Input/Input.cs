namespace BlockGameGL.Client.Input;

public class Input
{
    public static Dictionary<BindActions, Action> BindActions { get; set; } = new();

    public bool IsKeyboardMouse = true;
    
}