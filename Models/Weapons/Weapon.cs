using PlanetWars.Models.Weapons.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetWars.Models.Weapons
{
    public abstract class Weapon : IWeapon 
    {
        public abstract double Price { get; set; }

        public abstract int DestructionLevel { get; set; }
    }
}
