using PV3.Character;
using PV3.Miscellaneous;
using PV3.ScriptableObjects.Game;
using PV3.ScriptableObjects.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Home
{
    public class DisplayCombatClassSpells : MonobehaviourReference
    {
        private ChooseSpellForLoadout[] amountOfSpells;

        [SerializeField] private PlayerObject Player;
        [SerializeField] private IntValue SpellChosenIndex;
        [SerializeField] private ListOfSpellsObject ListOfSpellsObject;
        [SerializeField] private CombatClass spellType;

        [Header("UI Panel")]
        [SerializeField] private GameObject contentPanel;
        [SerializeField] private Button equipButton;

        private void Awake()
        {
            amountOfSpells = contentPanel.GetComponentsInChildren<ChooseSpellForLoadout>();

            for (var i = 0; i < amountOfSpells.Length; i++)
            {
                // Since Health Potion Spell is in the list of Spells, need to skip it. Will change it in the future.
                amountOfSpells[i].Initialize(ListOfSpellsObject.FindSpellAtIndex(i + 1, spellType));
                amountOfSpells[i].DisplayEquippedIcon(CheckIfSpellIsEquipped(amountOfSpells[i].Spell));
            }
        }

        public void UpdateSpellFocusAndEquippedIcons()
        {
            equipButton.interactable = false;

            for (var i = 0; i < amountOfSpells.Length; i++)
            {
                SpellChosenIndex.Value = -1;
                amountOfSpells[i].ResetSpellFocus();
                amountOfSpells[i].DisplayEquippedIcon(CheckIfSpellIsEquipped(amountOfSpells[i].Spell));
            }
        }

        private bool CheckIfSpellIsEquipped(SpellObject spell)
        {
            var foundSpell = false;

            for (var i = 0; i < Player.ListOfSpells.Count; i++)
            {
                if (spell != Player.ListOfSpells[i].spell) continue;
                foundSpell = true;
            }

            return foundSpell;
        }

        public void SetSpellToEquip(int index)
        {
            var isAlreadyEquipped = CheckIfSpellIsEquipped(amountOfSpells[index].Spell);
            equipButton.interactable = !isAlreadyEquipped;

            if (isAlreadyEquipped)
            {
                UpdateSpellFocusAndEquippedIcons();
                return;
            }
            amountOfSpells[index].SetSpellToEquip();
        }

        private void OnEnable()
        {
            UpdateSpellFocusAndEquippedIcons();
        }
    }
}