using PlanetWars.Models.Weapons.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetWars.Models.Weapons
{
    public abstract class Weapon : IWeapon 
    {
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

        public double Price { get; }

        public virtual int DestructionLevel { get; }
    }
}
