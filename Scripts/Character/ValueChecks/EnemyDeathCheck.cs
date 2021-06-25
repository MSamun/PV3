using PV3.Game;

namespace PV3.Character.ValueChecks
{
    public class EnemyDeathCheck : CharacterDeathCheck
    {
        public void SetCurrentEnemy()
        {
            Character = GameStateManager.CurrentEnemy;
        }
    }
}