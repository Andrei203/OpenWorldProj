using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TerrainTiles : MonoBehaviour
{   
    public AssetReference assetReference;

    private AsyncOperationHandle<GameObject> _operationHandle;
    private GameObject _tileAsset;

    private void Awake()
    {
        enabled = false;
    }

    public async Task InstantiateTileAsset()
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
    }
}