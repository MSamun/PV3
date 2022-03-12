// PV3 is a menu-based RPG game.
// This file is part of the PV3 distribution (https://github.com/MSamun/PV3)
// Copyright (C) 2021-2022 Matthew Samun.
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

using PV3.Characters.Common;
using PV3.Miscellaneous;
using PV3.ScriptableObjects.Spells;
using UnityEngine;

namespace PV3.UI.Tooltip
{
    public class StatusEffectTooltipTrigger : MonobehaviourReference
    {
        private StatusEffect _statusEffect;

        [Header("Pivot Points")]
        [SerializeField] protected PivotVertical _PivotVertical;
        [SerializeField] protected PivotHorizontal _PivotHorizontal;

        public void SetStatusEffect(StatusEffect effect)
        {
            _statusEffect = effect;
        }

        public void TriggerTooltip()
        {
            if (!_statusEffect.inUse) return;

            string headerDesc = _statusEffect.type == StatusType.DamageReduction ? "DMG Reduction" : $"{_statusEffect.type.ToString()}";
            string contenteDesc = TooltipDescriptionManager.GrabStatusEffectDescription(_statusEffect);
            string durationDesc = _statusEffect.duration == -1 ? "PASSIVE" : $"{_statusEffect.duration.ToString()} turn{(_statusEffect.duration > 1 ? "s" : string.Empty)}";

            TooltipController.InitializeTooltip(headerDesc, contenteDesc, durationDesc);
            TooltipController.InitializePivotPointAndPosition(_PivotHorizontal, _PivotVertical, transform.position);
        }
    }
}