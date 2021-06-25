// PV3 is a menu-based RPG game.
// This file is part of the PV3 distribution (https://github.com/MSamun/PV3)
// Copyright (C) 2021 Matthew Samun.
//
// This program is free software: you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the Free
// Software Foundation, version 3.
//
// This program is distributed in the hope that it will be useful, but WITHOUT
// ANY WARRANTY; without even the implied warranty of  MERCHANTABILITY or
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
// more details.
//
// You should have received a copy of the GNU General Public License along with
// this program. If not, see <http://www.gnu.org/licenses/>.

using PV3.Miscellaneous;
using UnityEngine;

namespace PV3.UI.Home
{
    public class HelpUI : MonobehaviourReference
    {
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
        private int currentPageIndex;

        private void OnEnable()
        {
            currentPageIndex = 0;
            DisplayAppropriateInfoPanel();
        }

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
    }
}