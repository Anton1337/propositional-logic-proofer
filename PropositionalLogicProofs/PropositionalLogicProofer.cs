using System;
using System.Collections.Generic;
using System.Text;

namespace PropositionalLogicProofs
{
    class PropositionalLogicProofer
    {
        private static bool debug = false;
        public static bool ForwardChaining(List<string> kb, string q)
        {
            Dictionary<string, int> count = CountSetup(kb);
            Dictionary<string, bool> inferred = InferredSetup(kb);
            Stack<string> agenda = AgendaSetup(kb);

            if (debug) Debug(count, inferred, agenda);

            while(agenda.Count > 0)
            {
                string p = agenda.Pop();

                if(!inferred[p])
                {
                    inferred[p] = true;

                    // Make a copy of the keys so that we can 
                    // loop through dictionary and manipulate it mid-loop.
                    List<string> keys = new List<string>(count.Keys); 
                    foreach (string c in keys)
                    {
                        string premises = c.Split("=")[0];
                        if (premises.Contains(p))
                        {
                            count[c] -= 1;

                            if (count[c] == 0)
                            {
                                Console.WriteLine(c);
                                string cHead = c[c.Length - 1].ToString();
                                if (cHead == q) return true;
                                agenda.Push(cHead);
                            }
                        }
                    }
                }
            }
            return false;
        }

        private static Dictionary<string, int> CountSetup(List<string> kb)
        {
            Dictionary<string, int> count = new Dictionary<string, int>();

            foreach(string sentence in kb)
            {
                string premises = sentence.Split("=")[0];
                count.TryAdd(sentence, premises.Length);
            }
            return count;
        }

        private static Dictionary<string, bool> InferredSetup(List<string> kb)
        {
            Dictionary<string, bool> inferred = new Dictionary<string, bool>();

            foreach(string sentence in kb)
            {
                foreach(char c in sentence)
                {
                    if(c != '=' && c != '>')
                    {
                        inferred.TryAdd(c.ToString(), false);
                    }
                }
            }
            return inferred;
        }

        private static Stack<string> AgendaSetup(List<string> kb)
        {
            Stack<string> agenda = new Stack<string>();
            foreach(string sentence in kb)
            {
                if(!sentence.Contains("=>"))
                {
                    agenda.Push(sentence);
                }
            }
            return agenda;
        }

        private static void Debug(Dictionary<string, int> count, Dictionary<string, bool> inferred, Stack<string> agenda)
        {
            // Count.
            Console.WriteLine("--Print Count--");
            foreach (KeyValuePair<string, int> c in count)
            {
                Console.WriteLine($"{c.Key}: {c.Value}");
            }
            Console.WriteLine("----------");

            // Inferred.
            Console.WriteLine("--Print Inferred--");
            foreach (KeyValuePair<string, bool> i in inferred)
            {
                Console.WriteLine($"{i.Key}: {i.Value}");
            }
            Console.WriteLine("----------");

            // Agenda.
            Console.WriteLine("--Print Agenda--");
            foreach (string s in agenda)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("----------");
        }
    }
}
