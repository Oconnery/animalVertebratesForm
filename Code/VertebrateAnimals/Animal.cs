using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Darren
{ 
    public class Animal
    {
        public double length;
        public double weight;
        public string name;
        public string genus;
        public string family;
        public string order;
        public string clas; 
        public string extinction_Stat;
        public string FileName;
        public string CurrentFile;
        public string converted_Length;
        public string converted_Weight;
        public DateTime filecreated;
        public DateTime fileedit;
        public string img;

        public Animal()
        {
            length = 0.00;
            converted_Length = "";
            weight = 0.00;
            converted_Weight = "";
            name = "";
            genus = "";
            family = "";
            order = "";
            clas = "" ;
            extinction_Stat = "";
            FileName = "";
            CurrentFile = "";
            img = "";
        }
        public double Length
        {
            get
            {
                return length;
            }
            set
            {
                length = value;
            }
        }

        public double Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string Genus
        {
            get
            {
                return genus;
            }
            set
            {
                genus = value;
            }
        }
        public string Family
        {
            get
            {
                return family;
            }
            set
            {
                family = value;
            }
        }
        public string Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
            }
        }
        public string Clas
        {
            get
            {
                return clas;
            }
            set
            {
                clas = value;
            }
        }
        private string Extinction_Stat
        {
            get
            {
                return extinction_Stat;
            }
            set
            {
                extinction_Stat = value;
            }
        }
        private string Image
        {
            get
            {
                return img;
            }
            set
            {
                img = value;
            }
        }

        ////////////////////////////////////////////////////////// METHODS START ///////////////////////////////////////////////////////////////////////////////

        //converts each element of each object in the array to one very long string
        public static string GetString(List <Animal> t)
        {
            string str = "";

            for (int a = 0; a < t.Count; a++)
            {
                    str += t[a].clas + Environment.NewLine;
                    str += t[a].name + Environment.NewLine;
                    str += t[a].genus + Environment.NewLine;
                    str += t[a].family + Environment.NewLine;
                    str += t[a].order + Environment.NewLine;
                    str += t[a].converted_Length + Environment.NewLine;
                    str += t[a].converted_Weight + Environment.NewLine;
                    str += t[a].extinction_Stat + Environment.NewLine;
                    str += t[a].img + Environment.NewLine;
                    str += t[a].filecreated + Environment.NewLine;
                    str += t[a].fileedit + Environment.NewLine;

                if (a != (t.Count-1))
                {
                    str += Environment.NewLine;
                }
            }
            return (str);
        }

        ////////////////////////////////////////////////////////// METHODS END///////////////////////////////////////////////////////////////////////////////
    }
}
