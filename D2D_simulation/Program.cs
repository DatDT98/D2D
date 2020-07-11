using System;
using System.Collections.Generic;
using System.Drawing;
using MathNet.Numerics.Distributions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Numerics;
using D2D_simulation;
using MathNet.Numerics;

namespace D2D_simulation
{
    class Program
    {
        static void Main()
        {

            //B1: Xay dung he thong: tao cac UE, FBS, MBS
            Create_D2Dsystem();
            //B2: Khoi tao cac su kien ban dau truoc khi mo phong
            Create_eventList();
            //B3: Tao fuction chay cac su kien trong qua trình mo phong
            Excuting_Event();
            //B4: Thu thap du lieu de phan tich
            Output();
        }
        public static void Create_D2Dsystem()
        {
            //B1-1: Tao MBS
            MBS new_MBS = new MBS(500, 500, 0)
            {
                Id_MBS = 1
            };
            Input.List_MBS.Add(new_MBS);

            //Khoi tao channel add vao listchanel cua MBS
            for (int i = 0; i < Input.NumberofChannel; i++)
            {
                Channel channel = new Channel();
                channel.channel_id = i + 1;
                Input.List_MBS[0].ListChannel.Add(channel);
            }
            //Chia thanh m BWP
            int indexBWP = 0;
            for (int i = 0; i < Input.NumberofBWP; i++)
            {
                BWP bwp = new BWP();
                bwp.Id_BWP = i + 1;
                Input.List_MBS[0].ListBWP.Add(bwp);
                for (int j = 0; j < Input.NumberofChannel / Input.NumberofBWP; j++)
                {
                    Channel channel = Input.List_MBS[0].ListChannel[indexBWP];
                    Input.List_MBS[0].ListBWP[i].ListChannel.Add(channel);
                    indexBWP++;
                }
            }
            //Khoi tao SBS
            for (int i = 0; i < Input.NumberofSBS; i++)
            {
                Random rd = new Random(Guid.NewGuid().GetHashCode());
                int x = 0, y = 0;
                if      (i % 4 == 0)
                {
                    x = rd.Next(0 + Input.SBSCover, 200);
                    y = rd.Next(0 + Input.SBSCover, 200);
                }
                else if (i % 4 == 1)
                {
                    x = rd.Next(0 + Input.SBSCover, 200);
                    y = rd.Next(200, 400 - Input.SBSCover);
                }
                else if (i % 4 == 2)
                {
                    x = rd.Next(200, 400 - Input.SBSCover);
                    y = rd.Next(0 + Input.SBSCover, 200);
                }
                else if (i % 4 == 3)
                {
                    x = rd.Next(200, 400 - Input.SBSCover);
                    y = rd.Next(200, 400 - Input.SBSCover);
                }
                SBS new_SBS = new SBS(x, y, 0);
                //Tao SUE trong vung phu cua SBS
                for (int j = 0; j < Input.NumberofSUE; j++)
                {
                    UE new_SUE = new UE(x + rd.Next(-Input.SBSCover, Input.SBSCover), y + rd.Next(-Input.SBSCover, Input.SBSCover), 0);
                    new_SUE.PowerTr = Input.SUE_Max_PowerTr;
                    new_SBS.Id_SBS = i + 1;
                    new_SUE.UE_Id = ++Input.UE_Id;
                    new_SBS.ListUE.Add(new_SUE);
                }
                //Tao Pair trong vung phu SBS
                for (int j = 0; j < Input.NumberofPair; j++)
                {
                    Pair pair = new Pair();
                    //Fixed again position Pair
                    UE t_UE = new UE(x + rd.Next(-Input.SBSCover, Input.SBSCover), y + rd.Next(-Input.SBSCover, Input.SBSCover), 0);
                    t_UE.UE_Id = ++Input.UE_Id;
                    UE r_UE = new UE(x + rd.Next(-Input.SBSCover, Input.SBSCover), y + rd.Next(-Input.SBSCover, Input.SBSCover), 0);
                    t_UE.PowerTr = Input.D2D_Max_PowerTr;
                    r_UE.PowerTr = Input.D2D_Max_PowerTr;
                    pair.UE_r = r_UE;
                    pair.UE_t = t_UE;
                    new_SBS.ListPair.Add(pair);
                }
                Input.List_SBS.Add(new_SBS);
            }
            //SBS chon BWP
            for (int i = 0; i < Input.NumberofSBS; i++)
            {
                List<BWP> _listBWP = new List<BWP>();
                for (int j = 0; j < Input.List_MBS[0].ListBWP.Count; j++)
                {
                    BWP newBWP = (BWP)Input.List_MBS[0].ListBWP[j].Clone();
                    _listBWP.Add(newBWP);
                }
                Request req = new Request(_listBWP);
                RequestProcess process = new RequestProcess();
                process.processBeconReq(req);
                Input.List_SBS[i].quickSort(_listBWP, 0, _listBWP.Count - 1);
                Console.WriteLine("SBS");
                for (int j = 0; j < Input.NumberOfBWP_permit; j++)
                {
                    Input.List_SBS[i].ListBWP.Add(_listBWP[j]);
                }
                Console.WriteLine("SBS_" + Input.List_SBS[i].Id_SBS);
                for (int j = 0; j < Input.List_SBS[i].ListBWP.Count; j++)
                {
                    Console.WriteLine("Id of BWP = " + Input.List_SBS[i].ListBWP[j].Id_BWP + " count=" + Input.List_SBS[i].ListBWP[j].Count);
                }
            }
            //Khoi tao vi tri M-UE
            for (int i = 0; i < Input.NumberofM_UE; i++)
            {
                UE newUE = new UE();
                newUE.UE_Id = ++Input.UE_Id;
                Random rd = new Random(Guid.NewGuid().GetHashCode());
                int x = 0, y = 0;
                if (i % 4 == 0)
                {
                    x = rd.Next(0, 200);
                    y = rd.Next(0, 200);
                }
                else if (i % 4 == 1)
                {
                    x = rd.Next(0, 200);
                    y = rd.Next(200, 400);
                }
                else if (i % 4 == 2)
                {
                    x = rd.Next(200, 400);
                    y = rd.Next(0, 200);
                }
                else if (i % 4 == 3)
                {
                    x = rd.Next(200, 400);
                    y = rd.Next(200, 400);
                }
                Position position = new Position(x, y, 0);
                newUE.Position = position;
                newUE.PowerTr = Input.Max_PowerTr;
                Input.List_MBS[0].ListUE.Add(newUE);
            }

            //Khoi tao request cap kenh cho M-UE
            for (int i = 0; i < Input.List_MBS[0].ListUE.Count; i++)
            {
                Request new_Request = new Request(Input.List_MBS[0].ListUE[i]);
                Input.List_MBS[0].ListRequest.Add(new_Request);
            }
            //MBS xu li cap kenh cho M-UE
            RequestProcess ReqProc = new RequestProcess();
            ReqProc.processRequest();
            
            //SBS chon BWP cap cho device
            //Check tung SBS xem BWP nao dang duoc su sung
            for(int i=0; i < Input.List_SBS.Count; i++)
            {
                SBS new_sbs = Input.List_SBS[i];
                //Kiem tra SBS(i) dang dung BWP nao thi cong them 1 muc nang luong
                for (int k = 0; k < Input.List_SBS.Count; k++)
                {
                    for (int j = 0; j < Input.List_SBS[k].ListBWP.Count; j++)
                    {
                        if (new_sbs.Id_SBS != Input.List_SBS[k].Id_SBS && new_sbs.ListBWP.Exists(x => x.Id_BWP == Input.List_SBS[k].ListBWP[j].Id_BWP) == true && Input.List_SBS[k].ListBWP[j].Active == true)
                        {
                           // Console.WriteLine("SBS" + new_sbs.Id_SBS + "-" + Input.List_SBS[k].Id_SBS + " use BWP " + Input.List_SBS[k].ListBWP[j].Id_BWP);
                            for(int m=0; m < new_sbs.ListBWP.Count; m++)
                            {
                                if(new_sbs.ListBWP[m].Id_BWP == Input.List_SBS[k].ListBWP[j].Id_BWP)
                                {
                                    double pathloss = new_sbs.PassLoss(Input.List_SBS[k].Position);
                                    double E = 100 - pathloss;
                                    E = Math.Pow(10, (E / 10));
                                    new_sbs.ListBWP[m].Pow_infer += E;
                                }
                            }
                        }
                    }
                }
                //Sort tang dan BWP theo muc nang luong de chon BWP cap cho device
                new_sbs. (new_sbs.ListBWP, 0, new_sbs.ListBWP.Count - 1);
                Console.WriteLine("SBS_" + i);
                for(int j=0; j < new_sbs.ListBWP.Count; j++)
                {
                    Console.WriteLine("BWP:" + new_sbs.ListBWP[j].Id_BWP + "_" + new_sbs.ListBWP[j].Pow_infer);
                }
                Console.WriteLine("-------------------------");
                //Chon r BWP dau de cap cho D2D
                int _indexBWP = 0;
                //Cap kenh cho tung pair
                int _indexchannel = 0;
                //Cap r kenh cho moi Pair
                for (int k = 0; k < new_sbs.ListPair.Count; k++)
                {
                    //cap cho D2D r kenh
                    for (int m = 0; m < Input.r; m++)
                    {
                        //Check _indexchannel > channel/BWP - 1 (do lay 1 kenh tham chieu)
                        if (_indexchannel == (Input.NumberofChannel / Input.NumberofBWP))
                        {
                            _indexchannel = 0;
                            _indexBWP++;
                        }
                        if (_indexBWP < Input.NumberofBWP_D2D)
                        {
                            //Danh dau SBS dang dung BWP nay
                            new_sbs.ListBWP[_indexBWP].Active = true;
                            Channel new_channel = new_sbs.ListBWP[_indexBWP].ListChannel[_indexchannel];
                            new_sbs.ListPair[k].UE_t.List_channel.Add(new_channel);
                            new_sbs.ListBWP[_indexBWP].ListChannel[_indexchannel].List_device.Add(new_sbs.ListPair[k].UE_t);
                            _indexchannel++;
                        }
                        else
                        {
                            Console.WriteLine("Sorry, don't have enough BWP");
                            break;
                        }
                    }
                }
                //Cap s kenh cho moi SUE
                for(int k=0; k < new_sbs.ListUE.Count; k++) {
                    //cap cho SUE s kenh
                    for (int m = 0; m < Input.s; m++)
                    {
                        //Check _indexchannel > channel/BWP - 1 ( do lay 1 kenh tham chieu)
                        if (_indexchannel == (Input.NumberofChannel / Input.NumberofBWP) )
                        {
                            _indexchannel = 0;
                            _indexBWP++;
                        }
                        if (_indexBWP < Input.NumberOfBWP_permit)
                        {
                            //Danh dau SBS dang dung BWP nay
                            new_sbs.ListBWP[_indexBWP].Active = true;
                            Channel new_channel = new_sbs.ListBWP[_indexBWP].ListChannel[_indexchannel];
                            new_sbs.ListUE[k].List_channel.Add(new_channel);
                            new_sbs.ListBWP[_indexBWP].ListChannel[_indexchannel].List_device.Add(new_sbs.ListUE[k]);
                            _indexchannel++;
                        }
                        else
                        {
                            Console.WriteLine("Sorry, don't have enough BWP");
                            break;
                        }
                    }
                }
            }

            // Gia su MUE o bien
            //Moi kenh co 1 muc interference cho phep
            for(int i=0; i < Input.List_MBS[0].ListUE.Count; i++)
            {
                double I_total,I_i;
                double linkgain, pathloss, shadowing;
                var lognormal = new LogNormal(0, 1);
                shadowing = lognormal.Sample();
                pathloss = 127 + 30 * Math.Log10(Input.MBSCover * Math.Pow(10, -3)); ;
                linkgain = pathloss + shadowing;
                Input.List_MBS[0].ListUE[i].Shadowing = shadowing;
                Input.List_MBS[0].ListUE[i].Linkgain = linkgain;
                I_total = Input.Max_PowerTr - linkgain - Input.SINR_threshold; //dB
                //Nhieu cho phep tren moi device
                I_i = I_total - 10 * Math.Log10(Input.List_SBS.Count);
                for(int j=0; j < Input.List_SBS.Count; j++)
                {
                    for(int k=0; k < Input.List_SBS[j].ListBWP.Count; k++)
                    {
                        if(Input.List_SBS[j].ListBWP[k].ListChannel.Exists(x => x.channel_id == Input.List_MBS[0].ListUE[i].channel_id)){
                            for(int m=0; m < Input.List_SBS[j].ListBWP[k].ListChannel.Count; m++)
                            {
                                if(Input.List_SBS[j].ListBWP[k].ListChannel[m].channel_id == Input.List_MBS[0].ListUE[i].channel_id)
                                {
                                    Input.List_SBS[j].ListBWP[k].ListChannel[m].Limit_interference = I_i;
                                }
                            }
                        }
                    }
                }
            }


            //Tinh cong suat phat cho phep.
            for(int i=0; i < Input.List_SBS.Count; i++)
            {
                for(int j=0; j < Input.List_SBS[i].ListBWP.Count; j++)
                {
                    for(int k=0; k < Input.List_SBS[i].ListBWP[j].ListChannel.Count; k++)
                    {
                        //Gioi han cong suat phat tren moi kenh
                        for (int m = 0; m < Input.List_SBS[i].ListBWP[j].ListChannel[k].List_device.Count; m++)
                        {
                            double linkgain, pathloss, shadowing;
                            var lognormal = new LogNormal(0, 1);
                            shadowing = lognormal.Sample();
                            pathloss = Input.List_SBS[i].PassLoss(Input.List_MBS[0].Position);
                            linkgain = pathloss + shadowing;
                            Input.List_SBS[i].ListBWP[j].ListChannel[k].List_device[m].Shadowing = shadowing;
                            Input.List_SBS[i].ListBWP[j].ListChannel[k].List_device[m].PowerTr = Input.List_SBS[i].ListBWP[j].ListChannel[k].Limit_interference + linkgain;

                        }
                    }
                }
                
            }









            //Tinh SINR tung M-UE
             double tPutMUE = 0;
             for (int i = 0; i < Input.List_MBS[0].ListUE.Count; i++)
             {
                 int channel_id = Input.List_MBS[0].ListUE[i].channel_id - 1;
                 double I_total = 0;
                 double P_r = Input.List_MBS[0].ListUE[i].PowerTr - Input.List_MBS[0].ListUE[i].Linkgain;
                 double SINR;
                 double M_Capacity = 0;
                //Xet SUE
                for (int k = 0; k < Input.List_SBS.Count; k++)
                {
                    for (int m = 0; m < Input.List_SBS[k].ListBWP.Count; m++)
                    {
                        for(int p=0; p < Input.List_SBS[k].ListBWP[m].ListChannel.Count; p++)
                        {
                            if (Input.List_SBS[k].ListBWP[m].ListChannel[p].channel_id == channel_id)
                            {
                                UE ue_t = Input.List_SBS[k].ListBWP[m].ListChannel[p].List_device[0];
                                double pathloss = ue_t.PassLoss(Input.List_MBS[0].Position);
                                double Linkgain = ue_t.Shadowing + pathloss;
                                I_total += Math.Pow(10, (ue_t.PowerTr / 10) - (Linkgain / 10));
                            }
                        }
                    }
                }
                 if (I_total != 0)
                 {
                     I_total = 10 * Math.Log10(I_total);
                     SINR = P_r - I_total;
                 }
                 else
                 {
                     SINR = 0;
                 }
                 //Capacity
                 {
                     double C;
                     if (SINR < 0.5 && SINR > Input.SINR_outage)
                         C = 0.25 * Input.B;
                     else if (SINR > 0.5 && SINR < 3.5)
                         C = 0.5 * Input.B;
                     else if (SINR > 3.5 && SINR < 6.5)
                         C = 1 * Input.B;
                     else if (SINR > 6.5 && SINR < 9)
                         C = 1.5 * Input.B;
                     else if (SINR > 9 && SINR < 12.5)
                         C = 2 * Input.B;
                     else if (SINR > 12.5 && SINR < 14.5)
                         C = 3 * Input.B;
                     else if (SINR > 14.5 && SINR < 16.5)
                         C = 3 * Input.B;
                     else if (SINR > 16.5 && SINR < 18.5)
                         C = 4 * Input.B;
                     else if (SINR >= 18.5)
                         C = 4.5 * Input.B;
                     else
                         C = 0;
                     M_Capacity += C;
                 }
                 Input.List_MBS[0].ListUE[i].SINR = SINR;
                 tPutMUE += M_Capacity;
             }
             Console.WriteLine("Throughput of MUE=" + tPutMUE);
            /*
             //Tinh SINR tung pair
             Console.WriteLine("SINR pair--------------------------");
             double throughput = 0;
             for (int i = 0; i < Input.List_MBS[0].ListPair.Count; i++)
             {
                 UE ue_t = Input.List_MBS[0].ListPair[i].UE_t;
                 UE ue_r = Input.List_MBS[0].ListPair[i].UE_r;
                 Console.WriteLine("Device" + ue_t.UE_Id);
                 double Capacity = 0;

                 for (int j = 0; j < ue_t.List_channel.Count; j++)
                 {
                     double P_r;
                     double I_total = 0;
                     double SINR, C;
                     Channel channel = Input.List_MBS[0].ListChannel[ue_t.List_channel[j].channel_id - 1];
                     //Tim ue trong list_device
                     int index = 0;
                     while (channel.List_device[index].UE_Id != ue_t.UE_Id)
                     {
                         index++;
                     }

                     P_r = channel.List_device[index].PowerTr - ue_t.List_channel[j].LinkGain;
                     for (int k = 0; k < channel.ListMUE.Count; k++)
                     {
                         double pathloss = channel.ListMUE[k].PassLoss(ue_r.Position);
                         var lognormal = new LogNormal(0, 1);
                         double shadowing = lognormal.Sample();
                         double linkgain = pathloss + shadowing;
                         I_total += Math.Pow(10, (channel.ListMUE[k].PowerTr / 10) - (linkgain / 10));
                     }
                     for (int k = 0; k < channel.List_device.Count; k++)
                     {
                         double pathloss = channel.List_device[k].PassLoss(ue_r.Position);
                         var lognormal = new LogNormal(0, 1);
                         double shadowing = lognormal.Sample();
                         double linkgain = pathloss + shadowing;
                         if (channel.List_device[k].UE_Id != ue_t.UE_Id)
                             I_total += Math.Pow(10, (channel.List_device[k].PowerTr / 10) - (linkgain / 10));

                     }
                     for (int k = 0; k < channel.ListSUE.Count; k++)
                     {
                         double pathloss = channel.ListSUE[k].PassLoss(ue_r.Position);
                         var lognormal = new LogNormal(0, 1);
                         double shadowing = lognormal.Sample();
                         double linkgain = pathloss + shadowing;
                         I_total += Math.Pow(10, (channel.ListSUE[k].PowerTr / 10) - (linkgain / 10));
                     }

                     if (I_total != 0)
                     {
                         I_total = 10 * Math.Log10(I_total);
                         SINR = P_r - I_total;
                         Console.WriteLine("=> SINR_" + ue_r.UE_Id + " = " + SINR + "   (dB)");

                     }
                     else
                     {
                         SINR = 0;
                     }
                     {
                         if (SINR < 0.5 && SINR > Input.SINR_outage)
                             C = 0.25 * Input.B;
                         else if (SINR > 0.5 && SINR < 3.5)
                             C = 0.5 * Input.B;
                         else if (SINR > 3.5 && SINR < 6.5)
                             C = 1 * Input.B;
                         else if (SINR > 6.5 && SINR < 9)
                             C = 1.5 * Input.B;
                         else if (SINR > 9 && SINR < 12.5)
                             C = 2 * Input.B;
                         else if (SINR > 12.5 && SINR < 14.5)
                             C = 3 * Input.B;
                         else if (SINR > 14.5 && SINR < 16.5)
                             C = 3 * Input.B;
                         else if (SINR > 16.5 && SINR < 18.5)
                             C = 4 * Input.B;
                         else if (SINR >= 18.5)
                             C = 4.5 * Input.B;
                         else
                             C = 0;
                     }
                     //Tinh Capacity cua Device
                     Capacity += C;
                 }
                 Console.WriteLine("Capacity=" + Capacity);
                 throughput += Capacity;

             }
             Console.WriteLine("throughput=" + throughput);
             Input.List_MBS[0].ListTP.Add(throughput);*/
        }
        public static void Create_eventList() { }
        public static void Excuting_Event() { }
        public static void Output()
        {
            /*    Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("Total D2D_pair = " + Input.List_Pair.Count);
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("L = 127 + 30*Log(d*10^-3) (dB) (distance: met)");
                Console.WriteLine("Total interference = (P_tr1/L1) + (P_tr2/L2) + ... + (P_tr10/L10)" +
                 "= 10^[(P_tr1(dBm)-L1(dB))/10] + 10^[(P_tr2(dBm)-L2(dB))/10] + ... + 10^[(P_tr10(dBm)-L10(dB))/10] ");
                Console.WriteLine("SINR(dB) = (P_tr-L)(dBm) - 10log_10_(I)(dB)");
                Console.WriteLine("------------------------------------------------------");*/
            /*    for (int i = 0; i < Input.List_MBS[0].ListUE.Count; i++)
                {
                    Console.WriteLine("UE" + Input.List_MBS[0].ListUE[i].UE_Id + "use channel:" + Input.List_MBS[0].ListUE[i].channel_id);
                }
                for (int i = 0; i < Input.List_SBS.Count; i++)
                {
                    Console.WriteLine("Id S-UE" + Input.List_SBS[i].ListUE[0].UE_Id);
                    for (int j = 0; j < Input.List_SBS[i].ListUE[0].List_channel_id.Count; j++)
                    {
                        Console.WriteLine(Input.List_SBS[i].ListUE[0].List_channel_id[j]);
                    }
                }
                for (int i = 0; i < Input.List_MBS[0].ListChannel.Count; i++)
                {
                    Console.WriteLine("Channel " + (i + 1));
                    for (int j = 0; j < Input.List_MBS[0].ListChannel[i].ListUE.Count; j++)
                    {
                        Console.WriteLine(Input.List_MBS[0].ListChannel[i].ListUE[j].UE_Id);
                    }
           *//*     }*//*
            for(int i = 0; i < Input.List_MBS[0].ListPair.Count; i++)
            {               
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Chose r channel higher linkgain");
                for (int j = 0; j < Input.List_MBS[0].ListPair[i].UE_t.List_channel.Count; j++)
                {
                    Console.WriteLine("Channel" + Input.List_MBS[0].ListPair[i].UE_t.List_channel[j].channel_id + " has linkgain:" + Input.List_MBS[0].ListPair[i].UE_t.List_channel[j].LinkGain);
                }
                Console.WriteLine("*********************************************************");
            }*/
            string path1 = Input.path + "\\Input with Power" + Input.D2D_Max_PowerTr + "_" + Input.SUE_Max_PowerTr + ".txt";
            StreamWriter sw1 = new StreamWriter(path1, false);
            sw1.WriteLine("L = 127 + 30*Log(d*10^-3) (dB) (distance: met)");
            sw1.WriteLine("Total interference = (P_tr1/L1) + (P_tr2/L2) + ... + (P_tr10/L10)" +
             "= 10^[(P_tr1(dBm)-L1(dB))/10] + 10^[(P_tr2(dBm)-L2(dB))/10] + ... + 10^[(P_tr10(dBm)-L10(dB))/10]  (W)");
            sw1.WriteLine("SINR(dB) = (P_tr-L)(dBm) - 10log_10_(I)(dB)");
            sw1.WriteLine("-------------------------------------");
            sw1.WriteLine("NumberOfMacroUE: " + Input.NumberofM_UE);
            sw1.WriteLine("NumberOfSBS: " + Input.NumberofSBS);
            sw1.WriteLine("NumberOfPairD2D: " + Input.NumberofPair);
            sw1.WriteLine("NumberOfChannel: " + Input.NumberofChannel);
            sw1.WriteLine("-------------------------------------");
            sw1.WriteLine("MBSCover: " + Input.MBSCover + " (m)");
            sw1.WriteLine("SBSCover: " + Input.SBSCover + " (m)");
            sw1.WriteLine("MaxPowerofMacro: " + Input.Max_PowerTr + " (dBm)");
            sw1.WriteLine("MaxPowerofDevice: " + Input.D2D_Max_PowerTr + " (dBm)");
            sw1.WriteLine("-------------------------------------");
            sw1.WriteLine("Information of MacroUE: ");
            for (int i = 0; i < Input.List_MBS[0].ListUE.Count; i++)
            {
                sw1.WriteLine("MacroUE has ID: " + Input.List_MBS[0].ListUE[i].UE_Id + " and shadowing=" + Input.List_MBS[0].ListUE[i].Shadowing + " use channel " + Input.List_MBS[0].ListUE[i].channel_id + " has list device using this channel");
                for (int j = 0; j < Input.List_MBS[0].ListChannel[Input.List_MBS[0].ListUE[i].channel_id - 1].ListMUE.Count; j++)
                {
                    UE ue = Input.List_MBS[0].ListChannel[Input.List_MBS[0].ListUE[i].channel_id - 1].ListMUE[j];
                    sw1.WriteLine("  UE_id:" + ue.UE_Id + " Power= " + ue.PowerTr + " has distance MBS is " +
                                                            ue.Position.d(ue.Position, Input.List_MBS[0].Position));
                }
                for (int j = 0; j < Input.List_MBS[0].ListChannel[Input.List_MBS[0].ListUE[i].channel_id - 1].ListSUE.Count; j++)
                {
                    UE ue = Input.List_MBS[0].ListChannel[Input.List_MBS[0].ListUE[i].channel_id - 1].ListSUE[j];
                    sw1.WriteLine("  ID of SUE:" + ue.UE_Id + " Power= " + ue.PowerTr + " has distance MBS is " +
                                                            ue.Position.d(ue.Position, Input.List_MBS[0].Position));
                }
                for (int j = 0; j < Input.List_MBS[0].ListChannel[Input.List_MBS[0].ListUE[i].channel_id - 1].List_device.Count; j++)
                {
                    UE ue = Input.List_MBS[0].ListChannel[Input.List_MBS[0].ListUE[i].channel_id - 1].List_device[j];
                    sw1.WriteLine("  ID of Device:" + ue.UE_Id + " Power= " + ue.PowerTr + " has distance MBS is " +
                                                            ue.Position.d(ue.Position, Input.List_MBS[0].Position));
                }
                sw1.WriteLine("=>SINR " + Input.List_MBS[0].ListUE[i].SINR);
            }
            for (int i = 0; i < Input.List_MBS[0].ListTP.Count; i++)
            {
                sw1.WriteLine("throughput=" + Input.List_MBS[0].ListTP[i]);

            }
            sw1.Close();
        }
    }
}
