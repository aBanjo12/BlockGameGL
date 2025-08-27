using System.Numerics;
using BlockGameGL.Client.Input;
using Silk.NET.Input;

namespace BlockGameGL.Client.Render;

public class Camera
{
    //Setup the camera's location, directions, and movement speed
    private Vector3 CameraPosition = new Vector3(8.0f, 8.0f, 32.0f);
    private Vector3 CameraFront = new Vector3(0.0f, 0.0f, -1.0f);
    private Vector3 CameraUp = Vector3.UnitY;
    private Vector3 CameraDirection = new Vector3(8.0f, 8.0f, 8.0f);
    private float CameraYaw = -90f;
    private float CameraPitch = 0f;
    private float CameraZoom = 45f;
    
    private Vector2 LastMousePosition = default;

    float movespeed = 0.05f;//(float) (window.Time * 100);
    private float aspectRatio = 16f / 9f;

    public Camera()
    {
        Input.Input.HeldActions.Add(BindActions.MoveForward, onW);
        Input.Input.HeldActions.Add(BindActions.MoveBackward, onS);
        Input.Input.HeldActions.Add(BindActions.MoveLeft, onA);
        Input.Input.HeldActions.Add(BindActions.MoveRight, onD);
        Input.Input.HeldActions.Add(BindActions.MoveUp, onSpace);
        Input.Input.HeldActions.Add(BindActions.MoveDown, onShift);
        Input.Input.MouseInput.PrimaryMouse.MouseMove += OnMouseMove;
        Console.WriteLine("Mouse move registeted");
    }
    
    public void SetUniforms(ref Shader shader)
    {
        shader.SetUniform("uModel", GetModelMatrix());
        shader.SetUniform("uView", GetViewMatrix());
        shader.SetUniform("uProjection", GetProjectionMatrix());
    }

    public Matrix4x4 GetModelMatrix()
    {
        return Matrix4x4.CreateTranslation(0, 0, 0);
    }

    public Matrix4x4 GetViewMatrix()
    {
        return Matrix4x4.CreateLookAt(CameraPosition, CameraPosition + CameraFront, CameraUp);
    }

    public Matrix4x4 GetProjectionMatrix()
    {
        return Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(CameraZoom), aspectRatio, 0.1f, 100.0f);
    }
    
    private void OnMouseMove(IMouse mouse, Vector2 position)
    {
        var lookSensitivity = 0.1f;
        if (LastMousePosition == default) { LastMousePosition = position; }
        else
        {
            var xOffset = (position.X - LastMousePosition.X) * lookSensitivity;
            var yOffset = (position.Y - LastMousePosition.Y) * lookSensitivity;
            LastMousePosition = position;

            CameraYaw += xOffset;
            CameraPitch -= yOffset;

            //We don't want to be able to look behind us by going over our head or under our feet so make sure it stays within these bounds
            CameraPitch = Math.Clamp(CameraPitch, -89.0f, 89.0f);

            CameraDirection.X = MathF.Cos(MathHelper.DegreesToRadians(CameraYaw)) * MathF.Cos(MathHelper.DegreesToRadians(CameraPitch));
            CameraDirection.Y = MathF.Sin(MathHelper.DegreesToRadians(CameraPitch));
            CameraDirection.Z = MathF.Sin(MathHelper.DegreesToRadians(CameraYaw)) * MathF.Cos(MathHelper.DegreesToRadians(CameraPitch));
            CameraFront = Vector3.Normalize(CameraDirection);
        }
    }
    
    private void onW()
    {
        CameraPosition += CameraFront * new Vector3(movespeed);
    }

    private void onS()
    {
        CameraPosition -= CameraFront * new Vector3(movespeed);
    }

    private void onA()
    {
        CameraPosition -= Vector3.Normalize(Vector3.Cross(CameraFront, CameraUp)) * new Vector3(movespeed);
    }

    private void onD()
    {
        CameraPosition += Vector3.Normalize(Vector3.Cross(CameraFront, CameraUp)) * new Vector3(movespeed);
    }
    
    private void onSpace()
    {
        CameraPosition += CameraUp * new Vector3(movespeed);
    }
    
    private void onShift()
    {
        CameraPosition += -CameraUp * new Vector3(movespeed);
    }
}