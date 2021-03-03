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
    public class EnemyDeathChecker : CharacterDeathChecker
    {
        private DetermineCurrentEnemyInStage currentEnemyInStage;

        protected override void Start()
        {
            currentEnemyInStage = GetComponent<DetermineCurrentEnemyInStage>();
            base.Start();
        }

        private void CheckIfThereIsNewEnemy()
        {
            if (currentEnemyInStage && Character != currentEnemyInStage.FindCurrentEnemy())
            {
                Character = currentEnemyInStage.FindCurrentEnemy();
            }
        }
        public override void CheckIfDead()
        {
            CheckIfThereIsNewEnemy();
            base.CheckIfDead();
        }

        public override void CheckIfDeadFromLinger()
        {
            CheckIfThereIsNewEnemy();
            base.CheckIfDeadFromLinger();
            CurrentTurnObject.SwitchToPlayerTurn();
        }

        public void ShowPortrait()
        {
            StartCoroutine(AnimateShowPortrait());
        }

        private System.Collections.IEnumerator AnimateShowPortrait()
        {
            yield return new WaitForSeconds(0.35f);
            Animator.SetTrigger("End");
            yield return new WaitForSeconds(TRANSITION_TIME);
        }
    }
}