using Silk.NET.Input;

namespace BlockGameGL.Client.Input.Button;

public class KeyboardEvents : IInputEvents<Key>
{
    private IKeyboard primaryKeyboard;

    public Dictionary<Key, Action> KeyEvents { get; set; } = new();
    public KeyboardEvents()
    {
        foreach (Key key in Enum.GetValues(typeof(Key)))
        {
            KeyEvents.Add(key, () => { Console.WriteLine("pressed " + key); });
        }
    }

    public void Init(IInputContext input)
    {
        primaryKeyboard = input.Keyboards.FirstOrDefault();
        if (primaryKeyboard != null)
        {
            primaryKeyboard.KeyDown += PollEvents;
        }
    }

    public void PollEvents(IKeyboard keyboard, Key key, int arg)
    {
        if (keyboard.Index == primaryKeyboard.Index)
            KeyEvents[key].Invoke();
    }
}