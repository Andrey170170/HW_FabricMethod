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
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Submit_OnClick(object sender, RoutedEventArgs e)
        {
            if (_choices.All(choice => choice != null))
            {
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
            _choices[0] = (sender as RadioButton)?.Content.ToString();
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
