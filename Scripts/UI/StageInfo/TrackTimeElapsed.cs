using PV3.Miscellaneous;
using TMPro;
using UnityEngine;

namespace PV3.UI.StageInfo
{
    public class TrackTimeElapsed : MonobehaviourReference
    {
        [SerializeField] private TextMeshProUGUI timeElapsedText;

        private float timer;

        private void Start()
        {
            timer = 0;
            timeElapsedText.text = "0:00";
        }

        private void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;
            timeElapsedText.text = $"{Mathf.Floor(timer / 60) % 60:0}:{Mathf.Floor(timer % 60):00}";
        }
    }
}