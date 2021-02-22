using UnityEngine;

namespace PV3.ScriptableObjects.UI
{
    // The asterik (*) implies that only one copy of this object is needed. A singleton might be good in this situation, but since I'm the only one working on this game
    // it's more of a visual reminder for me.
    [CreateAssetMenu(fileName = "New Status Effect Sprites Object", menuName = "Game/UI/Status Effect Sprites*")]
    public class StatusEffectSpritesObject : ScriptableObject
    {
        [Header("Status Effect Icons")]
        public Sprite damageBonusIcon;
        public Sprite blockBonusIcon;
        public Sprite dodgeBonusIcon;
        public Sprite damageReductionIcon;
        public Sprite criticalChanceIcon;
        public Sprite lingerIcon;
        public Sprite regenerateIcon;
        public Sprite stunIcon;

        [Header("Status Effect Frames")]
        public Sprite buffFrame;
        public Sprite debuffFrame;

        [Header("Status Effect Background Colours")]
        public Color buffBackgroundColour = new Color(0.04f, 0.3f, 0.04f, 1);       //Green-ish colour  (10, 76, 10, 255)
        public Color debuffBackgroundColour = new Color(0.3f, 0.04f, 0.04f, 1);     //Red-ish colour    (76, 10, 10, 255)

        [Header("Status Effect Icon Colours")]
        public Color buffIconColour = new Color(0, 0.79f, 0, 1);                    //Green     (0, 200, 0, 255)
        public Color debuffIconColour = new Color(0.79f, 0, 0, 1);                  //Red       (200, 0, 0, 255)
    }
}