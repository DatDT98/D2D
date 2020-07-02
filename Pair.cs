using System;
using System.Collections.Generic;
using System.Text;

namespace D2D_simulation
{
    class Pair
    {
        private UE _ue_r;
        public UE UE_r{
            get { return _ue_r; }
            set { _ue_r = value; }
        }
        private UE _ue_t;
        public UE UE_t
        {
            get { return _ue_t; }
            set { _ue_t = value; }
        }
        public Pair()
        {
        }
       

        //Tim pair
        /*public void Creat_Pair()
        {

            for (int i = 0; i < Input.List_UE.Count; i++)
            {
                //Tao Pair moi de add vao list_pair
                Pair pair = new Pair();
                double min = Input.D2DCover;
                bool is_pair = false;
                //neu UE_i da co pair thi nhay sang UE moi
                if (Input.List_UE[i].Paired == true)
                { }    
                //Neu UE chua co pair thi tim 
                else
                {
                    for (int j = i + 1; j < Input.List_UE.Count; j++)
                    {

                        if (Input.List_UE[j].Paired == false && Input.List_UE[i].Position.d(Input.List_UE[i].Position, Input.List_UE[j].Position) <= min)
                        {
                            //Gan min = d 3
                            Input.List_UE[i].Neighbour = Input.List_UE[j];
                            pair.UE_r = Input.List_UE[j];
                            pair.UE_t = Input.List_UE[i];
                            is_pair = true;
                            Input.List_UE[i].Paired = true;
                            Input.List_UE[j].Paired = true;
                            break;
                        }
                        //neu d < min thi gan lai min = d
                        
                    }
                    if (is_pair == true)
                    {
                        Input.List_MBS[0].AddPair(pair);
                    }
                    else
                    {
                        Input.List_MBS[0].AddUE(Input.List_UE[i]);
                    }
                }
                //Add pair vao list_pair
            }

        }*/
    }
}
