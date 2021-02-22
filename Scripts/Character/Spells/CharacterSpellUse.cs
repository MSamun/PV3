using UnityEngine;
using PV3.Character.StatusEffects;
using PV3.UI;
using PV3.Audio;
using PV3.ScriptableObjects.Game;
using PV3.ScriptableObjects.GameEvents;
using PV3.ScriptableObjects.UI;
using PV3.ScriptableObjects.Variables;

namespace PV3.Character.Spells
{
    public abstract class CharacterSpellUse : MonobehaviourReference
    {
        private bool isHealSpell;
        protected bool IsCasterPlayer;
        private bool canStatusEffectBeApplied;

        protected DetermineCurrentEnemyInStage determineCurrentEnemyScriptReference;
        private DisplayTurnNotification displayTurnNotificationScriptReference;

        [Header("Character Objects")]
        [SerializeField] protected CharacterObject Caster;
        [SerializeField] protected CharacterObject Target;
        [SerializeField] protected IntValue SpellIndex;

        [Header("Game Events")]
        [SerializeField] private GameEventObject OnCasterSpellDoneEvent;
        [SerializeField] private GameEventObject OnSpellSFXPlayEvent;

        [Header("Floating Text Objects")]
        [SerializeField] private FloatingTextObject FloatingTextObject;
        [SerializeField] private GameEventObject OnCasterFloatingTextDisplayEvent;
        [SerializeField] private GameEventObject OnTargetFloatingTextDisplayEvent;

        [Header("Spell VFX Objects")]
        [SerializeField] private SpellVFXMaterialsObject SpellVfxObject;
        [SerializeField] private GameEventObject OnCasterPortraitDisplaySpellVfxEvent;
        [SerializeField] private GameEventObject OnTargetPortraitDisplaySpellVfxEvent;

        protected virtual void Start()
        {
            determineCurrentEnemyScriptReference = GetComponentInParent<DetermineCurrentEnemyInStage>();
            displayTurnNotificationScriptReference = GetComponentInParent<DisplayTurnNotification>();
        }

        protected virtual void DisplayResultsOfTurn(bool isHealSpell = false)
        {
            if (isHealSpell)
            {
                displayTurnNotificationScriptReference.DisplaySpellUseNotification(Caster.name, Caster.ListOfSpells[SpellIndex.Value].spell.name,
                IsCasterPlayer);
            }
            else
            {
                displayTurnNotificationScriptReference.DisplaySpellUseNotification(Caster.name, Caster.ListOfSpells[SpellIndex.Value].spell.name,
                    Target.name, IsCasterPlayer);
            }
        }

        public void CheckComponentForNullBeforeExecution()
        {
            canStatusEffectBeApplied = true;
            isHealSpell = false;

            for (var i = 0; i < Caster.ListOfSpells[SpellIndex.Value].spell.components.Count; i++)
            {
                if (!Caster.ListOfSpells[SpellIndex.Value].spell.components[i]) continue;
                DetermineSpellComponent(Caster.ListOfSpells[SpellIndex.Value].spell.components[i]);
            }

            DisplayResultsOfTurn(isHealSpell);
            OnCasterSpellDoneEvent.Raise();
        }

        private void DetermineSpellComponent(SpellComponentObject component)
        {
            switch (component)
            {
                case DamageComponent damageComponent:
                    ExecuteDamageComponent(damageComponent);
                    break;
                case HealComponent healComponent:
                    isHealSpell = true;
                    ExecuteHealComponent(healComponent);
                    break;
                case StatusComponent statusComponent:
                    ExecuteStatusEffectComponent(statusComponent);
                    break;
            }
        }

        private void ExecuteDamageComponent(DamageComponent component)
        {
            canStatusEffectBeApplied = false;

            for (var i = 0; i < component.numberOfAttacks; i++)
            {
                var damage = CalculateSpellDamage(component);

                if (Target.HasBlockedAttack())
                {
                    FloatingTextObject.CreateNewFloatingText(FloatingTextObject.BlockedColor, "Blocked");
                    OnTargetFloatingTextDisplayEvent.Raise();

                    HealTargetFromBlockedAttack(damage);
                }
                else if (Target.HasDodgedAttack())
                {
                    FloatingTextObject.CreateNewFloatingText(FloatingTextObject.DodgedColor, "Dodged");
                    OnTargetFloatingTextDisplayEvent.Raise();

                    DamageCasterFromDodgedAttack(damage);
                }
                else
                {
                    if (Caster.HasLandedCriticalStrike())
                    {
                        damage *= 2;
                        FloatingTextObject.CreateNewFloatingText(FloatingTextObject.DodgedColor, "CRITICAL!");
                        OnTargetFloatingTextDisplayEvent.Raise();
                    }

                    canStatusEffectBeApplied = true;
                    Target.DeductHealth(damage);

                    FloatingTextObject.CreateNewFloatingText(FloatingTextObject.DamageColor, damage.ToString());
                    OnTargetFloatingTextDisplayEvent.Raise();

                    AudioManager.SpellSFX = Caster.ListOfSpells[SpellIndex.Value].spell.SfxClip;
                    OnSpellSFXPlayEvent.Raise();

                    PlaySpellVFXOnCaster(false);

                    if (component.healPercentage > 0)
                    {
                        ApplyLifesteal(damage, component.healPercentage);
                    }
                }
            }
        }

        private int CalculateSpellDamage(SpellComponentObject component)
        {
            var baseDamage = component.usePercentage ? component.GetPercentageOfValue(Target.MaxHealth.Value) : component.GetRandomValueBetweenRange();
            var attributeBonus = Mathf.RoundToInt(Caster.GetAttribute(component.attributeType) * component.attributePercentage);
            var damageBonus = Mathf.RoundToInt((baseDamage + attributeBonus) * (Caster.GetDamageBonus() / 100f));

            var subtotalDamage = baseDamage + attributeBonus + damageBonus;
            var damageReduction = Mathf.RoundToInt(subtotalDamage * (Target.GetDamageReduction() / 100f));

            var totalDamage = subtotalDamage - damageReduction;

            print($"Base Damage: {baseDamage.ToString()}");
            print($"Attribute Bonus: {attributeBonus.ToString()}");
            print($"Damage Bonus: {damageBonus.ToString()}");
            print($"Subtotal Damage: {subtotalDamage.ToString()}");
            print($"Damage Reduction: {damageReduction.ToString()}");
            print($"Total Damage: {totalDamage.ToString()}");

            if (totalDamage <= 0) totalDamage = 0;
            return totalDamage;
        }

        private void HealTargetFromBlockedAttack(int damage = 0)
        {
            var healAmount = Mathf.RoundToInt(damage * 0.25f);

            // Spell dealing a low amount of damage led to having the character heal for zero; I think healing for a minimum of one is the better route to take.
            // Although, it should only heal for one when the damage dealt does not equal to zero.
            if (healAmount <= 0 && damage != 0) healAmount = 1;
            Target.AddHealth(healAmount);

            FloatingTextObject.CreateNewFloatingText(FloatingTextObject.HealColor, healAmount.ToString());
            OnTargetFloatingTextDisplayEvent.Raise();
            PlaySpellVFXOnCaster(false);
        }

        private void DamageCasterFromDodgedAttack(int damage = 0)
        {
            var damageDealt = Mathf.RoundToInt(damage * 0.25f);

            if (damageDealt <= 0 && damage != 0) damageDealt = 1;
            Caster.DeductHealth(damageDealt);

            FloatingTextObject.CreateNewFloatingText(FloatingTextObject.DamageColor, damageDealt.ToString());
            OnCasterFloatingTextDisplayEvent.Raise();
            PlaySpellVFXOnCaster(true);
        }

        private void ApplyLifesteal(int damage = 0, float percentage = 0f)
        {
            var healAmount = Mathf.RoundToInt(damage * percentage);

            if (healAmount <= 0 && damage != 0) healAmount = 1;
            Caster.AddHealth(healAmount);

            FloatingTextObject.CreateNewFloatingText(FloatingTextObject.HealColor, healAmount.ToString());
            OnCasterFloatingTextDisplayEvent.Raise();
        }

        private void ExecuteHealComponent(HealComponent component)
        {
            var baseHeal = component.usePercentage ? component.GetPercentageOfValue(Caster.MaxHealth.Value) : component.GetRandomValueBetweenRange();
            var attributeBonus = Mathf.RoundToInt(Caster.GetAttribute(component.attributeType) * component.attributePercentage);
            var healBonus = Mathf.RoundToInt((baseHeal + attributeBonus) * (Caster.GetDamageBonus() / 100f));

            var totalHeal = baseHeal + attributeBonus + healBonus;

            if (totalHeal <= 0) totalHeal = 0;
            Caster.AddHealth(totalHeal);

            FloatingTextObject.CreateNewFloatingText(FloatingTextObject.HealColor, totalHeal.ToString());
            OnCasterFloatingTextDisplayEvent.Raise();

            PlaySpellVFXOnCaster(true);
        }

        private void ExecuteStatusEffectComponent(StatusComponent component)
        {
            if (!canStatusEffectBeApplied) return;

            var attributeBonus = Mathf.RoundToInt(Caster.GetAttribute(component.attributeType) * component.attributePercentage);
            var totalBonus = component.GetRandomValueBetweenRange() + attributeBonus;

            // Cannot self-inflict debuffs. Caster can only apply buffs to themselves and debuffs to Target.
            var statusEffectObject = component.isDebuff ? Target.StatusEffectObject : Caster.StatusEffectObject;
            var newStatusEffect = new StatusEffect(component.StatusType == StatusType.Random ? component.ChooseRandomStatusType() : component.StatusType, totalBonus,
                component.duration, component.isDebuff, component.usePercentage, component.isUnique, true);

            statusEffectObject.AddStatusEffect(newStatusEffect);
            DetermineWhichFloatingTextToDisplayBasedOnStatusEffect(newStatusEffect);
        }

        private void DetermineWhichFloatingTextToDisplayBasedOnStatusEffect(StatusEffect effect)
        {
            Color color;
            string text;

            if (effect.type == StatusType.Stun)
            {
                color = FloatingTextObject.StunnedColor;
                text = "Stunned";
            }
            else
            {
                color = effect.isDebuff ? FloatingTextObject.DamageColor : FloatingTextObject.HealColor;
                text = effect.type.ToString();

                if (!effect.isUnique)
                {
                    text += effect.isDebuff ? "--" : "++";
                }
            }

            FloatingTextObject.CreateNewFloatingText(color, text);

            if (effect.isDebuff)
            {
                OnTargetFloatingTextDisplayEvent.Raise();
            }
            else
            {
                OnCasterFloatingTextDisplayEvent.Raise();
            }
        }

        private void PlaySpellVFXOnCaster(bool playEffectOnCaster)
        {
            SpellVfxObject.SetParticleEffect(Caster.ListOfSpells[SpellIndex.Value].spell.VfxMaterial);

            if (playEffectOnCaster)
            {
                OnCasterPortraitDisplaySpellVfxEvent.Raise();
            }
            else
            {
                OnTargetPortraitDisplaySpellVfxEvent.Raise();
            }
        }
    }
}