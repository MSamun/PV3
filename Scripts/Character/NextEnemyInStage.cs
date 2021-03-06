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
using PV3.ScriptableObjects.GameEvents;
using PV3.ScriptableObjects.Stages;
using PV3.ScriptableObjects.Variables;
using UnityEngine;

namespace PV3.Character
{
    public class NextEnemyInStage : MonobehaviourReference
    {
        private int numberOfDeathsInStage;
        private bool isFinishedInitializingNewEnemy;

        [Header("Stage Information")]
        [SerializeField] private StageListObject ListOfStagesObject;
        [SerializeField] private IntValue StageListIndex;
        [SerializeField] private IntValue CurrentEnemyIndex;

        [Header("Game Events")]
        [SerializeField] private GameEventObject OnStageWinEvent;
        [SerializeField] private GameEventObject OnInitializeNewEnemyEvent;
        [SerializeField] private GameEventObject OnEnemyPortraitShowEvent;
        [SerializeField] private GameEventObject OnEnemySpellDoneEvent;

        // At the start of the Stage, CurrentEnemyIndex should be reset to 0. After resetting this value, all other components of the game that rely on the value of CurrentEnemyIndex
        // should then be updated. This includes:
        //      - [Player/Enemy]SpellUse.cs (to determine the current Enemy's Status Effects and Bonuses)
        //      - EnemySpellLogic.cs (to determine the current Enemy's Spells)
        //      - EnemyPortrait.cs (to determine the current Enemy's Health, Level, and Attributes)
        //      - CurrentStatusEffectObject (to reset all Status Effects that may have been on previous enemy).
        //      - DisplayEnemyStatusEffects.cs (to hide all Status Effects that may have been on previous enemy).

        // This process should then be repeated after every Enemy death, until either the Player completes or loses the Stage.
        private void Awake()
        {
            CurrentEnemyIndex.Value = 0;
            numberOfDeathsInStage = 0;
        }

        public void MoveOnToNextEnemy()
        {
            StartCoroutine(InitializeNewEnemy());
        }

        private System.Collections.IEnumerator InitializeNewEnemy()
        {
            isFinishedInitializingNewEnemy = false;
            numberOfDeathsInStage++;

            if (numberOfDeathsInStage >= ListOfStagesObject.listOfStages[StageListIndex.Value].Stage.listOfEnemies.Count)
            {
                OnStageWinEvent.Raise();
            }
            else
            {
                CurrentEnemyIndex.Value++;
                OnInitializeNewEnemyEvent.Raise();

                yield return new WaitUntil(() => isFinishedInitializingNewEnemy);

                OnEnemyPortraitShowEvent.Raise();

                yield return new WaitForSeconds(1.25f);

                OnEnemySpellDoneEvent.Raise();
            }
            yield return null;
        }

        public void FinishedInitializingNewEnemy()
        {
            isFinishedInitializingNewEnemy = true;
        }
    }
}