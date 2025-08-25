using BlockGameGL.Client.World.Block;
using Silk.NET.OpenGL;

namespace BlockGameGL.Client.World;

public class ClientWorld
{
    public static ClientWorld Instance = null;
    
    public ClientChunk[,] Chunks = new ClientChunk[8, 8];
    public Block.Block[] Blocks = [new BlockDirt()];

    private GL _gl;
    
    public ClientWorld(GL gl)
    {
        _gl = gl;
        Chunks[0,0] = new ClientChunk(_gl, new Block.Block[,,] {{{new BlockDirt(), new BlockDirt()}}});
    }
}