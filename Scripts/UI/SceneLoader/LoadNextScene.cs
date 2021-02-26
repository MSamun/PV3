using System.Collections;
using PV3.Miscellaneous;
using PV3.ScriptableObjects.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PV3.UI.SceneLoader
{
    // Uses the Loading Scene as the middle-man in between the two scenes that need to be loaded. This is an alternative for when some of the other Scenes become heavily populated
    // and need a significant amount of time to load. Since all the Scenes load up relatively quickly, a quick black cross-fade transition is more than enough.
    public class LoadNextScene : MonobehaviourReference
    {
        [Header("---------- VALUE COMPONENTS ----------")]
        [SerializeField] private IntValue SceneIndex;

        [Header("---------- UI COMPONENTS ----------")]
        [SerializeField] private Slider progressBar;
        [SerializeField] private TextMeshProUGUI progressText;
        [SerializeField] private TextMeshProUGUI tipText;
        public string[] tipArray = new string[5];

        private void Start()
        {
            progressText.text = "Loading... (0%)";
            tipText.text = $"TIP: {tipArray[Random.Range(0, tipArray.Length)]}";
            StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene()
        {
            yield return new WaitForSeconds(1.5f);

            var operation = SceneManager.LoadSceneAsync(SceneIndex.Value);

            while (!operation.isDone)
            {
                var progress = Mathf.Clamp01(operation.progress / 0.9f);

                progressBar.value = progress;
                progressText.text = $"Loading... ({Mathf.RoundToInt(progress * 100f).ToString()}%)";
                yield return null;
            }
        }
    }
}