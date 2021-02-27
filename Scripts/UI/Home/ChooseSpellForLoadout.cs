using PV3.Miscellaneous;
using PV3.ScriptableObjects.Game;
using PV3.ScriptableObjects.Variables;
using PV3.UI.Tooltip.Spell;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Home
{
    public class ChooseSpellForLoadout : MonobehaviourReference
    {
        private int index;
        public SpellObject Spell;
        [SerializeField] private IntValue SpellChosenIndex;

        [Header("UI Components")]
        [SerializeField] private Button Button;
        [SerializeField] private Image SpellFocus;
        [SerializeField] private Image EquippedSpellIcon;

        public void Initialize(SpellObject spell)
        {
            if (!spell)
            {
                gameObject.SetActive(false);
                return;
            }

            Spell = spell;
            index = Spell.spellID;

            Button.GetComponent<Image>().sprite = Spell.sprite;
            GetComponent<SpellTooltipTrigger>().Spell = Spell;

            ResetSpellFocus();
        }

        public void ResetSpellFocus()
        {
            if (SpellFocus.gameObject.activeInHierarchy && SpellChosenIndex.Value != index)
            {
                SpellFocus.gameObject.SetActive(false);
            }

            if (!SpellFocus.gameObject.activeInHierarchy && SpellChosenIndex.Value == index)
            {
                SpellFocus.gameObject.SetActive(true);
            }
        }

        public void DisplayEquippedIcon(bool shouldDisplay)
        {
            EquippedSpellIcon.gameObject.SetActive(shouldDisplay);
        }

        public void SetSpellToEquip()
        {
            SpellChosenIndex.Value = index;
            SpellFocus.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            ResetSpellFocus();
        }
    }
}