using Common;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{

    public class Day24 : DayBase, IDay
    {
        public List<Combatant> Combatants { get; set; }
        public Day24() : base(2018, 24)
        {
            string[] data = input.GetDataCached().SplitOnNewlineArray();
            string currentType = string.Empty;
            Combatants = new List<Combatant>();
            int counter = 1;
            foreach (string line in data)
            {
                if (line.EndsWith(":"))
                {
                    currentType = line.Replace(":", "");
                    counter = 1;
                }
                else if (line.Length > 1)
                {
                    string[] t = line.Tokenize();


                    Combatant c = new Combatant();
                    c.Type = currentType;
                    c.Count = int.Parse(t[0]);
                    c.HP = int.Parse(t[4]);
                    c.Immune = new List<string>();
                    c.Weaknesses = new List<string>();
                    int i = 7;
                    string addTo = string.Empty;
                    while (t[i] != "with")
                    {
                        if (t[i].Replace("(", "") == "weak")
                        {
                            addTo = "weakness";
                            i++;
                        }
                        else if (t[i].Replace("(", "") == "immune")
                        {
                            addTo = "immune";
                            i++;
                        }
                        else
                        {
                            if (addTo == "weakness")
                            {
                                c.Weaknesses.Add(t[i].Replace(",", "").Replace(";", "").Replace(")", ""));
                            }

                            if (addTo == "immune")
                            {
                                c.Immune.Add(t[i].Replace(",", "").Replace(";", "").Replace(")", ""));
                            }
                        }
                        i++;
                    }
                    while (t[i++] != "does")
                    {
                        ;
                    }

                    c.AttackDamage = int.Parse(t[i++]);
                    c.Attacktype = t[i];
                    c.Initiative = int.Parse(t.Last());
                    c.Name = c.Type + " group " + counter++;

                    Combatants.Add(c);
                }
            }

        }

        public void Run()
        {
            string result1 = Problem1();
            Console.WriteLine($"P1: {result1}");

            string result2 = Problem2();
            Console.WriteLine($"P2: {result2}");
        }

        public string Problem1()
        {
            bool done = false;
            while (!done)
            {
                //Console.WriteLine("Targeting");
                List<Target> round = new List<Target>();
                foreach (Combatant a in Combatants.Where(o => o.Count > 0).OrderByDescending(o => o.EffectivePower))
                {
                    Combatant best = null;
                    int bestAttackPower = 0;
                    foreach (Combatant t in Combatants.Where(o => o.Type != a.Type && o.Count > 0 && !round.Select(r => r.Defender).Contains(o)).OrderByDescending(o => o.EffectivePower).ThenBy(o => o.Initiative))
                    {
                        if (t.DefendAgainst(a.Attacktype, a.EffectivePower) > bestAttackPower)
                        {
                            best = t;
                            bestAttackPower = t.DefendAgainst(a.Attacktype, a.EffectivePower);
                        }
                    }
                    if (best != null)
                    {
                        round.Add(new Target { Attacker = a, Defender = best });
                    };
                }
                //Console.WriteLine("Attacking");
                {
                    foreach (Target t in round.OrderByDescending(o => o.Attacker.Initiative))
                    {
                        int attackPower = t.Defender.DefendAgainst(t.Attacker.Attacktype, t.Attacker.EffectivePower);
                        int killed = attackPower / t.Defender.HP;
                        t.Defender.Count -= killed;
                        if (t.Defender.Count < 0)
                        {
                            t.Defender.Count = 0;
                        }
                    }
                }

                if (Combatants.Where(c => c.Type == "Immune System").Select(c => c.Count).Sum() <= 0)
                {
                    return $"Infection wins with {Combatants.Where(c => c.Type == "Infection").Select(c => c.Count).Sum()}";
                }
                if (Combatants.Where(c => c.Type == "Infection").Select(c => c.Count).Sum() <= 0)
                {
                    return $"Immune System wins with {Combatants.Where(c => c.Type == "Immune System").Select(c => c.Count).Sum()}";
                }
            }
            return string.Empty;
        }

        public string Problem2()
        {
            bool ImmuneWinner = false;
            int boost = 0;

            List<Combatant> reset = Combatants.Select(c => new Combatant { AttackDamage = c.AttackDamage, Attacktype = c.Attacktype, Count = c.Count, HP = c.HP, Immune = c.Immune, Initiative = c.Initiative, Type = c.Type, Weaknesses = c.Weaknesses, Name = c.Name }).ToList();
            int lastCount = 0;

            while (!ImmuneWinner)
            {
                bool done = false;
                Combatants = reset.Select(c => new Combatant { AttackDamage = c.AttackDamage, Attacktype = c.Attacktype, Count = c.Count, HP = c.HP, Immune = c.Immune, Initiative = c.Initiative, Type = c.Type, Weaknesses = c.Weaknesses, Name = c.Name }).ToList();
                foreach (Combatant c in Combatants.Where(o => o.Type == "Immune System"))
                {
                    c.AttackDamage += boost;
                }

                //                Print();

                while (!done)
                {
                    //Console.WriteLine("Targeting");
                    List<Target> round = new List<Target>();
                    foreach (Combatant a in Combatants.Where(o => o.Count > 0).OrderByDescending(o => o.EffectivePower).ThenByDescending(o => o.Initiative))
                    {
                        Combatant best = null;
                        int bestAttackPower = 0;
                        foreach (Combatant t in Combatants.Where(o => o.Type != a.Type && o.Count > 0).OrderByDescending(o => o.EffectivePower).ThenByDescending(o => o.Initiative))
                        {
                            int attackPower = t.DefendAgainst(a.Attacktype, a.EffectivePower);
                            //                            if (a.Type == "Immune System")
                            //                                attackPower += boost;

                            //                            Console.WriteLine($"{a.Name} would deal {t.Name} {attackPower} damage");

                            if (attackPower > bestAttackPower && !round.Select(r => r.Defender).Contains(t))
                            {
                                best = t;
                                bestAttackPower = t.DefendAgainst(a.Attacktype, a.EffectivePower);
                            }
                        }
                        if (best != null)
                        {
                            round.Add(new Target { Attacker = a, Defender = best });
                        };
                    }
                    //Console.WriteLine("Atacking");
                    {
                        foreach (Target t in round.OrderByDescending(o => o.Attacker.Initiative))
                        {
                            int attackPower = t.Defender.DefendAgainst(t.Attacker.Attacktype, t.Attacker.EffectivePower);
                            int killed = attackPower / t.Defender.HP;
                            //                          Console.WriteLine($"{t.Attacker.Name} attacks {t.Defender.Name}, killing {killed} units");
                            t.Defender.Count -= killed;
                            if (t.Defender.Count < 0)
                            {
                                //                              Console.WriteLine($"{t.Defender.Name} is defeated");
                                t.Defender.Count = 0;
                            }
                        }
                    }

                    if (Combatants.Where(c => c.Type == "Immune System").Select(c => c.Count).Sum() <= 0)
                    {
                        return $"Infection wins with {Combatants.Where(c => c.Type == "Infection").Select(c => c.Count).Sum()} ({boost})";

                        done = true;
                    }
                    if (Combatants.Where(c => c.Type == "Infection").Select(c => c.Count).Sum() <= 0)
                    {
                        return $"Immune System wins with {Combatants.Where(c => c.Type == "Immune System").Select(c => c.Count).Sum()}, ({boost})";
                        done = true;
                        ImmuneWinner = true;
                    }

                    if (lastCount == Combatants.Select(c => c.Count).Sum())
                    {
                        return $"Draw ({boost})";
                        done = true;
                    }

                    lastCount = Combatants.Select(c => c.Count).Sum();
                }

                boost++;
            }
            return string.Empty;
        }

        public void Print()
        {
            foreach (Combatant c in Combatants)
            {
                Console.WriteLine($"{c.Name} Count: {c.Count}, HP: {c.HP}, AttackPower: {c.AttackDamage}, AttackType: {c.Attacktype} Immune:{string.Join(",", c.Immune)} Weak: {string.Join(",", c.Weaknesses)}, Initiative: {c.Initiative}");
            }
        }

        public class Target
        {
            public Combatant Attacker;
            public Combatant Defender;
        }


        public class Combatant
        {
            public string Type { get; set; }
            public int Count { get; set; }
            public int HP { get; set; }
            public string Attacktype { get; set; }
            public int AttackDamage { get; set; }
            public List<string> Weaknesses { get; set; }
            public List<string> Immune { get; set; }
            public int Initiative { get; set; }
            public string Name { get; set; }

            public int DefendAgainst(string attack, int HP)
            {
                if (Immune.Contains(attack))
                {
                    return 0;
                }


                if (Weaknesses.Contains(attack))
                {
                    return HP * 2;
                }

                return HP;
            }

            public int EffectivePower
            {
                get { return AttackDamage * Count; }
            }
        }
    }
}
