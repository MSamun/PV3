using PV3.Miscellaneous;
using UnityEngine;
using UnityEngine.Serialization;

namespace PV3.UI.Home
{
    [System.Serializable]
    public class TabInformation
    {
        public GameObject TabUnfocus;
        public GameObject TabFocus;
        public GameObject TabPanel;
    }

    public class TabUI : MonobehaviourReference
    {
        private int index;
        [SerializeField] private TabInformation[] TotalTabs;

        public void MoveToTab(int tabIndex = 0)
        {
            index = tabIndex;
            DisplayAppropriatePanel();
        }

        private void DisplayAppropriatePanel()
        {
            for (var i = 0; i < TotalTabs.Length; i++)
            {
                if (TotalTabs[i].TabUnfocus) TotalTabs[i].TabUnfocus.gameObject.SetActive(index != i);
                if (TotalTabs[i].TabFocus) TotalTabs[i].TabFocus.gameObject.SetActive(index == i);
                if (TotalTabs[i].TabPanel) TotalTabs[i].TabPanel.gameObject.SetActive(index == i);
            }
        }

        private void OnEnable()
        {
            index = 0;
            DisplayAppropriatePanel();
        }
    }
}