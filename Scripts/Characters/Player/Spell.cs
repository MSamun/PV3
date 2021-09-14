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

using PV3.Game;
using PV3.Miscellaneous;
using PV3.ScriptableObjects.Characters;
using PV3.ScriptableObjects.Game;
using PV3.UI.Tooltip;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.Characters.Player
{
    [RequireComponent(typeof(SpellTooltipTrigger))]
    public class Spell : MonobehaviourReference
    {
        [SerializeField] private int assignedSpellIndex;
        [SerializeField] private IntValue SpellUsedIndex;
        [SerializeField] private PlayerObject Player;

        [Header("UI Components")]
        [SerializeField] private Button spellButton;

        [SerializeField] private Image cooldownPanel;
        [SerializeField] private TextMeshProUGUI cooldownPanelText;
        [SerializeField] private TextMeshProUGUI spellNameText;
        [SerializeField] private TextMeshProUGUI spellCostText;

        [Header("Game Events")]
        [SerializeField] private GameEventObject OnSpellUseEvent;

        public void InitializeSpellButton()
        {
            if (!Player.SpellsListObject.SpellsList[assignedSpellIndex].Spell) return;

            spellButton.onClick.RemoveAllListeners();
            spellButton.onClick.AddListener(UseSpell);
            spellButton.GetComponent<Image>().sprite = Player.SpellsListObject.SpellsList[assignedSpellIndex].Spell.sprite;

            GetComponent<SpellTooltipTrigger>().Spell = Player.SpellsListObject.SpellsList[assignedSpellIndex].Spell;

            spellNameText.text = Player.SpellsListObject.SpellsList[assignedSpellIndex].Spell.name;
            spellCostText.text = Player.SpellsListObject.SpellsList[assignedSpellIndex].Spell.staminaCost.ToString();
        }

        private void UseSpell()
        {
            SpellUsedIndex.Value = assignedSpellIndex;

            Player.SpellsListObject.SetSpellOnCooldown(assignedSpellIndex);
            DisplayCooldownPanel();

            OnSpellUseEvent.Raise();
        }

        private void DisplayCooldownPanel()
        {
            spellButton.interactable = false;
            cooldownPanel.gameObject.SetActive(true);
            UpdateCooldownPanelDisplay();
        }

        private void UpdateCooldownPanelDisplay()
        {
            cooldownPanel.fillAmount = Mathf.Abs(Player.SpellsListObject.SpellsList[assignedSpellIndex].CooldownTimer / (float) Player.SpellsListObject.SpellsList[assignedSpellIndex].Spell.totalCooldown);
            cooldownPanelText.text = $"{Player.SpellsListObject.SpellsList[assignedSpellIndex].CooldownTimer.ToString()}";
        }

        public void DecrementCooldownTimer()
        {
            if (!Player.SpellsListObject.IsSpellOnCooldown(assignedSpellIndex) && GameStateManager.CurrentGameState != GameState.PlayerTurn) return;

            Player.SpellsListObject.DecrementSpellCooldownTimer(assignedSpellIndex);
            UpdateCooldownPanelDisplay();

            if (Player.SpellsListObject.IsSpellOnCooldown(assignedSpellIndex)) return;
            cooldownPanel.gameObject.SetActive(false);
        }

        public void EnableSpell()
        {
            if (!Player.SpellsListObject.IsSpellOnCooldown(assignedSpellIndex) &&
                !spellButton.interactable && Player.CurrentStamina.Value >= Player.SpellsListObject.SpellsList[assignedSpellIndex].Spell.staminaCost)
                spellButton.interactable = true;
        }

        public void DisableSpell()
        {
            if (spellButton.interactable)
                spellButton.interactable = false;
        }
    }
}