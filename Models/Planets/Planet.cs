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

        public double Budget { get; private set; }

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
            this.units.AddItem(unit);
        }

        public void AddWeapon(IWeapon weapon)
        {
            this.weapons.AddItem(weapon);
        }

        public string PlanetInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Planet: {this.Name}");
            sb.AppendLine($"--Budget: {this.Budget} billion QUID");

            List<string> unitNames = new List<string>();
            foreach (var unit in this.units.Models)
            {
                unitNames.Add(unit.GetType().Name);
            }

            if (unitNames.Count == 0)
            {
                sb.AppendLine("No units");
            }
            else
            {
                sb.AppendLine($"--Forces: {string.Join(", ", unitNames)}");
            }

            List<string> weaponNames = new List<string>();
            foreach (var weapon in this.weapons.Models)
            {
                weaponNames.Add(weapon.GetType().Name);
            }

            if (weaponNames.Count == 0)
            {
                sb.AppendLine("No weapons");
            }
            else
            {
                sb.AppendLine($"--Combat equipment: {string.Join(", ", weaponNames)}");
            }

            sb.AppendLine($"--Military Power: {this.MilitaryPower}");

            return sb.ToString();
        }

        public void Profit(double amount)
        {
            this.Budget += amount;
        }

        public void Spend(double amount)
        {
            if (this.Budget < amount)
            {
                throw new InvalidOperationException("Budget too low!");
            }

            this.Budget -= amount;
        }

        public void TrainArmy()
        {
            foreach (var unit in units.Models)
            {
                unit.IncreaseEndurance();
            }
        }
    }
}
