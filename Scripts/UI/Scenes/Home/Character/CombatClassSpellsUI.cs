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
using PV3.ScriptableObjects.Spells;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Scenes.Home.Character
{
    public class CombatClassSpellsUI : MonobehaviourReference
    {
        [SerializeField] private PlayerObject Player;
        [SerializeField] private IntValue SpellChosenID;
        [SerializeField] private ListOfSpellsObject ListOfSpellsInGame;
        [SerializeField] private CombatClass DisplayType;

        [Header("UI")] [SerializeField] private GameObject SpellSlotsContainer;
        [SerializeField] private Button EquipButton;

        private CombatClassSlotBehaviour[] _spellSlots;

        private void Awake()
        {
            _spellSlots = SpellSlotsContainer.GetComponentsInChildren<CombatClassSlotBehaviour>(true);
            if (_spellSlots.Length <= 0) return;

            for (var i = 0; i < _spellSlots.Length; i++)
            {
                SpellObject spell = ListOfSpellsInGame.FindSpellAtIndex(i, DisplayType);
                _spellSlots[i].Initialize(spell, HasPlayerEquippedSpell(spell));
            }
        }

        private void OnEnable()
        {
            ToggleEquipButton(false);
            SpellChosenID.Value = -1;

            UpdateAllSpellSlotsUI();
        }

        public void UpdateAllSpellSlotsUI()
        {
            for (var i = 0; i < _spellSlots.Length; i++)
            {
                if (!_spellSlots[i].Spell) continue;

                _spellSlots[i].ToggleFocusBorder();
                _spellSlots[i].ToggleEquippedIcon(HasPlayerEquippedSpell(_spellSlots[i].Spell));
            }
        }

        public void CheckIfNeedToDisableEquipButton(int index)
        {
            bool isSpellAlreadyEquipped = HasPlayerEquippedSpell(_spellSlots[index].Spell);
            bool doesSpellMatchPlayerCombatClass = Player.Class == _spellSlots[index].Spell.combatClass;

            ToggleEquipButton(!isSpellAlreadyEquipped && doesSpellMatchPlayerCombatClass);
            UpdateAllSpellSlotsUI();
        }

        private bool HasPlayerEquippedSpell(SpellObject spell)
        {
            for (var i = 0; i < Player.SpellsListObject.SpellsList.Count; i++)
            {
                if (Player.SpellsListObject.SpellsList[i].Spell == spell) return true;
            }

            return false;
        }

        private void ToggleEquipButton(bool shouldEnable)
        {
            if (EquipButton) EquipButton.interactable = shouldEnable;
        }


    }
}