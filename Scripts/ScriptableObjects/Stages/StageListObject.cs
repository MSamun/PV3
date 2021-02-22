using System.Collections.Generic;
using UnityEngine;

namespace PV3.ScriptableObjects.Stages
{
    // The asterik (*) implies that only one copy of this object is needed. A singleton might be good in this situation, but since I'm the only one working on this game
    // it's more of a visual reminder for me.
    [CreateAssetMenu(fileName = "New Stage List", menuName = "Stage Select/Stage List*")]
    public class StageListObject : ScriptableObject
    {
        [System.Serializable]
        public struct StageInformation
        {
            public StageInfoObject Stage;
            // [INSERT MISSION OBJECTS HERE]
        }

        [Header("Stage List")]
        public List<StageInformation> listOfStages = new List<StageInformation>();
    }
}