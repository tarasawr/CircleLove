using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IAddressableService
{
    UniTask DownloadDependenciesAsync();
    UniTask<T> LoadAssetAsync<T>(string address) where T : Object;
    void ReleaseAsset<T>(T asset) where T : Object;
}