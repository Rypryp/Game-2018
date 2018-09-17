using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public enum DrawMode {NoiseMap, ColorMap, Mesh};
    public DrawMode drawMode;

    const int MapChunkSize = 241;
    [Range(0,6)]
    public int LevelOfDetail;

    public float NoiseScale;
    public int Octaves;
    [Range(0,1)]
    public float Persistance;
    public float Lacunarity;

    public int Seed;
    public Vector2 Offset;

    public float MeshHeightMultiplier;
    public AnimationCurve MeshHeigthCurve;

    public bool AutoUpdate;

    public TerrainType[] Regions;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(MapChunkSize, MapChunkSize, Seed, NoiseScale, Octaves, Persistance, Lacunarity,Offset);

        Color[] colorMap = new Color[MapChunkSize * MapChunkSize];
        for(int y = 0; y < MapChunkSize; y++)
        {
            for (int x = 0; x < MapChunkSize; x++)
            {
                float currentHeigth = noiseMap[x, y];
                for(int i = 0; i < Regions.Length; i++)
                {
                    if(currentHeigth <= Regions[i].Heigth)
                    {
                        colorMap[y * MapChunkSize + x] = Regions[i].Color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();

        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeigthMap(noiseMap));
        }
        else if(drawMode == DrawMode.ColorMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, MapChunkSize, MapChunkSize));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, MeshHeightMultiplier, MeshHeigthCurve, LevelOfDetail), TextureGenerator.TextureFromColorMap(colorMap, MapChunkSize, MapChunkSize));
        }
    }

    private void OnValidate()
    {
        if(Lacunarity < 1)
        {
            Lacunarity = 1;
        }

        if(Octaves < 0)
        {
            Octaves = 0;
        }
    }
}

[System.Serializable]
public struct TerrainType
{
    public string Name;
    public float Heigth;
    public Color Color;
}
