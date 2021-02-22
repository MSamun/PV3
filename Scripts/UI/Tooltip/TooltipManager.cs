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

        public static void SetTooltipText(string content, string header = "")
        {
            Instance.tooltip.gameObject.SetActive(true);
            Instance.tooltip.SetText(content, header);
        }

        public static void SetPivotPoint(PivotHorizontal pivotHorizontal, PivotVertical pivotVertical)
        {
            Instance.tooltip.SetPivotPoint(pivotHorizontal, pivotVertical);
        }

        public static void DisplayTooltip(Vector3 position)
        {
            Instance.tooltip.SetPosition(position);
        }
    }
}