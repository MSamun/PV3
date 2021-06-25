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
using PV3.Miscellaneous;
using PV3.ScriptableObjects.UI;
using TMPro;
using UnityEngine;

namespace PV3.UI.FloatingText
{
    [Serializable]
    public class PreInstantiatedFloatingTextPrefabs
    {
        public GameObject Prefab;
        public TextMeshProUGUI TextComponent;

        public PreInstantiatedFloatingTextPrefabs(GameObject prefab = null, TextMeshProUGUI textComponent = null)
        {
            Prefab = prefab;
            TextComponent = textComponent;
        }
    }

    public class DisplayFloatingText : MonobehaviourReference
    {
        [SerializeField] private FloatingTextObject FloatingTextObject;

        [Header("")]
        [SerializeField] private List<PreInstantiatedFloatingTextPrefabs> FloatingTextPrefabs = new List<PreInstantiatedFloatingTextPrefabs>();

        private Transform floatingTextSpawnPosition;

        private void Start()
        {
            floatingTextSpawnPosition = GetComponent<Transform>();

            ResetFloatingTextPrefabsList();
            FindTextComponentsOfPrefabsInList();
        }

        private void ResetFloatingTextPrefabsList()
        {
            for (var i = 0; i < FloatingTextPrefabs.Count; i++)
            {
                if (i <= 2) continue;
                FloatingTextPrefabs.RemoveAt(i);
            }
        }

        private void FindTextComponentsOfPrefabsInList()
        {
            for (var i = 0; i < FloatingTextPrefabs.Count; i++)
            {
                if (!FloatingTextPrefabs[i].Prefab) continue;
                FloatingTextPrefabs[i].TextComponent = FloatingTextPrefabs[i].Prefab.gameObject.GetComponentInChildren<TextMeshProUGUI>(true);
            }
        }

        public void CheckIfHasAvailableFloatingTextPrefabToUse()
        {
            var index = -1;

            for (var i = 0; i < FloatingTextPrefabs.Count; i++)
            {
                if (!FloatingTextPrefabs[i].Prefab || FloatingTextPrefabs[i].Prefab.gameObject.activeInHierarchy) continue;

                index = i;
                break;
            }

            if (index == -1)
                InstantiateNewPrefab();
            else
                InitializePrefabValues(index);
        }

        private void InstantiateNewPrefab()
        {
            var go = Instantiate(FloatingTextObject.FloatingTextPrefab, floatingTextSpawnPosition.position, Quaternion.identity);
            go.transform.SetParent(floatingTextSpawnPosition, false);

            var newPrefab = new PreInstantiatedFloatingTextPrefabs(go, go.GetComponentInChildren<TextMeshProUGUI>(true));
            FloatingTextPrefabs.Add(newPrefab);
            InitializePrefabValues(FloatingTextPrefabs.Count - 1);
        }

        private void InitializePrefabValues(int index = -1)
        {
            if (index == -1 || !FloatingTextPrefabs[index].TextComponent) return;

            FloatingTextPrefabs[index].TextComponent.color = FloatingTextObject.newPrefabColor;
            FloatingTextPrefabs[index].TextComponent.text = FloatingTextObject.newPrefabText;

            FloatingTextPrefabs[index].Prefab.gameObject.SetActive(true);
        }
    }
}