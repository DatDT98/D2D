using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace D2D_simulation
{
    class Input
    {
        public static int NumberofM_UE = 50;
        public static int NumberofPair = 10;
        public static int NumberofSBS = 5;
        public static int NumberofChannel = 50;
        public static int NumberofBWP = 5;
        public static int NumberOfBWP_permit = 4;

        public static int s = 25; //S_UE
        public static int r = 5;  //D2D
        public static int UE_Id = 0;
        public static double Max_PowerTr = 23;//dB
        public static double D2D_Max_PowerTr = 23;//dB
        public static double SUE_Max_PowerTr = 23;//dB
        public static double SINR_threshold = 7.5;//dB
        public static double SINR_outage = -2.5;//dB

        public static double proportion_Allow_Power = 0.99;//dB
        public static double B = 0.18;

        public static int D2DCover = 50;
        public static double SBSCover = 10;
        public static double MBSCover = 200;

        public static List<MBS> List_MBS = new List<MBS>();
        public static List<Pair> List_Pair = new List<Pair>();
        public static List<UE> List_UE = new List<UE>();
        public static List<List<UE>> List_UE_in_Channel = new List<List<UE>>();
        
        public static List<SBS> List_SBS = new List<SBS>();
        public static List<double> List_SINR = new List<double>();

        //Noi luu tru file
        public static string path = "A:\\Du_an";

    }
}
