using PV3.ScriptableObjects.GameEvents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.Character.Portraits
{
    public abstract class CharacterPortraitUI : MonobehaviourReference
    {
        [SerializeField] protected CharacterObject Character;

        [Header("UI Components")]
        [SerializeField] protected Image Icon;
        [SerializeField] protected TextMeshProUGUI LevelText;
        [SerializeField] protected TextMeshProUGUI NameText;

        [Header("Game Events")]
        [SerializeField] protected GameEventObject OnAttributesCalculatedEvent;

        protected virtual void Start()
        {
            PopulateUIComponents();
            InitializeCharacterValues();
        }

        public virtual void PopulateUIComponents() { }

        protected void InitializeCharacterValues()
        {
            ResetSpellCooldowns();

            Character.StatusEffectObject.ResetStatusEffectList();
            Character.StatusEffectObject.BonusObject.ResetBonus();

            Character.InitializeHealthValues();
            Character.InitializeSubAttributes();

            // Health Bar gets initialized after Attributes of Character gets set.
            OnAttributesCalculatedEvent.Raise();
        }

        private void ResetSpellCooldowns()
        {
            for (var i = 0; i < Character.ListOfSpells.Count; i++)
            {
                Character.ListOfSpells[i].isOnCooldown = false;
                Character.ListOfSpells[i].cooldownTimer = 0;
            }
        }
    }
}