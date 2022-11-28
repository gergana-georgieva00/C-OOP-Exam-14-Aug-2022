using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Models.Planets
{
    public class Planet : IPlanet
    {
        private UnitRepository units;
        private WeaponRepository weapons;

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
            double totalAmount = 0;

            foreach (var unit in units.Models)
            {
                totalAmount += unit.EnduranceLevel;
            }
            foreach (var weapon in weapons.Models)
            {
                totalAmount += weapon.DestructionLevel;
            }

            if (this.units.Models.Any(u => u.GetType().Name == "AnonymousImpactUnit"))
            {
                totalAmount += totalAmount * 0.3;
            }
            if (this.weapons.Models.Any(w => w.GetType().Name == "NuclearWeapon"))
            {
                totalAmount += totalAmount * 0.45;
            }

            return totalAmount;
        }

        public IReadOnlyCollection<IMilitaryUnit> Army => units.Models;

        public IReadOnlyCollection<IWeapon> Weapons => weapons.Models;

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
