using System.Numerics;
using BlockGameGL.Client.World;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace BlockGameGL.Client.Render;

public class GameRenderer
{
    //private BufferObject<float> Vbo;
    //private BufferObject<uint> Ebo;
    //private VertexArrayObject<float> Vao;
    private Texture Texture;
    private Shader Shader;
    private GL Gl;
    private IWindow window;
    private Camera Camera;
    
    /*private readonly float[] Vertices =
    {
        //X    Y      Z       U     V
        -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
         0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
         0.5f,  0.5f, -0.5f,  1.0f, 0.0f,
         0.5f,  0.5f, -0.5f,  1.0f, 0.0f,
        -0.5f,  0.5f, -0.5f,  0.0f, 0.0f,
        -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,

        -0.5f, -0.5f,  0.5f,  0.0f, 1.0f,
         0.5f, -0.5f,  0.5f,  1.0f, 1.0f,
         0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
         0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
        -0.5f,  0.5f,  0.5f,  0.0f, 0.0f,
        -0.5f, -0.5f,  0.5f,  0.0f, 1.0f,

        -0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
        -0.5f,  0.5f, -0.5f,  1.0f, 0.0f,
        -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
        -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
        -0.5f, -0.5f,  0.5f,  0.0f, 1.0f,
        -0.5f,  0.5f,  0.5f,  1.0f, 1.0f,

         0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
         0.5f,  0.5f, -0.5f,  1.0f, 0.0f,
         0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
         0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
         0.5f, -0.5f,  0.5f,  0.0f, 1.0f,
         0.5f,  0.5f,  0.5f,  1.0f, 1.0f,

        -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
         0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
         0.5f, -0.5f,  0.5f,  1.0f, 1.0f,
         0.5f, -0.5f,  0.5f,  1.0f, 1.0f,
        -0.5f, -0.5f,  0.5f,  0.0f, 1.0f,
        -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,

        -0.5f,  0.5f, -0.5f,  0.0f, 0.0f,
         0.5f,  0.5f, -0.5f,  1.0f, 0.0f,
         0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
         0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
        -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
        -0.5f,  0.5f, -0.5f,  0.0f, 0.0f
    };*/

    public void SetWindow(IWindow window)
    {
        this.window = window;
    }

    public void OnLoad()
    {
        var input = window.CreateInput();
        Input.Input.InputContext = input;
        Input.Input.MouseInput.Init(input);
        Input.Input.KeyboardEvents.Init(input);
        Gl = GL.GetApi(window);
        ClientWorld.Instance = new ClientWorld(Gl);
        Gl.Disable(EnableCap.CullFace);
        Gl.Enable(GLEnum.DebugOutput);
        Gl.Enable(GLEnum.DebugOutputSynchronous);
        Gl.DebugMessageCallback((source, type, id, severity, length, message, userParam) =>
        {
            string msg = Silk.NET.Core.Native.SilkMarshal.PtrToString((nint)message);
            Console.WriteLine($"[OpenGL] {type} {severity} {id}: {msg}");
        }, IntPtr.Zero);

        Camera = new Camera();
        
        //Vbo = new BufferObject<float>(Gl, Vertices, BufferTargetARB.ArrayBuffer);
        //Vao = new VertexArrayObject<float>(Gl, Vbo);

        //Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 5, 0);
        //Vao.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 5, 3);

        Shader = new Shader(Gl, "Client/Render/Shaders/shader.vert", "Client/Render/Shaders/shader.frag");

        Texture = new Texture(Gl, "block.jpg");
    }

    public void OnRender(double deltaTime)
    {
        Gl.Enable(EnableCap.DepthTest);
        Gl.Clear((uint) (ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));

        ClientWorld.Instance.Chunks[0,0].Mesh.Bind();
        Texture.Bind();
        Shader.Use();
        //Shader.SetUniform("uTexture0", 0);
        
        Camera.SetUniforms(ref Shader);

        //We're drawing with just vertices and no indices, and it takes 36 vertices to have a six-sided textured cube
        Gl.DrawElements(PrimitiveType.Triangles, ClientWorld.Instance.Chunks[0,0].Mesh.IndexCount, DrawElementsType.UnsignedInt, UIntPtr.Zero);
    }
    
    public void OnUpdate(double deltaTime)
    {
        Input.Input.KeyboardEvents.PollHeldEvents();
    }

    public void OnFramebufferResize(Vector2D<int> newSize)
    {
        Gl.Viewport(newSize);
    }

    public void OnClose()
    {
        ClientWorld.Instance.Chunks[0,0].Mesh.Dispose();
        Shader.Dispose();
        Texture.Dispose();
    }
}