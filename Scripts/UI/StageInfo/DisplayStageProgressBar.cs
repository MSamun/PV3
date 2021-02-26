using PV3.Character;
using PV3.Miscellaneous;
using UnityEngine;

namespace PV3.UI.StageInfo
{
    [RequireComponent(typeof(DetermineCurrentEnemyInStage))]
    public class DisplayStageProgressBar : MonobehaviourReference
    {
        private DetermineCurrentEnemyInStage stageInfo;

        [Header("Progress Bars")]
        [SerializeField] private GameObject nonBossProgressBar;
        [SerializeField] private GameObject bossProgressBar;

        private void Awake()
        {
            stageInfo = GetComponent<DetermineCurrentEnemyInStage>();

            if (stageInfo.ListOfStagesObject.listOfStages[stageInfo.StageListIndex.Value].Stage.hasBoss)
            {
                nonBossProgressBar.gameObject.SetActive(false);
                bossProgressBar.gameObject.SetActive(true);

            }
            else
            {
                bossProgressBar.gameObject.SetActive(false);
                nonBossProgressBar.gameObject.SetActive(true);
            }
        }
    }
}