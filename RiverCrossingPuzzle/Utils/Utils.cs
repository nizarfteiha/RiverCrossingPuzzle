using System;
using System.Collections.Generic;
using System.Linq;
using RiverCrossingPuzzle.Core;
using RiverCrossingPuzzle.States;

namespace RiverCrossingPuzzle.Core
{
    public static class Utils
    {

        /// <summary>
        /// Accepts User Inputs, And Saves to a static dictionary in the Static Class Constraints
        /// </summary>
        public static void getUserInputAndSaveToConstraints()
        {
            Console.WriteLine("Who can drive the boat? Valid inputs:G,W,C,F.");
            string drivers = Console.ReadLine();
            Console.WriteLine("What is the boat capacity?");
            string boatCapacity = Console.ReadLine();
            Console.WriteLine("What is the initial state?");
            string initialState = Console.ReadLine();
            Console.WriteLine("What are the restrictions on riverside?");
            string riverRestrictions = Console.ReadLine();
            Console.WriteLine("What are the restrictions on boat?");
            string boatRestrictions = Console.ReadLine();

            ///Construct Constraints Dictionary

            Constraints.constraints.Add("drivers", drivers);
            Constraints.constraints.Add("boatCapacity", boatCapacity);
            Constraints.constraints.Add("initialState", initialState);
            Constraints.constraints.Add("riverRestrictions", riverRestrictions);
            Constraints.constraints.Add("boatRestrictions", boatRestrictions);
        }

        /// <summary>
        /// Creates a state from a given user input
        /// </summary>
        /// <param name="userInputState">The string the user input for the state</param>
        /// <param name="puzzleChars">The Characters Involved in state creation</param>
        /// <returns></returns>
        public static State CreateState(string userInputState, List<ICharacter> puzzleChars)
        {
            string[] initialstate = userInputState.Split(',');
            List<ICharacter> riverCharacters = CreateCharObjListFromString(initialstate, puzzleChars);
            KeyValuePair<List<ICharacter>, List<ICharacter>> kvp = new KeyValuePair<List<ICharacter>, List<ICharacter>>(riverCharacters, new List<ICharacter> { });
            RiverState riverState = new RiverState(kvp);
            State initialState = new State(riverState, new BoatState());
            return initialState;


        }

        /// <summary>
        /// Creates a List Of Characters According to Corresponding Characters in String, E.G G,W creates a list of Goat,Wolf
        /// </summary>
        /// <param name="characters">The list of characters to create instances for</param>
        /// <param name="puzzleChars">The Characters Involved in List Creation</param>
        /// <returns></returns>
        public static List<ICharacter> CreateCharObjListFromString(string[] characters, List<ICharacter> puzzleChars)
        {
            List<ICharacter> charList = new List<ICharacter> { };
            foreach (string character in characters)
            {
                //Can replace with switch case
                if (character == "G")
                {
                    charList.Add(puzzleChars.Where(item => item.GetType() == typeof(Goat)).ToList().First());
                }
                else if (character == "W")
                {
                    charList.Add(puzzleChars.Where(item => item.GetType() == typeof(Wolf)).ToList().First());
                }
                else if (character == "C")
                {
                    charList.Add(puzzleChars.Where(item => item.GetType() == typeof(Cabbage)).ToList().First());
                }
                else if (character == "F")
                {
                    charList.Add(puzzleChars.Where(item => item.GetType() == typeof(Farmer)).ToList().First());
                }
            }
            return charList;
        }

        /// <summary>
        /// Modifies the actual objects restrictions according to given restriction
        /// </summary>
        /// <param name="restrictionsString">User input Restrictions</param>
        /// <param name="puzzleChars">Actual Characters in the program</param>
        /// <param name="river">Boolean flag, representing whether this restriction is for river or boat</param>
        public static void createRestrictionsForObjects(string restrictionsString,List<ICharacter> puzzleChars, bool river)
        {
            if(String.IsNullOrEmpty(restrictionsString))
            {
                return;
            }
            string[] restrictions = restrictionsString.Split(" ");
            foreach (string restriction in restrictions)
            {
                string[] restrictionSplitted = restriction.Split(',');
                ICharacter character1 = GetObjectByString(restrictionSplitted[0]);
                ICharacter character2 = GetObjectByString(restrictionSplitted[1]);
                ICharacter char1ToRestrict = puzzleChars.Where(item => item.GetType() == character1.GetType()).ToList().First();
                ICharacter char2ToRestrict = puzzleChars.Where(item => item.GetType() == character2.GetType()).ToList().First();
                if (river)
                {
                    char1ToRestrict.riverRestricted.Add(character2);
                    char2ToRestrict.riverRestricted.Add(character1);
                }

                else
                {
                    char1ToRestrict.boatRestricted.Add(character2);
                    char2ToRestrict.boatRestricted.Add(character1);
                }

            }
        }

        public static ICharacter GetObjectByString(string characterString)
        {
            switch (characterString)
            {
                case "G":
                    return new Goat();
                case "W":
                    return new Wolf();
                case "C":
                    return new Cabbage();
                case "F":
                    return new Farmer();
                default:
                    return null;
            }
        }

        public static void printState(State state)
        {

            string riverState = state.riverState.ToString();
            string boatState = state.boatState.ToString();
            Console.WriteLine("River State: {0}", riverState);
            Console.WriteLine("Boat State: {0}", boatState);
            Console.WriteLine("_______");
        }
    }
}
