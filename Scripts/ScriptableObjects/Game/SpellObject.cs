using System.Collections.Generic;
using UnityEngine;

namespace PV3.ScriptableObjects.Game
{
    public enum WeaponType { Sword, Staff, Bow }
    public enum SpellType { Damage, Heal, Status }


    [CreateAssetMenu(fileName = "New Spell", menuName = "Spell/New Spell")]
    public class SpellObject : ScriptableObject
    {
        [Header("Base Information")]
        public int spellID;
        public new string name = string.Empty;
        public Sprite sprite;

        [Header("Combat Information")]

        public SpellType spellType;
        [Range(1, 6)] public int totalCooldown;

        [Header("Visuals")]
        public AudioClip SfxClip;
        public Material VfxMaterial;

        [Header("")]
        public List<SpellComponentObject> components;

        [Header("")]
        [TextArea(2, 7)] public string description;
    }
}