using PV3.Miscellaneous;
using PV3.ScriptableObjects.Stages;
using PV3.ScriptableObjects.Variables;
using UnityEngine;

namespace PV3.Character
{
    public class DetermineCurrentEnemyInStage : MonobehaviourReference
    {
        [SerializeField] private StageListObject listOfStagesObject;
        public StageListObject ListOfStagesObject => listOfStagesObject;

        [SerializeField] private IntValue stageListIndex;
        public IntValue StageListIndex => stageListIndex;

        [SerializeField] private IntValue currentEnemyIndex;
        public IntValue CurrentEnemyIndex => currentEnemyIndex;

        public CharacterObject FindCurrentEnemy()
        {
            return listOfStagesObject.listOfStages[stageListIndex.Value].Stage.listOfEnemies[currentEnemyIndex.Value].enemy;
        }
    }
}