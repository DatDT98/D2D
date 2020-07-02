using System;
using System.Collections.Generic;
using System.Text;

namespace D2D_simulation
{
    class RequestProcess
    {
        public RequestProcess() { }
        //Xu li request
        public void processRequest()
        {
            int indexChannel = 0;
            for(int i=0; i < Input.List_MBS[0].ListRequest.Count; i++)
            {
                //Cap kenh theo RR
                if (indexChannel != Input.List_MBS[0].ListChannel.Count)
                {
                    Input.List_MBS[0].ListRequest[i].UE.channel_id = Input.List_MBS[0].ListChannel[indexChannel].channel_id;
                    //Add UE vao list channel tuong ung
                    Input.List_MBS[0].ListChannel[indexChannel].AddMUE(Input.List_MBS[0].ListRequest[i].UE);
                    indexChannel++;
                }
                else
                    indexChannel = 0;
            }
        }
        //Xu li becon request tai SBS
        public void processBeconReq(Request req)
        {
            for(int i=0; i < Input.NumberofSBS; i++)
            {
                for (int j = 0; j < Input.List_SBS[i].ListBWP.Count; j++)
                {

                    if (Input.List_SBS[i].ListBWP[j] != null)
                    {
                        for(int k=0; k < req.bwp.Count; k++)
                        {
                            if (req.bwp[k].Id_BWP == Input.List_SBS[i].ListBWP[j].Id_BWP)
                                req.bwp[k].Count++;
                        }
                    }
                }
            }
        }
    }
}
