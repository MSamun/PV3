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

using DG.Tweening;
using PV3.Miscellaneous;
using PV3.ScriptableObjects.Character;
using PV3.ScriptableObjects.Game;
using UnityEngine;

namespace PV3.Character.ValueChecks
{
    public abstract class CharacterDeathCheck : MonobehaviourReference
    {
        [SerializeField] protected CharacterObject Character;
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Game Events")]
        [SerializeField] private GameEventObject OnCharacterNotDeadEvent;
        [SerializeField] private GameEventObject OnCharacterNotDeadFromLingerEvent;
        [SerializeField] private GameEventObject OnCharacterDeadEvent;

        public void CheckIfCharacterIsDead(bool isCheckFromLinger = false)
        {
            // When a Character dies from either a Spell or a Linger Effect, the functionality is the same for both cases (Enemy = move on to next enemy; Player = defeat).
            // When a Character does not die from a Spell or a Linger Effect, the functionality is different, depending on the case:
            // i.e. Enemy not dying from a Player Spell: first checks if Player has enough Stamina to use another Spell; if Player has enough Stamina, continue with Player Turn; else, go to Enemy Turn.
            // Player not dying from a Linger Effect: go to Enemy Turn. Linger Effect on a Character gets applied at the end of their turn.
            if (Character.IsDead())
            {
                FadePortraitOut();
            }
            else
            {
                // Not dead.
                if (isCheckFromLinger)
                    OnCharacterNotDeadFromLingerEvent.Raise();
                else
                    OnCharacterNotDeadEvent.Raise();
            }
        }

        private void FadePortraitOut()
        {
            canvasGroup.DOFade(0, 1f).OnComplete(() => OnCharacterDeadEvent.Raise());
        }

        public void FadePortraitIn()
        {
            canvasGroup.DOFade(1, 1f);
        }
    }
}