using System;
using System.Threading;
using System.Windows.Forms;
using System.Media;
using System.Drawing;

namespace DrunkPC
{
    class Program
    {
        public static Random _random = new Random();

        public static int _startupDelayDurationSeconds = 5;
        public static int _totalDurationSeconds = 10;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello");

            if (args.Length >= 2)
            {
                _startupDelayDurationSeconds = Convert.ToInt32(args[0]);
                _totalDurationSeconds = Convert.ToInt32(args[1]);
            }

            Thread drunkMouseThread = new Thread(new ThreadStart(DrunkMouseThread));
            Thread drunkKeyboardThread = new Thread(new ThreadStart(DrunkKeyboardThread));
            Thread drunkSoundThread = new Thread(new ThreadStart(DrunkSoundThread));
            Thread drunkPopupThread = new Thread(new ThreadStart(DrunkPopupThread));

            DateTime future = DateTime.Now.AddSeconds(_startupDelayDurationSeconds);
            Console.WriteLine("Waiting To Start Threads");
            while (future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

            drunkMouseThread.Start();
            drunkKeyboardThread.Start();
            drunkSoundThread.Start();
            drunkPopupThread.Start();

            future = DateTime.Now.AddSeconds(_totalDurationSeconds);
            Console.WriteLine("Terminating All Threads");
            while (future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

            drunkMouseThread.Abort();
            drunkKeyboardThread.Abort();
            drunkSoundThread.Abort();
            drunkPopupThread.Abort();
        }
        #region Thread Functions
        public static void DrunkMouseThread()
        {
            Console.WriteLine("Drunk Mouse Thread Started");

            int moveX = 0;
            int moveY = 0;

            while (true)
            {
                moveX = _random.Next(20) - 10;
                moveY = _random.Next(20) - 10;

                Cursor.Position = new System.Drawing.Point(Cursor.Position.X + moveX, Cursor.Position.Y + moveY);

                Thread.Sleep(5);

            }
        }
        public static void DrunkKeyboardThread()
        {
            Console.WriteLine("Drunk Keyboard Thread Started");

            while (true)
            {
                if (_random.Next(50) > 30)
                {
                    char key = (char)(_random.Next(25) + 65);

                    if (_random.Next(2) == 0)
                    {
                        key = Char.ToLower(key);
                    }

                    SendKeys.SendWait(key.ToString());
                }

                Thread.Sleep(_random.Next(500));
            }
        }
        public static void DrunkSoundThread()
        {
            Console.WriteLine("Drunk Sound Thread Started");

            while (true)
            {
                if (_random.Next(50) > 40)
                {
                    switch (_random.Next(2))
                    {
                        case 0:
                            SystemSounds.Asterisk.Play();
                            break;
                        case 1:
                            SystemSounds.Hand.Play();
                            break;
                    }
                    Thread.Sleep(1000);
                }
            }
        }
        public static void DrunkPopupThread()
        {
            Console.WriteLine("Drunk Popup Thread Started");

            while (true)
            {
                if (_random.Next(100) > 50)
                {
                    switch (_random.Next(2))
                    {
                        case 0:
                            MessageBox.Show("Internet Explorer has stopped working",
                                "Internet Explorer",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;
                        case 1:
                            MessageBox.Show("Your system is running low on resources",
                                "Microsoft Windows",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            break;
                    }
                }
                Thread.Sleep(1000);
            }
        }
        #endregion
    }
}
