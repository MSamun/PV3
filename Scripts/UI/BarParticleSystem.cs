using PV3.ScriptableObjects.Variables;
using UnityEngine;

namespace PV3.UI
{
    public class BarParticleSystem : MonobehaviourReference
    {
        [SerializeField] private IntValue MaxBarValue;
        [SerializeField] private IntValue CurrentBarValue;

        [Header("Particle System Components")]
        [SerializeField] private ParticleSystem BarInteriorParticleSystem = null;
        [Range(0.5f, 2f)] [SerializeField] private float ParticleEmissionWidth = 1.05f;
        [Range(10, 50)] [SerializeField] private int MaximumParticleEmission = 17;
        [Range(30, 70)] [SerializeField] private int ParticleEmissionRate = 30;

        private void Start()
        {
            BarInteriorParticleSystem.Play();
            AdjustParticlesBasedOffCurrentBarValue();
        }

        public void AdjustParticlesBasedOffCurrentBarValue()
        {
            // Can't adjust the particle system directly; have to introduce variables to each module to alter them.
            var shapeModule = BarInteriorParticleSystem.shape;
            var mainModule = BarInteriorParticleSystem.main;
            var emissionModule = BarInteriorParticleSystem.emission;

            var barFillPercentage = (float)CurrentBarValue.Value / MaxBarValue.Value;
            barFillPercentage = Mathf.Round(barFillPercentage * 100f) / 100f;

            var barEmissionRate = (int) (ParticleEmissionRate * barFillPercentage);

            shapeModule.radius = ParticleEmissionWidth * barFillPercentage;
            shapeModule.radius = Mathf.Clamp(shapeModule.radius, 0, ParticleEmissionWidth);

            mainModule.maxParticles = (int)(MaximumParticleEmission * barFillPercentage);
            mainModule.maxParticles = Mathf.Clamp(mainModule.maxParticles, 0, MaximumParticleEmission);

            emissionModule.rateOverTime = Mathf.Clamp(barEmissionRate, 0, ParticleEmissionRate);
        }
    }
}