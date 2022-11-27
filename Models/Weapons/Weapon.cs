using PlanetWars.Models.Weapons.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetWars.Models.Weapons
{
    public abstract class Weapon : IWeapon 
    {
        private int destructionLevel;

        public Weapon(double price, int destructionLevel)
        {
            this.Price = price;

            if (destructionLevel < 1)
            {
                throw new ArgumentException("Destruction level cannot be zero or negative.");
            }
            else if (destructionLevel > 10)
            {
                throw new ArgumentException("Destruction level cannot exceed 10 power points.");
            }

            this.DestructionLevel = destructionLevel;
        }

        public abstract double Price { get; set; }

        public abstract int DestructionLevel { get; set; }
    }
}
