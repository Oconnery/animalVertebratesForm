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
    /// Interaction logic for ViewOrder.xaml
    /// </summary>
    public partial class ViewOrder : Window
    {
        private string selectedOrder;
        private MainWindow f = new MainWindow();
        public bool ok;

        public ViewOrder()
        {
            InitializeComponent();
        }

        private void OK_BT_Click(object sender, RoutedEventArgs e)
        {
            //Ok Button
            selectedOrder = Convert.ToString(vorder_Input.Text);
            ok = true;
            this.Close();
        }

        private void Exit_BT_Click(object sender, RoutedEventArgs e)
        {
            //Exit
            ok = false;
            this.Close();
        }
        public string Selectedorder
        {
            get
            {
                return selectedOrder;
            }
        }

        private void vorder_Input_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
