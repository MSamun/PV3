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
using UnityEngine;

namespace PV3.UI.SceneLoader
{
    public class SceneTransition : MonobehaviourReference
    {
        [SerializeField] private Animator transitionAnim = null;
        [SerializeField] private float transitionTime = 0;

        public IEnumerator LoadSceneFadeTransition()
        {
            transitionAnim.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
        }
    }
}