using PV3.Miscellaneous;
using PV3.ScriptableObjects.Game;
using PV3.ScriptableObjects.GameEvents;
using PV3.ScriptableObjects.UI;
using UnityEngine;

namespace PV3.Character.StatusEffects
{
    public class ApplyCharacterUniqueEffects : MonobehaviourReference
    {
        [SerializeField] protected CharacterObject Character;
        [SerializeField] private FloatingTextObject FloatingTextObject;

        [Header("Game Events")]
        [SerializeField] private GameEventObject OnCharacterSpellDoneEvent;
        [SerializeField] private GameEventObject OnCharacterNotStunnedEvent;
        [SerializeField] private GameEventObject OnCharacterDisplayFloatingTextEvent;

        private int FindStatusEffectOfType(StatusType type)
        {
            for (var i = 0; i < Character.StatusEffectObject.CurrentStatusEffects.Count; i++)
            {
                if (!Character.StatusEffectObject.CurrentStatusEffects[i].inUse) continue;

                if (Character.StatusEffectObject.CurrentStatusEffects[i].type == type)
                {
                    return i;
                }
            }

            return -1;
        }

        // The Regenerate Effect gets applied at the beginning of a Character's Turn, while the Linger Effect gets applied at the end of a Character's Turn, hence the two methods.
        public void ApplyRegenerateEffect()
        {
            if (Character.CurrentHealth.Value <= 0) return;

            var index = FindStatusEffectOfType(StatusType.Regenerate);
            if (index == -1) return;

            CalculateUniqueStatusEffectValue(index, false);
        }

        public void ApplyLingerEffect()
        {
            var index = FindStatusEffectOfType(StatusType.Linger);
            if (index == -1) return;

            CalculateUniqueStatusEffectValue(index, true);
        }

        private void CalculateUniqueStatusEffectValue(int index, bool isDeduct)
        {
            var amount = Character.StatusEffectObject.CurrentStatusEffects[index].isPercentage ? Mathf.RoundToInt(Character.MaxHealth.Value * (Character.StatusEffectObject.CurrentStatusEffects[index].bonusAmount / 100f)) : Character.StatusEffectObject.CurrentStatusEffects[index].bonusAmount;

            FloatingTextObject.CreateNewFloatingText(isDeduct ? FloatingTextObject.DamageColor : FloatingTextObject.HealColor, amount.ToString());
            OnCharacterDisplayFloatingTextEvent.Raise();

            if (isDeduct)
            {
                Character.DeductHealth(amount);
            }
            else
            {
                Character.AddHealth(amount);
            }

            Character.StatusEffectObject.DecrementStatusEffectDuration(index);
        }

        public void CheckIfStunned()
        {
            var index = FindStatusEffectOfType(StatusType.Stun);

            if (index == -1)
            {
                OnCharacterNotStunnedEvent.Raise();
            }
            else
            {
                StartCoroutine(ApplyStunEffect(index));
            }
        }

        private System.Collections.IEnumerator ApplyStunEffect(int index)
        {
            print("Stun started!");
            yield return new WaitForSeconds(2f);

            OnCharacterSpellDoneEvent.Raise();
            Character.StatusEffectObject.DecrementStatusEffectDuration(index);
            print("Stun done!");
        }
    }
}