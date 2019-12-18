using System;
using System.Collections.Generic;
using System.Linq;

namespace RiverCrossingPuzzle.Core
{
    public class Boat
    {
        public List<ICharacter> drivers { get; set; }
        public int boatCapacity { get; set; }
        public Boat()
        {

        }
        public Boat(List<ICharacter> drivers,int boatCapacity)
        {
            this.drivers = drivers;
            this.boatCapacity = boatCapacity;
        }



        public static List<ICharacter> CreateBoatDriversList(string boatDriversString, List<ICharacter> puzzleCharacters)
        {
            string[] drivers = boatDriversString.Split(',');
            List<ICharacter> boatDrivers = new List<ICharacter> { };
            boatDrivers = Utils.CreateCharObjListFromString(drivers, puzzleCharacters);
            return boatDrivers;
        }
    }
}
