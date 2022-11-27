using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetWars.Models.Planets
{
    public class Planet : IPlanet
    {
        //private UnitRepository units;
        //private WeaponRepository weapons;

        public Planet(string name, double budget)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Planet name cannot be null or empty.");
            }

            this.Name = name;
            this.Budget = 0;

            if (budget < 0)
            {
                throw new ArgumentException("Budget's amount cannot be negative.");
            }

            this.Budget = budget;
        }

        public string Name { get; }

        public double Budget { get; }

        public double MilitaryPower => Math.Round(this.CalculateMilitaryPower(), 3);

        private double CalculateMilitaryPower()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<IMilitaryUnit> Army { get; }

        public IReadOnlyCollection<IWeapon> Weapons { get; }

        public void AddUnit(IMilitaryUnit unit)
        {
            throw new NotImplementedException();
        }

        public void AddWeapon(IWeapon weapon)
        {
            throw new NotImplementedException();
        }

        public string PlanetInfo()
        {
            throw new NotImplementedException();
        }

        public void Profit(double amount)
        {
            throw new NotImplementedException();
        }

        public void Spend(double amount)
        {
            throw new NotImplementedException();
        }

        public void TrainArmy()
        {
            throw new NotImplementedException();
        }
    }
}
