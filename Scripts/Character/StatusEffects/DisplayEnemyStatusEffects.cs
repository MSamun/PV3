namespace PV3.Character.StatusEffects
{
    public class DisplayEnemyStatusEffects : DisplayStatusEffects
    {
        private DetermineCurrentEnemyInStage currentEnemyReference;

        private void Start()
        {
            currentEnemyReference = GetComponentInParent<DetermineCurrentEnemyInStage>();
            DetermineCurrentEnemy();
        }

        public void DetermineCurrentEnemy()
        {
            CharacterObject = currentEnemyReference.FindCurrentEnemy();
        }
    }
}