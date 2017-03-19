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
using System.Windows.Shapes;

namespace Project_Darren
{
    /// <summary>
    /// Interaction logic for ViewClass.xaml
    /// </summary>
    public partial class ViewClass : Window
    {
        private string selectedClass;
        private MainWindow f = new MainWindow();
        public bool ok;
        
        
        public ViewClass()
        {
            InitializeComponent();
        }

        private void OK_BT_Click(object sender, RoutedEventArgs e)
        {
            selectedClass = Convert.ToString(vclass_Input.Text);
            ok = true;
            this.Close();
        }

        private void Exit_BT_Click(object sender, RoutedEventArgs e)
        {
            //Exit
            ok = false;
            this.Close();
        }
        public string SelectedClass
        {
            get
            {
                return selectedClass;
            }
        }
    }
}
