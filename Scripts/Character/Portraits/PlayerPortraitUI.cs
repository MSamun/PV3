using PV3.ScriptableObjects.UI;
using PV3.Serialization;
using UnityEngine;

namespace PV3.Character.Portraits
{
    public class PlayerPortraitUI : CharacterPortraitUI
    {
        [SerializeField] private PortraitIconSpritesObject PortraitSprites;

        public override void PopulateUIComponents()
        {
            var data = DataManager.LoadDataFromJson().PlayerData.BaseData;

            Icon.sprite = PortraitSprites.Icons[data.PortraitID];
            NameText.text = data.Name;
            LevelText.text = data.Level.ToString();
        }
    }
}