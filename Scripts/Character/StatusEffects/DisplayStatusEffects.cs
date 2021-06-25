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

using System.Collections.Generic;
using PV3.Miscellaneous;
using PV3.ScriptableObjects.Character;
using PV3.ScriptableObjects.UI;
using PV3.UI.Tooltip;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.Character.StatusEffects
{
    public class DisplayStatusEffects : MonobehaviourReference
    {
        [SerializeField] private StatusEffectSpritesObject StatusEffectSpritesObject;
        [SerializeField] protected CharacterObject Character;
        [SerializeField] private GameObject StatusEffectPrefab;

        [Header("")]
        [SerializeField] private List<GameObject> ListOfStatusEffectPrefabsDisplayed = new List<GameObject>();

        public void CheckIfHasAvailableStatusEffectPrefab()
        {
            if (Character.StatusEffectObject.CurrentStatusEffects.Count > ListOfStatusEffectPrefabsDisplayed.Count)
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

            var background = ListOfStatusEffectPrefabsDisplayed[index].transform.GetChild(0).GetComponentInChildren<Image>(true);
            background.color = StatusEffectSpritesObject.SetBackgroundColour(Character.StatusEffectObject.CurrentStatusEffects[index].isDebuff);

            var icon = ListOfStatusEffectPrefabsDisplayed[index].transform.GetChild(1).GetComponentInChildren<Image>(true);
            icon.color = StatusEffectSpritesObject.SetIconColour(Character.StatusEffectObject.CurrentStatusEffects[index].isDebuff);
            icon.sprite = StatusEffectSpritesObject.SetIconSprite(Character.StatusEffectObject.CurrentStatusEffects[index].type);

            var frame = ListOfStatusEffectPrefabsDisplayed[index].transform.GetChild(2).GetComponentInChildren<Image>(true);
            frame.sprite = StatusEffectSpritesObject.SetFrame(Character.StatusEffectObject.CurrentStatusEffects[index].isDebuff);

            ListOfStatusEffectPrefabsDisplayed[index].gameObject.GetComponent<StatusEffectTooltipTrigger>().SetStatusEffect(Character.StatusEffectObject.CurrentStatusEffects[index]);
            ListOfStatusEffectPrefabsDisplayed[index].gameObject.SetActive(true);
        }

        public void DisableUnusedPrefabs()
        {
            for (var i = 0; i < Character.StatusEffectObject.CurrentStatusEffects.Count; i++)
            {
                if (Character.StatusEffectObject.CurrentStatusEffects[i].inUse || !ListOfStatusEffectPrefabsDisplayed[i].gameObject.activeInHierarchy) continue;

                ListOfStatusEffectPrefabsDisplayed[i].gameObject.SetActive(false);
            }
        }
    }
}