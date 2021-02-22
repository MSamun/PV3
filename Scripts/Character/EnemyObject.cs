using PV3.Character.Spells;
using PV3.ScriptableObjects.Game;
using UnityEngine;

namespace PV3.Character
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Character/Enemy")]
    public class EnemyObject : CharacterObject
    {
        [Header("")]
        // Determines the order in which the Enemy would like to attack the Player. It'll prioritize the use of Spell-types in this array, and in the order the Spell-types are in.
        public SpellType[] orderOfSpellTypes = new SpellType[0];
    }
}