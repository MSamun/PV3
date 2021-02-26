using System.Collections;
using PV3.Miscellaneous;
using UnityEngine;

namespace PV3.UI.SceneLoader
{
    public class SceneTransition : MonobehaviourReference
    {
        [SerializeField] private Animator transitionAnim = null;
        [SerializeField] private float transitionTime = 0;

        public IEnumerator LoadSceneFadeTransition()
        {
            transitionAnim.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
        }
    }
}