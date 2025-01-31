using System.Numerics;
using BlockGameGL.Client.Input.Button;
using Silk.NET.Input;

namespace BlockGameGL.Client.Input.Vector;

public class MouseInput : IInputEvents<MouseButton>
{
    public IMouse PrimaryMouse;
    public Dictionary<MouseButton, Action> KeyEvents { get; set; }

    
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
        PrimaryMouse = input.Mice.FirstOrDefault();

        if (PrimaryMouse != null)
        {
            PrimaryMouse.Cursor.CursorMode = CursorMode.Raw;
            PrimaryMouse.MouseDown += PollEvents;
        }
    }

    public void PollEvents(IMouse mouse, MouseButton button)
    {
        if (mouse.Index == PrimaryMouse.Index)
        {
            KeyEvents[button].Invoke();
        }
    }
}