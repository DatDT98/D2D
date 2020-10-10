using System;
using System.Collections.Generic;
using System.Text;

namespace D2D_simulation
{
    class SBS
    {
        public SBS() { }
        public SBS(double x, double y, double z)
        {
            Position position = new Position(x, y, z);
            this.Position = position;
        }
        private Position _position;
        public Position Position
        {
            get { return _position; }
            set { _position = value; }
        }
        
        private int id_SBS;
        public int Id_SBS
        {
            get { return id_SBS; }
            set { id_SBS = value; }
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
        public List<int> List_SubChannel = new List<int>();

    }
}
