using PV3.Miscellaneous;
using UnityEngine;

namespace PV3.UI.FloatingText
{
    public class HideFloatingText : MonobehaviourReference
    {
        private const float DELAY_TIME = 0.75f;

        private void OnEnable()
        {
            transform.localPosition += new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
            StartCoroutine(HideObject());
        }

        private System.Collections.IEnumerator HideObject()
        {
            yield return new WaitForSeconds(DELAY_TIME);
            gameObject.SetActive(false);
        }
    }
}