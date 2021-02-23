using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Tooltip
{
    public class Tooltip : MonobehaviourReference
    {
        private RectTransform rectTransform;

        [SerializeField] protected int characterWrapLimit;

        [Header("UI Components")]
        [SerializeField] protected LayoutElement layoutElement;
        [SerializeField] protected TextMeshProUGUI headerText;
        [SerializeField] protected TextMeshProUGUI contentText;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (Input.touchCount <= 0) return;
            gameObject.SetActive(false);
        }

        public void SetText(string content, string header = "")
        {
            if (!headerText || !contentText) return;

            if (string.IsNullOrEmpty(header))
            {
                headerText.gameObject.SetActive(false);
            }
            else
            {
                headerText.gameObject.SetActive(true);
                headerText.text = header;
            }

            contentText.text = content;
            CheckIfNeedLayoutElement();
        }

        protected virtual void CheckIfNeedLayoutElement()
        {
            if (headerText.text == null) return;

            layoutElement.enabled = headerText.text.Length > characterWrapLimit || contentText.text.Length > characterWrapLimit;
        }

        public void SetPivotPoint(PivotHorizontal pivotHorizontal, PivotVertical pivotVertical)
        {
            rectTransform.pivot = new Vector2((int)pivotHorizontal / 2f, (int)pivotVertical / 2f);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}