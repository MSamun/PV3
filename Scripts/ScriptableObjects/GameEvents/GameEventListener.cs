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