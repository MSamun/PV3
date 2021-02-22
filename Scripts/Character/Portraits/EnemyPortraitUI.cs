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