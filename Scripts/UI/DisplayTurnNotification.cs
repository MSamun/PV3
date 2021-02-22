using UnityEngine;
using TMPro;

namespace PV3.UI
{
    public class DisplayTurnNotification : MonobehaviourReference
    {
        private TextMeshProUGUI turnNotificationText;
        private const string PLAYER_COLOR_TEXT = "#96FF00";
        private const string ENEMY_COLOR_TEXT = "#E13232";

        [SerializeField] private GameObject TurnNotificationObject;

        private void Awake()
        {
            turnNotificationText = TurnNotificationObject.gameObject.GetComponentInChildren<TextMeshProUGUI>(true);
        }

        public void DisplaySpellUseNotification(string casterName, string spellName, string targetName, bool isCasterPlayer)
        {
            if (!TurnNotificationObject.gameObject.activeInHierarchy) TurnNotificationObject.gameObject.SetActive(true);

            turnNotificationText.text = $"<color={(isCasterPlayer ? PLAYER_COLOR_TEXT : ENEMY_COLOR_TEXT)}>{casterName}</color> uses <color=#FFA000>{spellName}</color> on <color={(isCasterPlayer ? ENEMY_COLOR_TEXT : PLAYER_COLOR_TEXT)}>{targetName}</color>.";
        }

        public void DisplaySpellUseNotification(string casterName, string spellName, bool isCasterPlayer)
        {
            if (!TurnNotificationObject.gameObject.activeInHierarchy) TurnNotificationObject.gameObject.SetActive(true);

            // Different text for Healing Spells.
            turnNotificationText.text = $"<color={(isCasterPlayer ? PLAYER_COLOR_TEXT : ENEMY_COLOR_TEXT)}>{casterName}</color> uses <color=#FFA000>{spellName}</color>.";
        }
    }
}