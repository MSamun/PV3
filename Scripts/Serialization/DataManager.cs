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
        private static PlayerSaveData playerSaveData;
        private static SettingsSaveData settingsSaveData;
        private static ProgressionSaveData progressionSaveData;

        private const string PLAYER_DATA_FILE = "playerData.json";
        private const string SETTINGS_DATA_FILE = "settingsData.json";
        private const string PROGRESSION_DATA_FILE = "progressionData.json";

        private static string dataPath;

        public static PlayerSaveData LoadPlayerDataFromJson()
        {
            dataPath = $"{Application.persistentDataPath}/{PLAYER_DATA_FILE}";

            if (File.Exists(dataPath))
            {
                var content = File.ReadAllText(dataPath);
                playerSaveData = JsonUtility.FromJson<PlayerSaveData>(content);
            }
            else
            {
                playerSaveData = new PlayerSaveData();
                SavePlayerDataToJson();
            }

            return playerSaveData;
        }

        public static void UpdatePlayerBaseData(BaseData baseData)
        {
            playerSaveData.BaseData = baseData;
            SavePlayerDataToJson();
        }

        public static void UpdatePlayerAttributeData(AttributeData attributeData)
        {
            playerSaveData.AttributeData = attributeData;
            SavePlayerDataToJson();
        }

        public static void UpdatePlayerSpellData(int spellID, int index)
        {
            playerSaveData.SpellData[index].SpellID = spellID;
            SavePlayerDataToJson();
        }

        private static void SavePlayerDataToJson()
        {
            dataPath = $"{Application.persistentDataPath}/{PLAYER_DATA_FILE}";

            var content = JsonUtility.ToJson(playerSaveData, true);
            File.WriteAllText(dataPath, content);
        }

        public static SettingsSaveData LoadSettingsDataFromJson()
        {
            dataPath = $"{Application.persistentDataPath}/{SETTINGS_DATA_FILE}";

            if (File.Exists(dataPath))
            {
                var content = File.ReadAllText(dataPath);
                settingsSaveData = JsonUtility.FromJson<SettingsSaveData>(content);
            }
            else
            {
                settingsSaveData = new SettingsSaveData();
                SaveSettingsDataToJson();
            }

            return settingsSaveData;
        }

        public static void UpdateSettingsAudioData(AudioData audioData)
        {
            settingsSaveData.AudioData = audioData;
            SaveSettingsDataToJson();
        }

        private static void SaveSettingsDataToJson()
        {
            dataPath = $"{Application.persistentDataPath}/{SETTINGS_DATA_FILE}";
            var content = JsonUtility.ToJson(settingsSaveData, true);
            File.WriteAllText(dataPath, content);
        }

        public static ProgressionSaveData LoadProgressionDataFromJson()
        {
            dataPath = $"{Application.persistentDataPath}/{PROGRESSION_DATA_FILE}";

            if (File.Exists(dataPath))
            {
                var content = File.ReadAllText(dataPath);
                progressionSaveData = JsonUtility.FromJson<ProgressionSaveData>(content);
            }
            else
            {
                progressionSaveData = new ProgressionSaveData();
                SaveProgressionDataToJson();
            }

            return progressionSaveData;
        }

        public static void UpdateProgressionStageData(StageData stageData)
        {
            progressionSaveData.StageData = stageData;
            SaveProgressionDataToJson();
        }

        private static void SaveProgressionDataToJson()
        {
            dataPath = $"{Application.persistentDataPath}/{PROGRESSION_DATA_FILE}";
            var content = JsonUtility.ToJson(progressionSaveData, true);
            File.WriteAllText(dataPath, content);
        }
    }
}