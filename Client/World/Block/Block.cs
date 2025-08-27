namespace BlockGameGL.Client.World.Block;

public abstract class Block
{
    public bool[] FaceCullInfo = [ false, false, false, false, false, false ]; //cull if true

    // private float[] getVerts()
    // {
    //     int count = 0;
    //     foreach (bool cull in  FaceCullInfo)
    //     {
    //         if (cull)
    //             count++;
    //     }
    //     
    //     float[] verts = new float[count*CubeFaceTriangles[0].Length];
    //     count = 0;
    //     
    //     for (int i = 0; i < FaceCullInfo.Length; i++)
    //     {
    //         if (FaceCullInfo[i])
    //         {
    //             foreach (float v in CubeFaceTriangles[i])
    //             {
    //                 verts[count] = v;
    //                 count++;
    //             }
    //         }
    //     }
    //     
    //     return verts;
    // }
}