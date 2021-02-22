using UnityEngine;

namespace PV3.UI
{
    public class DisplaySpellVFX : MonobehaviourReference
    {
        private ParticleSystem spellParticle;
        private ParticleSystemRenderer spellParticleRenderer;

        [SerializeField] private SpellVFXMaterialsObject spellVfxMaterials;
        private void Awake()
        {
            spellParticle = GetComponentInChildren<ParticleSystem>();
            spellParticleRenderer = spellParticle.GetComponent<ParticleSystemRenderer>();
        }

        public void PlaySpellVfx()
        {
            if (!spellVfxMaterials.CurrentlyChosenMaterial) return;

            spellParticleRenderer.material = spellVfxMaterials.CurrentlyChosenMaterial;
            spellParticle.Play();
        }
    }
}