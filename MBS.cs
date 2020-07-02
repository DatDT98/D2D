using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace D2D_simulation
{
    class MBS
    {
        private Position _position;
        public Position Position
        {
            get { return _position; }
            set { _position = value; }
        }
        public MBS(double x, double y, double z)
        {
            Position position = new Position(x, y, z);
            this.Position = position;
        }
        private int id_MBS;
        public int Id_MBS
        {
            get { return id_MBS; }
            set { id_MBS = value; }
        }
        private List<Pair> listPair = new List<Pair>();
        public List<Pair> ListPair
        {
            set { listPair = value; }
            get { return listPair; }
        }

        public void AddPair(Pair p)
        {
            ListPair.Add(p);
        }
        private List<UE> listUE = new List<UE>();
        public List<UE> ListUE
        {
            set { listUE = value; }
            get { return listUE; }
        }

        public void AddUE(UE ue)
        {
            ListUE.Add(ue);
        }
        private List<Channel> listChannel = new List<Channel>();
        public List<Channel> ListChannel
        {
            set { listChannel = value; }
            get { return listChannel; }
        }

        public void AddChannel(Channel channel)
        {
            ListChannel.Add(channel);
        }
        private List<Request> listRequest = new List<Request>();
        public List<Request> ListRequest
        {
            set { listRequest = value; }
            get { return listRequest; }
        }
        private List<double> listTP = new List<double>();
        public List<double> ListTP
        {
            set { listTP = value; }
            get { return listTP; }
        }
        private List<BWP> listBWP = new List<BWP>();
        public List<BWP> ListBWP
        {
            set { listBWP = value; }
            get { return listBWP; }
        }
        public void AddRequest(Request req)
        {
            ListRequest.Add(req);
        }
        public List<int> SubChannel1 = new List<int>();
        public List<int> SubChannel2 = new List<int>();
        public List<int> SubChannel3 = new List<int>();


    }
}
