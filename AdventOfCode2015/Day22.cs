using Common;

namespace AdventOfCode2015
{
    public class Day22 : DayBase, IDay
    {
        private const int day = 22;
        private int enemyHP = 0;
        private int enemyAttack = 0;
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
            return Round(50, 500, 0, 0, 0, enemyHP, enemyAttack);
        }
        public int Problem2()
        {


            return 0;
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Result: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Result: {result2}");
        }


        public int Round(int health, int mana, int shieldCounter, int poisonCounter, int rechargeCounter, int enemyHealth, int enemyAttack)
        {
            int missileMana = int.MaxValue;
            int drainMana = int.MaxValue;
            int shieldMana = int.MaxValue;
            int poisonMana = int.MaxValue;
            int rechargeMana = int.MaxValue;

            if (poisonCounter > 0)
            {
                enemyHealth -= PoisonDamage;
                poisonCounter--;
            }

            if (rechargeCounter > 0)
            {
                mana += RechargeAmount;
                rechargeCounter--;
            }

            if (enemyHealth <= 0)
                return 0;


            if (mana <= MissileCost)
                return int.MaxValue;

            shieldCounter--;

            // Try Missile
            if (mana > MissileCost)
            {
                missileMana = MissileCost;
                mana -= MissileCost;
                enemyHealth -= MissileDamage;

                if (poisonCounter > 0)
                {
                    enemyHealth -= PoisonDamage;
                    poisonCounter--;
                }

                if (enemyHealth <= 0) return missileMana;

                health -= CalcDamage(enemyAttack, shieldCounter);
                if (health <= 0)
                    missileMana = int.MaxValue;
                else
                {
                    int result = Round(health, mana, --shieldCounter, poisonCounter, rechargeCounter, enemyHealth, enemyAttack);
                    if (result != int.MaxValue)
                        missileMana += result;
                    else
                        return int.MaxValue;
                }
            }

            // Try Drain
            if (mana > DrainCost)
            {
                drainMana = DrainCost;
                mana -= DrainCost;
                enemyHealth -= DrainDamage;
                health += DrainDamage;

                if (poisonCounter > 0)
                {
                    enemyHealth -= PoisonDamage;
                    poisonCounter--;
                }

                if (enemyHealth <= 0) return drainMana;

                health -= CalcDamage(enemyAttack, shieldCounter);
                if (health <= 0)
                    drainMana = int.MaxValue;
                else
                {
                    int result = Round(health, mana, --shieldCounter, poisonCounter, rechargeCounter, enemyHealth, enemyAttack);
                    if (result != int.MaxValue)
                        drainMana += result;
                    else
                        return int.MaxValue;
                }
            }

            // Try Shield
            if (mana > ShieldCost && shieldCounter <= 0)
            {
                shieldMana = ShieldCost;
                shieldCounter = ShieldCost;
                mana -= ShieldCost;

                if (poisonCounter > 0)
                {
                    enemyHealth -= PoisonDamage;
                    poisonCounter--;
                }
                if (enemyHealth <= 0) return poisonMana;

                health -= CalcDamage(enemyAttack, shieldCounter);

                if (health <= 0)
                    shieldMana = int.MaxValue;
                else
                {
                    int result = Round(health, mana, --shieldCounter, poisonCounter, rechargeCounter, enemyHealth, enemyAttack);
                    if (result != int.MaxValue)
                        shieldMana += result;
                    else
                        return int.MaxValue;
                }
            }

            // Try Poison
            if (mana > PoisonCost && poisonCounter <= 0)
            {
                poisonMana = PoisonCost;
                poisonCounter = PoisonRounds;
                mana -= PoisonCost; ;

                enemyHealth -= PoisonDamage;
                poisonCounter--;

                if (enemyHealth <= 0) return poisonMana;

                health -= CalcDamage(enemyAttack, shieldCounter);

                if (health <= 0)
                    poisonMana = int.MaxValue;
                else
                {
                    int result = Round(health, mana, --shieldCounter, poisonCounter, rechargeCounter, enemyHealth, enemyAttack);
                    if (result != int.MaxValue)
                        poisonMana += result;
                    else
                        return int.MaxValue;
                }
            }

            // Try Recharge
            if (mana > RechargeCost && rechargeCounter <= 0)
            {
                rechargeMana = RechargeCost;
                rechargeCounter = RechargeRounds;
                mana -= RechargeCost;

                mana += RechargeAmount;
                rechargeCounter--;

                if (poisonCounter > 0)
                {
                    enemyHealth -= PoisonDamage;
                    poisonCounter--;
                }

                if (enemyHealth <= 0) return rechargeMana;

                health -= CalcDamage(enemyAttack, shieldCounter);

                if (health <= 0)
                    rechargeMana = int.MaxValue;
                else
                {
                    int result = Round(health, mana, --shieldCounter, poisonCounter, rechargeCounter, enemyHealth, enemyAttack);
                    if (result != int.MaxValue)
                        rechargeMana += result;
                    else
                        return int.MaxValue;
                }
            }

            return (new int[] { missileMana, drainMana, shieldMana, poisonMana, rechargeMana }).Min();
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
        private const int RechargeAmount = 111;
        private const int RechargeRounds = 5;
    }
}
