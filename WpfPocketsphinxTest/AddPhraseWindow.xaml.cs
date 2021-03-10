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
using System.Windows.Threading;

namespace VoiceControllerTool
{
    /// <summary>
    /// Interaction logic for AddPhraseWindow.xaml
    /// </summary>
    public partial class AddPhraseWindow : Window
    {
        MainWindow main;
        MicrophoneHandler micHandler;


        bool recording;

        DispatcherTimer timer;
        TimeSpan time;

        public AddPhraseWindow(MainWindow _main)
        {
            InitializeComponent();

            micHandler = new MicrophoneHandler();

            LoadDevices();

            main = _main;
            recording = false;

            time = TimeSpan.FromSeconds(10);

            timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (time == TimeSpan.Zero) timer.Stop();
                time = time.Add(TimeSpan.FromSeconds(-1));

            }, Application.Current.Dispatcher);
        }

        private void AddPhraseBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// Recording button has three outputs that it can yield,
        /// 
        /// Green: Means that the voice input could be mathced to the key-phrase and 
        ///        that it will be sent of to validate if it already exists or not.
        ///         
        /// Yellow: Means that voice input couldn't be connected to the key-phrase. 
        ///         This could either mean that the voice command utterd wasn't close
        ///         enough or that the key-phrase enterd might be "too" long. 
        ///         
        /// Red:    Means that the keyphrase was invalidated, this could be because 
        ///         it already exists.
        ///         
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!recording) 
            {
                recording = true;
                ErrorLbl.Visibility = Visibility.Hidden;
                RecBtn.Background = new SolidColorBrush(Color.FromArgb(133, 30, 30, 0));
                RecBtn.Content = "Recording";     
              

                SetRecBtn();

                //Ask the user if what they have entered is correct an then start the recording
                MessageBoxResult result = MessageBox.Show("Is the keyphrase correct?\n " + AddPhraseBox.Text, "Confirm Keyphrase", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                if (result != MessageBoxResult.OK) return;
                

                micHandler.StartRecording(DevicesComboBox.SelectedIndex, DevicesComboBox.Text);
                
            

            }
            else if (AddPhraseBox.Text == "Enter keyphrase/ phrases" || AddPhraseBox.Text == string.Empty)
            {
                ErrorLbl.Visibility = Visibility.Visible;
                ErrorLbl.Content = "Cannot leave the field empty \n" +
                                    "or \n" +
                                    "enter the helper text";
                RecBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFA0A0A0"));
                RecBtn.Content = "Record";
                recording = false;

                SetRecBtn();
                //Stop recording

                micHandler.StopRecording();
            }
            else if(recording && AddPhraseBox.Text != "Enter keyphrase/ phrases" && AddPhraseBox.Text != string.Empty)
            {
               
                main.AddNewKeyphrase(AddPhraseBox.Text);


                //Use the timer to reset the indicator and to hold the window before sending the new keyphrase to the main, allowing the user to understand
                //what has happend.
                timer.Start();
                this.Close();
            }

            /*Check the 3 states and*/
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Turns the indicator button off and on, whenever it is capturing
        /// </summary>
        private void SetRecBtn()
        {
            if (recording == true)
            {
                indicatorBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5895FF"));
                indicatorBtn.Content = "REC";
            }
            else
            {
                indicatorBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFA0A0A0"));
                indicatorBtn.Content = "";
            }


            /*
              indicatorBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCCCC1E")); // yelllow 
              indicatorBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCC1E1E")); // red
              indicatorBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1ECC1E")); // green
             */
        }

        private void UpdateButtons() 
        {
        
        }


        /// <summary>
        /// Loads the devices (microphones) found on the current system.
        /// </summary>
        private void LoadDevices()
        {
            foreach (var d in micHandler.deviceSources)
            {
                DevicesComboBox.Items.Add(d.ProductName);
            }
            DevicesComboBox.SelectedIndex = 0;
        }

        
    }
}
