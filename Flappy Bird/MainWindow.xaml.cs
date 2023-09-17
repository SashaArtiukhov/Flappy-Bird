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

using System.Windows.Threading;

namespace Flappy_Bird
{
    
   


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer = new DispatcherTimer();

        int gravitation = 10;

        float score;

        bool gameOver = false;

        Rect bird;

        private void Start()
        {
            score = 0;
            Canvas.SetTop(flappyBird, 100);
            gameOver = false;

            foreach (var x in Scene.Children.OfType<Image>())
            {
                if (x is Image && (string)x.Tag == "pipe1")
                {
                    Canvas.SetLeft(x, 500);
                }
                if (x is Image && (string)x.Tag == "pipe2")
                {
                    Canvas.SetLeft(x, 800);
                }
                if (x is Image && (string)x.Tag == "pipe3")
                {
                    Canvas.SetLeft(x, 1000);
                }
            }

            timer.Start();
        }

        private void GameEngine(object sender, EventArgs e)
        {
            ScoreLbl.Content = $"Points: {score}";

            bird = new Rect(Canvas.GetLeft(flappyBird), Canvas.GetTop(flappyBird), flappyBird.Width - 12, flappyBird.Height - 6);

            Canvas.SetTop(flappyBird, Canvas.GetTop(flappyBird) + gravitation);

            if (Canvas.GetTop(flappyBird) + flappyBird.Height > 510 || Canvas.GetTop(flappyBird) < -30)
            {
                timer.Stop();
                gameOver = true;
                ScoreLbl.Content += " Press Enter to start from the beggining!";
            }

            foreach (var x in Scene.Children.OfType<Image>())
            {
                if ((string)x.Tag == "pipe1" || (string)x.Tag == "pipe2" || (string)x.Tag == "pipe3")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 5);

                    Rect pipe = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    
                    if (bird.IntersectsWith(pipe))
                    {
                        timer.Stop();
                        gameOver = true;
                        ScoreLbl.Content += " Press Enter to start from the beggining";
                    }
                }
                if ((string)x.Tag == "pipe1" && Canvas.GetLeft(x) < -70)
                {
                    Canvas.SetLeft(x, 800);
                    score = score + 0.5f;
                }
                if ((string)x.Tag == "pipe2" && Canvas.GetLeft(x) < -110)
                {
                    Canvas.SetLeft(x, 800);
                    score = score + 0.5f;
                }
                if ((string)x.Tag == "pipe3" && Canvas.GetLeft(x) < -200)
                {
                    Canvas.SetLeft(x, 800);
                    score = score + 0.5f;
                }
            }
            
        }





        public MainWindow()
        {           
            InitializeComponent();

            timer.Tick += GameEngine;
            timer.Interval = TimeSpan.FromSeconds(0.02);
            Start();
        }

        private void Scene_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                flappyBird.RenderTransform = new RotateTransform(-15, flappyBird.Width / 2, flappyBird.Height / 2);
                gravitation = -15;
            }

            if (e.Key == Key.Enter && gameOver == true)
            {
                Start();
            }
        }

        private void Scene_KeyUp(object sender, KeyEventArgs e)
        {
            flappyBird.RenderTransform = new RotateTransform(5, flappyBird.Width / 2, flappyBird.Height / 2);
            gravitation = 10;
        }
    }
}
