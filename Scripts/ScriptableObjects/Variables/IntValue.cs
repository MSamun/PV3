using UnityEngine;

namespace PV3.ScriptableObjects.Variables
{
    [CreateAssetMenu(fileName = "New Integer Value", menuName = "Variables/Integer")]
    public class IntValue : ScriptableObject
    {
        public int Value;

        public void SetToZero()
        {
            Value = 0;
        }
    }
}