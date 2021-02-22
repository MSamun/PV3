namespace PV3.Character.Portraits
{
    public class PlayerDeathChecker : CharacterDeathChecker
    {
        public override void CheckIfDeadFromLinger()
        {
            base.CheckIfDeadFromLinger();
            CurrentTurnObject.SwitchToEnemyTurn();
        }
    }
}