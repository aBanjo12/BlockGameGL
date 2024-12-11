using Silk.NET.Input;

namespace BlockGameGL.Client.Input.Button;

public class JoystickButtonEvents : IInputEvents<Silk.NET.Input.Button>
{
    public JoystickButtonEvents(int index)
    {
        
    }
    
    public IGamepad PlayerGamepad { get; set; }
    public void Init(IInputContext input)
    {
        foreach (Silk.NET.Input.Button button in Enum.GetValues(typeof(Silk.NET.Input.Button)))
        {
            KeyEvents.Add(button, () => { Console.WriteLine("pressed " + button); });
        }
    }

    public Dictionary<Silk.NET.Input.Button, Action> KeyEvents { get; set; }
}