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
            try
            {
                if (_choices.All(choice => choice != null) || _choices[0] != "Human")
                {
                    if (_choices[0] == "Human")
                    {
                        Weapon selectedWeapon = _choices[1] switch
                        {
                            "Sword" => new Sword(),
                            "Axe" => new Axe(),
                            "Bow" => new Bow(),
                            _ => throw new ArgumentOutOfRangeException()
                        };
                        Armor selectedArmor = _choices[2] switch
                        {
                            "Chain" => new Chain(),
                            "Breastplate" => new Breastplate(),
                            _ => throw new ArgumentOutOfRangeException()
                        };
                        Item selectedItem = _choices[3] switch
                        {
                            "Ring" => new Ring(),
                            "Brilliant" => new Brilliant(),
                            _ => throw new ArgumentOutOfRangeException()
                        };
                        _persons.Add(new Person(new HumanFactory(selectedWeapon, selectedArmor, new List<Item>
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
                            "Tau" => new Person(new TauFactory()),
                            _ => throw new ArgumentOutOfRangeException()
                        });
                    }

                    //MessageBox.Show(
                    //    _choices.Aggregate("", (current, choice) => current + ", " + choice) + "; " + _character);
                    var grid = (Grid) Characters.FindName("Character" + _character) ??
                               throw new InvalidOperationException();
                    var labels = FindVisualChildren<Label>(grid).ToList();
                    labels[4].Content = _persons[_character].Race;
                    labels[5].Content = _persons[_character].Weapon.Damage;
                    labels[6].Content = _persons[_character].Armor.Durability;
                    labels[7].Content = _persons[_character].Health;

                    _character += 1;
                    _choices = new string[4];
                    List<RadioButton> radioButtons = new List<RadioButton>();
                    WalkLogicalTree(radioButtons, rootpanel);
                    foreach (RadioButton rb in radioButtons)
                    {
                        rb.IsChecked = false;
                    }
                }
                //else if (_choices[0] != "Human")
                //{
                //    _persons.Add(_choices[0] switch
                //    {
                //        "Orc" => new Person(new OrcFactory()),
                //        "Aeldari" => new Person(new AeldariFactory()),
                //        "Tau" => new Person(new TauFactory())
                //    });

                //    _character += 1;
                //    _choices = new string[4];
                //    List<RadioButton> radioButtons = new List<RadioButton>();
                //    WalkLogicalTree(radioButtons, rootpanel);
                //    foreach (RadioButton rb in radioButtons)
                //    {
                //        rb.IsChecked = false;
                //    }
                //}
                else
                {
                    _character = 0;
                    _choices = new string[4];
                    List<RadioButton> radioButtons = new List<RadioButton>();
                    WalkLogicalTree(radioButtons, rootpanel);
                    foreach (RadioButton rb in radioButtons)
                    {
                        rb.IsChecked = false;
                    }

                    throw new Exception("Пожалуйста, выберите все пункты!");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
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
            _persons = new List<Person>();
            for (var i = 0; i < 5; i++)
            {
                var grid = (Grid)Characters.FindName("Character" + i.ToString()) ??
                           throw new InvalidOperationException();
                var labels = FindVisualChildren<Label>(grid).ToList();
                labels[4].Content = "";
                labels[5].Content = "";
                labels[6].Content = "";
                labels[7].Content = "";
            }
        }

        private void Race_OnChecked(object sender, RoutedEventArgs e)
        {
            if (sender == null || sender is not RadioButton radioButton) return;
            if ((_choices[0] = radioButton?.Content.ToString()!) == "Human")
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

        private void Weapon_OnChecked(object sender, RoutedEventArgs e)
        {
            _choices[1] = (sender as RadioButton)?.Content.ToString()!;
        }

        private void Armor_OnChecked(object sender, RoutedEventArgs e)
        {
            _choices[2] = (sender as RadioButton)?.Content.ToString()!;
        }

        private void Items_OnChecked(object sender, RoutedEventArgs e)
        {
            _choices[3] = (sender as RadioButton)?.Content.ToString()!;
        }

        private static void WalkLogicalTree(ICollection<RadioButton> radioButtons, object parent)
        {
            if (parent is not DependencyObject doParent) return;
            foreach (var child in LogicalTreeHelper.GetChildren(doParent))
            {
                if (child is RadioButton radioButton)
                {
                    radioButtons.Add(radioButton);
                }

                WalkLogicalTree(radioButtons, child);
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield return (T) Enumerable.Empty<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
                if (ithChild == null) continue;
                if (ithChild is T t) yield return t;
                foreach (T childOfChild in FindVisualChildren<T>(ithChild)) yield return childOfChild;
            }
        }
    }
}