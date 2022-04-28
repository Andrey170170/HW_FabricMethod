using System;
using System.Collections.Generic;
using System.Linq;
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
using HW_FabricMethod.Models;

namespace HW_FabricMethod
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _character = 0;
        private string[] _choices = new string[4];
        private bool isError = false;
        private static List<Person> _persons = new List<Person>();
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Submit_OnClick(object sender, RoutedEventArgs e)
        {
            if (_choices.All(choice => choice != null))
            {
                if (_choices[0] == "Human")
                {
                    Weapon selectedWeapon = _choices[1] switch
                    {
                        "Sword" => new Sword(),
                        "Axe" => new Axe(),
                        "Bow" => new Bow()
                    };
                    Armor selectedArmor = _choices[2] switch
                    {
                        "Chain" => new Chain(),
                        "Breastplate" => new Breastplate()
                    };
                    Item selectedItem = _choices[3] switch
                    {
                        "Ring" => new Ring(),
                        "Brilliant" => new Brilliant()
                    };
                    _persons.Add(new Person(new HumanFactory(selectedWeapon, selectedArmor, new()
                    {
                        selectedItem, selectedWeapon, selectedArmor
                    })));
                }
                else
                {
                    _persons.Add(_choices[0] switch
                    {
                        "Orc" => new Person(new OrcFactory()),
                        "Aeldari" => new Person(new AeldariFactory()),
                        "Tau" => new Person(new TauFactory())
                    });
                }
                
                MessageBox.Show(_choices.Aggregate("", (current, choice) => current + ", " + choice) + "; " + _character);
                _character += 1;
                _choices = new string[4];
                List<RadioButton> radioButtons = new List<RadioButton>();
                WalkLogicalTree(radioButtons, rootpanel);
                foreach (RadioButton rb in radioButtons)
                {
                    rb.IsChecked = false;
                }
            }
            else if (_choices[0] != "Human")
            {
                _persons.Add(_choices[0] switch
                {
                    "Orc" => new Person(new OrcFactory()),
                    "Aeldari" => new Person(new AeldariFactory()),
                    "Tau" => new Person(new TauFactory())
                });
                
                _character += 1;
                _choices = new string[4];
                List<RadioButton> radioButtons = new List<RadioButton>();
                WalkLogicalTree(radioButtons, rootpanel);
                foreach (RadioButton rb in radioButtons)
                {
                    rb.IsChecked = false;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите все пункты!");
                _character = 0;
                _choices = new string[4];
                List<RadioButton> radioButtons = new List<RadioButton>();
                WalkLogicalTree(radioButtons, rootpanel);
                foreach (RadioButton rb in radioButtons)
                {
                    rb.IsChecked = false;
                }
            }
        }

        private void Reset_OnClick(object sender, RoutedEventArgs e)
        {
            List<RadioButton> radioButtons = new List<RadioButton>();
            WalkLogicalTree(radioButtons, rootpanel);
            foreach (RadioButton rb in radioButtons)
            {
                rb.IsChecked = false;
            }

            _character = 0;
            _choices = new string[4];
            isError = false;
        }

        private void Race_OnChecked(object sender, RoutedEventArgs e)
        {
            if (sender != null && sender is RadioButton)
            {
                if ((_choices[0] = (sender as RadioButton)?.Content.ToString()) == "Human")
                {
                    WeaponGrid.Visibility = Visibility.Visible;
                    ArmorGrid.Visibility = Visibility.Visible;
                    ItemGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    WeaponGrid.Visibility = Visibility.Hidden;
                    ArmorGrid.Visibility = Visibility.Hidden;
                    ItemGrid.Visibility = Visibility.Hidden;
                }
            }
        }
        private void Weapon_OnChecked(object sender, RoutedEventArgs e)
        {
            _choices[1] = (sender as RadioButton)?.Content.ToString();
        }
        private void Armor_OnChecked(object sender, RoutedEventArgs e)
        {
            _choices[2] = (sender as RadioButton)?.Content.ToString();
        }
        private void Items_OnChecked(object sender, RoutedEventArgs e)
        {
            _choices[3] = (sender as RadioButton)?.Content.ToString();
        }

        private static void WalkLogicalTree(ICollection<RadioButton> radioButtons, object parent)
        {
            DependencyObject doParent = parent as DependencyObject;
            if (doParent == null) return;
            foreach (object child in LogicalTreeHelper.GetChildren(doParent))
            {
                if (child is RadioButton)
                {
                    radioButtons.Add(child as RadioButton);
                }
                WalkLogicalTree(radioButtons, child);
            }
        }
    }
}
