using System;
using PV3.Character;
using PV3.Miscellaneous;
using PV3.ScriptableObjects.Game;
using PV3.ScriptableObjects.GameEvents;
using UnityEngine;

namespace PV3.Serialization
{
    public class LoadPlayerSpellsFromJSON : MonobehaviourReference
    {
        [SerializeField] private PlayerObject PlayerObject;
        [SerializeField] private ListOfSpellsObject ListOfSpellsObject;

        [Header("Game Event")]
        [SerializeField] private GameEventObject OnLoadPlayerSpellsFromJsonEvent;

        private void OnEnable()
        {
            InitializePlayerSpells();
        }

        public void InitializePlayerSpells()
        {
            var playerSpellJsonData = DataManager.LoadDataFromJson().PlayerData.SpellData;
            var playerCombatClass = DataManager.LoadDataFromJson().PlayerData.BaseData.CombatClassID;

            for (var i = 0; i < PlayerObject.ListOfSpells.Count; i++)
            {
                PlayerObject.ListOfSpells[i].spell = ListOfSpellsObject.FindSpellByID(playerSpellJsonData[i].SpellID, (CombatClass)playerCombatClass);
            }

            OnLoadPlayerSpellsFromJsonEvent.Raise();
        }
    }
}