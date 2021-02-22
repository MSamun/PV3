using UnityEngine;

namespace PV3.UI.Home
{
    public class HelpUI : MonobehaviourReference
    {
        private int currentPageIndex;

        [Header("Page Buttons")]
        [SerializeField] private GameObject previousPageButton;
        [SerializeField] private GameObject nextPageButton;

        [Header("Page Indicators")]
        [SerializeField] private GameObject pageIndicatorLeft;
        [SerializeField] private GameObject pageIndicatorMiddle;
        [SerializeField] private GameObject pageIndicatorRight;

        [Header("Information Panels")]
        [SerializeField] private GameObject spellsAndSkillsInfoPanel;
        [SerializeField] private GameObject experienceInfoPanel;
        [SerializeField] private GameObject attributesAndSkillPointsInfoPanel;

        public void NextPage()
        {
            // Currently only three pages.
            currentPageIndex++;
            if (currentPageIndex > 2) currentPageIndex = 2;
            DisplayAppropriateInfoPanel();
        }

        public void PreviousPage()
        {
            currentPageIndex--;
            if (currentPageIndex < 0) currentPageIndex = 0;
            DisplayAppropriateInfoPanel();
        }

        private void DisplayAppropriateInfoPanel()
        {
            // On first page, so no need to display.
            previousPageButton.gameObject.SetActive(currentPageIndex != 0);

            // On last page, so no need to display.
            nextPageButton.gameObject.SetActive(currentPageIndex != 2);

            pageIndicatorLeft.gameObject.SetActive(currentPageIndex == 0);
            pageIndicatorMiddle.gameObject.SetActive(currentPageIndex == 1);
            pageIndicatorRight.gameObject.SetActive(currentPageIndex == 2);

            spellsAndSkillsInfoPanel.gameObject.SetActive(currentPageIndex == 0);
            experienceInfoPanel.gameObject.SetActive(currentPageIndex == 1);
            attributesAndSkillPointsInfoPanel.gameObject.SetActive(currentPageIndex == 2);
        }

        private void OnEnable()
        {
            currentPageIndex = 0;
            DisplayAppropriateInfoPanel();
        }
    }
}