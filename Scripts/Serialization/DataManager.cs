// PV3 is a menu-based RPG game.
// This file is part of the PV3 distribution (https://github.com/MSamun/PV3)
// Copyright (C) 2021 Matthew Samun.
//
// This program is free software: you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the Free
// Software Foundation, version 3.
//
// This program is distributed in the hope that it will be useful, but WITHOUT
// ANY WARRANTY; without even the implied warranty of  MERCHANTABILITY or
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
// more details.
//
// You should have received a copy of the GNU General Public License along with
// this program. If not, see <http://www.gnu.org/licenses/>.

using System.IO;
using UnityEngine;

namespace PV3.Serialization
{
    public static class DataManager
    {
        private const string PLAYER_DATA_FILE = "playerData.json";
        private const string SETTINGS_DATA_FILE = "settingsData.json";
        private const string PROGRESSION_DATA_FILE = "progressionData.json";
        private static PlayerSaveData _playerSaveData;
        private static SettingsSaveData _settingsSaveData;
        private static ProgressionSaveData _progressionSaveData;

        private static string _dataPath;

        public static PlayerSaveData LoadPlayerDataFromJson()
        {
            _dataPath = $"{Application.persistentDataPath}/{PLAYER_DATA_FILE}";

            if (File.Exists(_dataPath))
            {
                var content = File.ReadAllText(_dataPath);
                _playerSaveData = JsonUtility.FromJson<PlayerSaveData>(content);
            }
            else
            {
                _playerSaveData = new PlayerSaveData();
                SavePlayerDataToJson();
            }

            return _playerSaveData;
        }

        public static void UpdatePlayerBaseData(BaseData baseData)
        {
            _playerSaveData.BaseData = baseData;
            SavePlayerDataToJson();
        }

        public static void UpdatePlayerAttributeData(AttributeData attributeData)
        {
            _playerSaveData.AttributeData = attributeData;
            SavePlayerDataToJson();
        }

        public static void UpdatePlayerSpellData(int spellID, int index)
        {
            _playerSaveData.SpellData[index].SpellID = spellID;
            SavePlayerDataToJson();
        }

        private static void SavePlayerDataToJson()
        {
            _dataPath = $"{Application.persistentDataPath}/{PLAYER_DATA_FILE}";

            var content = JsonUtility.ToJson(_playerSaveData, true);
            File.WriteAllText(_dataPath, content);
        }

        public static SettingsSaveData LoadSettingsDataFromJson()
        {
            _dataPath = $"{Application.persistentDataPath}/{SETTINGS_DATA_FILE}";

            if (File.Exists(_dataPath))
            {
                var content = File.ReadAllText(_dataPath);
                _settingsSaveData = JsonUtility.FromJson<SettingsSaveData>(content);
            }
            else
            {
                _settingsSaveData = new SettingsSaveData();
                SaveSettingsDataToJson();
            }

            return _settingsSaveData;
        }

        public static void UpdateSettingsAudioData(AudioData audioData)
        {
            _settingsSaveData.AudioData = audioData;
            SaveSettingsDataToJson();
        }

        private static void SaveSettingsDataToJson()
        {
            _dataPath = $"{Application.persistentDataPath}/{SETTINGS_DATA_FILE}";
            var content = JsonUtility.ToJson(_settingsSaveData, true);
            File.WriteAllText(_dataPath, content);
        }

        public static ProgressionSaveData LoadProgressionDataFromJson()
        {
            _dataPath = $"{Application.persistentDataPath}/{PROGRESSION_DATA_FILE}";

            if (File.Exists(_dataPath))
            {
                var content = File.ReadAllText(_dataPath);
                _progressionSaveData = JsonUtility.FromJson<ProgressionSaveData>(content);
            }
            else
            {
                _progressionSaveData = new ProgressionSaveData();
                SaveProgressionDataToJson();
            }

            return _progressionSaveData;
        }

        public static void UpdateProgressionStageData(StageData stageData)
        {
            _progressionSaveData.StageData = stageData;
            SaveProgressionDataToJson();
        }

        private static void SaveProgressionDataToJson()
        {
            _dataPath = $"{Application.persistentDataPath}/{PROGRESSION_DATA_FILE}";
            var content = JsonUtility.ToJson(_progressionSaveData, true);
            File.WriteAllText(_dataPath, content);
        }
    }
}