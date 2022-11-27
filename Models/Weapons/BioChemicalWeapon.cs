using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetWars.Models.Weapons
{
    public class BioChemicalWeapon : Weapon
    {
        public BioChemicalWeapon(int destructionLevel) : base(3.2, destructionLevel)
        {
            this.DestructionLevel = destructionLevel;
        }

        public override double Price { get; set; }
        public override int DestructionLevel { get; set; }
    }
}
