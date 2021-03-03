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
    // The asterik (*) implies that only one copy of this object is needed. A singleton might be good in this situation, but since I'm the only one working on this game
    // it's more of a visual reminder for me.
    [CreateAssetMenu(fileName = "New Status Effect Sprites Object", menuName = "Game/UI/Status Effect Sprites*")]
    public class StatusEffectSpritesObject : ScriptableObject
    {
        [Header("Status Effect Icons")]
        public Sprite damageBonusIcon;
        public Sprite blockBonusIcon;
        public Sprite dodgeBonusIcon;
        public Sprite damageReductionIcon;
        public Sprite criticalChanceIcon;
        public Sprite lifestealIcon;
        public Sprite lingerIcon;
        public Sprite regenerateIcon;
        public Sprite stunIcon;

        [Header("Status Effect Frames")]
        public Sprite buffFrame;
        public Sprite debuffFrame;

        [Header("Status Effect Background Colours")]
        public Color buffBackgroundColour = new Color(0.04f, 0.3f, 0.04f, 1);       //Green-ish colour  (10, 76, 10, 255)
        public Color debuffBackgroundColour = new Color(0.3f, 0.04f, 0.04f, 1);     //Red-ish colour    (76, 10, 10, 255)

        [Header("Status Effect Icon Colours")]
        public Color buffIconColour = new Color(0, 0.79f, 0, 1);                    //Green     (0, 200, 0, 255)
        public Color debuffIconColour = new Color(0.79f, 0, 0, 1);                  //Red       (200, 0, 0, 255)
    }
}