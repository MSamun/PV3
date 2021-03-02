using System.Collections;
using PV3.Miscellaneous;
using PV3.ScriptableObjects.GameEvents;
using PV3.ScriptableObjects.Variables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PV3.UI.SceneLoader
{
    // If more Scenes are created and need a transition animation between them, then add it to this Enum list.
    // Make sure the Enum order is the same as the Build order in Unity Build Settings.
    public enum SceneIndexes { Title, Loading, Home, Game };

    public class SceneTransitionManager : MonobehaviourReference
    {
        [SerializeField] private bool useLoadScene;
        [SerializeField] private bool isTapScreenValid;


        [Header("Transition Components")]
        [SerializeField] private GameObject SceneTransition;
        [SerializeField] private IntValue SceneIndex;
        [SerializeField] private GameEventObject OnLoadNextSceneEvent;

        private void Update()
        {
            // Only use when one tap on the screen is required to transition to another Scene. As of now, only the Title Scene uses this feature.
            if (!isTapScreenValid) return;

            if (Input.touchCount > 0)
                LoadHomeScene();
        }

        public void LoadHomeScene()
        {
            SceneIndex.Value = (int)SceneIndexes.Home;
            OnLoadNextSceneEvent.Raise();
        }

        public void LoadGameScene()
        {
            SceneIndex.Value = (int)SceneIndexes.Game;
            OnLoadNextSceneEvent.Raise();
        }

        public void LoadTitleScene()
        {
            SceneIndex.Value = (int)SceneIndexes.Title;
            OnLoadNextSceneEvent.Raise();
        }

        public void StartTransitionCoroutine()
        {
            StartCoroutine(TransitionToNextScene());
        }

        private IEnumerator TransitionToNextScene()
        {
            yield return StartCoroutine(SceneTransition.GetComponent<SceneTransition>().LoadSceneFadeTransition());

            // Only use the Load Scene when the Scene you want to transition to is heavily populated; otherwise, just use the basic black fade transition.
            if (!useLoadScene || SceneIndex.Value == (int) SceneIndexes.Title)
                SceneManager.LoadScene(SceneIndex.Value);
            else
                SceneManager.LoadScene((int) SceneIndexes.Loading);
        }
    }
}