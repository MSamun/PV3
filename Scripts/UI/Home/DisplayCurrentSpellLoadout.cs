using PV3.Character;
using PV3.ScriptableObjects.Game;
using PV3.Serialization;
using PV3.UI.Tooltip.Spell;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Home
{
    public class DisplayCurrentSpellLoadout : MonobehaviourReference
    {
        [SerializeField] private PlayerObject PlayerObject;
        [SerializeField] private ListOfSpellsObject ListOfSpellsObject;

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
            var playerSpellJsonData = DataManager.LoadDataFromJson().PlayerData.SpellData;

            for (var i = 0; i < SpellObjects.Length; i++)
            {
                var obj = SetSpellBasedOffJsonID(playerSpellJsonData[i + 1].SpellID);
                if (!obj) continue;

                SpellObjects[i].GetComponent<SpellTooltipTrigger>().Spell = obj;
                SpellObjects[i].GetComponentInChildren<Button>(true).GetComponent<Image>().sprite = obj.sprite;
            }
        }

        private SpellObject SetSpellBasedOffJsonID(int spellID)
        {
            return ListOfSpellsObject.FindSpellByID(spellID, PlayerObject.Class);;
        }
    }
}