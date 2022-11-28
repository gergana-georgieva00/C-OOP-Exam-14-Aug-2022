using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Core
{
    public class Controller : IController
    {
        private PlanetRepository planets;

        public Controller()
        {
            planets = new PlanetRepository();
        }

        public string AddUnit(string unitTypeName, string planetName)
        {
            if (!this.planets.Models.Any(p => p.Name == planetName))
            {
                throw new InvalidOperationException($"Planet {planetName} does not exist!");
            }

            var type = Type.GetType($"PlanetWars.Models.MilitaryUnits.{unitTypeName}");

            if (type == null)
            {
                throw new InvalidOperationException($"{unitTypeName} still not available!");
            }
            if (planets.FindByName(planetName).Army.Any(m => m.GetType().Name == unitTypeName))
            {
                throw new InvalidOperationException($"{unitTypeName} already added to the Army of {planetName}!");
            }

            var militaryUnit = Activator.CreateInstance(type);
            var planet = planets.FindByName(planetName);
            planet.AddUnit((IMilitaryUnit)militaryUnit);

            return $"{unitTypeName} added successfully to the Army of {planetName}!";
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            if (!this.planets.Models.Any(p => p.Name == planetName))
            {
                throw new InvalidOperationException($"Planet {planetName} does not exist!");
            }

            var type = Type.GetType($"PlanetWars.Models.MilitaryUnits.{weaponTypeName}");

            if (type == null)
            {
                throw new InvalidOperationException($"{weaponTypeName} still not available!");
            }

            var weapon = Activator.CreateInstance(type);

            if (planets.FindByName(planetName).Weapons.Any(m => m.GetType().Name == weaponTypeName))
            {
                throw new InvalidOperationException($"{weaponTypeName} already added to the Weapons of {planetName}!");
            }

            var planet = planets.FindByName(planetName);
            planet.AddWeapon((IWeapon)weapon);

            return $"{planetName} purchased {weaponTypeName}!";
        }

        public string CreatePlanet(string name, double budget)
        {
            if (planets.Models.Any(p => p.Name == name))
            {
                return $"Planet {name} is already added!";
            }

            var newPlanet = new Planet(name, budget);
            planets.AddItem(newPlanet);
            return $"Successfully added Planet: {name}";
        }

        public string ForcesReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");
            foreach (var planet in this.planets.Models.OrderByDescending(p => p.MilitaryPower).ThenBy(p => p.Name))
            {
                sb.AppendLine(planet.PlanetInfo());
            }

            return sb.ToString().Trim();
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            var planet1 = this.planets.FindByName(planetOne);
            var planet2 = this.planets.FindByName(planetTwo);

            var winner = planet1;
            var loser = planet2;

            if (planet1.MilitaryPower > planet2.MilitaryPower)
            {
                winner = planet1;
                loser = planet2;
            }
            else if (planet1.MilitaryPower < planet2.MilitaryPower)
            {
                winner = planet2;
                loser = planet1;
            }
            else
            {
                if (planet1.Weapons.Any(w => w.GetType().Name == "Nuclear weapon") && planet2.Weapons.Any(w => w.GetType().Name == "Nuclear weapon"))
                {
                    planet1.Spend(planet1.Budget / 2);
                    planet2.Spend(planet2.Budget / 2);
                    return "The only winners from the war are the ones who supply the bullets and the bandages!";
                }
                else if (planet1.Weapons.Any(w => w.GetType().Name == "Nuclear weapon"))
                {
                    winner = planet1;
                }
                else if (planet2.Weapons.Any(w => w.GetType().Name == "Nuclear weapon"))
                {
                    winner = planet2;
                }
                else
                {
                    planet1.Spend(planet1.Budget / 2);
                    planet2.Spend(planet2.Budget / 2);
                    return "The only winners from the war are the ones who supply the bullets and the bandages!";
                }
            }

            winner.Spend(winner.Budget / 2);
            winner.Profit(loser.Budget / 2);
            loser.Spend(loser.Budget / 2);

            foreach (var weapon in loser.Weapons)
            {
                winner.Profit(weapon.Price);
            }
            foreach (var unit in loser.Army)
            {
                winner.Profit(unit.Cost);
            }

            planets.RemoveItem(loser.Name);

            return $"{winner.Name} destructed {loser.Name}!";
        }

        public string SpecializeForces(string planetName)
        {
            if (!this.planets.Models.Any(p => p.Name == planetName))
            {
                throw new InvalidOperationException($"Planet {planetName} does not exist!");
            }
            if (this.planets.Models.Any(p => p.Army.Count != 0))
            {
                throw new InvalidOperationException("No units available for upgrade!");
            }

            var planet = this.planets.FindByName(planetName);
            planet.TrainArmy();
            planet.Spend(1.25);

            return $"{planetName} has upgraded its forces!";
        }
    }
}
