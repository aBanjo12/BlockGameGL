using BlockGameGL.Client.Input.Button;
using Silk.NET.Input;

namespace BlockGameGL.Client.Input.Vector;

public class MouseInput : IInputEvents<MouseButton>
{
    private IMouse primaryMouse;
    
    public MouseInput()
    {
        KeyEvents = new Dictionary<MouseButton, Action>
        {
            {MouseButton.Left, () => Console.WriteLine("Left Mouse Button Pressed")},
            {MouseButton.Right, () => Console.WriteLine("Right Mouse Button Pressed")}
        };
    }
    
    public void Init(IInputContext input)
    {
        primaryMouse = input.Mice.FirstOrDefault();
        primaryMouse.Cursor.CursorMode = CursorMode.Raw;
    }

    public Dictionary<MouseButton, Action> KeyEvents { get; set; }
}