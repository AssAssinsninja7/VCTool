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

namespace WpfPocketsphinxTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        

        public MainWindow()
        {
            InitializeComponent();
        }



        private void PhraseListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            //PhraseListBox.Items.Add("poop");
            AddPhraseWindow addPhraseWindow = new AddPhraseWindow(this);
            addPhraseWindow.Show();
        }


        private void TrainPhrases_Click(object sender, RoutedEventArgs e)
        {
            /*1.Check if any phrases have been selected 
             *2. send selected phrases to the AI
              3. once the AI is done tell the user*/
        }

        private void RemovePhrase_Click(object sender, RoutedEventArgs e)
        {
            /*Remove the selected phrase from the list,
             make the user re-confrim (pop-up window) and remove*/
        }



        public void AddNewKeyphrase(string phrase)
        {
            PhraseListBox.Items.Add(phrase);
        }
    }
}
