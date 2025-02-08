using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class AddressableService : IAddressableService
{
    private readonly Dictionary<string, AsyncOperationHandle> _loadedAssets = new();
    private string[] _prefabAddresses;
    
    [Inject]
    public AddressableService(IConfigService configService)
    {
        _prefabAddresses = configService.GetConfig<EnemyConfig>().PrefabAddresses;
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
}