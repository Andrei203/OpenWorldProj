using System;
using System.Collections.Generic;
using UnityEngine;

public class TerrainChunkSystem : MonoBehaviour
{
    public List<TerrainTiles> tiles;
    //public float tileRadius = 0.6f;
    public Camera mainCamera;
    public float tileRange = 25.0F;

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
            tile.UpdateLOD(distance < tileRange ? distance < tileRange * 0.66F ? distance < tileRange * 0.33F ? 0 : 1 : 2 : -1);
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
}