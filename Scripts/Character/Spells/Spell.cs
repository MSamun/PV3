using System;
using PV3.ScriptableObjects.GameEvents;
using PV3.ScriptableObjects.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.Character.Spells
{
    public class Spell : MonobehaviourReference
    {
        [SerializeField] private int assignedSpellIndex;
        [SerializeField] private IntValue SpellUsedIndex;
        [SerializeField] private PlayerObject Player;

        [Header("UI Components")]
        [SerializeField] private Button spellButton;
        [SerializeField] private TextMeshProUGUI spellName;
        [SerializeField] private Image cooldownPanel;
        [SerializeField] private TextMeshProUGUI cooldownPanelText;

        [Header("Game Events")]
        [SerializeField] private GameEventObject OnSpellUseEvent;

        public void InitializeSpellButton()
        {
            spellButton.onClick.RemoveAllListeners();
            spellButton.onClick.AddListener(UseSpell);
            spellButton.GetComponent<Image>().sprite = Player.ListOfSpells[assignedSpellIndex].spell.sprite;

            if (spellName) spellName.text = Player.ListOfSpells[assignedSpellIndex].spell.name;
        }

        private void UseSpell()
        {
            SpellUsedIndex.Value = assignedSpellIndex;
            InitializeCooldown();
            OnSpellUseEvent.Raise();
        }

        private void InitializeCooldown()
        {
            Player.ListOfSpells[assignedSpellIndex].isOnCooldown = true;
            Player.ListOfSpells[assignedSpellIndex].cooldownTimer = Player.ListOfSpells[assignedSpellIndex].spell.totalCooldown;

            spellButton.interactable = false;
            cooldownPanel.gameObject.SetActive(true);
            UpdateCooldownPanelDisplay();
        }

        private void UpdateCooldownPanelDisplay()
        {
            cooldownPanel.fillAmount = Mathf.Abs(Player.ListOfSpells[assignedSpellIndex].cooldownTimer / (float)Player.ListOfSpells[assignedSpellIndex].spell.totalCooldown);
            cooldownPanelText.text = $"{Player.ListOfSpells[assignedSpellIndex].cooldownTimer.ToString()}";
        }

        public void DecrementCooldownTimer()
        {
            if (!Player.ListOfSpells[assignedSpellIndex].isOnCooldown) return;

            Player.ListOfSpells[assignedSpellIndex].cooldownTimer--;
            UpdateCooldownPanelDisplay();

            if (Player.ListOfSpells[assignedSpellIndex].cooldownTimer != 0) return;

            Player.ListOfSpells[assignedSpellIndex].isOnCooldown = false;
            cooldownPanel.gameObject.SetActive(false);
        }

        public void EnableSpell()
        {
            if (!Player.ListOfSpells[assignedSpellIndex].isOnCooldown && !spellButton.interactable)
                spellButton.interactable = true;
        }
    }
}