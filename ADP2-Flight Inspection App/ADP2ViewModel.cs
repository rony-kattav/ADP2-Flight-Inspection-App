using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP2_Flight_Inspection_App
{
    public partial class ADP2ViewModel
    {
        private IADP2Model model;
        private string vm_csv;
        public string VM_csv
        {
            get { return vm_csv; }
            set { vm_csv = value; }
               
        }
        private string vm_xml;
        public string VM_xml
        {
            get { return vm_xml; }
            set { vm_xml = value; }
        }


        public ADP2ViewModel(IADP2Model m)
        {
            model = m;
            VM_csv = "Enter CSV file path";
            VM_xml = "Enter XML file path";
            
        }
        
        
        public void modelStart()
        {

            //model.updateProperties(VM_csv, VM_xml);
            model.start();
        }


    }
}
