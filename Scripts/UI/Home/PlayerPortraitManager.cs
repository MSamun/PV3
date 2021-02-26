using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PV3.Character;
using PV3.Miscellaneous;
using PV3.ScriptableObjects.UI;
using PV3.ScriptableObjects.Variables;
using PV3.Serialization;

namespace PV3.UI.Home
{
    public class PlayerPortraitManager : MonobehaviourReference
    {
        private TMP_Dropdown genderDropdown;
        private TMP_InputField playerNameInput;
        private string tempPlayerName;

        [SerializeField] private PlayerObject Player;
        [SerializeField] private IntValue PortraitIconIndex;
        [SerializeField] private PortraitIconSpritesObject PortraitIconsObject;

        [Header("UI")]
        [SerializeField] private GameObject GenderDropdownObject;
        [SerializeField] private Image PlayerPortraitImageComponent;
        [SerializeField] private GameObject PlayerNameInputObject;
        [SerializeField] private GameObject MalePortraitContainer;
        [SerializeField] private GameObject FemalePortraitContainer;
        private void Awake()
        {
            genderDropdown = GenderDropdownObject.GetComponent<TMP_Dropdown>();
            playerNameInput = PlayerNameInputObject.GetComponent<TMP_InputField>();

            PlayerPortraitImageComponent.sprite = Player.portraitSprite;
            playerNameInput.text = Player.name;
        }

        public void ShowAppropriatePortraitsDependingOnDropdown()
        {
            if (genderDropdown.value == 0)
            {
                MalePortraitContainer.SetActive(true);
                FemalePortraitContainer.SetActive(false);
            }
            else
            {
                FemalePortraitContainer.SetActive(true);
                MalePortraitContainer.SetActive(false);
            }
        }

        public void UpdateTemporaryPortraitIcon()
        {
            PlayerPortraitImageComponent.sprite = PortraitIconsObject.Icons[PortraitIconIndex.Value];
        }

        public void UpdateTemporaryCharacterName()
        {
            if (playerNameInput.text.Length > 0)
            {
                tempPlayerName = playerNameInput.text;
            }
        }

        public void SetPlayerPortraitIconAndName()
        {
            var baseData = new BaseData(tempPlayerName, PortraitIconIndex.Value, (int)Player.Class, Player.Level.Value);
            DataManager.UpdatePlayerBaseData(baseData);
            DataManager.SaveDataToJson();
        }

        private void OnEnable()
        {
            PortraitIconIndex.Value = DataManager.LoadDataFromJson().PlayerData.BaseData.PortraitID;
            UpdateTemporaryPortraitIcon();
            playerNameInput.text = DataManager.LoadDataFromJson().PlayerData.BaseData.Name;
        }

        private void OnDisable()
        {
            var attributeData = new AttributeData(Player.Attributes.Strength, Player.Attributes.Dexterity, Player.Attributes.Constitution,
                Player.Attributes.Intelligence, Player.Attributes.Armor);
            DataManager.UpdatePlayerAttributeData(attributeData);

            for (var i = 0; i < Player.ListOfSpells.Count; i++)
            {
                var spellData = new SpellData(Player.ListOfSpells[i].spell.spellID);
                DataManager.UpdatePlayerSpellData(spellData, i);
            }

            DataManager.SaveDataToJson();
        }
    }
}