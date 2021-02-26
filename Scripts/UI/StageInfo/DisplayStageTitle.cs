using PV3.Miscellaneous;
using PV3.ScriptableObjects.Variables;
using TMPro;
using UnityEngine;

namespace PV3.UI.StageInfo
{
    public class DisplayStageTitle : MonobehaviourReference
    {
        private TextMeshProUGUI _title = null;

        [Header("Stage List Index")]
        [SerializeField] private IntValue StageListIndex = null;

        private void Awake()
        {
            _title = GetComponentInChildren<TextMeshProUGUI>(true);

            if (_title)
                // Account for element index starting at 0.
                _title.text = $"Stage {(StageListIndex.Value < 9 ? "0" : string.Empty)}{StageListIndex.Value + 1}";
            else
                Debug.LogError("DisplayStageTitle.cs does not have a reference to the TextMeshProUGUI component. Aborting...");
        }
    }
}