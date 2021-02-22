using PV3.Character;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.StageInfo
{
    public class StageProgressBar : MonobehaviourReference
    {
        [System.Serializable]
        private class ProgressBarPointers
        {
            public GameObject pointerFocus = null;
            public GameObject pointerUnfocus = null;
        }

        private DetermineCurrentEnemyInStage stageInfoScript;

        [SerializeField] private Slider slider;
        [Range(0.05f, 0.10f)] [SerializeField] private float barLerpSpeed = 0.075f;

        [Header("")]
        [SerializeField] private ProgressBarPointers[] Pointers = new ProgressBarPointers[0];
        private void Start()
        {
            stageInfoScript = GetComponentInParent<DetermineCurrentEnemyInStage>();

            slider.minValue = 0;
            slider.maxValue = stageInfoScript.ListOfStagesObject.listOfStages[stageInfoScript.StageListIndex.Value].Stage.listOfEnemies.Count - 1;
            slider.value = stageInfoScript.CurrentEnemyIndex.Value;

            UpdateBarPointer();
        }

        private void FixedUpdate()
        {
            if (!Mathf.Approximately(slider.value, stageInfoScript.CurrentEnemyIndex.Value))
            {
                UpdateBar();
            }
        }

        private void UpdateBar()
        {
            // Stage Bar has filled the progress bar more than it should, so you subtract to match them.
            // This method is in FixedUpdate to smoothly increase the bar, rather than an instant change in value.
            if (slider.value > stageInfoScript.CurrentEnemyIndex.Value)
            {
                slider.value -= barLerpSpeed;
                slider.value = Mathf.Clamp(slider.value, stageInfoScript.CurrentEnemyIndex.Value, slider.maxValue);
            }
            else
            {
                slider.value += barLerpSpeed;
            }

            UpdateBarPointer();
        }

        private void UpdateBarPointer()
        {
            Pointers[stageInfoScript.CurrentEnemyIndex.Value].pointerFocus.gameObject.SetActive(true);
            Pointers[stageInfoScript.CurrentEnemyIndex.Value].pointerUnfocus.gameObject.SetActive(false);
        }
    }
}