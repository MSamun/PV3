namespace PV3.Character.Spells
{
    public class EnemySpellUse : CharacterSpellUse
    {
        protected override void Start()
        {
            base.Start();
            DetermineCurrentCaster();
        }

        public void DetermineCurrentCaster()
        {
            Caster = determineCurrentEnemyScriptReference.FindCurrentEnemy();
        }

        protected override void DisplayResultsOfTurn(bool isHealSpell)
        {
            IsCasterPlayer = false;
            base.DisplayResultsOfTurn(isHealSpell);
        }
    }
}