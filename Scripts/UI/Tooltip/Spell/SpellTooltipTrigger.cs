using PV3.Character.Spells;
using PV3.ScriptableObjects.Game;
using UnityEngine;

namespace PV3.UI.Tooltip.Spell
{
    public class SpellTooltipTrigger : TooltipTrigger
    {
        [Header("")]
        [SerializeField] private SpellObject Spell;
    }
}