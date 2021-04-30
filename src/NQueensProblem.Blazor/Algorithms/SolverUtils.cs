using System;

namespace NQueensProblem.Blazor.Algorithms
{
    public class SolverUtils
    {
        public static int[] GenerateAllOneState(int countOfGene)
        {
            return new int[countOfGene];
        }

        public static int[] RandomizeState(int[] chromosome)
        {
            for (int i = 0; i < chromosome.Length; i++)
                chromosome[i] = (int)(new Random().NextDouble() * chromosome.Length);

            return chromosome;
        }

        public static int[] GenerateRandomState(int countOfGene)
        {
            return RandomizeState(GenerateAllOneState(countOfGene));
        }

        public static int GetHeuristicCost(int[] chromosome)
        {
            int heuristic = 0;

            for (int i = 0; i < chromosome.Length; i++)
                for (int j = i + 1; j < chromosome.Length; j++)
                    if (chromosome[i] == chromosome[j] || Math.Abs(chromosome[i] - chromosome[j]) == j - i)
                        heuristic += 1;

            return heuristic;
        }
    }
}
