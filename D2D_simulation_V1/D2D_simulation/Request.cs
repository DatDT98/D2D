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
        public Request(UE ue)
        {
            this.UE = ue;
        }
    }
}
