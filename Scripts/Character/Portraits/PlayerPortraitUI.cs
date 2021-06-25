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

using PV3.ScriptableObjects.Character;
using PV3.Serialization;

namespace PV3.Character.Portraits
{
    public class PlayerPortraitUI : CharacterPortraitUI
    {
        public override void PopulateUIComponents()
        {
            base.PopulateUIComponents();
            PopulatePlayerAttributesObjectFromJson();
        }

        private void PopulatePlayerAttributesObjectFromJson()
        {
            var data = DataManager.LoadPlayerDataFromJson().AttributeData;

            Character.Class = (CombatClass) DataManager.LoadPlayerDataFromJson().BaseData.CombatClassID;
            Character.Attributes.Strength = data.Strength;
            Character.Attributes.Dexterity = data.Dexterity;
            Character.Attributes.Constitution = data.Constitution;
            Character.Attributes.Intelligence = data.Intelligence;
            Character.Attributes.Armor = data.Armor;
        }
    }
}