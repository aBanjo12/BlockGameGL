using BlockGameGL.Client.Render;
using BlockGameGL.Client.World.Block;
using Silk.NET.OpenGL;

namespace BlockGameGL.Client.World;

public class ClientChunk
{
    private static uint X = 1;
    private static uint Y = 17;
    private static uint Z = 289;

    public static readonly uint[][] CubeFaceTriangles = new uint[][]
    {
        [
            Z,          X,          0,
            Z,          Z + X,      X
        ], //front
        [
            X + Y + Z,  Y,          X + Y,
            X + Y + Z,  Y + Z,      Y
        ], //back
        [
            Y + Z,      0,          Y, 
            Y + Z,      Z,          0
        ], //left
        [
            X + Z,      X + Y,      X,
            X + Z,      X + Y + Z,  X + Y
        ], //right
        [
            0,          X + Y,      Y, 
            0,          X,          X + Y
        ], //top
        [
            Y + Z,      Z + X,      Z,
            Y + Z,      X + Y + Z,  Z + X
        ] //bottom
    };
    
    private static float[] verts = GetVerts();
    public static BufferObject<float> Vbo;

    public bool[] ChunkCullInfo = [false, false, false, false, false, false]; //cull if true
    public Block.Block[,,] Blocks;

    public VertexArrayObject<float, uint> Mesh
    {
        get
        {
            if (dirty)
                return BuildChunkMesh();
            return _mesh;
        }
    }

    private VertexArrayObject<float, uint> _mesh;
    private GL _gl;
    bool dirty = true;
    //public VertexArrayObject<float, uint> Mesh => BuildChunkMesh();

    public ClientChunk(GL gl, Block.Block[,,] blocks)
    {
        if (Vbo == null)
            Vbo = new BufferObject<float>(gl, verts.ToArray(), BufferTargetARB.ArrayBuffer);
        _gl = gl;
        Blocks = blocks;
    }

    private VertexArrayObject<float, uint> BuildChunkMesh()
    {
        List<uint> indices = new List<uint>();

        int count = 0;
        
        foreach (var v in Enum.GetValues(typeof(BlockFace)))
        {
            if (!ChunkCullInfo[(int)v])
            {
                for (uint i = 0; i < 16; i++)
                {
                    for (uint j = 0; j < 16; j++)
                    {
                        for (uint k = 0; k < 16; k++)
                        {
                            if (Blocks[k, j, i] != null)
                            {
                                uint startIndex = k * X + j * Y + i * Z;
                                foreach (var f in CubeFaceTriangles[(int)v])
                                {
                                    indices.Add(startIndex + f);
                                    Console.WriteLine(startIndex + f);
                                    count++;
                                }    
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine(count);

        BufferObject<uint> Ibo = new BufferObject<uint>(_gl, indices.ToArray(), BufferTargetARB.ElementArrayBuffer);

        VertexArrayObject<float, uint> Vao = new VertexArrayObject<float, uint>(_gl, Vbo, Ibo, (uint)indices.Count);

        Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 3, 0);
        
        Vao.Bind();

        dirty = false;
        _mesh = Vao;
        return Vao;
    }

    private static float[] GetVerts()
    {
        List<float> vert = new List<float>();
        for (int i = 0; i < 17; i++)
        {
            for (int j = 0; j < 17; j++)
            {
                for (int k = 0; k < 17; k++)
                {
                    vert.Add(k);
                    vert.Add(j);
                    vert.Add(i);
                }
            }
        }

        return vert.ToArray();
    }
}