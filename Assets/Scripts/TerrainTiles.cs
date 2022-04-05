using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
//using UnityEngine.SocialPlatforms.GameCenter;

public class TerrainTiles : MonoBehaviour
{
 
    [Serializable]
    public class LODInfo
    {
        public AssetReference assetReference;
    }

    private int currentLOD = -1;
    //public AssetReference assetReference;
    public List<LODInfo> LODs;

    private AsyncOperationHandle<GameObject> _operationHandle;
    private GameObject _tileAsset;

    /*private void Awake()
    {
        enabled = false;
    }*/

    public void UpdateLOD(int lod)
    {
        if (lod != currentLOD)
        {
            currentLOD = lod;
            LoadLOD(lod);
        }
    }

    public async Task LoadLOD(int lod)
    {
        if (_tileAsset != null)
        {
            Addressables.ReleaseInstance(_operationHandle);
            Addressables.ReleaseInstance(_tileAsset);
        }
        if (lod >= 0)
        {
            LODInfo newLod = LODs[Math.Min(lod, LODs.Count - 1)];
            _operationHandle =
                newLod.assetReference.InstantiateAsync(transform.position, transform.rotation, transform);
            await _operationHandle.Task;
            _tileAsset = _operationHandle.Result;
        }
    }


  
    /*public async Task InstantiateTileAsset()
    {
        enabled = true;
        if (_tileAsset != null) return;
        
        _operationHandle = assetReference.InstantiateAsync(transform.position, transform.rotation, transform);

        await _operationHandle.Task;

        _tileAsset = _operationHandle.Result;
    }

    public void ReleaseTileAsset()
    {
        enabled = false;
        if (_tileAsset == null) return;
        
        Addressables.ReleaseInstance(_operationHandle);
        Addressables.ReleaseInstance(_tileAsset);
    }*/
}