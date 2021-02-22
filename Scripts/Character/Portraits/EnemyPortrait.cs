using UnityEngine;

namespace PV3.Character.Portraits
{
    public class EnemyPortrait : CharacterPortrait
    {
        private DetermineCurrentEnemyInStage currentEnemyReference;

        protected override void Start()
        {
            currentEnemyReference = GetComponentInParent<DetermineCurrentEnemyInStage>();
            base.Start();
        }

        // Since there are 3 - 4 Enemies per stage, the Character Portrait needs to be updated after every death, hence this method.
        public override void PopulatePortraitUIComponents()
        {
            Character = currentEnemyReference.FindCurrentEnemy();
            Character.Level.Value = currentEnemyReference.ListOfStagesObject.listOfStages[currentEnemyReference.StageListIndex.Value].Stage.listOfEnemies[currentEnemyReference.CurrentEnemyIndex.Value].level;

            base.PopulatePortraitUIComponents();
            InitializeCharacterValues();
        }

        public void ShowPortrait()
        {
            StartCoroutine(AnimateShowPortrait());
        }

        private System.Collections.IEnumerator AnimateShowPortrait()
        {
            yield return new WaitForSeconds(0.35f);
            portraitAnimator.SetTrigger("End");
            yield return new WaitForSeconds(TRANSITION_TIME);
        }

        public override void CheckIfCharacterIsDeadFromLinger()
        {
            if (Character.CurrentHealth.Value <= 0)
            {
                HidePortrait();
            }
            else
            {
                /*GameManager.Instance.CurrentTurnObject.SwitchToPlayerTurn();*/
            }
        }
    }
}