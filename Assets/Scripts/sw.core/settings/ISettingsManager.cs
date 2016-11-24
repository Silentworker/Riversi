namespace Assets.Scripts.sw.core.settings
{
    public interface ISettingsManager
    {
        object GetSetting(string settingName);

        void SetSetting(string name, object settingValue);
    }
}
