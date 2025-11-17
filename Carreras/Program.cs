using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carreras
{
    internal class Program
    {
        static void Main(string[] args)
        {

            const int canicas = 3;
            int[] pistas = new int[2] {5,10};
            int[] totales = new int[canicas];
            
            for (int pista = 0; pista < pistas.Length; pista++)
            {
                Stopwatch crono = new Stopwatch();
                crono.Start();

                Process proceso = new Process();

                proceso.StartInfo.FileName = @"C:\Users\marco\source\repos\PSP_Practica_1EV_marcosPalomoMendez\Pista\bin\Debug\Pista.exe";
                proceso.StartInfo.Arguments = canicas + " " + pistas[pista];
                proceso.StartInfo.RedirectStandardOutput = true;
                proceso.StartInfo.UseShellExecute = false;
                proceso.Start();
                proceso.WaitForExit();

                crono.Stop();                

                string salida = proceso.StandardOutput.ReadToEnd();
                string[] salidas = salida.Split('\n');

                int[] tiempos = new int[canicas];
                for (int canica = 0; canica < canicas; canica++)
                {
                    tiempos[canica] = int.Parse(salidas[canica]);
                    totales[canica] += tiempos[canica];
                }
                var ranking = tiempos.Select((tiempo, indice) => new { Canica = indice, Tiempo = tiempo, Pista = pista })
                                .OrderBy(x => x.Tiempo) // menor tiempo primero
                                .ToList(); 



                Console.WriteLine($"[Pista {pista}] --> {crono.ElapsedMilliseconds} ms");
                for (int i = 0; i < ranking.Count; i++)
                {
                    Console.WriteLine($"Posición {i + 1} --> Canica {ranking[i].Canica} con tiempo {ranking[i].Tiempo}" );
                }
                
            }
            /*
            var rankingFinal = totales.Select((tiempo, indice) => new { Canica = indice, Tiempo = tiempo})
                                .OrderBy(x => x.Tiempo) // menor tiempo primero
                                .ToList();
            for (int i = 0; i < rankingFinal.Count; i++)
            {
                Console.WriteLine($"Posición {i + 1} --> Canica {rankingFinal[i].Canica} con tiempo {rankingFinal[i].Tiempo}");
            }
            Console.WriteLine($"[Resultado final] --> ");
            */
        }
    }
}
