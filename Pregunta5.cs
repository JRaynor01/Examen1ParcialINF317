using System;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        int numPosiciones = 10000;
        int[] serie = new int[numPosiciones];
        serie[0] = 2;

        Parallel.For(1, numPosiciones, i =>
        {
            if (i % 2 == 1)
            {
                serie[i] = i + 1;
            }
            else
            {
                serie[i] = serie[i - 1] + serie[i - 2] + 1;
            }
        });

        // Imprimir los primeros 10 elementos de la serie
        for (int i = 0; i < 10; i++)
        {
            Console.Write(serie[i] + " ");
        }

        Console.WriteLine();
    }
}
