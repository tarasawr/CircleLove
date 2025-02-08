using UnityEngine;

public interface IConfigService
{
    T GetConfig<T>() where T : ScriptableObject;
}