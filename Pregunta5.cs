using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static List<int> serie = new List<int>();
    static object lockObj = new object();

    static void GenerarSerie(object posiciones)
    {
        int numPosiciones = (int)posiciones;

        lock (lockObj)
        {
            serie.Add(2);
        }

        for (int i = 1; i < numPosiciones; i++)
        {
            int nuevoValor;

            if (i % 2 == 1)
            {
                nuevoValor = i + 1;
            }
            else
            {
                lock (lockObj)
                {
                    nuevoValor = serie[i - 1] + serie[i - 2] + 1;
                }
            }

            lock (lockObj)
            {
                serie.Add(nuevoValor);
            }
        }
    }

    static void Main()
    {
        int numPosiciones = 10000;
        int numHilos = 4; // Establece el número de hilos deseados

        List<Thread> hilos = new List<Thread>();

        for (int i = 0; i < numHilos; i++)
        {
            Thread hilo = new Thread(GenerarSerie);
            hilos.Add(hilo);
        }

        foreach (Thread hilo in hilos)
        {
            hilo.Start(numPosiciones / numHilos);
        }

        foreach (Thread hilo in hilos)
        {
            hilo.Join();
        }

        // Imprimir los primeros 10 elementos de la serie
        for (int i = 0; i < 10; i++)
        {
            Console.Write(serie[i] + " ");
        }
    }
}
