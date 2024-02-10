using Ado.Net.Ls5.DBFirst_EF.Date._010224.Commands;
using Ado.Net.Ls5.DBFirst_EF.Date._010224.DBFirstModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ado.Net.Ls5.DBFirst_EF.Date._010224
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private ObservableCollection<City> allCities;
        public ObservableCollection<City> AllCities
        {
            get { return allCities; }
            set { allCities = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Schedule> citySchedule;
        public ObservableCollection<Schedule> CitySchedule
        {
            get { return citySchedule; }
            set { citySchedule = value; OnPropertyChanged(); }
        }

        private ObservableCollection<AirPlane> airPlaneWhichSchedule;
        public ObservableCollection<AirPlane> AirPlaneWhichSchedule
        {
            get { return airPlaneWhichSchedule; }
            set { airPlaneWhichSchedule = value; OnPropertyChanged(); }
        }

        private City selectedCity;
        public City SelectedCity
        {
            get { return selectedCity; }
            set { selectedCity = value; OnPropertyChanged(); }
        }

        private Schedule selectedSchedule;
        public Schedule SelectedSchedule
        {
            get { return selectedSchedule; }
            set { selectedSchedule = value; OnPropertyChanged(); }
        }


        private AirPlane selectedAirplane;
        public AirPlane SelectedAirplane
        {
            get { return selectedAirplane; }
            set { selectedAirplane = value; OnPropertyChanged(); }
        }

        private string pilotFullName;
        public string PilotFullName
        {
            get { return pilotFullName; }
            set { pilotFullName = value; OnPropertyChanged(); }
        }

        private string pilotAge;
        public string PilotAge
        {
            get { return pilotAge; }
            set { pilotAge = value; OnPropertyChanged(); }
        }



        private string forCity;
        public string ForCity
        {
            get { return forCity; }
            set { forCity = value; OnPropertyChanged(); }
        }


        private string forSchedule;
        public string ForSchedule
        {
            get { return forSchedule; }
            set { forSchedule = value; OnPropertyChanged(); }
        }


        private string forAirplane;
        public string ForAirplane
        {
            get { return forAirplane; }
            set { forAirplane = value; OnPropertyChanged(); }
        }


        private string forPilotFullName;
        public string ForPilotFullName
        {
            get { return forPilotFullName; }
            set { forPilotFullName = value; OnPropertyChanged(); }
        }

        public RelayCommand SelectedCityCommand { get; set; }
        public RelayCommand SelectedScheduleCommand { get; set; }
        public RelayCommand SelectedAirplaneCommand { get; set; }
        public RelayCommand PurchaseBtnCommand { get; set; }

        public Pilot Pilot { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            AirLanesDBEntities1 context = new AirLanesDBEntities1();

            //var cities = from c in context.Cities.Include(nameof(City.Schedules))
            //             select c;

            //var cityList = cities.ToList();
            //dataGrid1.ItemsSource = cityList;

            var cities = from c in context.Cities.Include(nameof(City.Schedules))
                         select c;

            AllCities = new ObservableCollection<City>(cities);

            SelectedCityCommand = new RelayCommand((obj) =>
            {
                //MessageBox.Show($"{SelectedCity.Name} is selected !");
                CitySchedule = new ObservableCollection<Schedule>(SelectedCity.Schedules);//Burda lazy loading false etmisem ona Airplanes gelmir.
            });

            SelectedScheduleCommand = new RelayCommand((obj) =>
            {
                if (SelectedSchedule != null)
                {
                    var check = from sc in context.Schedules.Include(nameof(Schedule.AirPlanes))
                                where sc.Id == SelectedSchedule.Id
                                select sc;

                    var data = check.FirstOrDefault(c => c.Id == SelectedSchedule.Id);
                    SelectedSchedule = data;
                    //AirPlaneWhichSchedule = new ObservableCollection<AirPlane>(data.AirPlanes);
                    AirPlaneWhichSchedule = new ObservableCollection<AirPlane>(SelectedSchedule.AirPlanes);

                }
                //AirPlaneWhichSchedule = new ObservableCollection<AirPlane>(SelectedSchedule.AirPlanes);
            });

            SelectedAirplaneCommand = new RelayCommand((obj) =>
            {
                if (SelectedAirplane != null)
                {
                    Pilot = context.Pilots.FirstOrDefault(p => p.Id == SelectedAirplane.PilotId);
                    if (Pilot != null)
                    {
                        PilotFullName = Pilot.FullName;
                        PilotAge = Pilot.Age.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Is not exist any pilot !");
                    }

                }
            });

            PurchaseBtnCommand = new RelayCommand((obj) =>
            {
                ForCity = SelectedCity.Name;
                ForSchedule = SelectedSchedule.Date.ToShortDateString();
                ForAirplane = SelectedAirplane.BrandName;
                ForPilotFullName = Pilot.FullName;
                MessageBox.Show("Purchase is successfully !");
            },
            (pred) =>
            {
                return SelectedCity != null && SelectedSchedule != null && SelectedAirplane != null && Pilot != null;
            }
            );





        }






    }
}
