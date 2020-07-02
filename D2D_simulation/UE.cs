using System;
using System.Collections.Generic;
using System.Text;
namespace D2D_simulation
{
    class UE
    {
        public UE() { }
        public UE(double x, double y, double z)
        {
            Position position = new Position(x, y, z);
            this.Position = position;
        }
        private double _powerTr;
        public double PowerTr
        {
            get { return _powerTr; }
            set { _powerTr = value; }
        }
        private UE _neighbour;
        public UE Neighbour
        {
            get { return _neighbour; }
            set { _neighbour = value; }
        }
        private Position _position;
        public Position Position
        {
            get { return _position; }
            set { _position = value; }
        }
        private bool _paired = false;
        public bool Paired
        {
            get { return _paired; }
            set { _paired = value; }
        }
        private int _channel_id;
        public int channel_id
        {
            get { return _channel_id; }
            set { _channel_id = value; }
        }
        private double _linkgain;
        public double Linkgain
        {
            get { return _linkgain; }
            set { _linkgain = value; }
        }
        private double _sinr;
        public double SINR
        {
            get { return _sinr; }
            set { _sinr = value; }
        }
        private List<int> listChannel_Id = new List<int>();
        public List<int> ListChannel_Id
        {
            get { return listChannel_Id; }
            set { listChannel_Id = value; }
        }
        public void Add_ListIdChannel(int channel)
        {
            ListChannel_Id.Add(channel);
        }
        private List<Channel> list_channel = new List<Channel>();
        public List<Channel> List_channel
        {
            get { return list_channel; }
            set { list_channel = value; }
        }
        public void Add_ListChannel(Channel channel)
        {
            List_channel.Add(channel);
        }
        private int _UE_Id;
        public int UE_Id
        {
            get { return _UE_Id; }
            set { _UE_Id = value; }
        }
        private double _shadowing;
        public double Shadowing
        {
            get { return _shadowing; }
            set { _shadowing = value; }
        }
        private double _Capacity;
        public double Capacity
        {
            get { return _Capacity; }
            set { _Capacity = value; }
        }
        public double PassLoss(Position pos)
        {
            double passloss;
            double distance = this.Position.d(this.Position, pos);

            passloss = 127 + 30*Math.Log10(distance*Math.Pow(10,-3));
            return passloss;
        }

        //Quicksort
        public int partition(List<Channel> arr, int low,
                                   int high)
        {
            double pivot = arr[high].LinkGain;

            // index of smaller element 
            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                // If current element is smaller  
                // than the pivot 
                if (arr[j].LinkGain < pivot)
                {
                    i++;

                    // swap arr[i] and arr[j] 
                    Channel temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            // swap arr[i+1] and arr[high] (or pivot) 
            Channel temp1 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp1;

            return i + 1;
        }


        /* The main function that implements QuickSort() 
        arr[] --> Array to be sorted, 
        low --> Starting index, 
        high --> Ending index */
        public void quickSort(List<Channel> arr, int low, int high)
        {
            if (low < high)
            {

                /* pi is partitioning index, arr[pi] is  
                now at right place */
                int pi = partition(arr, low, high);

                // Recursively sort elements before 
                // partition and after partition 
                quickSort(arr, low, pi - 1);
                quickSort(arr, pi + 1, high);
            }
        }

    }
}
