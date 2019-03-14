namespace Eco.Simulation.Types
{
    using System;
    using REYmod.Config;
    using Shared.Math;

    public class BalancedPlantSpecies : PlantSpecies
    {
        private Range _resourceRange = new Range(-1, -1);
        private float _seedDropChance = -1f;

        public BalancedPlantSpecies()
        {
            REYmodSettings.OnPlantYieldChange.Add(_setBalancedResourceRange);
            REYmodSettings.OnSeedDropChange.Add(_setBalancedSeedDropChance);
        }

        public new Range ResourceRange {
            get { return base.ResourceRange; }
            set {
                if (_resourceRange.Min < 0)
                {
                    _resourceRange = value;
                }
                _setBalancedResourceRange();
            }
        }

        public new float SeedDropChance {
            get { return base.SeedDropChance; }
            set {
                if (_seedDropChance < 0)
                {
                    _seedDropChance = value;
                }
                _setBalancedSeedDropChance();
            }
        }

        protected virtual void _setBalancedResourceRange(float multiplier = 1f)
        {
            base.ResourceRange = new Range(_resourceRange.Min, _resourceRange.Max * multiplier);
        }
        
        protected virtual void _setBalancedSeedDropChance(float multiplier = 1f)
        {
            base.SeedDropChance = _seedDropChance * multiplier;
        }
    }
}