using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using NAudio.Wave;
using Pocketsphinx;


namespace VoiceControllerTool
{
    class MicrophoneHandler
    {
        public enum SampelingRateEnum
        {
            [Description("8K")]
            EightK = 8000,
            [Description("11K")]
            ElevenK = 11025,
            [Description("16K")]
            SixteenK = 16000,
            [Description("22K")]
            TwentyTwoK = 22050,
            [Description("32K")]
            ThirtyTwoK = 32000,
            [Description("44K")]
            FortyFourK = 44100,
            [Description("48K")]
            FortyEightK = 48000,
        }

        public string Name { get; } //Microphone name
        public SampelingRateEnum SampelingRate { get; }
        public int ClipTime { get; }

        private Pocketsphinx.Decoder decoder; //both ms and ps have a definition for decoder thats why its specefied

        //BlockAlignReductionStream stream;
        DirectSoundOut output;
        WaveFileReader sourceFile;
        WaveFileWriter waveFileWriter;

        WaveIn sourceStream;


        public List<WaveInCapabilities> deviceSources { get; private set; }

        public MicrophoneHandler(string _name, SampelingRateEnum _sampelingRate, int _clipTime)
        {
            Name = _name;
            SampelingRate = _sampelingRate;
            ClipTime = _clipTime;


          
        }

        public MicrophoneHandler()
        {
            DetectAudioSources();
        }


        //Find audiosources conncected to the current device
        private void DetectAudioSources()
        {
            deviceSources = new List<WaveInCapabilities>();

            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                deviceSources.Add(WaveIn.GetCapabilities(i));
            }
           
        }

        private void SetupKeywordDecoder()
        {
            Config config = Pocketsphinx.Decoder.DefaultConfig();

            string speachData;



        }

        public void StartRecording(int index, string deviceName)
        {      
            if (deviceSources.Count == 0) return;

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Wave File (*.wav)|*.wav";


            save.ShowDialog();  //Switch this out with a list of all the objects

            int selectedDevice = index;

            sourceStream = new WaveIn();
            sourceStream.DeviceNumber = selectedDevice;

            sourceStream.WaveFormat = new WaveFormat(44100, WaveIn.GetCapabilities(selectedDevice).Channels);

            sourceStream.DataAvailable += new EventHandler<WaveInEventArgs>(SourceStream_DataAvailable);
            waveFileWriter = new WaveFileWriter(save.FileName, sourceStream.WaveFormat);

            sourceStream.StartRecording();
        }


        private void SourceStream_DataAvailable(object sender, WaveInEventArgs e)
        {
            waveFileWriter.WriteData(e.Buffer, 0, e.BytesRecorded);

            waveFileWriter.CreateObjRef();
            waveFileWriter.Flush(); //clear the ram so that it doesn't hold the old recording
        }


        public void StopRecording()
        {
            if (sourceStream != null)
            {
                sourceStream.StopRecording();
                sourceStream.Dispose();
                sourceStream = null;
            }

            if (waveFileWriter != null)
            {
                waveFileWriter.Dispose();
                waveFileWriter = null;
            }
        }


    }
}
