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
using UnityEngine;
using TMPro;

namespace PV3.UI
{
    public class DisplayTurnNotification : MonobehaviourReference
    {
        private TextMeshProUGUI turnNotificationText;
        private const string PLAYER_COLOR_TEXT = "#96FF00";
        private const string ENEMY_COLOR_TEXT = "#E13232";

        [SerializeField] private GameObject TurnNotificationObject;

        private void Awake()
        {
            turnNotificationText = TurnNotificationObject.gameObject.GetComponentInChildren<TextMeshProUGUI>(true);
        }

        public void DisplaySpellUseNotification(string casterName, string spellName, string targetName, bool isCasterPlayer)
        {
            if (!TurnNotificationObject.gameObject.activeInHierarchy) TurnNotificationObject.gameObject.SetActive(true);

            turnNotificationText.text = $"<color={(isCasterPlayer ? PLAYER_COLOR_TEXT : ENEMY_COLOR_TEXT)}>{casterName}</color> uses <color=#FFA000>{spellName}</color> on <color={(isCasterPlayer ? ENEMY_COLOR_TEXT : PLAYER_COLOR_TEXT)}>{targetName}</color>.";
        }

        public void DisplaySpellUseNotification(string casterName, string spellName, bool isCasterPlayer)
        {
            if (!TurnNotificationObject.gameObject.activeInHierarchy) TurnNotificationObject.gameObject.SetActive(true);

            // Different text for Healing Spells.
            turnNotificationText.text = $"<color={(isCasterPlayer ? PLAYER_COLOR_TEXT : ENEMY_COLOR_TEXT)}>{casterName}</color> uses <color=#FFA000>{spellName}</color>.";
        }
    }
}