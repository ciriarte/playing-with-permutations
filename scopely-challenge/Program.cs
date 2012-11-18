using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scopely_challenge
{
    class Program {
        static void Main(string[] args)
        {
            Part1();

            Part2();

            Part3();

            Part4();

            Part5();

            Part6();
            Console.ReadLine();
        }

        static void Part1()
        {
            Console.WriteLine("Given the following tree");
            var tree = @"/home/sports/basketball/NCAA/".BuildTree();
            tree.Insert(@"/home/sports/football");
            tree.Insert(@"/home/music/rap");
            tree.Insert(@"/home/music/rock");
            tree.Display();

            Console.WriteLine("Insert /home/music/rap/gangster");
            tree.Insert(@"/home/music/rap/gangster")
                .Display();
        }

        static void Part2()
        {
            Console.WriteLine("Given the following tree");
            var tree = @"/home/sports/basketball/NCAA/".BuildTree();
            tree.Insert(@"/home/sports/football");
            tree.Insert(@"/home/music/rap");
            tree.Insert(@"/home/music/rock");
            tree.Insert(@"/home/music/rap/gangster")
                .Display();

            Console.WriteLine("Dual Leaf Insert");
            tree.Insert(@"/home/sports/football/NFL|NCAA")
                .Display();
        }

        static void Part3()
        {
            Console.WriteLine("Given the following tree");
            var tree = @"/".BuildTree();
            tree.Display();

            Console.WriteLine("Combinatorial Leaf Insert");
            tree.Insert(@"/home/music/rap|rock|pop", token => token.Split('|').Permutations().ToArray())
                .Display();
        }

        static void Part4()
        {
            Console.WriteLine("Given the following tree");
            var tree = @"/".BuildTree();
            tree.Display();

            Console.WriteLine("Combinatorial Insert");
            tree.Insert(@"/home/sports|music/misc|favorites", token => token.Split('|').Permutations().ToArray())
                .Display();
        }

        static void Part5()
        {
            Console.WriteLine("Given the following tree");
            var tree = @"/".BuildTree();
            tree.Insert(@"/home/sports|music/misc|favorites", token => token.Split('|').Permutations().ToArray())
                .Display();

            Console.WriteLine("The collapsed tree should be: {0}", tree.Collapse());
        }

        static void Part6()
        {
            Console.WriteLine("Given the following tree");
            var tree = @"/".BuildTree();
            tree.Insert(@"/home/sports|music/misc|favorites", token => token.Split('|').Permutations().ToArray())
                .Display();

            Console.WriteLine("And I look for a synonym of /home/sports/");
            var synonim = tree.FindFirstSynonym(@"/home/sports");

            Console.WriteLine("Found: {0}", synonim);
        }
    }
}