using System;
using System.Linq;

namespace NQueensProblem.Blazor.Algorithms
{
    public class QueensGeneticAlgorithm
    {
        public int[] Solve(int countOfQueen, int populationSize, double mutationProbability, int numOfGenerations)
        {
            populationSize = populationSize - populationSize % 2;

            int[][] population = GeneratePopulation(countOfQueen, populationSize);

            int maxFitness = GetMaxFitness(countOfQueen);

            for (int x = 0; x < numOfGenerations; x++)
            {
                population = GetSelectedPopulation(population);

                population = HandleCrossovers(population, countOfQueen);

                for (int i = 0; i < populationSize; i++)
                {
                    if (GetFitness(population[i]) == maxFitness)
                        return population[i];

                    population[i] = TryToMutate(population[i], mutationProbability);
                    if (GetFitness(population[i]) == maxFitness)
                        return population[i];
                }
            }

            return null;
        }

        private int[][] HandleCrossovers(int[][] population, int countOfQueen)
        {
            for (int i = 0; i < population.Length; i += 2)
            {
                int crossoverPoint = (int)(new Random().NextDouble() * countOfQueen);

                for (int j = 0; j < crossoverPoint; j++)
                {
                    int tmp = population[i][j];
                    population[i][j] = population[i + 1][j];
                    population[i + 1][j] = tmp;
                }
            }

            return population;
        }

        private int[][] GetSelectedPopulation(int[][] population)
        {
            population = population.OrderBy(a => GetFitness(a)).ToArray();
            return population;
        }

        private int[] TryToMutate(int[] chromosome, double mutationProbability)
        {
            if (SatisfyProbability(mutationProbability))
                chromosome[(int)(new Random().NextDouble() * chromosome.Length)] = (int)(new Random().NextDouble() * chromosome.Length);

            return chromosome;
        }

        private bool SatisfyProbability(double probability)
        {
            return probability >= (int)new Random().NextDouble();
        }

        private int GetFitness(int[] chromosome)
        {
            return GetMaxFitness(chromosome.Length) - SolverUtils.GetHeuristicCost(chromosome);
        }

        private int GetMaxFitness(int countOfQueen)
        {
            return countOfQueen * (countOfQueen - 1) / 2;
        }

        private int[] GenerateChromosome(int countOfQueen)
        {
            return SolverUtils.GenerateRandomState(countOfQueen);
        }

        private int[][] GeneratePopulation(int countOfQueen, int populationSize)
        {
            int[][] population = new int[populationSize][];
            for (int i = 0; i < populationSize; i++)
                population[i] = GenerateChromosome(countOfQueen);

            return population;
        }
    }
}