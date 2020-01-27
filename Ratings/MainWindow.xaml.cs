using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using The_Algorithm;

namespace Ratings
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            bTimer.Elapsed += new ElapsedEventHandler(OnTimedEventB);
            aTimer.Interval = 50;
            bTimer.Interval = 5000;
        }

        private Dictionary<string, (float, float)> boardRatings = new Dictionary<string, (float, float)> // (curr, final)
        {
            { "/qa/", (-1, -1) },
            { "4/jp/", (-1, -1) },
            { "/gnfos/", (-1, -1) },
            { "/ota/", (-1, -1) }
        };
        private Dictionary<string, List<int>> ratingsLists = new Dictionary<string, List<int>>
        {
            { "/qa/", new List<int>() },
            { "4/jp/", new List<int>() },
            { "/gnfos/", new List<int>() },
            { "/ota/", new List<int>() }
        };
        private string finalRating;
        private string currBoard;
        private float j = -0.25F;
        private System.Timers.Timer aTimer = new System.Timers.Timer();
        private System.Timers.Timer bTimer = new System.Timers.Timer();

        private void CalcCurrent(List<int> ratings)
        {
            int ratingsTotal = 0;
            foreach (int rating in ratings)
            {
                ratingsTotal += rating;
            }
            
            float ratingFloat = ((float)ratingsTotal / ratings.Count);
            txtCurrRating.Text = ratingFloat.ToString();
            boardRatings[currBoard] = (ratingFloat, boardRatings[currBoard].Item2);
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            currBoard = (sender as Button).Content.ToString();
            boardImage.Source = new BitmapImage(new Uri("/Ratings;component/Images/" + currBoard.Replace("/", string.Empty) + "icon.png", UriKind.Relative));
            if (currBoard == "4/jp/" || currBoard == "/qa/")
            {
                foreach (Button btn in calcGrid.Children)
                {
                    btn.IsEnabled = false;
                }
                btnFinal.IsEnabled = false;
                txtCurrRating.Text = "0";
                txtFinalRating.Text = "0";
            }
            else {
                foreach (Button btn in calcGrid.Children)
                {
                    btn.IsEnabled = true;
                }
                btnFinal.IsEnabled = true;
                boardImage.Source = new BitmapImage(new Uri("/Ratings;component/Images/" + currBoard.Replace("/", string.Empty) + "icon.png", UriKind.Relative));
                txtCurrRating.Text = (boardRatings[currBoard].Item1 == -1) ? "N/A" : boardRatings[currBoard].Item1.ToString();
                txtFinalRating.Text = (boardRatings[currBoard].Item2 == -1) ? "Pending" : boardRatings[currBoard].Item2.ToString("F0");
            }
        }

        private void BtnQa_Click(object sender, RoutedEventArgs e)
        {
            Update(sender, e);
        }

        private void BtnFourjp_Click(object sender, RoutedEventArgs e)
        {
            Update(sender, e);
        }

        private void BtnOta_Click(object sender, RoutedEventArgs e)
        {
            Update(sender, e);
        }

        private void BtnWhat_Click(object sender, RoutedEventArgs e)
        {
            Update(sender, e);
        }

        private void BtnGhost_Click(object sender, RoutedEventArgs e)
        {
            Update(sender, e);
        }

        private void BtnGnfos_Click(object sender, RoutedEventArgs e)
        {
            Update(sender, e);
        }

        private void Calc(object sender, RoutedEventArgs e)
        {
            if (currBoard == null || currBoard == "4/jp")
            {
                return;
            }
            ratingsLists[currBoard].Add(Convert.ToInt32((sender as Button).Content.ToString()));
            CalcCurrent(ratingsLists[currBoard]);
        }

        private void Zero_Click(object sender, RoutedEventArgs e)
        {
            Calc(sender, e);
        }

        private void One_Click(object sender, RoutedEventArgs e)
        {
            Calc(sender, e);
        }

        private void Two_Click(object sender, RoutedEventArgs e)
        {
            Calc(sender, e);
        }

        private void Three_Click(object sender, RoutedEventArgs e)
        {
            Calc(sender, e);
        }

        private void Four_Click(object sender, RoutedEventArgs e)
        {
            Calc(sender, e);
        }

        private void Five_Click(object sender, RoutedEventArgs e)
        {
            Calc(sender, e);
        }

        private void Six_Click(object sender, RoutedEventArgs e)
        {
            Calc(sender, e);
        }

        private void Seven_Click(object sender, RoutedEventArgs e)
        {
            Calc(sender, e);
        }

        private void Eight_Click(object sender, RoutedEventArgs e)
        {
            Calc(sender, e);
        }

        private void Nine_Click(object sender, RoutedEventArgs e)
        {
            Calc(sender, e);
        }

        private void Ten_Click(object sender, RoutedEventArgs e)
        {
            Calc(sender, e);
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            if (currBoard == null || currBoard == "4/jp")
            {
                return;
            }
            if (ratingsLists[currBoard].Any()) 
            {
                ratingsLists[currBoard].RemoveAt(ratingsLists[currBoard].Count - 1);
                if (ratingsLists[currBoard].Any())
                {
                    CalcCurrent(ratingsLists[currBoard]);
                }
                else
                {
                    txtCurrRating.Text = "N/A";
                }
            }            
        }

        private void BtnFinal_Click(object sender, RoutedEventArgs e)
        {
            if (currBoard == null || currBoard == "4/jp")
            {
                return;
            }
            boardRatings[currBoard] = (boardRatings[currBoard].Item1, The_Algorithm.The_Algorithm.TheAlgorithm(boardRatings[currBoard].Item1));
            if (boardRatings[currBoard].Item2 > 10)
            {
                finalRating = "10";
                boardRatings[currBoard] = (boardRatings[currBoard].Item1, 10);
            }
            else if (boardRatings[currBoard].Item2 < 0)
            {
                finalRating = "0";
                boardRatings[currBoard] = (boardRatings[currBoard].Item1, 0);
            }
            else
                finalRating = boardRatings[currBoard].Item2.ToString("F0");
            txtFinalRating.Text = "CALCULATING";                        
            aTimer.Start();
            bTimer.Start();
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {                
                if (txtFinalRating.Foreground.Opacity == 1)
                    j = -0.25F;
                else if (txtFinalRating.Foreground.Opacity == 0)
                    j = 0.25F;
                txtFinalRating.Foreground.Opacity += j;
            });
        }

        private void OnTimedEventB(object source, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                aTimer.Stop();
                txtFinalRating.Foreground.Opacity = 1;
                txtFinalRating.Text = finalRating;
                bTimer.Stop();
            });
        }
    }
}
