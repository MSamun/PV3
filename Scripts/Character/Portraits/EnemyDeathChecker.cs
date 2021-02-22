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