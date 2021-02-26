using PV3.ScriptableObjects.Game;

namespace PV3.Character.StatusEffects
{
    [System.Serializable]
    public class StatusEffect
    {
        public StatusType type;
        public int bonusAmount;
        public int duration;
        public bool isDebuff;
        public bool isPercentage;
        public bool inUse;
        public bool isUnique;

        public StatusEffect(StatusType type = StatusType.Damage, int bonusAmount = 0, int duration = 0, bool isDebuff = false, bool isPercentage = false, bool isUnique = false, bool inUse = false)
        {
            this.type = type;
            this.bonusAmount = bonusAmount;
            this.duration = duration;
            this.isDebuff = isDebuff;
            this.isPercentage = isPercentage;
            this.inUse = inUse;
            this.isUnique = isUnique;
        }
    }
}