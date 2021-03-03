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
using TMPro;
using UnityEngine;

namespace PV3.UI.StageInfo
{
    public class TrackTimeElapsed : MonobehaviourReference
    {
        [SerializeField] private TextMeshProUGUI timeElapsedText;

        private float timer;

        private void Start()
        {
            timer = 0;
            timeElapsedText.text = "0:00";
        }

        private void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;
            timeElapsedText.text = $"{Mathf.Floor(timer / 60) % 60:0}:{Mathf.Floor(timer % 60):00}";
        }
    }
}