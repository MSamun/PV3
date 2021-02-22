using System;
using UnityEngine;

namespace PV3
{
    public class DisplayAdPopup : MonobehaviourReference
    {
        [Tooltip("The value is in seconds.")]
        [SerializeField] private float popupTimer = 60f;

        private void Start()
        {
            StartCoroutine(PopupTimer());
        }

        private System.Collections.IEnumerator PopupTimer()
        {
            while (true)
            {
                yield return new WaitForSeconds(popupTimer);
                
                print("Should execute right about now!");
                gameObject.transform.GetChild(0).gameObject.SetActive(true);

                yield return new WaitForSeconds(5f);
                
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
