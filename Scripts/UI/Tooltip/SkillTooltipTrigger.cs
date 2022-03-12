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

using PV3.Miscellaneous;
using PV3.ScriptableObjects.Characters;
using PV3.ScriptableObjects.Skills;
using UnityEngine;

namespace PV3.UI.Tooltip
{
    public class SkillTooltipTrigger : MonobehaviourReference
    {
        private SkillsObject Skill;
        public CharacterObject Character;

        [Header("Pivot Points")]
        [SerializeField] protected PivotVertical PivotVertical;
        [SerializeField] protected PivotHorizontal PivotHorizontal;

        public void SetSkill(SkillsObject skill)
        {
            Skill = skill;
        }

        public void TriggerTooltip()
        {
            if (!Skill || !Character)
            {
                Debug.LogWarning($"<color=yellow>WARNING:</color> Cannot display Skill Tooltip. Ignoring request... [SKILL: {Skill}, CHARACTER: {Character.name}]");
                return;
            }

            string contentDesc = TooltipDescriptionManager.GrabSkillDescription(Skill);
            const string COOLDOWN_DESC = "PASSIVE";

            TooltipController.InitializeTooltip(Skill.Name, contentDesc, COOLDOWN_DESC);
            TooltipController.InitializePivotPointAndPosition(PivotHorizontal, PivotVertical, transform.position);
            /*string contentDesc = TooltipDescriptionManager.GrabSpellDescription(Spell, Character.Attributes);
            var cooldownDesc = $"{Spell.totalCooldown.ToString()} turn{(Spell.totalCooldown > 1 ? "s" : string.Empty)}";

            TooltipController.InitializeTooltip(Spell.name, contentDesc, cooldownDesc, Spell.staminaCost.ToString());
            TooltipController.InitializePivotPointAndPosition(PivotHorizontal, PivotVertical, transform.position);*/
        }

    }
}