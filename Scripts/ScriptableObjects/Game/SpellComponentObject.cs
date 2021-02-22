using UnityEngine;

namespace PV3.ScriptableObjects.Game
{
    public enum AttributeType {None, Strength, Dexterity, Constitution, Intelligence, Armor}

    public abstract class SpellComponentObject : ScriptableObject
    {
        [Header("Base Information")]
        [Range(0, 100)] public int minimumValue = 3;
        [Range(0, 100)] public int maximumValue = 6;
        public bool usePercentage;

        [Header("Attribute Percentage")]
        public AttributeType attributeType;
        [Range(0, 1)]public float attributePercentage;

        public int GetRandomValueBetweenRange()
        {
            if (maximumValue < minimumValue) maximumValue = minimumValue++;

            // maximumValue is exclusive, so you add one.
            return Random.Range(minimumValue, maximumValue + 1);
        }

        public int GetPercentageOfValue(int value)
        {
            return value <= 0 || !usePercentage ? 0 : Mathf.RoundToInt(value * (GetRandomValueBetweenRange() / 100f));
        }
    }
}