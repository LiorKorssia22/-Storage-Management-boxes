using BoxesProject;
using System;
using System.Configuration;
using System.Windows;


namespace Porgram
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NotificationImplementation b = new NotificationImplementation();
            string expiration = ConfigurationManager.AppSettings["ExpirationSpanSec"];
            double expirationNum;
            double.TryParse(expiration, out expirationNum);

            string invoke = ConfigurationManager.AppSettings["InvocationSpan"];
            int numOfInvocation;
            int.TryParse(invoke, out numOfInvocation);
            TimeSpan spanOfInvocation = TimeSpan.FromSeconds(numOfInvocation);

            string checkSpan = ConfigurationManager.AppSettings["CheckingSpan"];
            int checkNum;
            int.TryParse(checkSpan, out checkNum);
            TimeSpan spanOfChecking = TimeSpan.FromSeconds(checkNum);

            string maxStock = ConfigurationManager.AppSettings["MaxStock"];
            int numOfStockMax;
            int.TryParse(maxStock, out numOfStockMax);

            string minStock = ConfigurationManager.AppSettings["MinStock"];
            int numOFStockMin;
            int.TryParse(minStock, out numOFStockMin);

            Shop(spanOfChecking, spanOfInvocation, expirationNum, numOfStockMax, numOFStockMin);
        }
        private static void Shop(TimeSpan spanCheck, TimeSpan invocation, double spanExpiration, int maxItem, int minItem)
        {
            NotificationImplementation b = new NotificationImplementation();
            Manager n = new Manager(b, spanCheck, invocation, spanExpiration, maxItem, minItem);
            string shop;
            double width;
            double height;
            int amount;
            int boxes;
            bool check1;
            bool check2;
            bool check3;
            bool main = true;
            while (main)
            {

                MessageBoxResult messageBoxResult = MessageBox.Show("Press YES for supplier\nPress NO for shopping\nPress CANCEL to Exit", "Hello! :)", MessageBoxButton.YesNoCancel);
                switch (messageBoxResult)
                {
                    case MessageBoxResult.Yes:
                        Console.WriteLine("Welcome Supplier ! How many boxes would you like to order?");
                        bool checkBox = int.TryParse(Console.ReadLine(), out boxes);
                        while (!checkBox || boxes < 1)
                        {
                            Console.WriteLine("Try again!  How many boxes would you like to order?");
                            checkBox = int.TryParse(Console.ReadLine(), out boxes);
                        }
                        for (int i = 1; i <= boxes; i++)
                        {
                            Console.WriteLine($"\nBox number {i}\n\nInsert width of the box:");
                            check1 = double.TryParse(Console.ReadLine(), out width);
                            while (!check1 || width < 1)
                            {
                                Console.WriteLine("Try again!  Insert width of the box:");
                                check1 = double.TryParse(Console.ReadLine(), out width);
                            }
                            Console.WriteLine("Insert height of the box");
                            check2 = double.TryParse(Console.ReadLine(), out height);
                            while (!check2 || height < 1)
                            {
                                Console.WriteLine("Try again!  Insert height of the box");
                                check2 = double.TryParse(Console.ReadLine(), out height);
                            }
                            Console.WriteLine("How many boxes?");
                            check3 = int.TryParse(Console.ReadLine(), out amount);
                            while (!check3 || amount < 1)
                            {
                                Console.WriteLine("Try again!  How many boxes?");
                                check3 = int.TryParse(Console.ReadLine(), out amount);
                            }
                            n.Supply(width, height, amount);
                        }
                        break;
                    case MessageBoxResult.No:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        bool collection = n.Show(out shop);
                        if (collection)
                        {
                            Console.WriteLine("Welcome to the 'THE BOXES'! Below is our Shop:\n");
                            Console.WriteLine(shop);
                            Console.Write("Let's start buying!\nPlease enter width of the box:");
                            check1 = double.TryParse(Console.ReadLine(), out width);
                            while (!check1 || width < 1)
                            {
                                Console.WriteLine("Try again!  Insert width of the box:");
                                check1 = double.TryParse(Console.ReadLine(), out width);
                            }
                            Console.Write("Please enter height of the box:");
                            check2 = double.TryParse(Console.ReadLine(), out height);
                            while (!check2 || height < 1)
                            {
                                Console.WriteLine("Try again!  Insert height of the box");
                                check2 = double.TryParse(Console.ReadLine(), out height);
                            }
                            Console.Write("How many boxes you would like to buy:");
                            check3 = int.TryParse(Console.ReadLine(), out amount);
                            while (!check3 || amount < 1)
                            {
                                Console.WriteLine("Try again!  How many boxes?");
                                check3 = int.TryParse(Console.ReadLine(), out amount);
                            }
                            bool answer = n.Purchase(width, height, amount);
                            if (answer)
                            {
                                Console.WriteLine($"\n");
                                n.Show(out shop);
                                Console.WriteLine(shop);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("-- PURCHASE SUCCESSFUL --");
                                Console.ForegroundColor = ConsoleColor.White;

                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("The purchase was not successful");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case MessageBoxResult.Cancel:
                        Console.WriteLine("\n\nThank You And Have A Nice Day!");
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
