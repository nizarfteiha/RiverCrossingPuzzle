using System;
using System.Collections.Generic;
using System.Linq;
using RiverCrossingPuzzle.Models;

namespace RiverCrossingPuzzle.States
{
    public class RiverState 
    {
        public KeyValuePair<List<ICharacter>, List<ICharacter>> state { get; set; }
        public RiverState(KeyValuePair<List<ICharacter>, List<ICharacter>> state)
        {
            this.state = state;
        }


        public bool checkLeftRiverOnRemove(ICharacter character)
        {
            List<ICharacter> remainingChars = this.state.Key.Except(new List<ICharacter> { character }).ToList();
            if (remainingChars.Count >= 2)
            {
                
                if (remainingChars[0].riverRestricted.Where(item => item.GetType() == remainingChars[1].GetType()).ToList().FirstOrDefault() != null)
                {
                    return false;
                }
            }
            return true;
        }


        public bool checkRightRiverOnRemove(ICharacter character)
        {
            List<ICharacter> remainingChars = this.state.Value.Except(new List<ICharacter> { character }).ToList();
            if (remainingChars.Count >= 2)
            {
                if (remainingChars[0].riverRestricted.Where(item => item.GetType() == remainingChars[1].GetType()).ToList().FirstOrDefault() != null)
                {
                    return false;
                }
            }
            return true;
        }

        public bool checkLeftRiverOnAdd(ICharacter character)
        {
            List<ICharacter> charsOnLeftHandSide = this.state.Key;
            charsOnLeftHandSide.Add(character);
            if (charsOnLeftHandSide.Count == 4)
            {
                return true;
            }
            for (int i = 0; i < charsOnLeftHandSide.Count; i++)
            {
                for (int j = i + 1; j < charsOnLeftHandSide.Count; j++)
                {
                    if (charsOnLeftHandSide[j].riverRestricted.Contains(charsOnLeftHandSide[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool checkRightRiverOnAdd(ICharacter character)
        {
            List<ICharacter> charsOnRightHandSide = new List<ICharacter>( this.state.Value) ;
            charsOnRightHandSide.Add(character);
            if(charsOnRightHandSide.Count == 4)
            {
                return true;
            }
            for(int i = 0; i < charsOnRightHandSide.Count ; i++)
            {
                for(int j = i+1 ; j< charsOnRightHandSide.Count; j++)
                {
                    if (charsOnRightHandSide[j].riverRestricted.Where(item => item.GetType() == charsOnRightHandSide[i].GetType()).ToList().FirstOrDefault() != null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        override
        public string ToString()
        {
            string lhs = "";
            foreach(ICharacter character in this.state.Key)
            {
                lhs = lhs + character.ToString();
            }
            string rhs = "";
            foreach (ICharacter character in this.state.Value)
            {
                rhs = rhs + character.ToString();
            }

            return "" + lhs + " | " + rhs;
        }
    }
}
