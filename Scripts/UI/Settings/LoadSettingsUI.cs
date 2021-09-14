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
using PV3.ScriptableObjects.Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PV3.UI.Settings
{
    public class LoadSettingsUI : MonobehaviourReference
    {
        // Need to disable/enable the Panels that call the Settings Scene to save on performance.
        // Disables panel when Settings Scene is open, enables panel when Settings Scene is closed.
        [SerializeField] private GameObject MainScreenPanel;
        [SerializeField] private GameEventObject OnToggleSettingsUI;

        public void LoadSettingsScene()
        {
            // Settings Scene Index in Build Settings is currently set at 4. If you change this value, make sure you change it in the Build Settings as well.
            if (!SceneManager.GetSceneByBuildIndex(4).isLoaded)
                SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);

            FireToggleSettingsEvent();
        }

        public void UnloadSettingsScene()
        {
            SceneManager.UnloadSceneAsync(4);
            FireToggleSettingsEvent();
        }

        public void ToggleMainScreenPanel()
        {
            if (MainScreenPanel)
            {
                MainScreenPanel.gameObject.SetActive(!MainScreenPanel.gameObject.activeInHierarchy);
                return;
            }

            Debug.LogWarning("<color=yellow>WARNING:</color> MainScreenPanel is set to NULL in LoadSettingsUI.cs. Ignoring request to toggle panel...", this);
        }

        private void FireToggleSettingsEvent()
        {
            if (OnToggleSettingsUI)
            {
                OnToggleSettingsUI.Raise();
                return;
            }

            Debug.LogWarning("<color=yellow>WARNING:</color> OnToggleSettingsUI is set to NULL in LoadSettingsUI.cs. Ignoring request to fire GameEvent...", this);
        }
    }
}