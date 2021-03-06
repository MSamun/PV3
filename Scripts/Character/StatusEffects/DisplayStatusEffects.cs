﻿// PV3 is a menu-based RPG game.
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

using System.Collections.Generic;
using PV3.Miscellaneous;
using PV3.ScriptableObjects.Game;
using PV3.ScriptableObjects.UI;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.Character.StatusEffects
{
    public class DisplayStatusEffects : MonobehaviourReference
    {
        [SerializeField] private StatusEffectSpritesObject StatusEffectSpritesObject;
        [SerializeField] protected CharacterObject CharacterObject;
        [SerializeField] private GameObject StatusEffectPrefab;

        // Strictly for visual representation. Has no effect on the actual Status Effects.
        [Header("")]
        [SerializeField] private List<GameObject> ListOfStatusEffectPrefabsDisplayed = new List<GameObject>();

        private void Start()
        {
            CharacterObject.StatusEffectObject.ResetStatusEffectList();
        }

        // Referenced in the hierarchy by Game Events Listener Holder -> [Player/Enemy] Events -> On[Player/Enemy]StatusEffectDeducted.
        public void DisableUnusedPrefabs()
        {
            for (var i = 0; i < ListOfStatusEffectPrefabsDisplayed.Count; i++)
            {
                if (CharacterObject.StatusEffectObject.CurrentStatusEffects[i].inUse) continue;
                ListOfStatusEffectPrefabsDisplayed[i].gameObject.SetActive(false);
            }
        }

        // Referenced in the hierarchy by Game Events Listener Holder -> [Player/Enemy] Events -> On[Player/Enemy]StatusEffectApplied.
        public void CheckIfHasAvailableStatusEffectPrefab()
        {
            if (CharacterObject.StatusEffectObject.CurrentStatusEffects.Count > ListOfStatusEffectPrefabsDisplayed.Count)
                InstantiateNewPrefab();
            else
                UseDisabledPrefabInList();
        }

        private void InstantiateNewPrefab()
        {
            var obj = Instantiate(StatusEffectPrefab, transform.position, Quaternion.identity);
            obj.transform.SetParent(transform, false);

            ListOfStatusEffectPrefabsDisplayed.Add(obj);
            PopulatePrefabUIComponents(ListOfStatusEffectPrefabsDisplayed.Count - 1);
        }

        private void UseDisabledPrefabInList()
        {
            for (var i = 0; i < ListOfStatusEffectPrefabsDisplayed.Count; i++)
            {
                if (ListOfStatusEffectPrefabsDisplayed[i].gameObject.activeInHierarchy) continue;
                PopulatePrefabUIComponents(i);
                return;
            }
        }

        private void PopulatePrefabUIComponents(int index = -1)
        {
            if (index == -1) return;

            // A bit of an unorthodox way of setting the Image components of a Prefab. Prefab children objects won't change order.
            InitializePrefabBackground(index);
            InitializePrefabIcon(index);
            InitializePrefabFrame(index);

            ListOfStatusEffectPrefabsDisplayed[index].gameObject.SetActive(true);
        }

        private void InitializePrefabBackground(int index)
        {
            var background = ListOfStatusEffectPrefabsDisplayed[index].transform.GetChild(0).GetComponentInChildren<Image>(true);

            background.color = CharacterObject.StatusEffectObject.CurrentStatusEffects[index].isDebuff ? StatusEffectSpritesObject.debuffBackgroundColour : StatusEffectSpritesObject.buffBackgroundColour;
        }

        private void InitializePrefabIcon(int index)
        {
            var icon = ListOfStatusEffectPrefabsDisplayed[index].transform.GetChild(1).GetComponentInChildren<Image>(true);

            icon.color = CharacterObject.StatusEffectObject.CurrentStatusEffects[index].isDebuff ? StatusEffectSpritesObject.debuffIconColour : StatusEffectSpritesObject.buffIconColour;

            switch(CharacterObject.StatusEffectObject.CurrentStatusEffects[index].type)
            {
                case StatusType.Damage:
                    icon.sprite = StatusEffectSpritesObject.damageBonusIcon;
                    break;
                case StatusType.Block:
                    icon.sprite = StatusEffectSpritesObject.blockBonusIcon;
                    break;
                case StatusType.Dodge:
                    icon.sprite = StatusEffectSpritesObject.dodgeBonusIcon;
                    break;
                case StatusType.DamageReduction:
                    icon.sprite = StatusEffectSpritesObject.damageReductionIcon;
                    break;
                case StatusType.Critical:
                    icon.sprite = StatusEffectSpritesObject.criticalChanceIcon;
                    break;
                case StatusType.Lifesteal:
                    icon.sprite = StatusEffectSpritesObject.lifestealIcon;
                    break;
                case StatusType.Linger:
                    icon.sprite = StatusEffectSpritesObject.lingerIcon;
                    break;
                case StatusType.Regenerate:
                    icon.sprite = StatusEffectSpritesObject.regenerateIcon;
                    break;
                case StatusType.Stun:
                    icon.sprite = StatusEffectSpritesObject.stunIcon;
                    break;
                default:
                    Debug.LogError("No Status Type found when trying to populate the Status Effect Prefab's icon sprite. Aborting...");
                    break;
            }
        }

        private void InitializePrefabFrame(int index)
        {
            var frame = ListOfStatusEffectPrefabsDisplayed[index].transform.GetChild(2).GetComponentInChildren<Image>(true);

            frame.sprite = CharacterObject.StatusEffectObject.CurrentStatusEffects[index].isDebuff ? StatusEffectSpritesObject.debuffFrame : StatusEffectSpritesObject.buffFrame;
        }
    }
}