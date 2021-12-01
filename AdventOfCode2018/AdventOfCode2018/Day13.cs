using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day13 : AdventOfCode2018
    {
        public MapTile[,] Map { get; set; }
        public List<Cart> Carts { get; set; }
        public int XDim { get; set; }
        public int YDim { get; set; }
        public Day13()
        {
            Carts = new List<Cart>();
            string[] mapdata = SplitLines(ReadData("13.txt"));
            XDim = mapdata[0].Length;
            YDim = mapdata.Count();
            Map = new MapTile[XDim, YDim];
            int cartCounter = 0;
            for (int y = 0; y < YDim; y++)

            {
                char[] tiledata = mapdata[y].ToCharArray();
                MapTile newTile = null;
                for (int x = 0; x < XDim; x++)
                {
                    if (x == 145 && y == 10)
                    {
                        Console.WriteLine();
                    }

                    newTile = null;
                    switch (tiledata[x])
                    {

                        case '|':
                            newTile = new MapTile();
                            newTile.Directions[0] = true;
                            newTile.Directions[2] = true;
                            break;
                        case '-':
                            newTile = new MapTile();
                            newTile.Directions[1] = true;
                            newTile.Directions[3] = true;
                            break;
                        case '/':
                            newTile = new MapTile();
                            if (y > 0 && Map[x, y - 1] != null && Map[x, y - 1].Directions[2] == true)
                            {
                                newTile.Directions[0] = true;
                                newTile.Directions[3] = true;
                            }
                            else
                            {
                                newTile.Directions[1] = true;
                                newTile.Directions[2] = true;
                            }
                            break;
                        case '\\':
                            newTile = new MapTile();
                            if (y > 0 && Map[x, y - 1] != null && Map[x, y - 1].Directions[2] == true)
                            {
                                newTile.Directions[0] = true;
                                newTile.Directions[1] = true;
                            }
                            else
                            {
                                newTile.Directions[2] = true;
                                newTile.Directions[3] = true;
                            }

                            break;
                        case '+':

                            newTile = new MapTile();
                            newTile.Directions[0] = true;
                            newTile.Directions[1] = true;
                            newTile.Directions[2] = true;
                            newTile.Directions[3] = true;
                            break;
                        case '^':
                            newTile = new MapTile();
                            newTile = new MapTile();
                            newTile.Directions[0] = true;
                            newTile.Directions[2] = true;
                            Carts.Add(new Cart { Direction = Directions.Up, X = x, Y = y, CartID = cartCounter++ });
                            break;
                        case 'v':

                            newTile = new MapTile();
                            newTile.Directions[0] = true;
                            newTile.Directions[2] = true;
                            Carts.Add(new Cart { Direction = Directions.Down, X = x, Y = y, CartID = cartCounter++ });
                            break;
                        case '>':
                            newTile = new MapTile();
                            newTile.Directions[1] = true;
                            newTile.Directions[3] = true;
                            Carts.Add(new Cart { Direction = Directions.Right, X = x, Y = y, CartID = cartCounter++ });
                            break;
                        case '<':
                            newTile = new MapTile();
                            newTile.Directions[1] = true;
                            newTile.Directions[3] = true;
                            Carts.Add(new Cart { Direction = Directions.Left, X = x, Y = y, CartID = cartCounter++ });
                            break;
                    }
                    Map[x, y] = newTile;
                }
            }
        }

        public bool MoveCart(Cart cart)
        {
            if (cart.CartID == 0)
            {
                cart.CartID = 0;
            }

            if (Map[cart.X, cart.Y].Directions.Where(d => d == true).Count() == 4)
            {
                if (cart.AI == 0)
                {
                    cart.Direction--;
                    if (cart.Direction < Directions.Up)
                    {
                        cart.Direction = Directions.Left;
                    }
                }
                if (cart.AI == 2)
                {
                    cart.Direction++;
                    if (cart.Direction > Directions.Left)
                    {
                        cart.Direction = Directions.Up;
                    }
                }
                cart.AI = (cart.AI + 1) % 3;
            }
            else
            {
                Directions camefrom = (Directions)(((int)cart.Direction + 2) % 4);

                for (int i = 0; i < 4; i++)
                {

                    if (i != (int)camefrom && Map[cart.X, cart.Y].Directions[i])
                    {
                        cart.Direction = (Directions)i;
                    }
                }
            }

            switch (cart.Direction)
            {
                case Directions.Up:
                    cart.Y--;
                    break;
                case Directions.Down:
                    cart.Y++;
                    break;
                case Directions.Right:
                    cart.X++;
                    break;
                case Directions.Left:
                    cart.X--;
                    break;
            }

            if (Carts.Where(c => c.X == cart.X && c.Y == cart.Y && !c.Crashed).Count() > 1)
            {
                return true;
            }

            return false;
        }

        public void Problem1()
        {
            Console.WriteLine("Problem 1");
            bool running = true;
            int xCrash = 0, yCrash = 0;

            while (running)
            {
                List<int> MovedCarts = new List<int>();

                for (int y = 0; y < YDim; y++)
                {
                    for (int x = 0; x < XDim; x++)
                    {
                        Cart currentCart = Carts.Where(c => c.X == x && c.Y == y && !MovedCarts.Contains(c.CartID)).FirstOrDefault();
                        if (currentCart != null)
                        {
                            if (MoveCart(currentCart))
                            {
                                running = false;
                                xCrash = currentCart.X;
                                yCrash = currentCart.Y;
                                x = int.MaxValue - 1;
                                y = int.MaxValue - 1;
                            }
                            MovedCarts.Add(currentCart.CartID);
                        }
                    }
                }

 /*               foreach (Cart cart in Carts)
                {
                    Console.Write($"{cart.X},{cart.Y} ");
                }
                Console.WriteLine();
                */

            }

            Console.Write($"Crash at {xCrash},{yCrash}");
        }
        public void Problem2()
        {
            Console.WriteLine("Problem 1");
            bool running = true;
            int xCrash = 0, yCrash = 0;
            ulong counter = 0;

            while (Carts.Where(c => !c.Crashed).Count() > 1)
            {
                foreach (Cart currentCart in (Carts.Where(c => !c.Crashed).OrderBy(c => c.Y).ThenBy(c => c.X)))
                {
                    if (currentCart != null)
                    {
                        if (MoveCart(currentCart))
                        {
                            for (int i = Carts.Count - 1; i >= 0; i--)
                            {
                                if (Carts[i].X == currentCart.X && Carts[i].Y == currentCart.Y && !Carts[i].Crashed)
                                {

                                    Carts[i].Crashed = true;
                                }

                            }
                            Console.WriteLine($"Crash at {currentCart.X},{currentCart.Y}, {Carts.Where(c => !c.Crashed).Count()}");
                        }
                    }
                }
                counter++;

            }
            Cart remainingCart = Carts.Where(c => !c.Crashed).Single();

            Console.WriteLine($"Last cart {remainingCart.X},{remainingCart.Y} after {counter} moves");
        }


    }


    public class Cart
    {
        public int CartID { get; set; }
        public Directions Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int AI { get; set; }
        public bool Crashed { get; set; }
    }

    public class MapTile
    {
        public bool[] Directions { get; set; }
        public MapTile()
        {
            Directions = new bool[4];
        }
    }

    public enum Directions
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }
}
