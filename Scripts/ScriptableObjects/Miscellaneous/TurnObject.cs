// PV3 is a menu-based RPG game.
// This file is part of the PV3 distribution (https://github.com/MSamun/PV3)
// Copyright (C) 2021 Matthew Samun.
//
// This program is free software: you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the Free
// Software Foundation, version 3.
//
// This program is distributed in the hope that it will be useful, but WITHOUT
// ANY WARRANTY; without even the implied warranty of  MERCHANTABILITY or
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
// more details.
//
// You should have received a copy of the GNU General Public License along with
// this program. If not, see <http://www.gnu.org/licenses/>.

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