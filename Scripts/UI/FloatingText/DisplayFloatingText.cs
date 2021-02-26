using PV3.Miscellaneous;
using TMPro;
using UnityEngine;
using PV3.ScriptableObjects.UI;

namespace PV3.UI.FloatingText
{
    [System.Serializable]
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
        private Transform floatingTextSpawnPosition;
        [SerializeField] private FloatingTextObject FloatingTextObject;

        [Header("")]
        [SerializeField] private System.Collections.Generic.List<PreInstantiatedFloatingTextPrefabs> FloatingTextPrefabs = new System.Collections.Generic.List<PreInstantiatedFloatingTextPrefabs>();

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
            {
                InstantiateNewPrefab();
            }
            else
            {
               InitializePrefabValues(index);
            }
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