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
using System.IO;
using Microsoft;
using Microsoft.Win32;

namespace Project_Darren
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        List<Animal> y = new List<Animal>(); //This is the main list
        List<Animal> temp = new List<Animal>(); //This is the temp list for Cut, copy and paste
        Animal Animal1 = new Animal();
        public string path = "";
        public string storedPath = "";
        public string imgPath = "";
        public int CurrentID = 0;

        public MainWindow()
        {
            InitializeComponent();
            disable();

        }

        private void Class_Input_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetClasSelection(Convert.ToString(Class_Input.SelectedItem)); //Sets 

            if (y.Count == 0)
            {
                y.Add(new Animal());
                MessageBox.Show("1 Animal added automatically");
            }

            changeEdit();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (y.Count != 0)
            {
                if (Order_Input.IsEnabled == true)
                {
                    disable();
                }
                else
                {
                    enable();
                }
            }
            else { MessageBox.Show("You need to do one of the following:\n1. Open a file of animals.\n2. Add an animal under View>Add", "USER ERROR"); }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //this is the save button 

            if (path != "")
            {
                //save
                string createText;
                createText = Animal.GetString(y);
                changeEdit(); //Changes the date of the current animal
                System.IO.File.WriteAllText(path, createText); //saves to the file 
                disable();
                MessageBox.Show("Animal has been saved","Sucess");
            }
            else
            {
                MessageBox.Show("You need to open or save the whole database in order to save an animal.","USER ERROR");
            }
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if (y.Count!=0)
            {
                if (CurrentID != 0)
                {
                    CurrentID--;
                    set_All();
                }
                else if (CurrentID == 0)
                {
                    CurrentID = (y.Count - 1);
                    set_All();
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //this is open (under file)
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            storedPath = (System.AppDomain.CurrentDomain.BaseDirectory + "Database");
            path = (System.AppDomain.CurrentDomain.BaseDirectory + "Database");

            try //if the user presses the X button to close the openfiledialog, then nothing happens
            {
                openFileDialog.DefaultExt = ".txt";
                openFileDialog.Filter = "Text document (.txt)|*.txt";
                openFileDialog.InitialDirectory = path;
                openFileDialog.ShowDialog();
                path = openFileDialog.FileName;

                var filestream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
                var file = new System.IO.StreamReader(filestream);//, System.Text.Encoding.UTF8, true, 128);
        
                y.Clear(); //Clear the whole List

                for (int i = 0; !file.EndOfStream ; i++) //runs until file has no text
                {
                    y.Add(new Animal()); 
                    
                    y[i].clas = file.ReadLine();
                    y[i].name = file.ReadLine();
                    y[i].genus = file.ReadLine();
                    y[i].family = file.ReadLine();
                    y[i].order = file.ReadLine();
                    y[i].converted_Length = file.ReadLine();
                    y[i].length = Convert.ToDouble(y[i].converted_Length);
                    y[i].converted_Weight = file.ReadLine();
                    y[i].weight = Convert.ToDouble(y[i].converted_Weight);
                    y[i].extinction_Stat = file.ReadLine();
                    y[i].img = file.ReadLine();
                    y[i].filecreated = Convert.ToDateTime(file.ReadLine());
                    y[i].fileedit = Convert.ToDateTime(file.ReadLine());

                    file.ReadLine(); //Becuase the txt file has a space between each object
                }

                CurrentID = 0;
                set_All();
                filestream.Close();
                file.Close();
              }
              catch { }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            // this is save (under file)
            // save works as a save as at first, and thereafter it saves to the selected file.

            string createText;
            createText = Animal.GetString(y);

            try
            {
                if (path == "")
                {
                    // save as if there is no file saved or opened already
                    Microsoft.Win32.SaveFileDialog saveFile = new Microsoft.Win32.SaveFileDialog();
                    saveFile.CreatePrompt = false;
                    saveFile.OverwritePrompt = true;
                    saveFile.DefaultExt = ".txt";
                    saveFile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveFile.ShowDialog();
                    path = saveFile.FileName;
                    File.WriteAllText(path, createText);
                }
                else
                {
                    //save 
                    System.IO.File.WriteAllText(path, createText);
                }
            }
            catch { }
            disable();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            //this is save as (under file)

            string createText;
            createText = Animal.GetString(y);

            Microsoft.Win32.SaveFileDialog saveFile = new Microsoft.Win32.SaveFileDialog();
            saveFile.CreatePrompt = true;
            saveFile.OverwritePrompt = true;
            saveFile.DefaultExt = ".txt";
            //default filename
            saveFile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            try
            {
                saveFile.ShowDialog();
                path = saveFile.FileName;
                File.AppendAllText(path, createText);
            }
            catch { }
            disable();
        }

        private void Name_Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (y.Count == 0)
            {
                y.Add(new Animal());
                MessageBox.Show("1 Animal added automatically");
            }

            y[CurrentID].name = Name_Input.Text;
            changeEdit();
        }

        private void Order_Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (y.Count == 0)
            {
                y.Add(new Animal());
                MessageBox.Show("1 Animal added automatically");
            }
            y[CurrentID].order = Order_Input.Text;
            changeEdit();
        }

        private void Family_Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (y.Count == 0)
            {
                y.Add(new Animal());
                MessageBox.Show("1 Animal added automatically");
            }

            y[CurrentID].family = Family_Input.Text;
            changeEdit();
        }

        private void Genus_Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (y.Count == 0)
            {
                y.Add(new Animal());
                MessageBox.Show("1 Animal added automatically");
            }

            y[CurrentID].genus = Genus_Input.Text;
            changeEdit();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //this is length
            //the name was changed to Length_Input, it is no longer textBox

            if(Length_Input.Text == "0")
            {
                MessageBox.Show("Length must not be zero");
                Length_Input.Text = "";
            }

            if (y.Count == 0)
            {
                y.Add(new Animal());
                MessageBox.Show("1 Animal added automatically");
            }

            y[CurrentID].converted_Length = Length_Input.Text;

            try
            {
                y[CurrentID].length = Convert.ToDouble(y[CurrentID].converted_Length);
            }
            catch
            {
                Length_Input.Text = "";
                Length_Input.SelectionStart = 0;
                Length_Input.SelectionLength = Length_Input.Text.Length;
            }
            changeEdit();
        }

        private void Weight_Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Weight_Input.Text == "0")
            {
                MessageBox.Show("Weight must not be zero");
                Weight_Input.Text = "";
            }

            if (y.Count == 0)
            {
                y.Add(new Animal());
                MessageBox.Show("1 Animal added automatically");
            }

            y[CurrentID].converted_Weight = Weight_Input.Text;

            try
            {
                y[CurrentID].weight = Convert.ToDouble(y[CurrentID].converted_Weight);
            }
            catch
            {
                Weight_Input.Text = "";
                Weight_Input.SelectionStart = 0;
                Weight_Input.SelectionLength = Weight_Input.Text.Length;
            }

            changeEdit();
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (y.Count == 0)
            {
                y.Add(new Animal());
                MessageBox.Show("1 Animal added automatically");
            }

            y[CurrentID].extinction_Stat = "Extinct";
            changeEdit();
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            if (y.Count == 0)
            {
                y.Add(new Animal());
                MessageBox.Show("1 Animal added automatically");
            }

            y[CurrentID].extinction_Stat = "Threatened";
            changeEdit();
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            if (y.Count == 0)
            {
                y.Add(new Animal());
                MessageBox.Show("1 Animal added automatically");
            }

            y[CurrentID].extinction_Stat = "Least Concerned";
            changeEdit();
        }

        private void Image_Path_TextChanged(object sender, TextChangedEventArgs e)
        {
            y[CurrentID].img = Image_Path.Text;
        }

        private void Set_Image_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                changeImage();
                changeEdit();
            }
            catch
            {
                MessageBox.Show("The Path specified does not lead to an image in the Database folder.");
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentID != (y.Count - 1) && y.Count!=0)
            {
                CurrentID++;
                set_All();
            }
            else if (CurrentID == (y.Count-1))
            {
                CurrentID = 0;
                set_All();
            }
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("                 Made by:\nOisin Connery - B00692376\nDarren Chen -   B00694022");
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            //this is view help
            View_Help view = new View_Help();
            view.Show();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            //This is ReDisplay

            //Opens the initial database

            try  
            {
                var filestream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
                var file = new System.IO.StreamReader(filestream);//, System.Text.Encoding.UTF8, true, 128);

                y.Clear(); //Clear the whole List

                for (int i = 0; !file.EndOfStream; i++) //runs until file has no text
                {
                    y.Add(new Animal());

                    y[i].clas = file.ReadLine();
                    y[i].name = file.ReadLine();
                    y[i].genus = file.ReadLine();
                    y[i].family = file.ReadLine();
                    y[i].order = file.ReadLine();
                    y[i].converted_Length = file.ReadLine();
                    y[i].length = Convert.ToDouble(y[i].converted_Length);
                    y[i].converted_Weight = file.ReadLine();
                    y[i].weight = Convert.ToDouble(y[i].converted_Weight);
                    y[i].extinction_Stat = file.ReadLine();
                    y[i].img = file.ReadLine();
                    y[i].filecreated = Convert.ToDateTime(file.ReadLine());
                    y[i].fileedit = Convert.ToDateTime(file.ReadLine());

                    file.ReadLine(); //Becuase the txt file has a space between each object
                }

                CurrentID = 0;
                set_All();
                Next.IsEnabled = true;
                Previous.IsEnabled = true;
                filestream.Close();
                file.Close();
            }
            catch
            {
                MessageBox.Show("Must have a database to redisplay initial values");
            }
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            //this is add, under view.
            y.Insert(CurrentID, new Animal());
            y[CurrentID].filecreated = DateTime.Now;
            y[CurrentID].fileedit = DateTime.Now;
            set_All();
            enable();
        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_9(object sender, RoutedEventArgs e)
        {
            //this is delete

            if (y.Count > 1)
            {
                y.RemoveAt(CurrentID);
                CurrentID = 0;
                set_All();
            }
            else if (y.Count == 0)
            {
                MessageBox.Show("Cannot delete an animal until one is added to the form", "Empty Form");
            }
            else //i.e. y.Count is 1
            {
                y.RemoveAt(CurrentID);
                //set all textboxes etc. to default values

                Class_Input.Text = "";
                Name_Input.Text = "";
                Genus_Input.Text = "";
                Family_Input.Text = "";
                Order_Input.Text = "";
                Length_Input.Text = "";
                Weight_Input.Text = "";

                Extinct.IsChecked = false;
                Threatened.IsChecked = false;
                Least_Concerned.IsChecked = false;

                Animal_Type.Content = "Animal " + (CurrentID + 1) + " of " + y.Count;

                last_Edit.Content = "Last edit: 00/00/0000 at 00:00";
                created.Content = "Created: 00/00/0000 at 00:00 ";
            }
        }

        private void MenuItem_Click_10(object sender, RoutedEventArgs e)
        {
            //this is cut
            if (y.Count!=0 && y.Count !=1)  //Ensures that the user can't use copy when they have not created atleast one animal or opened a file of animals.
            {
                try
                {
                    temp[0] = y[CurrentID];//copy animal to temp object
                    y.RemoveAt(CurrentID);//deletes animal from y
                    set_All(); //sets form to the current ID which is what the object was so for right now it should be what the next animal was

                }
                catch
                {
                    //Error-if temp[0] doesn't exist.
                    temp.Add(new Animal());
                    temp[0] = y[CurrentID];//copy animal to temp object
                    y.RemoveAt(CurrentID);//deletes animal from y
                    set_All();
                }
                MessageBox.Show("The animal has been cut");
            }
        }

        private void MenuItem_Click_11(object sender, RoutedEventArgs e)
        {
            //this is copy
            if (y.Count != 0) //Ensures that the user can't use copy when they have not created atleast one animal or opened a file of animals.
            {
                try
                {
                    temp[0] = y[CurrentID];
                }
                catch
                {
                    temp.Add(new Animal());
                    temp[0] = y[CurrentID];//copy animal to temp object
                }
                MessageBox.Show("The animal has been copied");
            }
        }

        private void MenuItem_Click_12(object sender, RoutedEventArgs e)
        {
            //this is paste
            if (y.Count != 0) //&& y.Count != 1)
            {
                if (temp.Count != 0)//check if temporary object is null
                {
                    y.Insert(CurrentID, temp[0]);
                    set_All();
                }
                else
                {
                    MessageBox.Show("Cut or copy an animal first");
                }
            }
            else
            {
                MessageBox.Show("Cut or copy an animal first");
            }
        }
        private void MenuItem_Click_14(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItem_Click_15(object sender, RoutedEventArgs e)
        {
            


        }
        private void MenuItem_Click_13(object sender, RoutedEventArgs e)
        {
            ViewClass Vclass = new ViewClass();
            Vclass.ShowDialog();
            string requestedClass;

            if (Vclass.ok == true)
            {
                requestedClass = Vclass.SelectedClass;
                selectClass(requestedClass);
            }
        }

        /// <summary>
        /// /////////////////////////////////////////////////////// METHODS START ///////////////////////////////////////////////////////////////////////////////
        /// </summary>

        private void disable()
        {
            //this function simply disables all of the fields in the file so that the user cannot make changes.
            Order_Input.IsEnabled = false;
            Name_Input.IsEnabled = false;
            Family_Input.IsEnabled = false;
            Genus_Input.IsEnabled = false;
            Length_Input.IsEnabled = false;
            Weight_Input.IsEnabled = false;
            Class_Input.IsEnabled = false;
            Extinct.IsEnabled = false;
            Threatened.IsEnabled = false;
            Least_Concerned.IsEnabled = false;
            Image_Path.IsEnabled = false;
            Set_Image.IsEnabled = false;
        }

        private void enable()
        {
            //this function simply enables all of the fields in the file so that the user can make changes.
            Order_Input.IsEnabled = true;
            Name_Input.IsEnabled = true;
            Family_Input.IsEnabled = true;
            Genus_Input.IsEnabled = true;
            Length_Input.IsEnabled = true;
            Weight_Input.IsEnabled = true;
            Class_Input.IsEnabled = true;
            Extinct.IsEnabled = true;
            Threatened.IsEnabled = true;
            Least_Concerned.IsEnabled = true;
            Image_Path.IsEnabled = true;
            Set_Image.IsEnabled = true;
        }

        private void set_All()
        {
            //set_All sets the form text to the corrosponding elements, for the animal represented by CurrentID. It also calls changeImage.

            Class_Input.Text = y[CurrentID].clas;
            Class_Input.SelectedItem = y[CurrentID].clas;
            Name_Input.Text = y[CurrentID].name;
            Genus_Input.Text = y[CurrentID].genus;
            Family_Input.Text = y[CurrentID].family;
            Order_Input.Text = y[CurrentID].order;
            Length_Input.Text = y[CurrentID].converted_Length;
            Weight_Input.Text = y[CurrentID].converted_Weight;
            Image_Path.Text = y[CurrentID].img;

            last_Edit.Content = "Last edit: " + y[CurrentID].fileedit;
            created.Content = "Created: " + y[CurrentID].filecreated;

            if (y[CurrentID].extinction_Stat == "Extinct")
            {
                Extinct.IsChecked = true;
            }
            else Extinct.IsChecked = false;

            if (y[CurrentID].extinction_Stat == "Threatened")
            {
                Threatened.IsChecked = true;
            }
            else Threatened.IsChecked = false;

            if (y[CurrentID].extinction_Stat == "Least Concerned")
            {
                Least_Concerned.IsChecked = true;
            }
            else Least_Concerned.IsChecked = false;

            Animal_Type.Content = "Animal " + (CurrentID + 1) + " of " + y.Count;
            changeImage();
        }

        private void SetClasSelection(string a)
        {
            //This method take the string form of the selected item, and changes the clas property to the appropriate string.
            if (a == "System.Windows.Controls.ListBoxItem: Amphibian")
            {
                y[CurrentID].clas = "Amphibian";
            }
            if (a == "System.Windows.Controls.ListBoxItem: Reptile")
            {
                y[CurrentID].clas = "Reptile";
            }
            if (a == "System.Windows.Controls.ListBoxItem: Bird")
            {
                y[CurrentID].clas = "Bird";
            }
            if (a == "System.Windows.Controls.ListBoxItem: Mammal")
            {
                y[CurrentID].clas = "Mammal";
            }
            if (a == "System.Windows.Controls.ListBoxItem: Fish")
            {
                y[CurrentID].clas = "Fish";
            }
        }

        private void changeEdit()
        {
            //This function changes the fileedit property to todays date, and updates the last_Edit text.
            if (Order_Input.IsEnabled == true)
            {
                y[CurrentID].fileedit = DateTime.Now;
                last_Edit.Content = "Last edit: " + DateTime.Now;
            }
        }

        private void changeImage()
        {
            try
            {
                //This function changes the image of the current animal to the corrosponding value.
                if (y[CurrentID].img == "")
                {
                    Img_Lbl.Content = "NO IMAGE SELECTED";
                    image.Source = null; //sets image to blank if this img path is empty
                }
                else
                {
                    Img_Lbl.Content = "";
                    imgPath = storedPath + "\\" + y[CurrentID].img; //gets the complete path to the image

                    BitmapImage useimage = new BitmapImage(new Uri(imgPath, UriKind.Absolute));
                    image.Source = useimage; //Picture on the form is equal to useimage
                }
            }
            catch { MessageBox.Show("Image Error\n" + imgPath); }
        }

        private void MenuItem_Click_16(object sender, RoutedEventArgs e)
        {
            //sort Class
            if (y.Count > 0)
            {
                var x = (from a in y
                         orderby a.Clas ascending
                         select a).ToList();
                //update myanimalList with sorted list
                y = x;
                
                set_All();
                Animal_Type.Content = "Animal " + (CurrentID + 1) + " of " + y.Count;
            }
            else
            {
                MessageBox.Show("Open database first");
            }
        }

        private void MenuItem_Click_17(object sender, RoutedEventArgs e)
        {
            //sort order
            if (y.Count > 0)
            {
                var x = (from a in y
                         orderby a.Order ascending
                         select a).ToList();
                //update myanimalList with sorted list
                y = x;

                set_All();
                Animal_Type.Content = "Animal " + (CurrentID + 1) + " of " + y.Count;

            }
            else
            {
                MessageBox.Show("Open database first");
            }
        }

        private void MenuItem_Click_18(object sender, RoutedEventArgs e)
        {
            //sort family
            if (y.Count > 0)
            {
                var x = (from a in y
                         orderby a.Family ascending
                         select a).ToList();
                //update myanimalList with sorted list
                y = x;

                set_All();
                Animal_Type.Content = "Animal " + (CurrentID + 1) + " of " + y.Count;
            }
            else
            {
                MessageBox.Show("Open database first");
            }
        }

        private void selectClass(string requestedClass)
        {
            List<Animal> tempAnimals = new List<Animal>(2);

            try
            {
                var selectClass = from an in y where an.Clas == requestedClass select an;

                foreach (Animal a in selectClass)
                {
                    tempAnimals.Add(a);
                }
                y = tempAnimals;
                CurrentID = 0;
                Animal1 = y[0];
                set_All();
            }
            catch
            {
                MessageBox.Show("No Animal matches Selected Class");
            }
        }

        private void MenuItem_Click_19(object sender, RoutedEventArgs e)
        {
            ViewOrder Vorder = new ViewOrder();
            Vorder.ShowDialog();
            string requestedOrder;

            if (Vorder.ok == true)
            {
                requestedOrder = Vorder.Selectedorder;
                selectOrder(requestedOrder);
            }
        }

        private void selectOrder(string requestedOrder)
        {
            List<Animal> tempAnimals = new List<Animal>(2);

            try
            {
                var selectOrder = from an in y where an.Order == requestedOrder select an;

                foreach (Animal a in selectOrder)
                {
                    tempAnimals.Add(a);
                }
                y = tempAnimals;
                CurrentID = 0;
                Animal1 = y[0];
                set_All();
            }
            catch
            {
                MessageBox.Show("No Animal matches Selected Order");
            }
        }

        private void MenuItem_Click_20(object sender, RoutedEventArgs e)
        {
            ViewFamily Vfamily = new ViewFamily();
            Vfamily.ShowDialog();
            string requestedFamily;

            if (Vfamily.ok == true)
            {
                requestedFamily = Vfamily.Selectedfamily;
                selectFamily(requestedFamily);
            }
        }

        private void selectFamily(string requestedFamily)
        {
            List<Animal> tempAnimals = new List<Animal>(2);

            try
            {
                var selectFamily = from an in y where an.Family == requestedFamily select an;

                foreach (Animal a in selectFamily)
                {
                    tempAnimals.Add(a);
                }
                y = tempAnimals;
                CurrentID = 0;
                Animal1 = y[0];
                set_All();
            }
            catch
            {
                MessageBox.Show("No Animal matches Selected Family");
            }
        }

        /// /////////////////////////////////////////////////////// METHODS END ///////////////////////////////////////////////////////////////////////////////
    }
}