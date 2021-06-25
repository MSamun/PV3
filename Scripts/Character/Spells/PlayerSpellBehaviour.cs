using PV3.Game;

namespace PV3.Character.Spells
{
    public class PlayerSpellBehaviour : CharacterSpellBehaviour
    {
        public void DetermineCurrentTarget()
        {
            Target = GameStateManager.CurrentEnemy;
        }
    }
}