using PV3.Game;

namespace PV3.Character.Spells
{
    public class EnemySpellBehaviour : CharacterSpellBehaviour
    {
        public void DetermineCurrentCaster()
        {
            Caster = GameStateManager.CurrentEnemy;
        }
    }
}