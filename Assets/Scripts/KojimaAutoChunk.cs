using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KojimaAutoChunk : MonoBehaviour
{
    public LoadFromString prefab;
    public Vector2Int numChunks;
    public Vector2 chunkSize;

    [ContextMenu("Load Chunks")]
    public void LoadChunks()
    {
        for (int x = 0; x < numChunks.x; x++)
        {
            for (int y = 0; y < numChunks.y; y++)
            {
                LoadFromString go = Instantiate(prefab, new Vector3(y * chunkSize.x, 0.0F, x * chunkSize.y), Quaternion.identity,
                    transform);
                go.file = "kojima_" + x + "-" + y + ".obj";
            }
        }
    }
}
