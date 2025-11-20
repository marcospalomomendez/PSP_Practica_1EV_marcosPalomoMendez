using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pista
{
    internal class Program
    {
       static void Main(string[] args)
        {

            if (args.Length < 2)
            {
                Console.WriteLine("Uso: <canicas> <longitud>");
                return;
            }

            int numero_canicas = int.Parse(args[0]);
            int longitudPista = int.Parse(args[1]);

            Thread[] canicas = new Thread[numero_canicas];
            Stopwatch[] cronos = new Stopwatch[numero_canicas];
            
            /*
            Stopwatch cronoParalelo = new Stopwatch();
            cronoParalelo.Start();
            */
            Random rnd = new Random();
            List<int> pausas = new List<int>();
            for (int i = 0; i < numero_canicas; i++)
            {
                cronos[i]= new Stopwatch();
                cronos[i].Start();

                int dorsal = i;
                int pausa;
                do
                {
                    pausa = rnd.Next(1, 25 * canicas.Length);
                } while (pausas.Contains(pausa));

                pausas.Add(pausa);

                canicas[i] = new Thread(() =>
                {
                    cronos[dorsal] = Stopwatch.StartNew();

                    Carrera(longitudPista, dorsal, pausa);

                    cronos[dorsal].Stop();
                });
                canicas[i].Start();
            }

            for (int i = 0; i < numero_canicas; i++)
            {
                canicas[i].Join();
                Console.WriteLine(cronos[i].ElapsedMilliseconds);
            }

        }

        static void Carrera(int Longitud, int dorsal, int pausa)
        {
            for (int i = 0; i < Longitud; i++)
            {
     
                Thread.Sleep(250 + pausa);

            }
        }
    }
}
