using PV3.ScriptableObjects.Game;
using UnityEngine;

namespace PV3.UI.Tooltip.Spell
{
    public class SpellTooltipTrigger : MonobehaviourReference
    {
        public SpellObject Spell;

        [Header("Pivot Points")]
        [SerializeField] protected PivotVertical PivotVertical;
        [SerializeField] protected PivotHorizontal PivotHorizontal;

        public void DisplayTooltip()
        {
            TooltipManager.SetTooltipText(Spell.SetDescription(), Spell.totalCooldown.ToString(), Spell.name);
            TooltipManager.SetPivotPoint(PivotHorizontal, PivotVertical, true);
            TooltipManager.DisplayTooltip(transform.position, true);
        }
    }
}