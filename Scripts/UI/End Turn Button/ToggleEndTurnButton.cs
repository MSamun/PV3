using UnityEngine;
using UnityEngine.UI;

namespace PV3
{
    public class ToggleEndTurnButton : MonobehaviourReference
    {
        private Button _button = null;
        [SerializeField] private Image _image = null;

        private void Awake()
        {
            _button = GetComponentInChildren<Button>(true);
        }

        public void DisableButton()
        {
            _button.interactable = false;
            _image.color = new Color(0.59f, 0.59f, 0.59f, 1);
        }

        public void EnableButton()
        {
            _button.interactable = true;
            _image.color = Color.white;
        }
    }
}