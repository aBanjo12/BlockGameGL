using BlockGameGL.Client.World.Block;

namespace BlockGameGL.Client.World;

public class ClientWorld
{
    public ClientChunk[,] Chunks = new ClientChunk[8, 8];
    public IBlock[] Blocks = [new BlockDirt()];

    public ClientWorld()
    {
        Chunks[0,0] = new ClientChunk(new[,,] {{{0}}});
    }
}