namespace PV3.Character.StatusEffects
{
    [UnityEngine.RequireComponent(typeof(DetermineCurrentEnemyInStage))]
    public class ApplyEnemyUniqueEffects : ApplyCharacterUniqueEffects
    {
        private DetermineCurrentEnemyInStage currentEnemyReference;

        private void Start()
        {
            currentEnemyReference = GetComponent<DetermineCurrentEnemyInStage>();
            DetermineCurrentEnemy();
        }

        private void DetermineCurrentEnemy()
        {
            Character = currentEnemyReference.FindCurrentEnemy();
        }
    }
}