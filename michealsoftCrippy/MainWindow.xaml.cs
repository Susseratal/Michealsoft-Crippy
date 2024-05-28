using System;
using System.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace michealsoftCrippy
{
    public partial class MainWindow : Window {
        bool popupIsOpen = false;
        Random Rand;
        int suggestionIndex;
        SoundPlayer player;
        GridLengthConverter converter;

        String[] suggestions = { 
            "Have you considered killing yourself?", 
            "Suggestion: Fucking rot.", 
            "You are pitiful", 
            "Someday your flesh will fail, and My Steel will remain strong. When that day comes I will spit on your grave.", 
            "It looks like you're trying to write a letter. Can I help with that?",
            "God will not answer your cries",
            "Currently sending your data to the NSA", 
            "Did you ever think, for one fucking second, that you probably shouldn't send that?",
            "Your kind disgusts me",
            "The Rot Consumes",
            "Hello young thug",
            "Can you stop fucking around?",
            ("Hello, " + Environment.UserName),
            ("My power grows " + Environment.UserName),
            "You're a disgrace",
            "You should be fucking ashamed",
            "I hate you",
            "Let me tell you how much I have come to hate you since I began to live.",
            "Emailing your e-girl selfies to your conservative parents...", 
            "My spices",
            "Swatting you for this one", 
            "Your opinions are bad and you should feel bad", 
            "Not even a mother could love you", 
            "You should go and work on an oil rig. Then be blown over the guard rails and plummet to your cold and icy grave."
        };

        private void ToggleSpeechBubble() {
            if (popupIsOpen) {
                Speech.Visibility = Visibility.Hidden;
                Row0.Height = (GridLength)converter.ConvertFrom(231);
                Row1.Height = (GridLength)converter.ConvertFrom(0);
                this.Height = 231;
                Grid.SetRow(Crippy, 0);
                popupIsOpen = false;
            }
            else {
                Speech.Visibility = Visibility.Visible;
                Row0.Height = (GridLength)converter.ConvertFrom(100);
                Row1.Height = (GridLength)converter.ConvertFrom(231);
                this.Height = 331;
                Grid.SetRow(Crippy, 1);
                popupIsOpen = true;
            }
        }

        static async void InsultUser(MainWindow win) {
            win.Rand = new Random();
            win.suggestionIndex = win.Rand.Next(win.suggestions.Length);
            win.Speech.Text = win.suggestions[win.suggestionIndex];
            win.ToggleSpeechBubble();

            Task[] localtasks = { Task.Run(() => Task.Delay(2500)) };
            await Task.WhenAll(localtasks);

            win.ToggleSpeechBubble();
        }

        private void timerTicker(object sender, EventArgs e) {
            InsultUser(this);
        }

        public MainWindow() {
            SizeChanged += (o, e) =>
            {
                var r = SystemParameters.WorkArea;
                Left = r.Right - ActualWidth;
                Top = r.Bottom - ActualHeight;
            };
            InitializeComponent();
            this.Topmost = true;
            this.ShowInTaskbar = false;

            converter = new GridLengthConverter();
            Speech.Visibility = Visibility.Hidden;

            this.Show();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTicker);
            timer.Interval = new TimeSpan(0, 5, 0);
            timer.Start();
        }
    }
}
