using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCodeDay10
{
    public class Program
    {
        static void Main(string[] args)
        {
            var navigationSubsystem = File.ReadAllLines("Resources/input.txt");                

            var Openers = "[{(<";
            var Closers = "]})>";
            var score = 0;

            foreach (string line in navigationSubsystem)
            {
                var navigationStack = new Stack<char>();                
                score = SearchLineForErrors(Openers, Closers, line, navigationStack, score);                
            }
            Console.WriteLine($"The total syntax error score for these errors is {score} points.");
        }

        private static int SearchLineForErrors(string Openers, string Closers, string line, Stack<char> navigationStack, int score)
        {
            for (int a = 0; a < line.Length; a++)
            {
                if (Openers.Contains(line[a]))
                {
                    navigationStack.Push(line[a]);
                }
                else if (Closers.Contains(line[a]))
                {
                    var c = navigationStack.Pop();
                    var indexOfCloserCharacter = Openers.IndexOf(c);

                    if (Closers[indexOfCloserCharacter] != line[a])
                    {
                        Console.WriteLine($"{line} - Expected {Closers[indexOfCloserCharacter]}, but found {line[a]} instead.");
                        score = CalculateErrorScore(line, score, a);
                        break;
                    }
                }
            }

            return score;
        }

        private static int CalculateErrorScore(string line, int score, int a)
        {
            switch (line[a])
            {
                case ')':
                    score += 3;
                    break;
                case ']':
                    score += 57;
                    break;
                case '}':
                    score += 1197;
                    break;
                case '>':
                    score += 25137;
                    break;
            }

            return score;
        }
    }    
}