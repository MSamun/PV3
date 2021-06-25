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

using System;
using System.Collections.Generic;
using PV3.ScriptableObjects.Character;
using UnityEngine;

namespace PV3.ScriptableObjects.Stages
{
    [CreateAssetMenu(fileName = "New Stage", menuName = "Stage Select/New Stage")]
    public class StageInfoObject : ScriptableObject
    {
        [Header("Basic Stage Information")]
        public int stageID;

        public GameObject stageBackground;
        public bool hasBoss;

        [Header("List of Enemies in Stage")]
        public List<StageInformation> listOfEnemies = new List<StageInformation>();

        [Serializable]
        public class StageInformation
        {
            public EnemyObject enemy;
            public int level;
        }
    }
}