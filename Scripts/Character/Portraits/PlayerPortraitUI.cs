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
            var data = DataManager.LoadDataFromJson().PlayerData;

            Icon.sprite = PortraitSprites.Icons[data.BaseData.PortraitID];
            NameText.text = data.BaseData.Name;
            LevelText.text = data.BaseData.Level.ToString();
        }
    }
}