using System;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        int totalPuntos = 1000000;
        int puntosEnCirculo = 0;

        // Utilizamos Parallel.For con una reducción para realizar el cálculo en paralelo
        Parallel.For(0, totalPuntos, () => 0, (i, loop, subtotal) =>
        {
            double x = GetRandomDouble();
            double y = GetRandomDouble();

            // Verificar si el punto está dentro del círculo (radio = 1)
            if (x * x + y * y <= 1)
            {
                subtotal++;
            }

            return subtotal;
        },
        (subtotal) =>
        {
            // Combinar los resultados parciales de cada hilo
            lock (reduceLock)
            {
                puntosEnCirculo += subtotal;
            }
        });

        // Calcular PI utilizando la fórmula de Monte Carlo
        double piEstimado = 4.0 * puntosEnCirculo / totalPuntos;

        Console.WriteLine($"Estimación de PI (paralelo): {piEstimado}");
    }

    // Método para obtener un número aleatorio entre 0 y 1
    static double GetRandomDouble()
    {
        lock (randomLock)
        {
            return random.NextDouble();
        }
    }

    private static Random random = new Random();
    private static object randomLock = new object();
    private static object reduceLock = new object();
}
