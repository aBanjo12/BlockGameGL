using Silk.NET.OpenGL;

namespace BlockGameGL.Client.Render
{
    public class VertexArrayObject<TVertexType, TIndexType> : IDisposable
        where TVertexType : unmanaged
        where TIndexType : unmanaged
    {
        private readonly uint _handle;
        private readonly GL _gl;
        public uint IndexCount;

        public VertexArrayObject(GL gl, BufferObject<TVertexType> vbo, BufferObject<TIndexType> ebo, uint indexCount)
        {
            IndexCount = indexCount;
            _gl = gl;
            _handle = _gl.GenVertexArray();
            Bind();
            ebo.Bind();
            vbo.Bind(); // Bind VBO to VAO
        }

        public unsafe void VertexAttributePointer(uint index, int count, VertexAttribPointerType type, uint vertexSize, int offSet)
        {
            _gl.VertexAttribPointer(index, count, type, false, vertexSize * (uint) sizeof(TVertexType), (void*) (offSet * sizeof(TVertexType)));
            _gl.EnableVertexAttribArray(index);
        }

        public void Bind()
        {
            _gl.BindVertexArray(_handle);
        }

        public void Dispose()
        {
            _gl.DeleteVertexArray(_handle);
        }
    }
}