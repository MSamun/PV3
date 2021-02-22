using PV3.ScriptableObjects.Game;
using PV3.ScriptableObjects.GameEvents;
using PV3.ScriptableObjects.Variables;
using UnityEngine;

namespace PV3.Character.Spells
{
    public class EnemySpellLogic : MonobehaviourReference
    {
        private DetermineCurrentEnemyInStage scriptReference;
        private EnemyObject currentEnemy;

        [Header("Enemy Spell Index", order = 1)]
        [SerializeField] private IntValue EnemySpellIndex;

        [Header("Spell Use Event", order = 1)]
        [SerializeField] private GameEventObject OnSpellUseEvent;

         private void Start()
        {
            scriptReference = GetComponentInParent<DetermineCurrentEnemyInStage>();
            DetermineCurrentEnemy();
        }

        public void DecrementCooldownTimersOfAllSpells()
        {
            for (var i = 0; i < currentEnemy.ListOfSpells.Count; i++)
            {
                if (!currentEnemy.ListOfSpells[i].isOnCooldown) continue;
                currentEnemy.ListOfSpells[i].cooldownTimer--;

                if (currentEnemy.ListOfSpells[i].cooldownTimer != 0) continue;
                currentEnemy.ListOfSpells[i].isOnCooldown = false;
            }
        }

        public void DetermineCurrentEnemy()
        {
            currentEnemy = (EnemyObject)scriptReference.FindCurrentEnemy();
        }

        public void UseSpellBasedOffOrderListType()
        {
            StartCoroutine(InitiateLogic());
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private System.Collections.IEnumerator InitiateLogic()
        {
            yield return new WaitForSeconds(1.25f);

            const int NO_SPELL_FOUND_INDEX = -1;
            var localIndex = NO_SPELL_FOUND_INDEX;

            if (EnemyHasLessThanHalfHealth())
            {
                localIndex = GrabIndexOfAvailableSpellType(SpellType.Heal);
            }

            if (localIndex == NO_SPELL_FOUND_INDEX)
            {
                for (var i = 0; i < currentEnemy.orderOfSpellTypes.Length; i++)
                {
                    if (currentEnemy.orderOfSpellTypes[i] == SpellType.Heal) continue;

                    localIndex = GrabIndexOfAvailableSpellType(currentEnemy.orderOfSpellTypes[i]);
                    if (localIndex != NO_SPELL_FOUND_INDEX) break;
                }
            }

            // If Enemy has no available Spells to use, then use the first Spell in its List of Spells, regardless of the Spell's current status.
            // This line of code should never execute, but serves as a solution to a rare edge-case.
            if (localIndex == NO_SPELL_FOUND_INDEX)
            {
                localIndex = 0;
                Debug.LogError("Rare edge-case found! Enemy has no available Spells to use. Using the first Spell...");
            }

            EnemySpellIndex.Value = localIndex;
            PutSpellOnCooldown(localIndex);
            OnSpellUseEvent.Raise();

            yield return null;
        }
        private bool EnemyHasLessThanHalfHealth()
        {
            return currentEnemy.CurrentHealth.Value < currentEnemy.MaxHealth.Value / 2;
        }

        private int GrabIndexOfAvailableSpellType(SpellType type)
        {
            for (var i = 0; i < currentEnemy.ListOfSpells.Count; i++)
            {
                if (currentEnemy.ListOfSpells[i].spell.spellType == type && !SpellIsOnCooldown(i))
                {
                    return i;
                }
            }
            return -1;
        }

        private bool SpellIsOnCooldown(int index)
        {
            return currentEnemy.ListOfSpells[index].isOnCooldown;
        }

        private void PutSpellOnCooldown(int index)
        {
            currentEnemy.ListOfSpells[index].isOnCooldown = true;
            currentEnemy.ListOfSpells[index].cooldownTimer = currentEnemy.ListOfSpells[index].spell.totalCooldown;
        }
    }
}