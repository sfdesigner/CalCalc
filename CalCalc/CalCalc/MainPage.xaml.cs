using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CalCalc
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int startHour;
        int startMin;
        int startSec;
        int finishHour;
        int finishMin;
        int finishSec;
        int calcHour;
        int calcMin;
        int calcSec;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void CalcTimeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                startHour = int.Parse(StartHourTextBox.Text);
                startMin = int.Parse(StartMinTextBox.Text);
                startSec = int.Parse(StartSecTextBox.Text);
                finishHour = int.Parse(FinishHourTextBox.Text);
                finishMin = int.Parse(FinishMinTextBox.Text);
                finishSec = int.Parse(FinishSecTextBox.Text);

                int startTime = (startHour * 60 * 60) + (startMin * 60) + (startSec);
                int finishTime = (finishHour * 60 * 60) + (finishMin * 60) + (finishSec);

                if (finishTime < startTime) throw new InvalidOperationException("Finish time is earlier that start time.");

                int calcTime = finishTime - startTime;

                Debug.WriteLine($"start: {startTime}; finish: {finishTime}");

                Debug.WriteLine($"Time: {calcTime}");

                // Find hours
                calcHour = calcTime / (60 * 60);
                Debug.WriteLine($"Hours: {calcHour}");
                calcTime = calcTime % (60 * 60);
                Debug.WriteLine($"Time (minutes hours): {calcTime}");

                // Find minutes
                calcMin = calcTime / 60;
                Debug.WriteLine($"Minutes: {calcMin}");
                calcTime = calcTime % 60;
                Debug.WriteLine($"Time (minus minutes): {calcTime}");

                // Find seconds
                calcSec = calcTime;
                Debug.WriteLine($"Seconds: {calcSec}");

                DisplayCalc();
            }
            catch (FormatException fEx)
            {
                CalcResultTextBlock.Text = fEx.Message;
            }
            catch (InvalidOperationException ioEx)
            {
                CalcResultTextBlock.Text = ioEx.Message;
            }
        }

        private void DisplayCalc()
        {
            string calcResult = $"{calcHour} hours, {calcMin} minutes, and {calcSec} seconds";
            CalcResultTextBlock.Text = calcResult;
        }
    }
}
