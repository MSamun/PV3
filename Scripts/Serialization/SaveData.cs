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

namespace PV3.Serialization
{
    [System.Serializable]
    public class GameSaveData
    {
        public PlayerData PlayerData;
        public AudioData AudioData;

        public GameSaveData()
        {
            AudioData = new AudioData();
            PlayerData = new PlayerData {BaseData = new BaseData(), AttributeData = new AttributeData(), SpellData = new SpellData[6]};
        }
    }

    [System.Serializable]
    public class AudioData
    {
        public float BackgroundMusicVolume;
        public float ButtonSfxVolume;

        public AudioData(float backgroundMusicVolume = 1f, float buttonSfxVolume = 1f)
        {
            BackgroundMusicVolume = backgroundMusicVolume;
            ButtonSfxVolume = buttonSfxVolume;
        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public BaseData BaseData;
        public AttributeData AttributeData;
        public SpellData[] SpellData;
    }

    [System.Serializable]
    public class BaseData
    {
        public string Name;
        public int PortraitID;
        public int CombatClassID;
        public int Level;
        public int SkillPoints;
        public int CurrentExp;

        public BaseData(string name = "", int portraitID = 0, int combatClassID = 0, int level = 0, int skillPoints = 0, int currentExp = 0)
        {
            Name = name;
            PortraitID = portraitID;
            CombatClassID = combatClassID;
            Level = level;
            SkillPoints = skillPoints;
            CurrentExp = currentExp;
        }
    }

    [System.Serializable]
    public class AttributeData
    {
        public int Strength;
        public int Dexterity;
        public int Constitution;
        public int Intelligence;
        public int Armor;

        public AttributeData(int strength = 5, int dexterity = 5, int constitution = 5, int intelligence = 5, int armor = 5)
        {
            Strength = strength;
            Dexterity = dexterity;
            Constitution = constitution;
            Intelligence = intelligence;
            Armor = armor;
        }
    }

    [System.Serializable]
    public class SpellData
    {
        public int SpellID;

        public SpellData(int spellID = 0)
        {
            SpellID = spellID;
        }
    }
}