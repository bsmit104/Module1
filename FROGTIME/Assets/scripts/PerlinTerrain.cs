//////////////////asked chatgpt about perlin noise and then asked how i would add different 32x32 tiles to it/////////////
////////////i changed values around and stuff but had trouble getting tiles to show, still working on that part///////////
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int terrainWidth = 2000;
    public int terrainLength = 2000;
    public float scale = 50f;
    public float heightMultiplier = 50f;
    public Texture2D tileTexture1;
    public Texture2D tileTexture2;

    [System.Obsolete]
    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    [System.Obsolete]
    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = terrainWidth + 1;
        terrainData.size = new Vector3(terrainWidth, heightMultiplier, terrainLength);

        float[,] heights = GenerateHeights();
        terrainData.SetHeights(0, 0, heights);

        ApplyTextures(terrainData);

        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[terrainWidth, terrainLength];

        for (int x = 0; x < terrainWidth; x++)
        {
            for (int y = 0; y < terrainLength; y++)
            {
                float xCoord = (float)x / terrainWidth * scale;
                float yCoord = (float)y / terrainLength * scale;

                float height = Mathf.PerlinNoise(xCoord, yCoord);
                heights[x, y] = height;
            }
        }

        return heights;
    }

/////////////////was trying to get tiles to show, asked chatgpt//////////////////
    [System.Obsolete]
    void ApplyTextures(TerrainData terrainData)
    {
        float[,,] splatmapData = new float[terrainWidth, terrainLength, terrainData.alphamapLayers];

        for (int x = 0; x < terrainWidth; x++)
        {
            for (int y = 0; y < terrainLength; y++)
            {
                float xCoord = (float)x / terrainWidth * scale;
                float yCoord = (float)y / terrainLength * scale;

                float height = Mathf.PerlinNoise(xCoord, yCoord);

                int textureIndex = (height > 0.5f) ? 0 : 1;

                splatmapData[x, y, textureIndex] = 1;
            }
        }

        terrainData.SetAlphamaps(0, 0, splatmapData);

        terrainData.splatPrototypes = new SplatPrototype[]
        {
            new SplatPrototype { texture = tileTexture1, tileSize = new Vector2(16, 16) },
            new SplatPrototype { texture = tileTexture2, tileSize = new Vector2(16, 16) }
        };
    }
}
