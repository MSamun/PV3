// PV3 is a menu-based RPG game.
// This file is part of the PV3 distribution (https://github.com/MSamun/PV3)
// Copyright (C) 2021 Matthew Samun.
//
// This program is free software: you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the Free
// Software Foundation, version 3.
//
// This program is distributed in the hope that it will be useful, but WITHOUT
// ANY WARRANTY; without even the implied warranty of  MERCHANTABILITY or
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
// more details.
//
// You should have received a copy of the GNU General Public License along with
// this program. If not, see <http://www.gnu.org/licenses/>.

using PV3.Miscellaneous;
using PV3.ScriptableObjects.Game;
using UnityEngine;

namespace PV3.Characters.Common
{
    public class BarParticleSystem : MonobehaviourReference
    {
        [SerializeField] private IntValue MaxBarValue;
        [SerializeField] private IntValue CurrentBarValue;

        [Header("Particle System Components")]
        [SerializeField] private ParticleSystem BarInteriorParticleSystem;

        [Range(0.5f, 2f)] [SerializeField] private float ParticleEmissionWidth = 1.15f;
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

            var barFillPercentage = (float) CurrentBarValue.Value / MaxBarValue.Value;
            barFillPercentage = Mathf.Round(barFillPercentage * 100f) / 100f;

            var barEmissionRate = (int) (ParticleEmissionRate * barFillPercentage);

            shapeModule.radius = ParticleEmissionWidth * barFillPercentage;
            shapeModule.radius = Mathf.Clamp(shapeModule.radius, 0, ParticleEmissionWidth);

            mainModule.maxParticles = (int) (MaximumParticleEmission * barFillPercentage);
            mainModule.maxParticles = Mathf.Clamp(mainModule.maxParticles, 0, MaximumParticleEmission);

            emissionModule.rateOverTime = Mathf.Clamp(barEmissionRate, 0, ParticleEmissionRate);
        }
    }
}