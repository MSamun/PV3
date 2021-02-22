using UnityEngine;

namespace PV3.ScriptableObjects.Game
{
    // The asterik (*) implies that only one copy of this object is needed. A singleton might be good in this situation, but since I'm the only one working on this game
    // it's more of a visual reminder for me.
    [CreateAssetMenu(fileName = "New Character Bonus", menuName = "Character/Bonus*")]
    public class CharacterGameBonuses : ScriptableObject
    {
        public int BlockBonus { get; private set; }
        public int DodgeBonus { get; private set; }
        public int DamageBonus { get; private set; }
        public int CriticalBonus { get; private set; }
        public int DamageReductionBonus { get; private set; }

        public void InfluenceBonus(bool isEffectDebuff = false, StatusType type = StatusType.Damage, int amount = 0)
        {
            if (amount == 0) return;

            if (type == StatusType.Damage)
                DamageBonus = isEffectDebuff ? DamageBonus -= amount : DamageBonus += amount;
            else if (type == StatusType.Block)
                BlockBonus = isEffectDebuff ? BlockBonus -= amount : BlockBonus += amount;
            else if (type == StatusType.Dodge)
                DodgeBonus = isEffectDebuff ? DodgeBonus -= amount : DodgeBonus += amount;
            else if (type == StatusType.DamageReduction)
                DamageReductionBonus = isEffectDebuff ? DamageReductionBonus -= amount : DamageReductionBonus += amount;
            else if (type == StatusType.Critical)
                CriticalBonus = isEffectDebuff ? CriticalBonus -= amount : CriticalBonus += amount;
        }

        // Whenever a new Stage starts, this gets called.
        public void ResetBonus()
        {
            DamageBonus = 0;
            DodgeBonus = 0;
            BlockBonus = 0;
            DamageReductionBonus = 0;
            CriticalBonus = 0;
        }

        // After a Status Effect's duration has reached zero, reset its bonus.
        public void ResetBonus(StatusType type)
        {
            if (type == StatusType.Damage)
                DamageBonus = 0;
            else if (type == StatusType.Block)
                BlockBonus = 0;
            else if (type == StatusType.Dodge)
                DodgeBonus = 0;
            else if (type == StatusType.DamageReduction)
                DamageReductionBonus = 0;
            else if (type == StatusType.Critical)
                CriticalBonus = 0;
        }
    }
}