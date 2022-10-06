using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHTO2
{
    public static class Calc
    {
        #region Объявление переменных
        public static double D = 0;
        public static double L = 0;
        public static double Q = 0;
        public static double CAin = 0;
        public static double T = 0;
        public static double k01 = 0;
        public static double Ea1 = 0;
        public static double k02 = 0;
        public static double Ea2 = 0;
        public static double del_x0 = 0;
        public static double Ku = 0;
        public static double e_max = 0;
        public static double q_max = 0;


        public static double S = 0;
        public static double u = 0;
        public static double tR = 0;
        public static double Teta = 0;
        public static double k1 = 0;
        public static double k2 = 0;
        public static double q = 0;
        public static double e = 0;
        public static double ea = 0;
        public static double M1 = 0;
        public static double N1 = 0;
        public static double CBmax = 0;

        public static List<List<double>> CA = new List<List<double>>();
        public static List<List<double>> CB = new List<List<double>>();
        public static List<List<double>> CB1 = new List<List<double>>();
        public static List<double> x = new List<double>();
        public static List<double> t = new List<double>();
        //public static double[][] CA;
        //public static double[][] CB;
        //public static double[][] CB1;
        public static int kkk = 0;
        public static int M = 0;
        public static int N = 0;
        public static double del_x = 0;
        public static double del_t = 0;
    
        #endregion
        // Поиск максимального значения в двумерном списке (массиве)
        public static double MaxValueArray(List<List<double>> arr, int M, int N)
        {
            double max = 0;

            for (int j = 0; j <= N; j++)
            {
                for (int i = 0; i <= M; i++)
                {
                    if(max < arr[j][i])
                    {
                        max = arr[j][i];
                    }
                }
            }

            return max;
        }


        public static void Calucation()
        {
            try
            {
                S = (Math.PI * Math.Pow(D, 2)) / 4;
                u = (Q * Math.Pow(10, -3)) / S;
                tR = L / u;
                Teta = 2 * tR;
                k1 = k01 * Math.Exp(-Ea1 / (8.31 * (T + 273)));
                k2 = k02 * Math.Exp(-Ea2 / (8.31 * (T + 273)));
                q = 0;
                e = 2 * e_max;

                while (e >= e_max && q <= q_max)
                {
                    x.Clear();
                    t.Clear();
                    if (q == 0)
                    {
                        del_x = del_x0;
                        del_t = (Ku * del_x) / u;
                        M = Convert.ToInt32(Math.Round(L / del_x));
                        N = Convert.ToInt32(Math.Round(Teta / del_t));

                    }
                    else
                    {
                        del_x = del_x / 2;
                        del_t = del_t / 2;
                        M = 2 * M;
                        N = 2 * N;
                    }
                    for (int j = 0; j <= N; j++)
                    {
                        CA.Add(new List<double>());
                        CB.Add(new List<double>());
                        CB1.Add(new List<double>());
                        for (int i = 0; i <= M; i++)
                        {
                            CA[j].Add(0);
                            CB[j].Add(0);
                            CB1[j].Add(0);
                        }
                    }


                    for (int i = 0; i <= M; i++)
                    {
                        x.Add(i * del_x);
                        CA[0][i] = 0;
                        CB[0][i] = 0;
                    }
                    for (int j = 1; j <= N; j++)
                    {
                        t.Add(j * del_t);
                        CA[j][0] = CAin;
                        CB[j][0] = 0;
                    }

                    for (int j = 0; j <= N - 1; j++)
                    {
                        for (int i = 1; i <= M; i++)
                        {
                            CA[j + 1][i] = (CA[j][i] + Ku * CA[j + 1][i - 1]) / (1 + Ku + k1 * del_t);
                            CB[j + 1][i] = (CB[j][i] + Ku * CB[j + 1][i - 1] + 2 * k1 * del_t * CA[j + 1][i]) / (1 + Ku + k2 * del_t);
                        }
                    }
                    if (q != 0)
                    {
                        double sum = 0;
                        for (int j = 1; j <= N1; j++)
                        {
                            for (int i = 1; i <= M1; i++)
                            {
                                sum += Math.Pow(CB[2 * j][2 * i] - CB1[j][i], 2);
                            }
                        }

                        ea = Math.Sqrt((1 / (M1 * N1)) * sum);
                        CBmax = MaxValueArray(CB, M, N);

                        e = (ea / CBmax) * 100;
                    }
                    for (int j = 1; j <= N; j++)
                    {
                        for (int i = 1; i <= M; i++)
                        {
                            CB1[j][i] = CB[j][i];
                        }
                    }
                    M1 = M;
                    N1 = N;
                    q++;
                }
            }
            catch
            {
                throw;
            }

        }
    }
}
