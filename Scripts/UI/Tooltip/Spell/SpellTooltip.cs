using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Tooltip.Spell
{
    [ExecuteInEditMode]
    public class SpellTooltip : Tooltip
    {
        [SerializeField] private TextMeshProUGUI spellCooldownText;

        public void SetSpellText(string content, string cooldownAmount, string header = "")
        {
            spellCooldownText.text = $"{cooldownAmount} Turns";
            SetText(content, header);
        }

        protected override void CheckIfNeedLayoutElement()
        {
            if (headerText.text == null || spellCooldownText.text == null) return;

            layoutElement.enabled = headerText.text.Length + spellCooldownText.text.Length > characterWrapLimit || contentText.text.Length > characterWrapLimit;
        }
    }
}