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

namespace PV3.UI.Tooltip
{
    public enum PivotHorizontal
    {
        Right,
        Center,
        Left
    }

    public enum PivotVertical
    {
        Top,
        Center,
        Bottom
    }

    public class TooltipController : MonobehaviourReference
    {
        private static TooltipController _instance;
        [SerializeField] private TooltipUI Tooltip;

        private void Awake()
        {
            if (_instance != null)
                Destroy(gameObject);
            else
                _instance = this;
        }

        public static void InitializeTooltip(string header, string desc, string cooldown = "", string staminaCost = "")
        {
            _instance.Tooltip.PopulateTooltipComponents(header, desc, cooldown, staminaCost);
        }

        public static void InitializePivotPointAndPosition(PivotHorizontal pivotX, PivotVertical pivotY, Vector3 position)
        {
            _instance.Tooltip.SetPivotAndPosition(position, pivotX, pivotY);
            _instance.Tooltip.Show();
        }
    }
}