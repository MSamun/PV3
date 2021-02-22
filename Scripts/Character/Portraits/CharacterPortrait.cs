using PV3.ScriptableObjects.GameEvents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.Character.Portraits
{
    [RequireComponent(typeof(Animator))]
    public class CharacterPortrait : MonobehaviourReference
    {
        protected const float TRANSITION_TIME = 0.75f;
        protected Animator portraitAnimator;

        [SerializeField] protected CharacterObject Character;

        [Header("UI Components")]
        [SerializeField] protected Image icon;
        [SerializeField] protected TextMeshProUGUI nameText;
        [SerializeField] protected TextMeshProUGUI levelText;

        [Header("Game Events")]
        [SerializeField] protected GameEventObject OnAttributesCalculatedEvent;
        [SerializeField] private GameEventObject OnCharacterNotDead;
        [SerializeField] private GameEventObject OnHidePortrait;

        protected virtual void Start()
        {
            portraitAnimator = gameObject.GetComponent<Animator>();
            PopulatePortraitUIComponents();
            InitializeCharacterValues();
        }

        public virtual void PopulatePortraitUIComponents()
        {
            icon.sprite = Character.portraitSprite;
            nameText.text = Character.name;
            levelText.text = Character.Level.Value.ToString();
        }

        protected void InitializeCharacterValues()
        {
            ResetCooldownOfAllSpells();
            ResetStatusEffects();
            ResetBonuses();

            Character.InitializeHealthValues();
            Character.InitializeSubAttributes();

            // Health Bar gets initialized after Attributes of Character gets set.
            OnAttributesCalculatedEvent.Raise();
        }

        private void ResetCooldownOfAllSpells()
        {
            for (var i = 0; i < Character.ListOfSpells.Count; i++)
            {
                Character.ListOfSpells[i].isOnCooldown = false;
                Character.ListOfSpells[i].cooldownTimer = 0;
            }
        }

        private void ResetStatusEffects()
        {
            Character.StatusEffectObject.ResetStatusEffectList();
        }

        private void ResetBonuses()
        {
            Character.StatusEffectObject.BonusObject.ResetBonus();
        }

        public void CheckIfCharacterIsDead()
        {
            if (Character.CurrentHealth.Value > 0)
            {
                OnCharacterNotDead.Raise();
            }
            else
            {
                HidePortrait();
            }
        }

        // I've had an annoying issue for a few weeks and I have finally figured out a (working) solution.
        // The check to see if a Character is dead is made in the beginning of their turn, and Linger gets applied at the end of the turn.
        // Previously, if the Character died from the damage caused by Linger, it would cause some unexpected behaviour.
        // For example, if Enemy #1 died from Linger, the game would skip Enemy #2, and go straight to Enemy #3.
        public virtual void CheckIfCharacterIsDeadFromLinger()
        {
            if (Character.CurrentHealth.Value <= 0)
            {
                HidePortrait();
            }
            else
            {
                /*GameManager.Instance.CurrentTurnObject.SwitchToEnemyTurn();*/
            }
        }

        protected void HidePortrait()
        {
            StartCoroutine(AnimateHidePortrait());
        }

        private System.Collections.IEnumerator AnimateHidePortrait()
        {
            yield return new WaitForSeconds(0.4f);
            portraitAnimator.SetTrigger("Start");
            yield return new WaitForSeconds(TRANSITION_TIME);

            OnHidePortrait.Raise();
        }
    }
}