using PV3.Miscellaneous;
using PV3.ScriptableObjects.GameEvents;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PV3.Settings
{
    public class LoadSettingsUI : MonobehaviourReference
    {
        // Need to disable/enable the Panels that call the Settings Scene to save on performance.
        // Disables panel when Settings Scene is open, enables panel when Settings Scene is closed.
        [SerializeField] private GameObject mainScreenPanel;
        [SerializeField] private GameEventObject OnSettingsUILoadedEvent;
        public void LoadSettingsScene()
        {
            // Settings Scene Index in Build Settings is currently set at 4. If you change this value,
            // make sure you've changed it in the Build Settings as well.
            if (!SceneManager.GetSceneByBuildIndex(4).isLoaded)
            {
                SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
            }

            OnSettingsUILoadedEvent.Raise();
        }

        public void UnloadSettingsScene()
        {
            SceneManager.UnloadSceneAsync(4);
            OnSettingsUILoadedEvent.Raise();
        }

        public void ToggleMainScreenPanel()
        {
            if (mainScreenPanel)
            {
                mainScreenPanel.gameObject.SetActive(!mainScreenPanel.gameObject.activeInHierarchy);
            }
        }
    }
}