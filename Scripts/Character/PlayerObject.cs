using UnityEngine;

namespace PV3.Character
{
    // The asterik (*) implies that only one copy of this object is needed. A singleton might be good in this situation, but since I'm the only one working on this game
    // it's more of a visual reminder for me.
    [CreateAssetMenu(fileName = "New Player", menuName = "Character/Player*")]
    public class PlayerObject : CharacterObject { }
}