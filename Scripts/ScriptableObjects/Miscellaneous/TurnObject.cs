using PV3.ScriptableObjects.GameEvents;
using UnityEngine;

namespace PV3.ScriptableObjects.Miscellaneous
{
    public enum Turn { Player, Enemy }

    // The asterik (*) implies that only one copy of this object is needed. A singleton might be good in this situation, but since I'm the only one working on this game
    // it's more of a visual reminder for me.
    [CreateAssetMenu(fileName = "New Turn Object", menuName = "Game/Current Turn*")]
    public class TurnObject : ScriptableObject
    {
        public Turn CurrentTurn { get; private set; }

        [Header("Start Player Turn Event")]
        [SerializeField] private GameEventObject OnStartPlayerTurnEvent = null;

        [Header("Start Enemy Turn Event")]
        [SerializeField] private GameEventObject OnStartEnemyTurnEvent = null;

        public void SwitchToPlayerTurn()
        {
            CurrentTurn = Turn.Player;

            if (OnStartPlayerTurnEvent)
                OnStartPlayerTurnEvent.Raise();
            else
                Debug.LogError("TurnObject.cs does not have a reference to OnStartPlayerTurnEvent. Aborting...");
        }

        public void SwitchToEnemyTurn()
        {
            CurrentTurn = Turn.Enemy;

            if (OnStartEnemyTurnEvent)
                OnStartEnemyTurnEvent.Raise();
            else
                Debug.LogError("TurnObject.cs does not have a reference to OnStartEnemyTurnEvent. Aborting...");
        }
    }
}