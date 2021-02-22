using UnityEngine;
using UnityEngine.Serialization;

namespace PV3.ScriptableObjects.Game
{
    [CreateAssetMenu(fileName = "New Damage Component", menuName = "Spell/Components/Damage")]
    public class DamageComponent : SpellComponentObject
    {
        [Header("")]
        public WeaponType WeaponType;

        [Header("")]
        [Range(0, 1)] public float healPercentage;
        [Range(1, 4)] public int numberOfAttacks = 1;
    }
}