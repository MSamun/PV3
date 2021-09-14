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

using System;
using PV3.Miscellaneous;
using UnityEngine;

namespace PV3.UI.Scenes.Home
{
    [Serializable]
    public class TabInformation
    {
        public GameObject TabUnfocus;
        public GameObject TabFocus;
        public GameObject TabPanel;
    }

    public class TabUI : MonobehaviourReference
    {
        [SerializeField] private TabInformation[] TotalTabs;
        private int _index;

        private void OnEnable()
        {
            _index = 0;
            DisplayAppropriatePanel();
        }

        public void MoveToTab(int tabIndex = 0)
        {
            _index = tabIndex;
            DisplayAppropriatePanel();
        }

        private void DisplayAppropriatePanel()
        {
            for (var i = 0; i < TotalTabs.Length; i++)
            {
                if (TotalTabs[i].TabUnfocus)
                    TotalTabs[i].TabUnfocus.gameObject.SetActive(_index != i);

                if (TotalTabs[i].TabFocus)
                    TotalTabs[i].TabFocus.gameObject.SetActive(_index == i);

                if (TotalTabs[i].TabPanel)
                    TotalTabs[i].TabPanel.gameObject.SetActive(_index == i);
            }
        }
    }
}