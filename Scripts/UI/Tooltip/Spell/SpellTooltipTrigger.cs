using PV3.ScriptableObjects.Game;
using PV3.UI.SpellDescription;
using UnityEngine;

namespace PV3.UI.Tooltip.Spell
{
    public class SpellTooltipTrigger : MonobehaviourReference
    {

        public SpellObject Spell;

        public Character.CharacterObject Character;

        [Header("Pivot Points")]
        [SerializeField] protected PivotVertical PivotVertical;
        [SerializeField] protected PivotHorizontal PivotHorizontal;

        public void DisplayTooltip()
        {
            var spellDesc = SpellDescriptionManager.SetDescription(Spell, Character.Attributes);
            var cooldownDesc = $"{Spell.totalCooldown.ToString()} turn{(Spell.totalCooldown > 1 ? "s" : string.Empty)}";

            TooltipManager.SetTooltipText(spellDesc, cooldownDesc, Spell.name);
            TooltipManager.SetPivotPoint(PivotHorizontal, PivotVertical, true);
            TooltipManager.DisplayTooltip(transform.position, true);
        }
    }
}