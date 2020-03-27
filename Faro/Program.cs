using System;
using System.Collections.Generic;
using System.Linq;

namespace Faro
{
    class Program
    {
        static void Main(string[] args)
        {
            //var startingdesk = Suits().SelectMany(suit => Ranks().Select(rank => new { Suit = suit, Rank = rank }));
            var startingDesk = (from s in Suits().LogQuery("SuitGeneration")
                               from k in Ranks().LogQuery("RankGeneration")
                               select new { Suit = s, Rank = k }).LogQuery("Starting Desk")
                               .ToArray();
            //Display each card that we've generated and placed in startingDesk in the console.
            foreach(var card in startingDesk)
            {
                Console.WriteLine(card);
            }
            Console.WriteLine("\n");
            //var top = startingDesk.Take(26);
            //var bottom = startingDesk.Skip(26);
            //var shuffle = top.InterLeaveSequenceWith(bottom);

            //foreach(var c in shuffle)
            //{
            //    Console.WriteLine(c);
            //}
            var times = 0;
            var shuffle = startingDesk;
            do
            {
                shuffle = shuffle.Skip(26).LogQuery("Bottom Half").InterLeaveSequenceWith(shuffle.Take(26).LogQuery("Top Half"))
                    .LogQuery("Shuffle").ToArray();
                foreach (var card in shuffle)
                {
                    Console.WriteLine(card);
                }
                times++;
                Console.WriteLine(times);
                
            }
            while (!startingDesk.SequenceEqual(shuffle));
            Console.WriteLine(times);
            Console.ReadLine();
        }
        static IEnumerable<string> Suits()
        {
            yield return "clubs";
            yield return "diamonds";
            yield return "hearts";
            yield return "spades";
        }
        static IEnumerable<string> Ranks()
        {
            yield return "two";
            yield return "three";
            yield return "four";
            yield return "five";
            yield return "six";
            yield return "seven";
            yield return "eight";
            yield return "nine";
            yield return "ten";
            yield return "jack";
            yield return "queen";
            yield return "king";
            yield return "ace";
        }
    }
}
