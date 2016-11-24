using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts.sw.core.eventdispatcher;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.sw.core.settings
{
    public abstract class AnstractSettingsManager : ISettingsManager
    {
        [Inject]
        private IEventDispatcher eventDispatcher;

        private readonly Dictionary<string, SettingItem> _settingDictionary = new Dictionary<string, SettingItem>();
        private BinaryFormatter _formatter = new BinaryFormatter();

        protected AnstractSettingsManager()
        {
            Prepare();

            SyncronizeWithPlayerPrefs();
        }

        public virtual void Prepare()
        {
            // for override
        }

        private void SyncronizeWithPlayerPrefs()
        {
            foreach (var pair in _settingDictionary)
            {
                var name = pair.Key;
                var item = pair.Value;

                if (PlayerPrefs.HasKey(name))
                {
                    LoadSettingFromPlayerPrefs(name, item);
                }
                else
                {
                    SaveSettingToPlayerPrefs(name, item);
                }
            }
        }

        private void LoadSettingFromPlayerPrefs(string name, SettingItem item)
        {
            if (item.ValueType == typeof(int))
            {
                item.CurrentValue = PlayerPrefs.GetInt(name);
            }
            else if (item.ValueType == typeof(float))
            {
                item.CurrentValue = PlayerPrefs.GetFloat(name);
            }
            else if (item.ValueType == typeof(string))
            {
                item.CurrentValue = PlayerPrefs.GetString(name);
            }
            else
            {
                var base64 = PlayerPrefs.GetString(name);
                var bytes = Convert.FromBase64String(base64);
                using (var stream = new MemoryStream(bytes))
                {
                    item.CurrentValue = _formatter.Deserialize(stream);
                }
                item.CurrentValue = Convert.ChangeType(item.CurrentValue, item.ValueType);
            }
            Debug.LogFormat("Load setting [{0}] from PlayerPrefs", name);
        }

        private void SaveSettingToPlayerPrefs(string name, SettingItem item)
        {
            if (item.ValueType == typeof(int))
            {
                PlayerPrefs.SetInt(name, (int)item.CurrentValue);
            }
            else if (item.ValueType == typeof(float))
            {
                PlayerPrefs.SetFloat(name, (float)item.CurrentValue);
            }
            else if (item.ValueType == typeof(string))
            {
                PlayerPrefs.SetString(name, (string)item.CurrentValue);
            }
            else
            {
                using (var stream = new MemoryStream())
                {
                    _formatter.Serialize(stream, item.CurrentValue);
                    var bytes = stream.ToArray();
                    PlayerPrefs.SetString(name, Convert.ToBase64String(bytes));
                }
            }
            PlayerPrefs.Save();
            Debug.LogFormat("Save setting [{0}] to PlayerPrefs", name);
        }

        protected void InitSetting(string name, Type valueType, object defaultValue)
        {
            _settingDictionary.Add(name, new SettingItem()
            {
                ValueType = valueType,
                DefaultValue = defaultValue,
                CurrentValue = defaultValue
            });
            Debug.LogFormat("Setting {0} inited. Type: {1}", name, valueType);
        }

        public object GetSetting(string settingName)
        {
            SettingItem item;
            return _settingDictionary.TryGetValue(settingName, out item) ? item.CurrentValue : null;
        }

        public void SetSetting(string name, object settingValue)
        {
            SettingItem item;
            if (_settingDictionary.TryGetValue(name, out item))
            {
                if (item.CurrentValue == settingValue) return;
                item.CurrentValue = settingValue;
                SaveSettingToPlayerPrefs(name, item);
            }
            else
            {
                Debug.LogErrorFormat("Setting [{0}] is not inited", name);
                throw new Exception("Wrong setting type");
            }
        }

        private void ClearSettings()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
