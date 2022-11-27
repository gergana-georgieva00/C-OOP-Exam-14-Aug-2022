using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
{
    public class UnitRepository : IRepository<IMilitaryUnit>
    {
        private List<IMilitaryUnit> models;

        public UnitRepository()
        {
            this.models = new List<IMilitaryUnit>();
        }

        public IReadOnlyCollection<IMilitaryUnit> Models => this.models.AsReadOnly();

        public void AddItem(IMilitaryUnit unit)
        {
            this.models.Add(unit);
        }

        public IMilitaryUnit FindByName(string inputTypeName)
        {
            return this.models.Find(u => u.GetType().Name == inputTypeName);
        }

        public bool RemoveItem(string inputTypeName)
        {
            if (this.models.Any(m => m.GetType().Name == inputTypeName))
            {
                this.models = this.models.Where(m => m.GetType().Name != inputTypeName).ToList();
                return true;
            }

            return false;
        }
    }
}
