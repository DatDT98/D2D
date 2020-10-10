using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace D2D_simulation
{
    class Input
    {
        public static int NumberofM_UE = 50;
        /*public static int NumberofPair = 40;
        public static int NumberofSBS = 60;*/
        public static int NumberofSBS = 50;

        public static int NumberofChannel = 60 ;

        public static int s = 1;
        public static int r = 1;
        public static double proportion_Allow_Power = 1;//dB
        public static int Type = 1; //1: Linkgain,  2: RR

        public static int UE_Id = 0;

        public static double Max_PowerTr = 23;//dB
        public static double D2D_Max_PowerTr = 23;//dB
        public static double SUE_Max_PowerTr = 23;//dB
        public static double SINR_threshold = 7.5;//dB
        public static double SINR_outage = -2.5;//dB
        public static double B = 0.18;
        public static double SINR_target = 20;//dB
        public static double N0 = -143.97;//dB

        public static int D2DCover = 50;
        public static double MBSCover = 400;

        public static List<MBS> List_MBS = new List<MBS>();
        public static List<SBS> List_SBS = new List<SBS>();
        public static List<double> List_SINR = new List<double>();

        //Noi luu tru file
        public static string path = "A:\\Du_an\\result3";

    }
}
