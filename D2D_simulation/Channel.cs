using System;
using System.Collections.Generic;
using System.Text;

namespace D2D_simulation
{
    class Channel
    {
        public Channel() { }
        private int _channel_id;
        public int channel_id
        {
            get { return _channel_id; }
            set { _channel_id = value; }
        }
        private double _linkgain;
        public double LinkGain
        {
            get { return _linkgain; }
            set { _linkgain = value; }
        }
        private double _maxPower;
        public double MaxPower
        {
            get { return _maxPower; }
            set { _maxPower = value; }
        }
        private double _D2DmaxPower;
        public double D2D_MaxPower
        {
            get { return _D2DmaxPower; }
            set { _D2DmaxPower = value; }
        }
        private double limit_interference;
        public double Limit_interference
        {
            get { return limit_interference; }
            set { limit_interference = value; }
        }
        private double _SUEmaxPower;
        public double SUE_MaxPower
        {
            get { return _SUEmaxPower; }
            set { _SUEmaxPower = value; }
        }
        private List<UE> listMUE = new List<UE>();
        public List<UE> ListMUE
        {
            set { listMUE = value; }
            get { return listMUE; }
        }
        public void AddMUE(UE ue)
        {
            ListMUE.Add(ue);
        }
        private List<UE> list_device = new List<UE>();
        public List<UE> List_device
        {
            set { list_device = value; }
            get { return list_device; }
        }
        public void AddD2D(UE ue)
        {
            List_device.Add(ue);
        }
        private List<UE> listSUE = new List<UE>();
        public List<UE> ListSUE
        {
            set { listSUE = value; }
            get { return listSUE; }
        }
        public void AddSUE(UE ue)
        {
            ListSUE.Add(ue);
        }
    }
}
