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

// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
//
// Author: Ryan Hipple
// Date:   10/04/17
//
// * Thank you very much for this code, Ryan. This shit is a lifesaver. *
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;

namespace PV3.ScriptableObjects.GameEvents
{
    public class GameEventListener : MonoBehaviour
    {
        [Header("Events To Look Out For")]
        [SerializeField] private GameEventObject[] Events = null;

        [Header("What Happens When Event Gets Invoked")]
        [SerializeField] private UnityEvent Response = null;

        public void OnEventRaised()
        {
            Response.Invoke();
        }

        private void OnEnable()
        {
            for (var i = Events.Length - 1; i >= 0; i--)
                Events[i].RegisterListener(this);
        }

        private void OnDisable()
        {
            for (var i = Events.Length - 1; i >= 0; i--)
                Events[i].UnRegisterListener(this);
        }
    }
}