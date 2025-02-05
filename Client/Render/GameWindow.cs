using System.Numerics;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace BlockGameGL.Client.Render;

public class GameWindow
{
    private IWindow window; 
    private GameRenderer Renderer = new();
    
    public void Run()
    {
        var options = WindowOptions.Default;
        options.Size = new Vector2D<int>(800, 600);
        options.Title = "LearnOpenGL with Silk.NET";
        window = Window.Create(options);
        
        window.Load += Renderer.OnLoad;
        window.Render += Renderer.OnRender;
        window.FramebufferResize += Renderer.OnFramebufferResize;
        window.Closing += Renderer.OnClose;
        window.Update += Renderer.OnUpdate;

        Renderer.SetWindow(window);
        window.Run();

        window.Dispose();
    }
}