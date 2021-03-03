// PV3 is a menu-based RPG game.
// This file is part of the PV3 distribution (https://github.com/MSamun/PV3)
// Copyright (C) 2021 Matthew Samun.
//
// This program is free software: you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the Free
// Software Foundation, version 3.
//
// This program is distributed in the hope that it will be useful, but WITHOUT
// ANY WARRANTY; without even the implied warranty of  MERCHANTABILITY or
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
// more details.
//
// You should have received a copy of the GNU General Public License along with
// this program. If not, see <http://www.gnu.org/licenses/>.

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