using Common;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2015
{
    public class Day22 : DayBase, IDay
    {
        private const int day = 22;
        private int enemyHP = 0;
        private int enemyAttack = 0;
        private int best = int.MaxValue;

        public Day22(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            List<string> data = input.GetDataCached().SplitOnNewline();
            enemyHP = data[0].Split(' ').Last().ToInt();
            enemyAttack = data[1].Split(' ').Last().ToInt();
        }
        public int Problem1()
        {
            return Player(50, 500, 0, 0, 0, enemyHP, enemyAttack, "");
        }
        public int Problem2()
        {
            return 0;
        }

        public void Run()
        {
            int result1 = Problem1();
            WriteAnswer(1, "Result: {result}", result1);

            int result2 = Problem2();
            WriteAnswer(2, "Result: {result}", result2);
        }


        public int Round(int health, int mana, int shieldCounter, int poisonCounter, int rechargeCounter, int enemyHealth, int enemyAttack, string history, int usedMana)
        {
            int missileMana = int.MaxValue;
            int drainMana = int.MaxValue;
            int shieldMana = int.MaxValue;
            int poisonMana = int.MaxValue;
            int rechargeMana = int.MaxValue;

            history += $"{Environment.NewLine}-- Player turn --{Environment.NewLine}";
            history += $"- Player has {health} hit points, {(shieldCounter > 0 ? 7 : 0)} armor. {mana} mana {Environment.NewLine}";
            history += $"- Boss has {enemyHealth} hit points{Environment.NewLine}";

            if (shieldCounter > 0)
            {
                shieldCounter--;
                history += $"Shield's timer is now {shieldCounter}.{Environment.NewLine}";
                if (shieldCounter == 0)
                {
                    history += $"Shiled wears off, decreasing armor by 7.{Environment.NewLine}";
                }
            }

            if (poisonCounter > 0)
            {
                enemyHealth -= PoisonDamage;
                poisonCounter--;
                history += $"Poison deals {PoisonDamage}; its timer is now {poisonCounter}{Environment.NewLine}";
            }

            if (enemyHealth <= 0)
            {
                history += $"This kills the Boss.{Environment.NewLine}";
                if (mana < best)
                {
                    best = mana;
                    Console.WriteLine(history);
                    Console.WriteLine("--------------------------------------------------------");

                }
                return mana;
            }
            if (rechargeCounter > 0)
            {
                mana += RechargeAmount;
                rechargeCounter--;
                history += $"Recharge provides {RechargeAmount} mana; its timer is now {rechargeCounter}.{Environment.NewLine}";
                if (rechargeCounter == 0)
                    history += $"Recharge wears off{Environment.NewLine}";
            }

            if (mana <= MissileCost)
            {
                history += $"Out of mana{Environment.NewLine}";
                return int.MaxValue;
            }

            // Try Missile
            if (mana > MissileCost)
            {
                string localHistory = history;
                localHistory += $"Player casts Magic Missile, dealing {MissileDamage} damage.{Environment.NewLine}";

                missileMana = MissileCost;
                enemyHealth -= MissileDamage;

                localHistory += $"{Environment.NewLine}-- Boss turn --{Environment.NewLine}";
                localHistory += $"- Player has {health} hit points, {(shieldCounter > 0 ? 7 : 0)} armor. {mana - MissileCost} mana {Environment.NewLine}";
                localHistory += $"- Boss has {enemyHealth} hit points.{Environment.NewLine}";

                if (shieldCounter > 0)
                {
                    localHistory += $"Shield timer is now {shieldCounter - 1}.{Environment.NewLine}";
                    if (shieldCounter - 1 == 0)
                        localHistory += $"Shield wears off{Environment.NewLine}";
                }

                if (poisonCounter > 0)
                {
                    enemyHealth -= PoisonDamage;
                    localHistory += $"Poison deals {PoisonDamage}; its timer is now {poisonCounter - 1}.{Environment.NewLine}";
                    if (poisonCounter - 1 == 0)
                        localHistory += $"Poison wears off{Environment.NewLine}";
                }

                if (rechargeCounter > 0)
                {
                    mana += RechargeAmount;
                    localHistory += $"Recharge provides {RechargeAmount} mana, its timer is now {rechargeCounter - 1}.{Environment.NewLine}";
                    if (rechargeCounter - 1 == 0)
                        localHistory += $"Recharge wears off{Environment.NewLine}";
                }

                if (enemyHealth > 0)
                {
                    int damage = CalcDamage(enemyAttack, shieldCounter);
                    int missileHealth = health - damage;

                    localHistory += $"Boss attacks for {damage} damage{Environment.NewLine}";

                    if (missileHealth <= 0)
                    {
                        localHistory += $"This kills the player";
                        missileMana = int.MaxValue;
                    }
                    else
                    {
                        int result = Round(missileHealth, mana - MissileCost, shieldCounter - 1, poisonCounter - 1, rechargeCounter - 1, enemyHealth, enemyAttack, localHistory, usedMana + MissileCost);
                        if (result != int.MaxValue)
                        {
                            missileMana += result;
                        }
                        else
                            missileMana = int.MaxValue;
                    }
                }
                else
                {
                    localHistory += $"This kills the Boss. and the player wins.{Environment.NewLine}";
                }

            }

            // Try Drain
            if (mana > DrainCost)
            {
                string localHistory = history;
                localHistory += $"Player casts Drain, dealing {DrainDamage} and healing {DrainDamage} hit points.{Environment.NewLine}";

                drainMana = DrainCost;
                enemyHealth -= DrainDamage;
                health += DrainDamage;

                localHistory += $"{Environment.NewLine}-- Boss turn --{Environment.NewLine}";
                localHistory += $"- Player has {health} hit points, {(shieldCounter > 0 ? 7 : 0)} armor. {mana - DrainCost} mana {Environment.NewLine}";
                localHistory += $"- Boss has {enemyHealth} hit points.{Environment.NewLine}";

                if (shieldCounter > 0)
                {
                    localHistory += $"Shield timer is now {shieldCounter - 1}.{Environment.NewLine}";
                    if (shieldCounter - 1 == 0)
                        localHistory += $"Shield wears off{Environment.NewLine}";
                }

                if (poisonCounter > 0)
                {
                    enemyHealth -= PoisonDamage;
                    localHistory += $"Poison deals {PoisonDamage}; its timer is now {poisonCounter - 1}.{Environment.NewLine}";
                    if (poisonCounter - 1 == 0)
                        localHistory += $"Poison wears off{Environment.NewLine}";
                }

                if (rechargeCounter > 0)
                {
                    mana += RechargeAmount;
                    localHistory += $"Recharge provides {RechargeAmount} mana, its timer is now {rechargeCounter - 1}.{Environment.NewLine}";
                    if (rechargeCounter - 1 == 0)
                        localHistory += $"Recharge wears off{Environment.NewLine}";
                }

                if (enemyHealth > 0)
                {
                    int damage = CalcDamage(enemyAttack, shieldCounter);
                    int drainHealth = health - damage;

                    localHistory += $"Boss attacks for {damage} damage{Environment.NewLine}";

                    if (drainHealth <= 0)
                    {
                        localHistory += $"This kills the player";
                        drainMana = int.MaxValue;
                    }
                    else
                    {
                        int result = Round(drainHealth, mana - DrainCost, shieldCounter - 1, poisonCounter - 1, rechargeCounter - 1, enemyHealth, enemyAttack, localHistory, usedMana + DrainCost);
                        if (result != int.MaxValue)
                        {
                            drainMana += result;
                        }
                        else
                            drainMana = int.MaxValue;
                    }
                }
                else
                {
                    localHistory += $"This kills the Boss. and the player wins.{Environment.NewLine}";
                }
            }

            // Try Shield
            if (mana > ShieldCost && shieldCounter <= 0)
            {
                string localHistory = history;
                localHistory += $"Player casts Shield, increasing armor by {ShieldDamage}.{Environment.NewLine}";

                shieldMana = ShieldCost;
                shieldCounter = ShieldRounds;

                localHistory += $"{Environment.NewLine}-- Boss turn --{Environment.NewLine}";
                localHistory += $"- Player has {health} hit points, {(shieldCounter > 0 ? 7 : 0)} armor. {mana - ShieldCost} mana {Environment.NewLine}";
                localHistory += $"- Boss has {enemyHealth} hit points{Environment.NewLine}";

                if (shieldCounter > 0)
                {

                    localHistory += $"Shield timer is now {shieldCounter - 1}.{Environment.NewLine}";
                    if (shieldCounter - 1 == 0)
                        localHistory += $"Shield wears off{Environment.NewLine}";
                }

                if (poisonCounter > 0)
                {
                    enemyHealth -= PoisonDamage;

                    localHistory += $"Poison deals {PoisonDamage}; its timer is now {poisonCounter - 1}.{Environment.NewLine}";
                    if (poisonCounter - 1 == 0)
                        localHistory += $"Poison wears off{Environment.NewLine}";
                }

                if (rechargeCounter > 0)
                {
                    mana += RechargeAmount;
                    localHistory += $"Recharge provides {RechargeAmount} mana, its timer is now {rechargeCounter}.{Environment.NewLine}";
                    if (rechargeCounter == 0)
                        localHistory += $"Recharge wears off{Environment.NewLine}";
                }

                if (enemyHealth > 0)
                {
                    int damage = CalcDamage(enemyAttack, shieldCounter);
                    int shieldHealth = health - damage;

                    localHistory += $"Boss attacks for {damage} damage{Environment.NewLine}";

                    if (shieldHealth <= 0)
                    {
                        localHistory += $"This kills the player";
                        shieldMana = int.MaxValue;
                    }
                    else
                    {
                        int result = Round(shieldHealth, mana - ShieldCost, shieldCounter, poisonCounter, rechargeCounter, enemyHealth, enemyAttack, localHistory, usedMana + ShieldCost);
                        if (result != int.MaxValue)
                        {
                            shieldMana += result;
                        }
                        else
                            shieldMana = int.MaxValue;
                    }
                }
                else
                {
                    localHistory += $"This kills the Boss. and the player wins.{Environment.NewLine}";
                }
            }

            // Try Poison
            if (mana > PoisonCost && poisonCounter <= 0)
            {
                string localHistory = history;
                localHistory += $"Player casts Poison.{Environment.NewLine}";

                poisonMana = PoisonCost;
                poisonCounter = PoisonRounds;

                localHistory += $"{Environment.NewLine}-- Boss turn --{Environment.NewLine}";
                localHistory += $"- Player has {health} hit points, {(shieldCounter > 0 ? 7 : 0)} armor. {mana - PoisonCost} mana {Environment.NewLine}";
                localHistory += $"- Boss has {enemyHealth} hit points{Environment.NewLine}";

                if (shieldCounter > 0)
                {
                    shieldCounter--;
                    localHistory += $"Shield timer is now {shieldCounter}.{Environment.NewLine}";
                    if (shieldCounter == 0)
                        localHistory += $"Shield wears off{Environment.NewLine}";
                }

                if (poisonCounter > 0)
                {
                    enemyHealth -= PoisonDamage;
                    poisonCounter--;
                    localHistory += $"Poison deals {PoisonDamage}; its timer is now {poisonCounter}.{Environment.NewLine}";
                    if (poisonCounter == 0)
                        localHistory += $"Poison wears off{Environment.NewLine}";
                }

                if (rechargeCounter > 0)
                {
                    mana += RechargeAmount;
                    rechargeCounter--;
                    localHistory += $"Recharge provides {RechargeAmount} mana, its timer is now {rechargeCounter}.{Environment.NewLine}";
                    if (rechargeCounter == 0)
                        localHistory += $"Recharge wears off{Environment.NewLine}";
                }

                if (enemyHealth > 0)
                {
                    int damage = CalcDamage(enemyAttack, shieldCounter);
                    int poisonHealth = health - damage;

                    localHistory += $"Boss attacks for {damage} damage{Environment.NewLine}";

                    if (poisonHealth <= 0)
                    {
                        localHistory += $"This kills the player {Environment.NewLine}";
                        poisonMana = int.MaxValue;
                    }
                    else
                    {
                        int result = Round(poisonHealth, mana - PoisonCost, shieldCounter, poisonCounter, rechargeCounter, enemyHealth, enemyAttack, localHistory, usedMana + PoisonCost);
                        if (result != int.MaxValue)
                        {
                            poisonMana += result;
                        }
                        else
                            poisonMana = int.MaxValue;
                    }
                }
                else
                {
                    localHistory += $"This kills the Boss. and the player wins.{Environment.NewLine}";
                }
            }

            // Try Recharge
            if (mana > RechargeCost && rechargeCounter <= 0)
            {
                string localHistory = history;
                localHistory += $"Player casts Recharge.{Environment.NewLine}";

                rechargeMana = RechargeCost;
                rechargeCounter = RechargeRounds;

                localHistory += $"{Environment.NewLine}-- Boss turn --{Environment.NewLine}";
                localHistory += $"- Player has {health} hit points, {(shieldCounter > 0 ? 7 : 0)} armor. {mana - RechargeCost} mana {Environment.NewLine}";
                localHistory += $"- Boss has {enemyHealth} hit points{Environment.NewLine}";

                if (shieldCounter > 0)
                {
                    shieldCounter--;
                    localHistory += $"Shield timer is now {shieldCounter}.{Environment.NewLine}";
                    if (shieldCounter == 0)
                        localHistory += $"Shield wears off{Environment.NewLine}";
                }

                if (poisonCounter > 0)
                {
                    enemyHealth -= PoisonDamage;
                    poisonCounter--;
                    localHistory += $"Poison deals {PoisonDamage}; its timer is now {poisonCounter}.{Environment.NewLine}";
                    if (poisonCounter == 0)
                        localHistory += $"Poison wears off{Environment.NewLine}";
                }

                if (rechargeCounter > 0)
                {
                    mana += RechargeAmount;
                    rechargeCounter--;
                    localHistory += $"Recharge provides {RechargeAmount} mana, its timer is now {rechargeCounter}.{Environment.NewLine}";
                    if (rechargeCounter == 0)
                        localHistory += $"Recharge wears off{Environment.NewLine}";
                }

                if (enemyHealth > 0)
                {
                    int damage = CalcDamage(enemyAttack, shieldCounter);
                    int rechargeHealth = health - damage;
                    localHistory += $"Boss attacks for {damage} damage{Environment.NewLine}";

                    if (rechargeHealth <= 0)
                    {
                        localHistory += $"This kills the player";
                        rechargeMana = int.MaxValue;
                    }
                    else
                    {
                        int result = Round(rechargeHealth, mana - RechargeCost, shieldCounter, poisonCounter, rechargeCounter, enemyHealth, enemyAttack, localHistory, usedMana + RechargeCost);
                        if (result != int.MaxValue)
                        {
                            rechargeMana += result;
                            //     Console.WriteLine($"Recharge ({rechargeMana})");
                        }
                        else
                            rechargeMana = int.MaxValue;
                    }
                }
                else
                {
                    localHistory += $"This kills the Boss. and the player wins.{Environment.NewLine}";
                }
            }

            return (new int[] { missileMana, drainMana, shieldMana, poisonMana, rechargeMana }).Min();
        }

        public int Player(int health, int mana, int shieldCounter, int poisonCounter, int rechargeCounter, int enemyHealth, int enemyAttack, string history)
        {
            history += $"{Environment.NewLine}-- Player turn --{Environment.NewLine}";
            history += $"- Player has {health} hit points, {(shieldCounter > 0 ? 7 : 0)} armor. {mana} mana {Environment.NewLine}";
            history += $"- Boss has {enemyHealth} hit points{Environment.NewLine}";

            if (shieldCounter > 0)
            {
                shieldCounter--;
                history += $"Shield's timer is now {shieldCounter}.{Environment.NewLine}";
                if (shieldCounter == 0)
                {
                    history += $"Shield wears off, decreasing armor by 7.{Environment.NewLine}";
                }
            }

            if (poisonCounter > 0)
            {
                enemyHealth -= PoisonDamage;
                poisonCounter--;
                history += $"Poison deals {PoisonDamage}; its timer is now {poisonCounter}{Environment.NewLine}";
            }

            if (enemyHealth <= 0)
            {
                history += $"This kills the Boss.{Environment.NewLine}";
                if (mana < best)
                {
                    best = mana;
                    Console.WriteLine(history);
                    Console.WriteLine("--------------------------------------------------------");

                }
                return mana;
            }

            if (rechargeCounter > 0)
            {
                mana += RechargeAmount;
                rechargeCounter--;
                history += $"Recharge provides {RechargeAmount} mana; its timer is now {rechargeCounter}.{Environment.NewLine}";
                if (rechargeCounter == 0)
                    history += $"Recharge wears off{Environment.NewLine}";
            }

            if (mana <= MissileCost)
            {
                history += $"Out of mana{Environment.NewLine}";
                return int.MaxValue;
            }

            // execute actions
            return new int[] { Boss("Missile", health, mana, shieldCounter, poisonCounter, rechargeCounter, enemyHealth, enemyAttack, history),
            Boss("Drain", health, mana, shieldCounter, poisonCounter, rechargeCounter, enemyHealth, enemyAttack, history),
            Boss("Shield", health, mana, shieldCounter, poisonCounter, rechargeCounter, enemyHealth, enemyAttack, history),
            Boss("Poison", health, mana, shieldCounter, poisonCounter, rechargeCounter, enemyHealth, enemyAttack, history),
            Boss("Recharge", health, mana, shieldCounter, poisonCounter, rechargeCounter, enemyHealth, enemyAttack, history),}.Min();
        }

        int Boss(string action, int health, int mana, int shieldCounter, int poisonCounter, int rechargeCounter, int enemyHealth, int enemyAttack, string history)
        {
            int usedMana = int.MaxValue;
            switch (action)
            {
                case "Missile":
                    if (mana < MissileCost)
                        return int.MaxValue;
                    history += $"Player casts Magic Missile, dealing {MissileDamage} damage.{Environment.NewLine}";
                    usedMana = MissileCost;
                    enemyHealth -= MissileDamage;
                    break;
                case "Drain":
                    if (mana < DrainCost)
                        return int.MaxValue;
                    history += $"Player casts Drain, dealing {DrainDamage} and healing {DrainDamage} hit points.{Environment.NewLine}";
                    usedMana = DrainCost;
                    enemyHealth -= DrainDamage;
                    health += DrainDamage;
                    break;
                case "Shield":
                    if (mana < ShieldCost || shieldCounter > 0)
                        return int.MaxValue;
                    history += $"Player casts Shield, increasing armor by {ShieldDamage}.{Environment.NewLine}";
                    shieldCounter = ShieldRounds;
                    break;
                case "Poison":
                    if (mana < PoisonCost || poisonCounter > 0)
                        return int.MaxValue;
                    history += $"Player casts Poison.{Environment.NewLine}";
                    poisonCounter = PoisonRounds;
                    break;
                case "Recharge":
                    if (mana < RechargeCost || rechargeCounter > 0)
                        return int.MaxValue;
                    history += $"Player casts Recharge.{Environment.NewLine}";
                    poisonCounter = RechargeRounds;
                    break;
            }

            history += $"{Environment.NewLine}-- Boss turn --{Environment.NewLine}";
            history += $"- Player has {health} hit points, {(shieldCounter > 0 ? 7 : 0)} armor. {mana - MissileCost} mana {Environment.NewLine}";
            history += $"- Boss has {enemyHealth} hit points.{Environment.NewLine}";

            if (shieldCounter > 0)
            {
                shieldCounter--;
                history += $"Shield timer is now {shieldCounter}.{Environment.NewLine}";
                if (shieldCounter == 0)
                    history += $"Shield wears off{Environment.NewLine}";
            }

            if (poisonCounter > 0)
            {
                enemyHealth -= PoisonDamage;
                poisonCounter--;
                history += $"Poison deals {PoisonDamage}; its timer is now {poisonCounter}.{Environment.NewLine}";
                if (poisonCounter == 0)
                    history += $"Poison wears off{Environment.NewLine}";
            }

            if (rechargeCounter > 0)
            {
                mana += RechargeAmount;
                rechargeCounter--;
                history += $"Recharge provides {RechargeAmount} mana, its timer is now {rechargeCounter}.{Environment.NewLine}";
                if (rechargeCounter == 0)
                    history += $"Recharge wears off{Environment.NewLine}";
            }

            if (enemyHealth > 0)
            {
                int damage = CalcDamage(enemyAttack, shieldCounter);
                health -= damage;

                history += $"Boss attacks for {damage} damage{Environment.NewLine}";

                if (health <= 0)
                {
                    history += $"This kills the player";
                    return int.MaxValue;
                }
                else
                {
                    int result = Player(health, mana - usedMana, shieldCounter - 1, poisonCounter - 1, rechargeCounter - 1, enemyHealth, enemyAttack, history);
                    if (result != int.MaxValue)
                    {
                        usedMana += result;
                    }
                    else
                        return int.MaxValue;
                }
            }
            else
            {
                history += $"This kills the Boss. and the player wins.{Environment.NewLine}";
            }
            return usedMana;
        }

        private int CalcDamage(int damage, int shieldCounter)
        {
            if (shieldCounter > 0)
                damage -= ShieldDamage;
            return MathHelpers.Highest(1, damage);
        }

        private const int MissileCost = 53;
        private const int MissileDamage = 4;
        private const int DrainCost = 73;
        private const int DrainDamage = 2;
        private const int ShieldCost = 113;
        private const int ShieldDamage = 7;
        private const int ShieldRounds = 6;
        private const int PoisonCost = 173;
        private const int PoisonDamage = 3;
        private const int PoisonRounds = 6;
        private const int RechargeCost = 229;
        private const int RechargeAmount = 101;
        private const int RechargeRounds = 5;
    }
}
