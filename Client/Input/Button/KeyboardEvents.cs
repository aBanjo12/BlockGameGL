using Silk.NET.Input;

namespace BlockGameGL.Client.Input.Button;

public class KeyboardEvents : IInputEvents<Key>
{
    public IKeyboard PrimaryKeyboard;

    public Dictionary<Key, Action> KeyEvents { get; set; } = new();
    public List<Key> HeldKeys = new();

    public KeyboardEvents()
    {
        foreach (Key key in Enum.GetValues(typeof(Key)))
        {
            try
            {
                KeyEvents.Add(key, () => { });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        foreach (var pair in KeybindList.Keybinds)
        {
            if (pair.Key.Item2)
            {
                KeyEvents[pair.Key.Item1] = () => { Input.HeldActions[pair.Value].Invoke(); };
                continue;
            }

            KeyEvents[pair.Key.Item1] = () => { Input.BindActions[pair.Value].Invoke(); };

        }
    } 
    
    public void Init(IInputContext input)
    {
        PrimaryKeyboard = input.Keyboards.FirstOrDefault();
        if (PrimaryKeyboard != null)
        {
            PrimaryKeyboard.KeyDown += PollPressedEvents;
            PrimaryKeyboard.KeyUp += PollKeyUp;
        }
    }

    public void PollHeldEvents()
    {
        foreach (var key in HeldKeys)
        {
            KeyEvents[key].Invoke();
        }
    }

    public void PollPressedEvents(IKeyboard keyboard, Key key, int arg)
    {
        if (!HeldKeys.Contains(key))
        {
            HeldKeys.Add(key);
        }
        if (keyboard.Index == PrimaryKeyboard.Index)
            KeyEvents[key].Invoke();
    }

    public void PollKeyUp(IKeyboard keyboard, Key key, int arg)
    {
        if (HeldKeys.Contains(key))
        {
            HeldKeys.Remove(key);
        }
    }
}