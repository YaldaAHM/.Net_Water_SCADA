using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ReporterWPF.Management.VMs
{
    public class InputAnalog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public InputAnalog()
        {
        }
        public InputAnalog(int Id1, string Name1)
        {

            this.Id = Id1;
            this.Name = Name1;
            this.NotifyPropertyChanged("Id");
            this.NotifyPropertyChanged("Name");
        }
        

        public int Id1
        {
            get { return Id; }
            set { Id = value; }
        }

        public string Name1
        {
            get { return Name; }
            set { Name = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
