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

using PV3.Character.StatusEffects;
using PV3.Miscellaneous;
using PV3.ScriptableObjects.Spells;
using UnityEngine;

namespace PV3.UI.Tooltip
{
    public class StatusEffectTooltipTrigger : MonobehaviourReference
    {
        [SerializeField] private StatusEffect statusEffect;

        [Header("Pivot Points")]
        [SerializeField] protected PivotVertical PivotVertical;

        [SerializeField] protected PivotHorizontal PivotHorizontal;

        public void SetStatusEffect(StatusEffect effect)
        {
            print(effect.type.ToString());
            statusEffect = effect;
        }

        public void TriggerTooltip()
        {
            if (!statusEffect.inUse) return;

            var headerDesc = statusEffect.type == StatusType.DamageReduction ? "Damage Reduction" : $"{statusEffect.type.ToString()}";
            var contenteDesc = TooltipDescriptionManager.GrabStatusEffectDescription(statusEffect);
            var durationDesc = $"{statusEffect.duration.ToString()} turn{(statusEffect.duration > 1 ? "s" : string.Empty)}";

            TooltipController.InitializeTooltip(headerDesc, contenteDesc, durationDesc);
            TooltipController.InitializePivotPointAndPosition(PivotHorizontal, PivotVertical, transform.position);
        }
    }
}