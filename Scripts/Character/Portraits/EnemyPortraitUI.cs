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

using UnityEngine;

namespace PV3.Character.Portraits
{
    [RequireComponent(typeof(DetermineCurrentEnemyInStage))]
    public class EnemyPortraitUI : CharacterPortraitUI
    {
        private DetermineCurrentEnemyInStage currentEnemyInStage;

        protected override void Start()
        {
            currentEnemyInStage = GetComponent<DetermineCurrentEnemyInStage>();
            base.Start();
        }

        public override void PopulateUIComponents()
        {
            Character = currentEnemyInStage.FindCurrentEnemy();
            Character.Level.Value = currentEnemyInStage.ListOfStagesObject.listOfStages[currentEnemyInStage.StageListIndex.Value].Stage
                .listOfEnemies[currentEnemyInStage.CurrentEnemyIndex.Value].level;

            Icon.sprite = Character.portraitSprite;
            NameText.text = Character.name;
            LevelText.text = Character.Level.Value.ToString();

            InitializeCharacterValues();
        }
    }
}