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
using UnityEngine;

namespace PV3.Miscellaneous
{
    public class DisplayAdPopup : MonobehaviourReference
    {
        [Tooltip("The value is in seconds.")]
        [SerializeField] private float popupTimer = 60f;

        private void Start()
        {
            StartCoroutine(PopupTimer());
        }

        private IEnumerator PopupTimer()
        {
            while (true)
            {
                yield return new WaitForSeconds(popupTimer);

                print("Should execute right about now!");
                gameObject.transform.GetChild(0).gameObject.SetActive(true);

                yield return new WaitForSeconds(5f);

                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}