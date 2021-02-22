using System.Collections.Generic;
using UnityEngine;
using PV3.Character.StatusEffects;
using PV3.ScriptableObjects.GameEvents;

namespace PV3.ScriptableObjects.Game
{
    // The asterik (*) implies that only one copy of this object is needed. A singleton might be good in this situation, but since I'm the only one working on this game
    // it's more of a visual reminder for me.
    [CreateAssetMenu(fileName = "New Status Effect Object", menuName = "Character/Status Effects List*")]
    public class CurrentStatusEffectObject : ScriptableObject
    {
        public CharacterGameBonuses BonusObject;

        [Header("Game Events")]
        [SerializeField] private GameEventObject OnStatusEffectAppliedEvent;
        [SerializeField] private GameEventObject OnStatusEffectDeductedEvent;

        [Header("")]
        public List<StatusEffect> CurrentStatusEffects = new List<StatusEffect>();

        public void AddStatusEffect(StatusEffect newEffect)
        {
            var index = CheckIfThereIsEmptySlotInList();

            if (index == -1)
                CurrentStatusEffects.Add(newEffect);
            else
                CurrentStatusEffects[index] = newEffect;

            ApplyStatusEffectBonus(newEffect);
        }

        public void ResetStatusEffectList()
        {
            for (var i = 0; i < CurrentStatusEffects.Count; i++)
            {
                if (i > 2)
                    CurrentStatusEffects.RemoveAt(i);
                else
                    RemoveStatusEffect(i);
            }

            OnStatusEffectDeductedEvent.Raise();
        }

        // Buffs are deducted at the beginning of the character's turn.
        public void FindAllBuffStatusEffects()
        {
            // Unique Status Effects (Regenerate, Linger, Stun) durations get deducted after applied.
            for (var i = 0; i < CurrentStatusEffects.Count; i++)
            {
                if (!CurrentStatusEffects[i].inUse || CurrentStatusEffects[i].isUnique || CurrentStatusEffects[i].isDebuff) continue;
                DecrementStatusEffectDuration(i);
            }

            OnStatusEffectDeductedEvent.Raise();
        }

        // Player Debuffs are deducted at the beginning of the Enemy's turn; Enemy Debuffs are deducted at the beginning of the Player's turn.
        public void FindAllDebuffStatusEffects()
        {
            // Unique Status Effects (Regenerate, Linger, Stun) durations get deducted after applied.
            for (var i = 0; i < CurrentStatusEffects.Count; i++)
            {
                if (!CurrentStatusEffects[i].inUse || CurrentStatusEffects[i].isUnique || !CurrentStatusEffects[i].isDebuff) continue;
                DecrementStatusEffectDuration(i);
            }

            OnStatusEffectDeductedEvent.Raise();
        }

        public void FindAllStatusEffectsInUseToDecrementTimer()
        {
            // Unique Status Effects (Regenerate, Linger, Stun) durations get deducted after applied.
            // Non-Unique Status Effects (Damage, Block, Dodge) durations get deducted at the end of the Character's turn.
            for (var i = 0; i < CurrentStatusEffects.Count; i++)
            {
                if (!CurrentStatusEffects[i].inUse || CurrentStatusEffects[i].isUnique) continue;
                DecrementStatusEffectDuration(i);
            }
            OnStatusEffectDeductedEvent.Raise();
        }

        public void DecrementStatusEffectDuration(int index)
        {
            CurrentStatusEffects[index].duration--;
            if (CurrentStatusEffects[index].duration != 0) return;

            BonusObject.ResetBonus(CurrentStatusEffects[index].type);
            RemoveStatusEffect(index);
            OnStatusEffectDeductedEvent.Raise();
        }

        private void RemoveStatusEffect(int index = -1)
        {
            if (index == -1) return;

            CurrentStatusEffects[index].type = StatusType.Damage;
            CurrentStatusEffects[index].bonusAmount = 0;
            CurrentStatusEffects[index].duration = 0;
            CurrentStatusEffects[index].isDebuff = false;
            CurrentStatusEffects[index].inUse = false;
            CurrentStatusEffects[index].isUnique = false;
        }

        private int CheckIfThereIsEmptySlotInList()
        {
            for (var i = 0; i < CurrentStatusEffects.Count; i++)
            {
                if (CurrentStatusEffects[i].inUse) continue;
                return i;
            }

            return -1;
        }

        private void ApplyStatusEffectBonus(StatusEffect effect)
        {
            BonusObject.InfluenceBonus(effect.isDebuff, effect.type, effect.bonusAmount);
            OnStatusEffectAppliedEvent.Raise();
        }
    }
}