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

namespace PV3.UI.FloatingText
{
    public class HideFloatingText : MonobehaviourReference
    {
        private const float DELAY_TIME = 0.75f;

        private void OnEnable()
        {
            transform.localPosition += new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
            StartCoroutine(HideObject());
        }

        private System.Collections.IEnumerator HideObject()
        {
            yield return new WaitForSeconds(DELAY_TIME);
            gameObject.SetActive(false);
        }
    }
}