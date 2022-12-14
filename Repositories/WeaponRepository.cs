using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private List<IWeapon> weapons;

        public WeaponRepository()
        {
            weapons = new List<IWeapon>();
        }

        public IReadOnlyCollection<IWeapon> Models => this.weapons.AsReadOnly();

        public void AddItem(IWeapon weapon)
        {
            this.weapons.Add(weapon);
        }

        public IWeapon FindByName(string weaponType)
        {
            if (this.weapons.Any(w => w.GetType().Name == weaponType))
            {
                return this.weapons.Find(w => w.GetType().Name == weaponType);
            }

            return null;
        }

        public bool RemoveItem(string weaponTypeName)
        {
            if (this.weapons.Any(w => w.GetType().Name == weaponTypeName))
            {
                this.weapons = this.weapons.Where(w => w.GetType().Name != weaponTypeName).ToList();
                return true;
            }

            return false;
        }
    }
}
