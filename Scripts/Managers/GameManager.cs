using PV3.Miscellaneous;
using UnityEngine;

namespace PV3
{
    public class GameManager : MonobehaviourReference
    {
        [Header("UI Panels")]
        [SerializeField] private GameObject VictoryScreen;
        [SerializeField] private GameObject DefeatScreen;

        public void DisplayVictoryScreen()
        {
            VictoryScreen.gameObject.SetActive(true);
        }

        public void DisplayDefeatScreen()
        {
            PauseGame();
            DefeatScreen.gameObject.SetActive(true);
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            print(Time.timeScale.ToString());
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
            print(Time.timeScale.ToString());
        }
    }
}