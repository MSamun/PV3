using PV3.Character;
using PV3.UI.Tooltip.Spell;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Home
{
    public class DisplayCurrentSpellLoadout : MonobehaviourReference
    {
        [SerializeField] private PlayerObject Player;
        [SerializeField] private GameObject[] SpellObjects;

        private void Awake()
        {
            UpdateSpellObjectUIComponents();
        }

        public void UpdateSpellObjectUIComponents()
        {
            for (var i = 0; i < SpellObjects.Length; i++)
            {
                SpellObjects[i].GetComponentInChildren<Button>(true).GetComponent<Image>().sprite = Player.ListOfSpells[i + 1].spell.sprite;
                SpellObjects[i].GetComponent<SpellTooltipTrigger>().Spell = Player.ListOfSpells[i + 1].spell;
            }
        }
    }
}