using DG.Tweening;
using PV3.Miscellaneous;
using UnityEngine;

namespace PV3.UI.Stages
{
    public class DisplayCombatCanvas : MonobehaviourReference
    {
        [SerializeField] private CanvasGroup canvasGroup;

        public void Display()
        {
            canvasGroup.DOFade(1f, 0.75f);
        }
    }
}