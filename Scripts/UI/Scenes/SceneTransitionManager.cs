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

using DG.Tweening;
using PV3.Miscellaneous;
using PV3.ScriptableObjects.Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PV3.UI.Scenes
{
    // If more Scenes are created and need a transition animation between them, then add it to this Enum list.
    // Make sure the Enum order is the same as the Build order in the Unity Build Settings.
    public enum SceneIndexes
    {
        Title,
        Loading,
        Home,
        Game
    }

    public class SceneTransitionManager : MonobehaviourReference
    {
        [Tooltip("If you're transitioning to a heavily populated Scene, have this set to true.")]
        [SerializeField] private bool UseLoadScene;

        [SerializeField] private CanvasGroup BlackCoverImageCanvasGroup;
        [SerializeField] private IntValue SceneIndex;

        private void Awake()
        {
            if (!BlackCoverImageCanvasGroup)
            {
                Debug.LogWarning("<color=yellow>WARNING:</color> BlackCoverImageCanvasGroup is NULL in SceneTransitionManager.cs. Ignoring request to fade Scene in or out...", this);
                return;
            }

            if (!BlackCoverImageCanvasGroup.gameObject.activeInHierarchy)
                BlackCoverImageCanvasGroup.gameObject.SetActive(true);

            if (BlackCoverImageCanvasGroup.alpha < 1f)
                BlackCoverImageCanvasGroup.alpha = 1f;

            BlackCoverImageCanvasGroup.DOFade(0f, 2.25f).OnComplete(() => BlackCoverImageCanvasGroup.gameObject.SetActive(false));
        }

        public void LoadHomeScene()
        {
            if (!SceneIndex)
            {
                Debug.LogError("<color=red>ERROR:</color> SceneIndex is NULL in SceneTransitionManager.cs. Ignoring request to load Home Scene...", this);
                return;
            }

            SceneIndex.Value = (int)SceneIndexes.Home;
            TweenTransition();
        }

        public void LoadGameScene()
        {
            if (!SceneIndex)
            {
                Debug.LogError("<color=red>ERROR:</color> SceneIndex is NULL in SceneTransitionManager.cs. Ignoring request to load Game Scene...", this);
                return;
            }

            SceneIndex.Value = (int)SceneIndexes.Game;
            TweenTransition();
        }

        public void LoadTitleScene()
        {
            // Use this method only when you want to go BACK to the Title Scene from either the Game or Home Scene.
            if (!SceneIndex)
            {
                Debug.LogError("<color=red>ERROR:</color> SceneIndex is NULL in SceneTransitionManager.cs. Ignoring request to load Title Scene...", this);
                return;
            }

            SceneIndex.Value = (int)SceneIndexes.Title;
            TweenTransition();
        }

        private void TweenTransition()
        {
            if (!BlackCoverImageCanvasGroup)
            {
                Debug.LogError("<color=red>ERROR:</color> BlackCoverImageCanvasGroup is NULL in SceneTransitionManager.cs. Ignoring request to fade in and out between Scenes...", this);
                LoadNextScene();
                return;
            }

            BlackCoverImageCanvasGroup.gameObject.SetActive(true);

            if (BlackCoverImageCanvasGroup.alpha != 0)
                BlackCoverImageCanvasGroup.alpha = 0;

            BlackCoverImageCanvasGroup.DOFade(1f, 1.5f).OnComplete(LoadNextScene);
        }

        private void LoadNextScene()
        {
            if (!UseLoadScene)
                SceneManager.LoadScene(SceneIndex.Value);
            else
                SceneManager.LoadScene((int)SceneIndexes.Loading);
        }
    }
}