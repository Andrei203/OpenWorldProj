using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjChunkSystem : MonoBehaviour
{
    private LoadFromString[] tiles;
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
        tiles = FindObjectsOfType<LoadFromString>();
        
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
        foreach (LoadFromString tile in tiles)
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
}