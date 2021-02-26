using PV3.Miscellaneous;
using PV3.ScriptableObjects.GameEvents;
using PV3.ScriptableObjects.Miscellaneous;
using UnityEngine;

namespace PV3.Character.Portraits
{
    [RequireComponent(typeof(Animator), typeof(CharacterPortraitUI))]
    public class CharacterDeathChecker : MonobehaviourReference
    {
        protected const float TRANSITION_TIME = 0.75f;
        protected Animator Animator;

        [SerializeField] protected CharacterObject Character;
        [SerializeField] protected TurnObject CurrentTurnObject;

        [Header("Game Events")]
        [SerializeField] protected GameEventObject OnCharacterNotDeadEvent;
        [SerializeField] protected GameEventObject OnHidePortraitEvent;

        protected virtual void Start()
        {
            Animator = gameObject.GetComponent<Animator>();
        }

        public virtual void CheckIfDead()
        {
            if (Character.CurrentHealth.Value > 0)
            {
                OnCharacterNotDeadEvent.Raise();
            }
            else
            {
                StartCoroutine(AnimateHidePortrait());
            }
        }

        public virtual void CheckIfDeadFromLinger()
        {
            if (Character.CurrentHealth.Value > 0) return;

            StartCoroutine(AnimateHidePortrait());
        }

        private System.Collections.IEnumerator AnimateHidePortrait()
        {
            yield return new WaitForSeconds(0.4f);
            Animator.SetTrigger("Start");
            yield return new WaitForSeconds(TRANSITION_TIME);

            OnHidePortraitEvent.Raise();
        }
    }
}