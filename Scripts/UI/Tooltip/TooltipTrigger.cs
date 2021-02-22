using UnityEngine;

namespace PV3.UI.Tooltip
{
    public enum PivotHorizontal
    {
        Right,
        Center,
        Left
    }

    public enum PivotVertical
    {
        Top,
        Center,
        Bottom
    }

    public class TooltipTrigger : MonobehaviourReference
    {
        [Header("Pivot Points")]
        [SerializeField] private PivotVertical PivotVertical;
        [SerializeField] private PivotHorizontal PivotHorizontal;

        [Header("Tooltip Information")]
        [SerializeField] private string header;
        [TextArea(2, 6)][SerializeField] private string content;

        public void DisplayTooltip()
        {
            TooltipManager.SetTooltipText(content, header);
            TooltipManager.SetPivotPoint(PivotHorizontal, PivotVertical);
            TooltipManager.DisplayTooltip(transform.position);
        }
    }
}