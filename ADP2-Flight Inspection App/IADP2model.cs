using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ADP2_Flight_Inspection_App
{
    public interface IADP2Model : INotifyPropertyChanged
    {
        double Speed { set; get; }
        int Time { set; get; }
        int numOfRows { get; }


        void connect();

        void start();

        void stop();

        

    }
}
