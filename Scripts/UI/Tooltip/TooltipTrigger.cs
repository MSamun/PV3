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
        [SerializeField] protected PivotVertical PivotVertical;
        [SerializeField] protected PivotHorizontal PivotHorizontal;

        [Header("Tooltip Information")]
        [SerializeField] protected string header;
        [TextArea(2, 6)][SerializeField] protected string content;

        public void DisplayTooltip()
        {
            TooltipManager.SetTooltipText(content, header);
            TooltipManager.SetPivotPoint(PivotHorizontal, PivotVertical, false);
            TooltipManager.DisplayTooltip(transform.position, false);
        }
    }
}