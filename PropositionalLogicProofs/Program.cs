using System;
using System.Collections.Generic;

namespace PropositionalLogicProofs
{
    class Program
    {
        /*
         * TODO:
         * Maybe get a "sentence" class that holds the body & head of the sentence in separate variables.
         * Looping through the dictionary and modifying it causes issues! check:
         * https://stackoverflow.com/questions/15418467/how-can-i-write-these-variables-into-one-line-of-code-in-c
         */
        static void Main(string[] args)
        {
            //RunExample1();
            RunExample2();

            Console.ReadLine(); // Makes sure console doesn't close.
        }

        static void RunExample1()
        {
            List<string> kb = new List<string>() {"A", "B", "AB=>L", "AP=>L", "BL=>M", "LM=>P", "P=>Q" };
            string q = "Q";
            bool success = PropositionalLogicProofer.ForwardChaining(kb, q);
            Console.WriteLine(success);
        }

        static void RunExample2()
        {
            List<string> kb = new List<string>() { "A=>G", "AG=>F", "C", "C=>A", "FC=>H", "H=>F", "HF=>M", "MA=>B" };
            string q = "B";
            bool success = PropositionalLogicProofer.ForwardChaining(kb, q);
            Console.WriteLine(success);
        }
    }
}
