using System.Collections.Generic;
using Assets.Scripts.consts;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.settings;

namespace Assets.Scripts.controller.settings
{
    public class SettingsManager : AnstractSettingsManager
    {
        protected override void Prepare()
        {
            InitSetting(SettingName.Turn, typeof(byte), CellState.black);
            InitSetting(SettingName.Cells, typeof(List<Cell>), null);
            InitSetting(SettingName.AIPlayer, typeof(byte), Side.Dark);
            InitSetting(SettingName.PlayersAmount, typeof(byte), 1);
            InitSetting(SettingName.SoundVolume, typeof(byte), 10);
            InitSetting(SettingName.MusicVolume, typeof(byte), 10);
        }
    }
}