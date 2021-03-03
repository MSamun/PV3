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

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PV3.Character;
using PV3.Miscellaneous;
using PV3.ScriptableObjects.UI;
using PV3.ScriptableObjects.Variables;
using PV3.Serialization;

namespace PV3.UI.Home
{
    public class PlayerPortraitManager : MonobehaviourReference
    {
        private TMP_Dropdown genderDropdown;
        private TMP_InputField playerNameInput;
        private string tempPlayerName;

        [SerializeField] private PlayerObject Player;
        [SerializeField] private IntValue PortraitIconIndex;
        [SerializeField] private PortraitIconSpritesObject PortraitIconsObject;

        [Header("UI")]
        [SerializeField] private GameObject GenderDropdownObject;
        [SerializeField] private Image PlayerPortraitImageComponent;
        [SerializeField] private GameObject PlayerNameInputObject;
        [SerializeField] private GameObject MalePortraitContainer;
        [SerializeField] private GameObject FemalePortraitContainer;
        private void Awake()
        {
            genderDropdown = GenderDropdownObject.GetComponent<TMP_Dropdown>();
            playerNameInput = PlayerNameInputObject.GetComponent<TMP_InputField>();

            PlayerPortraitImageComponent.sprite = Player.portraitSprite;
            playerNameInput.text = Player.name;
        }

        public void ShowAppropriatePortraitsDependingOnDropdown()
        {
            if (genderDropdown.value == 0)
            {
                MalePortraitContainer.SetActive(true);
                FemalePortraitContainer.SetActive(false);
            }
            else
            {
                FemalePortraitContainer.SetActive(true);
                MalePortraitContainer.SetActive(false);
            }
        }

        public void UpdateTemporaryPortraitIcon()
        {
            PlayerPortraitImageComponent.sprite = PortraitIconsObject.Icons[PortraitIconIndex.Value];
        }

        public void UpdateTemporaryCharacterName()
        {
            if (playerNameInput.text.Length > 0)
            {
                tempPlayerName = playerNameInput.text;
            }
        }

        public void SetPlayerPortraitIconAndName()
        {
            var baseData = new BaseData(tempPlayerName, PortraitIconIndex.Value, (int)Player.Class, Player.Level.Value);
            DataManager.UpdatePlayerBaseData(baseData);
            DataManager.SaveDataToJson();
        }

        private void OnEnable()
        {
            PortraitIconIndex.Value = DataManager.LoadDataFromJson().PlayerData.BaseData.PortraitID;
            UpdateTemporaryPortraitIcon();
            playerNameInput.text = DataManager.LoadDataFromJson().PlayerData.BaseData.Name;
        }

        private void OnDisable()
        {
            var attributeData = new AttributeData(Player.Attributes.Strength, Player.Attributes.Dexterity, Player.Attributes.Constitution,
                Player.Attributes.Intelligence, Player.Attributes.Armor);
            DataManager.UpdatePlayerAttributeData(attributeData);

            for (var i = 0; i < Player.ListOfSpells.Count; i++)
            {
                var spellData = new SpellData(Player.ListOfSpells[i].spell.spellID);
                DataManager.UpdatePlayerSpellData(spellData, i);
            }

            DataManager.SaveDataToJson();
        }
    }
}