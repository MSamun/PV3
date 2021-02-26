using PV3.Miscellaneous;
using PV3.ScriptableObjects.UI;
using PV3.ScriptableObjects.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Home
{
    public class ChooseCharacterPortrait : MonobehaviourReference
    {
        private int index;
        [SerializeField] private IntValue PortraitIconIndex;
        [SerializeField] private PortraitIconSpritesObject PortraitIconObject;
        [SerializeField] private bool useFemalePortraits;

        [Header("UI")]
        [SerializeField] private Button Button;
        [SerializeField] private Image IconFocus;

        private void Start()
        {
            // Originally, I had Male and Female Portrait Icons in separate arrays, but it was causing some graphical and serialization issues.
            // Female Portrait Icons start at Element 32 in the array, so the index needs to start at 32.
            // I'm aware of the whole "magic number" concept; this method is sure to come back and bite me in the ass.
            // I'll adjust the code to fix the apparent issue with this solution... hopefully.
            // "This is nothing more than a band-aid solution. I will fix this in the future." - every developer in existence
            index = useFemalePortraits ? transform.GetSiblingIndex() + 32 : transform.GetSiblingIndex();
            Button.GetComponent<Image>().sprite = PortraitIconObject.Icons[index];
            ResetPortraitFocus();
        }

        public void ResetPortraitFocus()
        {
            if (IconFocus.gameObject.activeInHierarchy && PortraitIconIndex.Value != index)
            {
                IconFocus.gameObject.SetActive(false);
            }

            if (!IconFocus.gameObject.activeInHierarchy && PortraitIconIndex.Value == index)
            {
                IconFocus.gameObject.SetActive(true);
            }
        }

        public void SetPortraitIcon()
        {
            IconFocus.gameObject.SetActive(true);
            PortraitIconIndex.Value = index;
        }

        public void OnEnable()
        {
            ResetPortraitFocus();
        }
    }
}