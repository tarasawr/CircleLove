using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class AddressableService : IAddressableService, IInitializable
{
    private readonly List<string> _prefabAddresses = new ()
    {
        "Assets/Prefab/RectangleEnemy.prefab",
        "Assets/Prefab/CapsuleEnemy.prefab"
    };

    private readonly Dictionary<string, AsyncOperationHandle> _loadedAssets = new ();

    public void Initialize()
    {
        DownloadDependenciesAsync().Forget();
    }

    public UniTask DownloadDependenciesAsync()
    {
        var tasks = _prefabAddresses.Select(async address =>
        {
            var handle = Addressables.DownloadDependenciesAsync(address);
            _loadedAssets[address] = handle;
            await handle.Task.AsUniTask();
        });
        return UniTask.WhenAll(tasks);
    }

    public async UniTask<T> LoadAssetAsync<T>(string address) where T : Object
    {
        var handle = Addressables.LoadAssetAsync<T>(address);
        await handle.Task.AsUniTask();
        return handle.Result;
    }

    public void ReleaseAsset<T>(T asset) where T : Object
    {
        Addressables.Release(asset);
    }

    public string GetRandomPrefabAddress()
    {
        return _prefabAddresses[Random.Range(0, _prefabAddresses.Count)];
    }
}