using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporterWPF.Management.VMs
{
    class StateVM : INotifyPropertyChanged
    {
        private State _selectedState;
        public State SelectedState
        {
            get
            {
                return _selectedState;
            }
            set
            {
                if (_selectedState != value)
                {
                    _selectedState = value;
                    onpropertychanged("SelectedState");
                    // Perform any pre-notification process here.
                    if (null != PropertyChanged)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedState"));
                    }
                }
            }
        }
        public StateVM()
        {
            States = new ObservableCollection<State>();
            UpDate();
        }

        public void UpDate()
        {
            States.Clear();
            using (var db = new ABFAEntities())
            {

                var sharestan = from state in db.States
                                select state;

                foreach (var item in sharestan)
                {
                    States.Add(new State() { ID_State = item.ID_State, State1 = item.State1 });
                }
            }

        }
        public StateVM(int stateId)
        {
            var d = SelectedState;
            State st = this.states.FirstOrDefault(x => x.ID_State == stateId);
            States.Clear();
            States.Add(st);

        }
        public void selectchange(int stateId)
        {
            SelectedState = States.FirstOrDefault(x => x.ID_State == stateId);
            onpropertychanged("SelectedState");

        }
        public void RuleSelectState(int stateId)
        {
            State st = States.FirstOrDefault(x => x.ID_State == stateId);
            States.Clear();
            States.Add(st);

        }
        public void AddState(int num, short id, string stateName)
        {
            //State state = new State { ID = id, Name = stateName };
            //this.States.Add(state);
        }

        public void RemoveState(string stateName)
        {
            //States.Remove(new State() { Name = stateName });
        }

        public void ClearStates()
        {
            States.Clear();
        }

        private ObservableCollection<State> states;

        public ObservableCollection<State> States
        {
            get { return states; }
            set
            {
                states = value;
                onpropertychanged("States");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void onpropertychanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler(this, new PropertyChangedEventArgs(name));
        }
    }
}

  