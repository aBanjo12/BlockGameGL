using Silk.NET.OpenGL;

namespace BlockGameGL.Client.Render
{
    public class VertexArrayObject<TVertexType> : IDisposable
        where TVertexType : unmanaged
    {
        private readonly uint _handle;
        private readonly GL _gl;

        public VertexArrayObject(GL gl, BufferObject<TVertexType> vbo)
        {
            _gl = gl;
            _handle = _gl.GenVertexArray();
            Bind();
            vbo.Bind(); // Bind VBO to VAO
        }

        public unsafe void VertexAttributePointer(uint index, int count, VertexAttribPointerType type, uint vertexSize, int offSet)
        {
            _gl.VertexAttribPointer(
                index,
                count,
                type,
                false,
                vertexSize * (uint)sizeof(TVertexType),
                (void*)(offSet * sizeof(TVertexType))
            );
            _gl.EnableVertexAttribArray(index);
        }

        public void Bind()
        {
            Console.WriteLine(_handle);
            _gl.BindVertexArray(_handle);
        }

        public void Dispose()
        {
            _gl.DeleteVertexArray(_handle);
        }
    }
}