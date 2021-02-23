using System.Runtime.CompilerServices;
using PV3.UI.Tooltip.Spell;
using UnityEngine;

namespace PV3.UI.Tooltip
{
    public class TooltipManager : MonobehaviourReference
    {
        private static TooltipManager Instance;

        [SerializeField] private Tooltip tooltip;
        [SerializeField] private SpellTooltip spellTooltip;

        private void Awake()
        {
            Instance = this;
        }

        // Used for non-Spell Tooltips (Attributes, Buttons in Home Screen, or any UI Component that needs more information.)
        public static void SetTooltipText(string content, string header)
        {
            Instance.tooltip.gameObject.SetActive(true);
            Instance.tooltip.SetText(content, header);
        }

        // Used for Spell Tooltips.
        public static void SetTooltipText(string content, string cooldown, string header)
        {
            Instance.spellTooltip.gameObject.SetActive(true);
            Instance.spellTooltip.SetSpellText(content, cooldown, header);
        }

        public static void SetPivotPoint(PivotHorizontal pivotHorizontal, PivotVertical pivotVertical, bool isSpellTooltip)
        {
            if (isSpellTooltip)
            {
                Instance.spellTooltip.SetPivotPoint(pivotHorizontal, pivotVertical);
            }
            else
            {
                Instance.tooltip.SetPivotPoint(pivotHorizontal, pivotVertical);
            }
        }

        public static void DisplayTooltip(Vector3 position, bool isSpellTooltip)
        {
            if (isSpellTooltip)
            {
                Instance.spellTooltip.SetPosition(position);
            }
            else
            {
                Instance.tooltip.SetPosition(position);
            }
        }


    }
}