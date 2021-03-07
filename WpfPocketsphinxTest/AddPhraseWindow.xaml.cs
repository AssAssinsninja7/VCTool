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

namespace WpfPocketsphinxTest
{
    /// <summary>
    /// Interaction logic for AddPhraseWindow.xaml
    /// </summary>
    public partial class AddPhraseWindow : Window
    {
        MainWindow main;
        bool recording;
        

        public AddPhraseWindow(MainWindow _main)
        {
            InitializeComponent();

            main = _main;
            recording = false;
        }

        private void AddPhraseBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RecBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!recording)
            {
                recording = true;
                ErrorLbl.Visibility = Visibility.Hidden;
                RecBtn.Background = new SolidColorBrush(Color.FromArgb(133, 30, 30, 0));
                RecBtn.Content = "Recording";
         

                //Start detecting Voice inpout

            }
            else if (AddPhraseBox.Text == "Enter keyphrase/ phrases" || AddPhraseBox.Text == string.Empty)
            {
                ErrorLbl.Visibility = Visibility.Visible;
                ErrorLbl.Content = "Cannot leave the field empty \n" +
                                    "or \n" +
                                    "enter the helper text";
                recording = false;
                //Stop recording
            }
            else if(recording && AddPhraseBox.Text != "Enter keyphrase/ phrases" && AddPhraseBox.Text != string.Empty)
            {
                main.AddNewKeyphrase(AddPhraseBox.Text);
                this.Close();
            }

            /*Check the 3 states and*/
        }
    }
}
