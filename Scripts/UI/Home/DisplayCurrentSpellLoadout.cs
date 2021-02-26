using PV3.Character;
using PV3.Miscellaneous;
using PV3.UI.Tooltip.Spell;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Home
{
    public class DisplayCurrentSpellLoadout : MonobehaviourReference
    {
        [SerializeField] private PlayerObject PlayerObject;

        [Header("")]
        [SerializeField] private GameObject[] SpellObjects;

        private void Awake()
        {
            for (var i = 0; i < SpellObjects.Length; i++)
            {
                SpellObjects[i].GetComponent<SpellTooltipTrigger>().Character = PlayerObject;
            }

            InitializeSpellObjects();
        }

        public void InitializeSpellObjects()
        {
            for (var i = 0; i < SpellObjects.Length; i++)
            {
                SpellObjects[i].GetComponent<SpellTooltipTrigger>().Spell = PlayerObject.ListOfSpells[i + 1].spell;
                SpellObjects[i].GetComponentInChildren<Button>(true).GetComponent<Image>().sprite = PlayerObject.ListOfSpells[i + 1].spell.sprite;
            }
        }
    }
}