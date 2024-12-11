using Silk.NET.Input;

namespace BlockGameGL.Client.Input.Button;

public interface IInputEvents<T> where T : notnull
{
    public void Init(IInputContext input);
    public Dictionary<T, Action> KeyEvents { get; set; }
}