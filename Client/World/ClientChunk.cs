using BlockGameGL.Client.Render;
using Silk.NET.OpenGL;

namespace BlockGameGL.Client.World;

public class ClientChunk
{
    public Block.Block[,,] Blocks;
    public VertexArrayObject<float> Mesh {
        get
        {
            if (dirty)
                return BuildChunkMesh();
            return _mesh;
        }
    }

    private VertexArrayObject<float> _mesh;
    private GL _gl;
    bool dirty  = true;
    //public VertexArrayObject<float, uint> Mesh => BuildChunkMesh();

    public ClientChunk(GL gl, Block.Block[,,] blocks)
    {
        _gl = gl;
        Blocks = blocks;
    }

    private VertexArrayObject<float> BuildChunkMesh()
    {
        List<float> verts = new List<float>();

        foreach (var block in Blocks)
        {
            verts.AddRange(block.Vertices);
        }
        
        BufferObject<float> Vbo = new BufferObject<float>(_gl, verts.ToArray(), BufferTargetARB.ArrayBuffer);
        VertexArrayObject<float> Vao = new VertexArrayObject<float>(_gl, Vbo);
        
        Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 5, 0);
        Vao.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 5, 3);

        dirty = false;
        _mesh = Vao;
        return Vao;
    }
}