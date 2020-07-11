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
        private BWP bwp;
        public BWP BWP
        {
            get { return bwp; }
            set { bwp = value; }
        }
        private List<BWP> listBWP = new List<BWP>();
        public List<BWP> ListBWP
        {
            set { listBWP = value; }
            get { return listBWP; }
        }
        private List<Pair> listPair = new List<Pair>();
        public List<Pair> ListPair
        {
            set { listPair = value; }
            get { return listPair; }
        }
        private List<UE> listUE = new List<UE>();
        public List<UE> ListUE
        {
            set { listUE = value; }
            get { return listUE; }
        }
        public int partition(List<BWP> arr, int low,
                                  int high)
        {
            double pivot = arr[high].Count;

            // index of smaller element 
            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                // If current element is smaller  
                // than the pivot 
                if (arr[j].Count < pivot)
                {
                    i++;
                    // swap arr[i] and arr[j] 
                    BWP temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            // swap arr[i+1] and arr[high] (or pivot) 
            BWP temp1 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp1;
            return i + 1;

        }
        /* The main function that implements QuickSort() 
        arr[] --> Array to be sorted, 
        low --> Starting index, 
        high --> Ending index */
        public void quickSort(List<BWP> arr, int low, int high)
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

        public int lvPow_partition(List<BWP> arr, int low,
                                  int high)
        {
            double pivot = arr[high].Pow_infer;

            // index of smaller element 
            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                // If current element is smaller  
                // than the pivot 
                if (arr[j].Pow_infer < pivot)
                {
                    i++;
                    // swap arr[i] and arr[j] 
                    BWP temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            // swap arr[i+1] and arr[high] (or pivot) 
            BWP temp1 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp1;
            return i + 1;

        }
        /* The main function that implements QuickSort() 
        arr[] --> Array to be sorted, 
        low --> Starting index, 
        high --> Ending index */
        public void lvPow_quickSort(List<BWP> arr, int low, int high)
        {
            if (low < high)
            {

                /* pi is partitioning index, arr[pi] is  
                now at right place */
                int pi = lvPow_partition(arr, low, high);

                // Recursively sort elements before 
                // partition and after partition 
                lvPow_quickSort(arr, low, pi - 1);
                lvPow_quickSort(arr, pi + 1, high);
            }
        }
        public double PassLoss(Position pos)
        {
            double passloss;
            double distance = this.Position.d(this.Position, pos);

            passloss = 127 + 30 * Math.Log10(distance * Math.Pow(10, -3));
            return passloss;
        }
    }
}
