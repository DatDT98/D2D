using System;
using System.Collections.Generic;
using System.Text;

namespace D2D_simulation
{
    class Request
    {
        public Request() { }
        private UE ue;
        public UE UE
        {
            get { return ue; }
            set { ue = value; }
        }
        private SBS _sbs;
        public SBS sbs
        {
            get { return _sbs; }
            set { _sbs = value; }
        }
        private List<BWP> _bwp;
        public List<BWP> bwp
        {
            get { return _bwp; }
            set { _bwp = value; }
        }
        public Request(UE ue)
        {
            this.UE = ue;
        }
        public Request(List<BWP> bwp)
        {
            this.bwp = bwp;
        }
    }
}
