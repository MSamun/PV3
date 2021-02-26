using PV3.Miscellaneous;
using PV3.ScriptableObjects.Stages;
using PV3.ScriptableObjects.Variables;
using UnityEngine;

namespace PV3.UI.StageInfo
{
    public class DisplayStageBackground : MonobehaviourReference
    {
        [Header("Stage List"), SerializeField]
        private StageListObject stageList = null;

        [Header("Stage List Index"), SerializeField]
        private IntValue stageListIndex = null;

        private void Start()
        {
            var parentTransform = gameObject.GetComponent<Transform>();

            if (!stageList.listOfStages[stageListIndex.Value].Stage.stageBackground || parentTransform.childCount != 0) return;

            var stageBackground = Instantiate(stageList.listOfStages[stageListIndex.Value].Stage.stageBackground, parentTransform.position,
                Quaternion.identity);
            stageBackground.transform.SetParent(parentTransform, false);
            stageBackground.name = "Stage Background";
        }
    }
}