using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.ComponentsData;
using ReactiveUI;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WpfApplication1.ViewModel
{
    class ViewModelMain : ReactiveObject, System.ComponentModel.INotifyPropertyChanged
    {
        public List<ComponentData> components;
        public int nextIndex;
        
        public ViewModelMain()
        {
            components = new List<ComponentData>();
            nextIndex = 0;
            //this.WhenAnyValue(x => x.NewChanges).Subscribe(
            //    x => ShowParam1()
            //    );

            ListBoxSourse = new ObservableCollection<string>();
            ListBoxSourse.Add("11");
            AddDuctCommand = ReactiveCommand.CreateFromTask(async () => await AddDuct());
            ShowParamCommand = ReactiveCommand.CreateFromTask(async () => await ShowParam());
        }
       

        private async Task AddDuct()
        {
            ListBoxSourse.Add("Add duct");

            ComponentData component = new RoundDuctData("" + nextIndex);
            components.Add(component);
            NewChanges = nextIndex;
            NewChanges = -1;
            nextIndex++;
            //viewportLayout1.Add3DComponent(component);
        }
        private async Task ShowParam()
        {
            string message;


            message = "components:\n";
            int i = 1;
            foreach (var com in components)
            {
                message += "Component" + i + " (" + com.X + ", " + com.Y + ", " + com.Z + ")\n";
                i++;
            }
            listBoxSourse.Add(message);

            //listBoxSourse = new ObservableCollection<string>(ListBoxSourse);
           
            //NewChanges = NewChanges;
        }
        public ObservableCollection<string> ListBoxSourse
        {
            get { return listBoxSourse; }
            set
            {
                this.RaiseAndSetIfChanged(ref listBoxSourse, value);
            }
        }
        //OnPropertyChanged("ListBoxSourse");
        // PropertyChanged+=(this, new System.ComponentModel.PropertyChangedEventArgs("ListBoxSourse"));
        //public string NewChanges
        //{
        //    get { return NewChanges; }
        //    set
        //    {
        //        this.RaiseAndSetIfChanged(ref listBoxSourse, value);
        //        //OnPropertyChanged("ListBoxSourse");
        //        // PropertyChanged+=(this, new System.ComponentModel.PropertyChangedEventArgs("ListBoxSourse"));
        //    }
        //}
        private int newChanges = -1;

        public int NewChanges
        {
            get { return newChanges; }
            set { this.RaiseAndSetIfChanged(ref newChanges, value); }
        }
        public ObservableCollection<string> listBoxSourse;
        private int selectedComponentIndex = -1;

        public int NewChSelectedComponentIndexanges
        {
            get { return selectedComponentIndex; }
            set { this.RaiseAndSetIfChanged(ref selectedComponentIndex, value); }
        }
        
        public ReactiveCommand AddDuctCommand { get; }
        public ReactiveCommand ShowParamCommand { get; }

        
       

    }
}
