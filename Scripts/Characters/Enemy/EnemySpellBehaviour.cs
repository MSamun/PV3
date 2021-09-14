using PV3.Characters.Common;
using PV3.Game;

namespace PV3.Characters.Enemy
{
    public class EnemySpellBehaviour : CharacterSpellBehaviour
    {
        public void DetermineCurrentCaster()
        {
            Caster = GameStateManager.CurrentEnemy;
        }
    }
}