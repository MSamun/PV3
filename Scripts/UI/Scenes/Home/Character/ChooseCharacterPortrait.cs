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
using PV3.ScriptableObjects.Home;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Scenes.Home.Character
{
    public class ChooseCharacterPortrait : MonobehaviourReference
    {
        [SerializeField] private IntValue PortraitIconIndex;
        [SerializeField] private PortraitIconSpritesObject PortraitIconObject;
        [SerializeField] private bool useFemalePortraits;

        [Header("UI")]
        [SerializeField] private Button Button;

        [SerializeField] private Image IconFocus;
        private int index;

        private void Start()
        {
            // Originally, I had Male and Female Portrait Icons in separate arrays, but it was causing some graphical and serialization issues.
            // Female Portrait Icons start at Element 32 in the array, so the index needs to start at 32.
            // I'm aware of the whole "magic number" concept; this method is sure to come back and bite me in the ass.
            // I'll adjust the code to fix the apparent issue with this solution... hopefully.
            // "This is nothing more than a band-aid solution. I will fix this in the future." - every developer in existence
            index = useFemalePortraits ? transform.GetSiblingIndex() + 32 : transform.GetSiblingIndex();
            Button.GetComponent<Image>().sprite = PortraitIconObject.Icons[index];
            ResetPortraitFocus();
        }

        public void OnEnable()
        {
            ResetPortraitFocus();
        }

        public void ResetPortraitFocus()
        {
            if (IconFocus.gameObject.activeInHierarchy && PortraitIconIndex.Value != index) IconFocus.gameObject.SetActive(false);

            if (!IconFocus.gameObject.activeInHierarchy && PortraitIconIndex.Value == index) IconFocus.gameObject.SetActive(true);
        }

        public void SetPortraitIcon()
        {
            IconFocus.gameObject.SetActive(true);
            PortraitIconIndex.Value = index;
        }
    }
}