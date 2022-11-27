using PlanetWars.Models.MilitaryUnits.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetWars.Models.MilitaryUnits
{
    public abstract class MilitaryUnit : IMilitaryUnit
    {
        private int endurance;

        public MilitaryUnit(double cost)
        {
            this.Cost = cost;
            this.endurance = 1;
        }

        public double Cost { get; }

        public int EnduranceLevel { get { return this.endurance; } }

        public void IncreaseEndurance()
        {
            this.endurance++;

            if (endurance > 20)
            {
                this.endurance = 20;
                throw new ArgumentException("Endurance level cannot exceed 20 power points.");
            }
        }
    }
}
