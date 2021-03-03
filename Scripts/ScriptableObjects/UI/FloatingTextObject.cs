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

using UnityEngine;

namespace PV3.ScriptableObjects.UI
{
    [CreateAssetMenu(fileName = "New Floating Text", menuName = "Game/UI/Floating Text*")]
    public class FloatingTextObject : ScriptableObject
    {
        [HideInInspector] public Color newPrefabColor = Color.red;
        [HideInInspector] public string newPrefabText = string.Empty;

        public GameObject FloatingTextPrefab;

        [Header("Floating Text Colors")]
        public readonly Color DamageColor = new Color(1, 0.2f, 0.2f, 1);         // Red-ish color (255, 51, 51, 255);
        public readonly Color HealColor = new Color(0, 0.79f, 0, 1);             // Green-ish color (0, 200, 0, 255);
        public readonly Color BlockedColor = new Color(0.59f, 0.59f, 0.59f, 1);  // Grey color (150, 150, 150, 255);
        public readonly Color DodgedColor = new Color(1, 0.79f, 0, 1);           // Yellow-ish color (255, 201, 0, 255);
        public readonly Color StunnedColor = new Color(0.4f, 0, 1, 1);           // Purple-ish color (102, 0, 255, 255);


        public void CreateNewFloatingText(Color color, string value = "")
        {
            newPrefabColor = color;
            newPrefabText = value;
        }
    }
}