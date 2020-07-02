using System;
using System.Collections.Generic;
using System.Text;

namespace D2D_simulation
{
    class BWP : ICloneable
    {
        public BWP() { }
        private List<Channel> listChannel = new List<Channel>();
        public List<Channel> ListChannel
        {
            set { listChannel = value; }
            get { return listChannel; }
        }
        private int id_BWP;
        public int Id_BWP
        {
            get { return id_BWP; }
            set { id_BWP = value; }
        }
        private int count=0;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        //Copy object
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
