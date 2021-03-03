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