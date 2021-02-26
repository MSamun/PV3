using System.Collections;
using PV3.Miscellaneous;
using PV3.ScriptableObjects.Variables;
using TMPro;
using UnityEngine;

namespace PV3.UI.StageInfo
{
    public class TrackTimeElapsed : MonobehaviourReference
    {
        [SerializeField] private IntValue TimeElapsedInLevel = null;
        [SerializeField] private TextMeshProUGUI timeElapsedText = null;

        private bool testingBool = false;
        private void Start()
        {
            TimeElapsedInLevel.SetToZero();
            timeElapsedText.text = string.Format("{0:0}:00", TimeElapsedInLevel.Value);

            testingBool = true;
            StartCoroutine(TrackCurrentTimeElapsedInLevel());
        }

        private IEnumerator TrackCurrentTimeElapsedInLevel()
        {
            float seconds = 0;
            float minutes = 0;

            while (testingBool)
            {
                yield return new WaitForSeconds(1f);
                TimeElapsedInLevel.Value += 1;

                seconds = Mathf.Floor(TimeElapsedInLevel.Value % 60);
                minutes = Mathf.Floor(TimeElapsedInLevel.Value / 60) % 60;

                timeElapsedText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
            }
            yield return null;
        }
    }
}