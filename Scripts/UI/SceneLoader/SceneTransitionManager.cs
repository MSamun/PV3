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