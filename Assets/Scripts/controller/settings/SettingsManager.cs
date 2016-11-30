using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.settings;

namespace Assets.Scripts.controller.settings
{
    public class SettingsManager : AnstractSettingsManager
    {
        protected override void Prepare()
        {
            InitSetting(SettingName.Turn, typeof (byte), CellState.black);
            InitSetting(SettingName.Cells, typeof (Cell[,]), null);
            InitSetting(SettingName.SoundVolume, typeof (int), 10);
            InitSetting(SettingName.MusicVolume, typeof (int), 10);
        }
    }
}