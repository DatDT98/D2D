using System;
using System.Collections.Generic;
using System.Text;

namespace D2D_simulation
{
    class RequestProcess
    {
        public RequestProcess() { }
        //Xu li request
        public static void processRequest()
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
                {
                    indexChannel = 0;
                    Input.List_MBS[0].ListRequest[i].UE.channel_id = Input.List_MBS[0].ListChannel[indexChannel].channel_id;
                    //Add UE vao list channel tuong ung
                    Input.List_MBS[0].ListChannel[indexChannel].AddMUE(Input.List_MBS[0].ListRequest[i].UE);
                    indexChannel++;
                }
            }
        }
    }
}
