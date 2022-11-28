using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets;
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
            var type = Type.GetType(unitTypeName);
            var militaryUnit = Activator.CreateInstance(type);

            if (!this.planets.Models.Any(p => p.Name == planetName))
            {
                throw new InvalidOperationException($"Planet {planetName} does not exist!");
            }
            if (militaryUnit == null)
            {
                throw new InvalidOperationException($"{unitTypeName} still not available!");
            }
            if (planets.FindByName(planetName).Army.Any(m => m.GetType().Name == unitTypeName))
            {
                throw new InvalidOperationException($"{unitTypeName} already added to the Army of {planetName}!");
            }

            var planet = planets.FindByName(planetName);
            planet.AddUnit((IMilitaryUnit)militaryUnit);

            return $"{unitTypeName} added successfully to the Army of {planetName}!";
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            var type = Type.GetType(weaponTypeName);
            var weapon = Activator.CreateInstance(type);

            if (!this.planets.Models.Any(p => p.Name == planetName))
            {
                throw new InvalidOperationException($"Planet {planetName} does not exist!");
            }
            if (weapon == null)
            {
                throw new InvalidOperationException($"{weaponTypeName} still not available!");
            }
            if (planets.FindByName(planetName).Weapons.Any(m => m.GetType().Name == weaponTypeName))
            {
                throw new InvalidOperationException($"{weaponTypeName} already added to the Weapons of {planetName}!");
            }

            var planet = planets.FindByName(planetName);
            planet.AddWeapon((IWeapon)weapon);

            return null;
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
            throw new NotImplementedException();
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            throw new NotImplementedException();
        }

        public string SpecializeForces(string planetName)
        {
            throw new NotImplementedException();
        }
    }
}
