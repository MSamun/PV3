using System.Collections.Generic;
using PV3.Character;
using UnityEngine;

namespace PV3.ScriptableObjects.Stages
{
    [CreateAssetMenu(fileName = "New Stage", menuName = "Stage Select/New Stage")]
    public class StageInfoObject : ScriptableObject
    {
        [System.Serializable]
        public class StageInformation
        {
            public EnemyObject enemy;
            public int level;
        }

        [Header("Basic Stage Information")]
        public int stageID = 0;
        public GameObject stageBackground = null;
        public bool hasBoss = false;

        [Header("List of Enemies in Stage")]
        public List<StageInformation> listOfEnemies = new List<StageInformation>();
    }
}