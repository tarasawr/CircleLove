using UnityEngine;

namespace Services
{
    public interface IConfigService
    {
        T GetConfig<T>() where T : ScriptableObject;
    }
}