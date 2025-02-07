using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Pool;

namespace Enemy
{
    public class AddressablesPool<T> where T : Component
    {
        private readonly IObjectPool<T> _pool;
        private readonly Transform _poolParent;
        private readonly IAddressableService _addressableService;

        public AddressablesPool(int initialSize, int maxSize, IAddressableService addressableService)
        {
            _addressableService = addressableService;
            _poolParent = new GameObject($"{typeof(T).Name}Pool").transform;
            _pool = new ObjectPool<T>(Create, OnGet, OnRelease, OnDestroy,true, initialSize, maxSize);
        }

        public T Get() => _pool.Get();

        public void Release(T instance) => _pool.Release(instance);

        private T Create()
        {
            string prefabAddress = _addressableService.GetRandomPrefabAddress();
            var op = Addressables.InstantiateAsync(prefabAddress, _poolParent);
            op.WaitForCompletion();
            return op.Result.GetComponent<T>();
        }

        private void OnGet(T instance)
        {
            instance.gameObject.SetActive(true);
        }

        private void OnRelease(T instance)
        {
            instance.gameObject.SetActive(false);
            instance.transform.SetParent(_poolParent);
        }

        private void OnDestroy(T instance)
        {
            Object.Destroy(instance.gameObject);
        }
    }
}