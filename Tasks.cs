using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHTO2
{
    class Tasks
    {
        private Dictionary<string, string> tasksList = new Dictionary<string, string>();
        private Dictionary<string, double[]> granPoint = new Dictionary<string, double[]>();
        private Dictionary<string, string> formalTaskList = new Dictionary<string, string>();
        double XYPointsCount;
        double FuncAccuracy;
        public static double Param1Min;
        public static double Param1Max;
        public static double Param2Min;
        public static double Param2Max;
        public int CalculationsCount { get; private set; }


        public Tasks()
        {

        }

        public static double MainFunction1(double t1, double t2 = 1)
        {
            return 0;
        }
      
        public static double MFunction1(double t1, double t2 = 1)
        {
            return (Math.Exp(t1));

        }
      
        //TODO
        public static bool Contidions3(double t1, double t2)
        {
            return t1 >= -18000 && t1 <= 700000 && t2 >= -8000 && t2 <= 800;
        }


        public void SetAccuracy(double accX, double accZ)
        {
            XYPointsCount = accX;
            FuncAccuracy = accZ;
        }
        public static void SetMinMax(double param1Min, double param1Max, double param2Min, double param2Max)
        {
            Param1Min = param1Min;
            Param1Max = param1Max;
            Param2Min = param2Min;
            Param2Max = param2Max;
        }



    }
}
