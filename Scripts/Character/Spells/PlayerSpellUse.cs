namespace PV3.Character.Spells
{
    public class PlayerSpellUse : CharacterSpellUse
    {
        protected override void Start()
        {
            base.Start();
            DetermineCurrentTarget();
        }

        private void DetermineCurrentTarget()
        {
            Target = determineCurrentEnemyScriptReference.FindCurrentEnemy();
        }

        protected override void DisplayResultsOfTurn(bool isHealSpell)
        {
            IsCasterPlayer = true;
            base.DisplayResultsOfTurn(isHealSpell);
        }
    }
}