using UnityEngine;

namespace PV3.ScriptableObjects.Game
{
    public enum StatusType { Random, Damage, Block, Dodge, DamageReduction, Critical, Linger, Regenerate, Stun}

    [CreateAssetMenu(fileName = "New Status Component", menuName = "Spell/Components/Status")]
    public class StatusComponent : SpellComponentObject
    {
        [Header("Duration of Status Effect")]
        [Range(1, 6)] public int duration = 2;

        [Header("Type of Status Effect")]
        public StatusType StatusType;
        public bool isDebuff;
        public bool isUnique;

        public StatusType ChooseRandomStatusType()
        {
            return (StatusType)Random.Range(1, 6);
        }
    }
}