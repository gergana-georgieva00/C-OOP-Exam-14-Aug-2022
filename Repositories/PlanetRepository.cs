using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private List<IPlanet> planets;

        public PlanetRepository()
        {
            this.planets = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => this.planets.AsReadOnly();

        public void AddItem(IPlanet planet)
        {
            this.planets.Add(planet);
        }

        public IPlanet FindByName(string name)
        {
            return this.planets.Find(p => p.Name == name);
        }

        public bool RemoveItem(string planetName)
        {
            if (this.planets.Any(p => p.Name == planetName))
            {
                this.planets = this.planets.Where(p => p.Name != planetName).ToList();
                return true;
            }

            return false;
        }
    }
}
