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