using System;
using System.Collections.Generic;
using UnityEngine;

public class TerrainChunkSystem : MonoBehaviour
{
    public GameObject TestChunkfab;
    private TerrainTiles[] tiles;
    //public float tileRadius = 0.6f;
    public Camera mainCamera;
    
    public float[] LODRanges = new float[0];

    //private CullingGroup _cullingGroup;
    //private BoundingSphere[] _boundingSpheres = new BoundingSphere[0];

    private void OnEnable()
    {
        //InitCullingGroup();
    }

    private void OnDisable()
    {
        //_cullingGroup.Dispose();
    }

    public void Start()
    {
        tiles = FindObjectsOfType<TerrainTiles>();
        
    }

    /*private void InitCullingGroup()
    {
        _cullingGroup = new CullingGroup();
        _boundingSpheres = new BoundingSphere[tiles.Count];

        for (var i = 0; i < _boundingSpheres.Length; i++)
        {
            _boundingSpheres[i].position = tiles[i].transform.position;
            _boundingSpheres[i].radius = tileRadius;
        }
        
        _cullingGroup.SetBoundingSpheres(_boundingSpheres);
        _cullingGroup.targetCamera = Camera.main;
        
        _cullingGroup.onStateChanged = OnStateChanged;
    }
    
    private void OnStateChanged(CullingGroupEvent evt)
    {
        Debug.Log("GHBNADIGFJAHYUGTGTFTGF");
        if (evt.hasBecomeVisible)
            tiles[evt.index].InstantiateTileAsset();
        
        if(evt.hasBecomeInvisible)
            tiles[evt.index].ReleaseTileAsset();
    }*/

    private void Update()
    {
        foreach (TerrainTiles tile in tiles)
        {
            float distance = Vector3.Distance(mainCamera.transform.position, tile.transform.position);

            int LOD = -1;
            for (int i = 0; i < LODRanges.Length; i++)
            {
                if(distance > LODRanges[i]) continue;
                LOD = i;
                break;
            }
            tile.UpdateLOD(LOD);
            
            
            /*bool inRange = Vector3.Distance(mainCamera.transform.position, tile.transform.position) < tileRange;
            if (!tile.enabled && inRange)
            {
                tile.InstantiateTileAsset();
            }
            else if (tile.enabled && !inRange)
            {
                tile.ReleaseTileAsset();
            }*/
        }
    }  
            [ContextMenu("Generate Test Chunks")]
            public void GenerateTestChunk()
            {
                for (int x = -5; x <= 5; x++)
                {
                    for (int y = -5; y <= 5; y++)
                    {
                        GameObject test = Instantiate(TestChunkfab, new Vector3(x * 100, 0, y * 100), Quaternion.identity,
                            transform);
                        test.name = "Chunk" + x + "," + y;
                    }
                }
            }
}