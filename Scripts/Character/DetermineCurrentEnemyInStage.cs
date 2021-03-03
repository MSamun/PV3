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
using PV3.ScriptableObjects.Stages;
using PV3.ScriptableObjects.Variables;
using UnityEngine;

namespace PV3.Character
{
    public class DetermineCurrentEnemyInStage : MonobehaviourReference
    {
        [SerializeField] private StageListObject listOfStagesObject;
        public StageListObject ListOfStagesObject => listOfStagesObject;

        [SerializeField] private IntValue stageListIndex;
        public IntValue StageListIndex => stageListIndex;

        [SerializeField] private IntValue currentEnemyIndex;
        public IntValue CurrentEnemyIndex => currentEnemyIndex;

        public CharacterObject FindCurrentEnemy()
        {
            return listOfStagesObject.listOfStages[stageListIndex.Value].Stage.listOfEnemies[currentEnemyIndex.Value].enemy;
        }
    }
}