using UnityEngine;

namespace PV3.Character
{
    // The asterik (*) implies that only one copy of this object is needed. A singleton might be good in this situation, but since I'm the only one working on this game,
    // it's more of a visual reminder for me.
    [CreateAssetMenu(fileName = "New Attribute List", menuName = "Character/Attribute List*")]
    public class AttributesObject : ScriptableObject
    {
        [Header("---------- ATTRIBUTES ----------")]
        [SerializeField] [Range(1, 200)] private int strength;
        public int Strength => strength;

        [SerializeField] [Range(1, 200)] private int dexterity;
        public int Dexterity => dexterity;

        [SerializeField] [Range(1, 200)] private int constitution;
        public int Constitution => constitution;

        [SerializeField] [Range(1, 200)] private int intelligence;
        public int Intelligence => intelligence;

        [SerializeField] [Range(1, 200)] private int armor;
        public int Armor => armor;
    }
}