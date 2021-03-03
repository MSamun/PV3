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
using PV3.ScriptableObjects.Variables;
using TMPro;
using UnityEngine;

namespace PV3.UI.StageInfo
{
    public class DisplayStageTitle : MonobehaviourReference
    {
        private TextMeshProUGUI _title = null;

        [Header("Stage List Index")]
        [SerializeField] private IntValue StageListIndex = null;

        private void Awake()
        {
            _title = GetComponentInChildren<TextMeshProUGUI>(true);

            if (_title)
                // Account for element index starting at 0.
                _title.text = $"Stage {(StageListIndex.Value < 9 ? "0" : string.Empty)}{StageListIndex.Value + 1}";
            else
                Debug.LogError("DisplayStageTitle.cs does not have a reference to the TextMeshProUGUI component. Aborting...");
        }
    }
}