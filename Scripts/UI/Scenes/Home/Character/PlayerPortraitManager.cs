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

using PV3.Miscellaneous;
using PV3.ScriptableObjects.Characters;
using PV3.ScriptableObjects.Game;
using PV3.ScriptableObjects.Home;
using PV3.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Scenes.Home.Character
{
    public class PlayerPortraitManager : MonobehaviourReference
    {
        [SerializeField] private PlayerObject Player;
        [SerializeField] private IntValue PortraitIconIndex;
        [SerializeField] private PortraitIconSpritesObject PortraitIconsObject;

        [Header("UI")]
        [SerializeField] private GameObject GenderDropdownObject;

        [SerializeField] private Image PlayerPortraitImageComponent;
        [SerializeField] private GameObject PlayerNameInputObject;
        [SerializeField] private GameObject MalePortraitContainer;
        [SerializeField] private GameObject FemalePortraitContainer;

        private TMP_Dropdown genderDropdown;
        private TMP_InputField playerNameInput;
        private string tempPlayerName;

        private void Awake()
        {
            genderDropdown = GenderDropdownObject.GetComponent<TMP_Dropdown>();
            playerNameInput = PlayerNameInputObject.GetComponent<TMP_InputField>();

            tempPlayerName = DataManager.LoadPlayerDataFromJson().BaseData.Name;

            PlayerPortraitImageComponent.sprite = Player.PortraitSprite;
            playerNameInput.text = tempPlayerName;
        }

        private void OnEnable()
        {
            PortraitIconIndex.Value = DataManager.LoadPlayerDataFromJson().BaseData.PortraitID;
            playerNameInput.text = DataManager.LoadPlayerDataFromJson().BaseData.Name;

            UpdateTemporaryPortraitIcon();
        }

        public void ShowAppropriatePortraitsDependingOnDropdown()
        {
            // Dropdown menu values: 0 means Male, 1 means Female.
            MalePortraitContainer.SetActive(genderDropdown.value == 0);
            FemalePortraitContainer.SetActive(genderDropdown.value == 1);
        }

        public void UpdateTemporaryPortraitIcon()
        {
            PlayerPortraitImageComponent.sprite = PortraitIconsObject.Icons[PortraitIconIndex.Value];
        }

        public void UpdateTemporaryCharacterName()
        {
            if (playerNameInput.text.Length > 0) tempPlayerName = playerNameInput.text;
        }

        public void SetPlayerPortraitIconAndName()
        {
            var baseData = new BaseData(tempPlayerName, PortraitIconIndex.Value, (int) Player.Class, Player.Level.Value);
            DataManager.UpdatePlayerBaseData(baseData);
        }
    }
}