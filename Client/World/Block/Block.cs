namespace BlockGameGL.Client.World.Block;

public abstract class Block
{
    public bool[] FaceCullInfo = [ true, true, true, true, true, true ];

    public static readonly float[][] CubeFaceTriangles = new float[][]
    {
        // Front face
        new float[]
        {
            // Triangle 1
            -0.5f, -0.5f,  0.5f,   0.0f, 0.0f,
            0.5f, -0.5f,  0.5f,   1.0f, 0.0f,
            0.5f,  0.5f,  0.5f,   1.0f, 1.0f,

            // Triangle 2
            0.5f,  0.5f,  0.5f,   1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,   0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,   0.0f, 0.0f,
        },

        // Back face
        new float[]
        {
            0.5f, -0.5f, -0.5f,   0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,   1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,   1.0f, 1.0f,

            -0.5f,  0.5f, -0.5f,   1.0f, 1.0f,
            0.5f,  0.5f, -0.5f,   0.0f, 1.0f,
            0.5f, -0.5f, -0.5f,   0.0f, 0.0f,
        },

        // Left face
        new float[]
        {
            -0.5f, -0.5f, -0.5f,   0.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,   1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,   1.0f, 1.0f,

            -0.5f,  0.5f,  0.5f,   1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,   0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,   0.0f, 0.0f,
        },

        // Right face
        new float[]
        {
            0.5f, -0.5f,  0.5f,   0.0f, 0.0f,
            0.5f, -0.5f, -0.5f,   1.0f, 0.0f,
            0.5f,  0.5f, -0.5f,   1.0f, 1.0f,

            0.5f,  0.5f, -0.5f,   1.0f, 1.0f,
            0.5f,  0.5f,  0.5f,   0.0f, 1.0f,
            0.5f, -0.5f,  0.5f,   0.0f, 0.0f,
        },

        // Top face
        new float[]
        {
            -0.5f,  0.5f,  0.5f,   0.0f, 0.0f,
            0.5f,  0.5f,  0.5f,   1.0f, 0.0f,
            0.5f,  0.5f, -0.5f,   1.0f, 1.0f,

            0.5f,  0.5f, -0.5f,   1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,   0.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,   0.0f, 0.0f,
        },

        // Bottom face
        new float[]
        {
            -0.5f, -0.5f, -0.5f,   0.0f, 0.0f,
            0.5f, -0.5f, -0.5f,   1.0f, 0.0f,
            0.5f, -0.5f,  0.5f,   1.0f, 1.0f,

            0.5f, -0.5f,  0.5f,   1.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,   0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,   0.0f, 0.0f,
        }
    };

    public float[] Vertices => getVerts();

    private float[] getVerts()
    {
        int count = 0;
        foreach (bool cull in  FaceCullInfo)
        {
            if (cull)
                count++;
        }
        
        float[] verts = new float[count*CubeFaceTriangles[0].Length];
        count = 0;
        
        for (int i = 0; i < FaceCullInfo.Length; i++)
        {
            if (FaceCullInfo[i])
            {
                foreach (float v in CubeFaceTriangles[i])
                {
                    verts[count] = v;
                    count++;
                }
            }
        }
        
        return verts;
    }
}