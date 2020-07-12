using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace D2D_simulation
{
    class Input
    {
        public static int NumberofM_UE = 15;
        public static int NumberofPair = 3;
        public static int NumberofSUE = 6;
        public static int NumberofSBS = 4;
        public static int NumberofChannel = 15;
        public static int NumberofBWP = 5;
        public static int NumberOfBWP_permit = 3;

        public static double k = 0.001;

        public static int NumberofBWP_SUE = 2;
        public static int NumberofBWP_D2D = NumberOfBWP_permit - NumberofBWP_SUE;
        public static int s = 1; // ratio using channel S_UE
        public static int r = 1;  //  D2D

        public static int UE_Id = 0;
        public static double Max_PowerTr = 23;//dB
        public static double D2D_Max_PowerTr = 23;//dB
        public static double SUE_Max_PowerTr = 23;//dB
        public static double SINR_threshold = 7.5;//dB
        public static double SINR_outage = -2.5;//dB

        public static double proportion_Allow_Power = 0.99;//dB
        public static double B = 0.18;

        public static int SBSCover = 200;
        public static double MBSCover = 400;
        public static int D2DCover = 10;

        public static List<MBS> List_MBS = new List<MBS>();
        public static List<List<UE>> List_UE_in_Channel = new List<List<UE>>();
        
        public static List<SBS> List_SBS = new List<SBS>();
        public static List<double> List_SINR = new List<double>();

        //Noi luu tru file
        public static string path = "A:\\Du_an";
    }
}
