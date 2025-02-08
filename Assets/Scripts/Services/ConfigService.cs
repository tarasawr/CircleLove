using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public class ConfigService : IConfigService
    {
        private Dictionary<Type, ScriptableObject> _configDictionary = new();

        private const string ConfigsPath = "Configs";

        public T GetConfig<T>() where T : ScriptableObject
        {
            var type = typeof(T);

            if (_configDictionary.TryGetValue(type, out ScriptableObject config))
            {
                return config as T;
            }

            T loadedConfig = Resources.Load<T>($"{ConfigsPath}/{type.Name}");
            if (loadedConfig != null)
            {
                _configDictionary.Add(type, loadedConfig);
                return loadedConfig;
            }

            T[] loadedConfigs = Resources.LoadAll<T>(ConfigsPath);
            if (loadedConfigs != null && loadedConfigs.Length > 0)
            {
                loadedConfig = loadedConfigs[0];
                _configDictionary.Add(type, loadedConfig);
                return loadedConfig;
            }

            throw new Exception($"Not found {type} Resources/{ConfigsPath}");
        }
    }
}