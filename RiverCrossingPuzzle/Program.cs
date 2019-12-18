using System;
using System.Collections.Generic;
using System.Linq;
using RiverCrossingPuzzle.Core;
using RiverCrossingPuzzle.States;

namespace RiverCrossingPuzzle
{
    class Program
    {
        static void Main(string[] args)
        {

            ///Create Objects Required for the story to operate
            List<ICharacter> puzzleChars = new List<ICharacter> { new Farmer(), new Goat(), new Wolf(), new Cabbage() };
            ///Get User Input
            Utils.getUserInputAndSaveToConstraints();
            ///Link Objects in the list to the constraints
            Boat boat = new Boat(Boat.CreateBoatDriversList(Constraints.constraints["drivers"], puzzleChars), Int32.Parse(Constraints.constraints["boatCapacity"]));
            State currentState = Utils.CreateState(Constraints.constraints["initialState"], puzzleChars);
            Utils.createRestrictionsForObjects(Constraints.constraints["riverRestrictions"], puzzleChars, river: true);
            Utils.createRestrictionsForObjects(Constraints.constraints["boatRestrictions"], puzzleChars, river: false);
            bool isDone = false;
            ICharacter driver = null;
            bool isStuck = false;
            while (!isDone)
            {
                //Start Algorithm
                if (currentState.riverState.state.Key.Count == 0 && currentState.boatState.peopleInsideBoat.Count == 0)
                {
                    //Nothing on left river side, also nothing on boat, done
                    Console.WriteLine("Done!");
                    Utils.printState(currentState);
                }
                else
                {
                    if ((currentState.boatState.peopleInsideBoat.Count == 0))
                    {
                        driver = currentState.riverState.state.Key.FirstOrDefault(charc => boat.drivers.Contains(charc));
                        if (driver == null)
                        {
                            Console.WriteLine("No drivers, done");
                            isDone = true;
                            break;
                        }
                        currentState.riverState.state.Key.Remove(driver);
                        currentState.boatState.peopleInsideBoat.Add(driver);
                    }
                    else
                    {
                        List<ICharacter> characters = new List<ICharacter>(currentState.riverState.state.Key);
                        foreach (ICharacter character in characters)
                        {
                            if (currentState.boatState.peopleInsideBoat.Count < boat.boatCapacity)
                            {

                                if (character.boatRestricted.Where(item => item.GetType() ==
                                driver.GetType()).ToList().FirstOrDefault() == null && currentState.riverState.
                                checkLeftRiverOnRemove(character))
                                {
                                    currentState.riverState.state.Key.Remove(character);
                                    currentState.boatState.peopleInsideBoat.Add(character);
                                    Utils.printState(currentState);
                                }

                            }
                            else
                            {
                                if (isStuck)
                                {
                                    //Exchange
                                    currentState.riverState.state.Key.Add(currentState.boatState.peopleInsideBoat.Where(item => item.GetType() != driver.GetType()).ToList().FirstOrDefault());
                                    currentState.boatState.peopleInsideBoat.Remove(currentState.boatState.peopleInsideBoat.Where(item => item.GetType() != driver.GetType()).ToList().FirstOrDefault());
                                    currentState.boatState.peopleInsideBoat.Add(currentState.riverState.state.Key.ElementAt(0));
                                    currentState.riverState.state.Key.RemoveAt(0);
                                    Utils.printState(currentState);
                                    isStuck = false;
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }

                        }

                        //We have a ready trip, need to move to other side
                        //Check if this is the last move
                        if (currentState.riverState.state.Key.Count == 0)
                        {
                            List<ICharacter> peopleInsideBoat = new List<ICharacter>(currentState.boatState.peopleInsideBoat);
                            foreach (ICharacter character in peopleInsideBoat)
                            {

                                currentState.riverState.state.Value.Add(character);
                                currentState.boatState.peopleInsideBoat.Remove(character);
                            }
                            Utils.printState(currentState);
                            isDone = true;
                            break;
                        }
                        else
                        {
                            List<ICharacter> peopleInsideBoat = new List<ICharacter>(currentState.boatState.peopleInsideBoat);
                            foreach (ICharacter character in peopleInsideBoat)
                            {
                                //Do not Let him down if driver, otherwise let down and check if no conflicts on RHS
                                if (boat.drivers.Where(item => item.GetType() == character.GetType()).ToList().FirstOrDefault() == null && currentState.riverState.checkRightRiverOnAdd(character))
                                {
                                    currentState.boatState.peopleInsideBoat.Remove(character);
                                    currentState.riverState.state.Value.Add(character);
                                    Utils.printState(currentState);
                                }
                                else if (boat.drivers.Where(item => item.GetType() == character.GetType()).ToList().FirstOrDefault() == null)
                                {
                                    //Stuck, can't remove to right hand side - Remove on left hand side and get other
                                    currentState.boatState.peopleInsideBoat.Add(currentState.riverState.state.Value[0]);
                                    currentState.riverState.state.Value.RemoveAt(0);
                                    currentState.boatState.peopleInsideBoat.Remove(character);
                                    currentState.riverState.state.Value.Add(character);
                                    isStuck = true;
                                }
                            }
                        }

                    }

                }
            }
            Utils.printState(currentState);
            Console.WriteLine("Done!");
        }
    }
}
