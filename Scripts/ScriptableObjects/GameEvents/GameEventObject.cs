// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
//
// Author: Ryan Hipple
// Date:   10/04/17
//
// * Thank you very much for this code, Ryan. This shit is a lifesaver. *
// ----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace PV3.ScriptableObjects.GameEvents
{
    [CreateAssetMenu(fileName = "New Game Event", menuName = "Game/Game Event")]
    public class GameEventObject : ScriptableObject
    {
        private List<GameEventListener> listeners = new List<GameEventListener>();

        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised();
        }

        public void RegisterListener(GameEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnRegisterListener(GameEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}