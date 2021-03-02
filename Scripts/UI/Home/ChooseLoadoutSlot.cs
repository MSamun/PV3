using System;
using PV3.Character;
using PV3.Miscellaneous;
using PV3.ScriptableObjects.Game;
using PV3.ScriptableObjects.Variables;
using PV3.UI.Tooltip.Spell;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Home
{
    public class ChooseLoadoutSlot : MonobehaviourReference
    {
        private SpellObject spell;

        [SerializeField] private PlayerObject Player;
        [SerializeField] private ListOfSpellsObject ListOfSpells;
        [SerializeField] private IntValue LoadoutIndexToPutNewSpell;
        [SerializeField] private IntValue SpellChosenIndex;

        [Header("UI")]
        [SerializeField] private GameObject SpellChosenSlotObject;

        public void InitializeSpellChosen()
        {
            spell = ListOfSpells.FindSpellByID(SpellChosenIndex.Value, Player.Class);
            SpellChosenSlotObject.GetComponentInChildren<Button>(true).GetComponent<Image>().sprite = spell.sprite;
            SpellChosenSlotObject.GetComponent<SpellTooltipTrigger>().Spell = spell;
            SpellChosenSlotObject.GetComponent<SpellTooltipTrigger>().Character = Player;
        }

        public void PutNewSpellInLoadoutSlot()
        {
            Player.ListOfSpells[LoadoutIndexToPutNewSpell.Value].spell = spell;
        }
    }
}