using System.IO;
using UnityEngine;

namespace PV3.Serialization
{
    public static class DataManager
    {
        private static GameSaveData gameSaveData;

        private const string DATA_FILE = "data.json";
        private static readonly string DATA_PATH = $"{Application.persistentDataPath}/{DATA_FILE}";

        public static GameSaveData LoadDataFromJson()
        {
            if (File.Exists(DATA_PATH))
            {
                var content = File.ReadAllText(DATA_PATH);
                gameSaveData = JsonUtility.FromJson<GameSaveData>(content);
            }
            else
            {
                gameSaveData = new GameSaveData();
                SaveDataToJson();
            }

            return gameSaveData;
        }

        public static void SaveDataToJson()
        {
            var content = JsonUtility.ToJson(gameSaveData, true);
            File.WriteAllText(DATA_PATH, content);
        }

        public static void UpdatePlayerBaseData(BaseData baseData)
        {
            gameSaveData.PlayerData.BaseData = baseData;
        }

        public static void UpdatePlayerAttributeData(AttributeData attributeData)
        {
            gameSaveData.PlayerData.AttributeData = attributeData;
        }

        public static void UpdatePlayerSpellData(SpellData spellData, int index)
        {
            gameSaveData.PlayerData.SpellData[index] = spellData;
        }

        public static void UpdateAudioData(AudioData audioData)
        {
            gameSaveData.AudioData = audioData;
        }
    }
}