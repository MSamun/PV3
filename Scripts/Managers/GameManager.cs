﻿using PV3.Character;
using PV3.ScriptableObjects.Game;
using PV3.ScriptableObjects.GameEvents;
using PV3.Serialization;
using UnityEngine;

namespace PV3
{
    public class GameManager : MonobehaviourReference
    {
        [Header("Game Objects")]
        [SerializeField] private ListOfSpellsObject ListOfSpellsObject;
        [SerializeField] private PlayerObject PlayerObject;
        [SerializeField] private GameEventObject OnLoadedPlayerSpellsFromJsonEvent;

        [Header("UI Panels")]
        [SerializeField] private GameObject VictoryScreen;
        [SerializeField] private GameObject DefeatScreen;

        private void Awake()
        {
            InitializePlayerSpells();
        }

        private void InitializePlayerSpells()
        {
            var playerSpellJsonData = DataManager.LoadDataFromJson().PlayerData.SpellData;

            for (var i = 0; i < PlayerObject.ListOfSpells.Count; i++)
            {
                PlayerObject.ListOfSpells[i].spell = ListOfSpellsObject.FindSpellByID(playerSpellJsonData[i].SpellID);

                if (!PlayerObject.ListOfSpells[i].spell)
                {
                    print($"Could not find the Spell ID in the JSON file ---> {playerSpellJsonData[i].SpellID.ToString()}");
                }
            }

            print("Executed - Game Manager!");
            OnLoadedPlayerSpellsFromJsonEvent.Raise();
        }

        public void DisplayVictoryScreen()
        {
            VictoryScreen.gameObject.SetActive(true);
        }

        public void DisplayDefeatScreen()
        {
            PauseGame();
            DefeatScreen.gameObject.SetActive(true);
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
        }
    }
}