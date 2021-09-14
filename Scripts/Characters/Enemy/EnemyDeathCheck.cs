using PV3.Characters.Common;
using PV3.Game;

namespace PV3.Characters.Enemy
{
    public class EnemyDeathCheck : CharacterDeathCheck
    {
        public void SetCurrentEnemy()
        {
            Character = GameStateManager.CurrentEnemy;
        }
    }
}