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
using PV3.ScriptableObjects.Game;
using PV3.ScriptableObjects.Stages;
using UnityEngine;

namespace PV3.Game
{
    public class MoveToNextEnemyInStage : MonobehaviourReference
    {
        [Header("Stage Information")]
        [SerializeField] private IntValue CurrentEnemyIndex;

        [SerializeField] private StageListObject ListOfStagesObject;
        [SerializeField] private IntValue StageListIndex;

        [Header("Game Events")]
        [SerializeField] private GameEventObject OnNoMoreEnemiesEvent;
        [SerializeField] private GameEventObject OnInitializeNewEnemyEvent;
        [SerializeField] private GameEventObject OnStartBossEncounterEvent;

        private int numberOfDeaths;

        private void Awake()
        {
            CurrentEnemyIndex.Value = 0;
            numberOfDeaths = 0;
        }

        // Current Enemy Index gets set to 0 at runtime. Whenever an Enemy dies, the Current Enemy Index value gets incremented.
        // All other components and scripts that rely on the Current Enemy Index get updated. This includes:

        //      - [Player/Enemy]SpellBehaviour.cs   -> Determines Player's Target and Enemy's Caster; checks for current Status Effects and Bonuses.
        //      - EnemyStanceLogic.cs               -> Determines Enemy's Current Stance and how it should use its Spells.
        //      - EnemySpells.cs                    -> Decrements Enemy's Spells, uses Enemy Spell, and sets Enemy Spells on cooldown.
        //      - EnemyDeathCheck.cs                -> Keeps track of Enemy's Current Health.
        //      - EnemyStaminaCheck.cs              -> Keeps track of Enemy's Current Stamina.
        //      - EnemyPortraitUI.cs                -> Updates Enemy values and its corresponding UI components (Icon, Level, Name, Health/Stamina Bars, Attributes).
        //      - DisplayEnemyStatusEffects.cs      -> Keeps track of current Status Effects on Enemy.
        public void MoveOnToNextEnemy()
        {
            numberOfDeaths++;

            if (numberOfDeaths >= ListOfStagesObject.listOfStages[StageListIndex.Value].Stage.listOfEnemies.Count)
            {
                OnNoMoreEnemiesEvent.Raise();
            }
            else
            {
                CurrentEnemyIndex.Value++;

                if (ListOfStagesObject.listOfStages[StageListIndex.Value].Stage.hasBoss &&
                    numberOfDeaths == ListOfStagesObject.listOfStages[StageListIndex.Value].Stage.listOfEnemies.Count - 1)
                {
                    OnStartBossEncounterEvent.Raise();
                    return;
                }

                OnInitializeNewEnemyEvent.Raise();
            }
        }
    }
}