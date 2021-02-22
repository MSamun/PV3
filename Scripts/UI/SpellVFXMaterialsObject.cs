using UnityEngine;

namespace PV3.UI
{
    // The asterik (*) implies that only one copy of this object is needed. A singleton might be good in this situation, but since I'm the only one working on this game
    // it's more of a visual reminder for me.
    [CreateAssetMenu(fileName = "New Spell VFX Object", menuName = "Game/UI/Spell VFX Materials*")]
    public class SpellVFXMaterialsObject : ScriptableObject
    {
        public Material CurrentlyChosenMaterial { get; private set; }

        public void SetParticleEffect(Material material)
        {
            CurrentlyChosenMaterial = material;
        }
    }
}