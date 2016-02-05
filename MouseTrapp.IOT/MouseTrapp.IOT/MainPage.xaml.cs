﻿using System;
using System.Collections.Generic;
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
// Enable asynchronous tasks
using System.Threading.Tasks;
// Enable access to the GPIO bus on the RPi2
using Windows.Devices.Gpio;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MouseTrapp.IOT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // Define the physical pin connected to the LED.
        private const int LED_PIN = 12;
        // Deifne a variable to represent the pin as an object.
        private GpioPin pin;
        // Define a variable to hold the value of the pin (HIGH or LOW).
        private GpioPinValue pinValue;
        // Define a time used to control the frequency of events.
        private DispatcherTimer timer;
        // Define a color brushes for the on screen representation of the LED.
        private SolidColorBrush redBrush = new SolidColorBrush(Windows.UI.Colors.Red);
        private SolidColorBrush grayBrush = new SolidColorBrush(Windows.UI.Colors.LightGray);

        public MainPage()
        {
            this.InitializeComponent();

            // Create an instance of a Timer that will raise an event every 500ms
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;

            // Initialize the GPIO bus
            InitGpioAsync();
        }

        private async Task InitGpioAsync()
        {
            // Get the default GPIO controller
            var gpio = await GpioController.GetDefaultAsync();

            // If the default GPIO controller is not present, then the device 
            // running this app isn't capable of GPIO operations.
            if (gpio == null)
            {
                pin = null;
                GpioStatus.Text = "There is no GPIO controller on this device.";
                return;
            }

            // Open the GPIO channel
            pin = gpio.OpenPin(LED_PIN);

            // As long as the pin object is not null, proceed
            if (pin != null)
            {
                // Define the pin as an output pin
                pin.SetDriveMode(GpioPinDriveMode.Output);
                // Define the initial status as LOW (off)
                pinValue = GpioPinValue.Low;
                // Write the tate to the pin
                pin.Write(pinValue);
                // Update the on screen text to indicate that the GPIO is ready
                GpioStatus.Text = "GPIO pin is initialized correctly.";
                // Start the timer.
                timer.Start();
            }

        }

        private void Timer_Tick(object sender, object e)
        {
            // This Timer event will be raised on each timer interval (defined above)

            if (pinValue == GpioPinValue.Low)
            {
                // If the current state of the pin is LOW (off), then set it to HIGH (on)
                // and update the on screen UI to represent the LED in the on state
                pinValue = GpioPinValue.High;
                LedGraphic.Fill = redBrush;
            }
            else
            {
                // If the current state of the pin is HIGH (on), then set it to LOW (off)
                // and update the on screen UI to represent the LED in the off state
                pinValue = GpioPinValue.Low;
                LedGraphic.Fill = grayBrush;
            }
            // Write the state to to pin
            pin.Write(pinValue);

        }
    }
}
