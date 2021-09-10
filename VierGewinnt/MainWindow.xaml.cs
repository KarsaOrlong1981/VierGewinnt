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


namespace VierGewinnt
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[] high = { 5, 5, 5, 5, 5, 5, 5 };
        bool red = true;
        Label[,] Feld = new Label[7, 6];
        Ellipse[,] Ell = new Ellipse[7, 6];
        int moves = 42;
        
        

        private bool Row(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            
           
              
            
            if (Feld[x1, y1].Content == Feld[x2, y2].Content &&
                Feld[x2, y2].Content == Feld[x3, y3].Content &&
                Feld[x3, y3].Content == Feld[x4, y4].Content
                )

                return true;
            else

                return false;
        }
        private void Reset(string s)
        {
            MessageBox.Show(s);
            int counter = 0;
            red = true;
            for (int i = 0; i < 7; i++)
                high[i] = 5;
            foreach (Label L in Feld)
            {
                L.Foreground = Brushes.White;
                L.Background = Brushes.White;
                L.Content = counter;
                counter++;
            }
            foreach (Ellipse E in Ell)
            {
                E.Fill = Brushes.White;
                
            }
            moves = 42;

        }

        private bool Diagonal1() // von oben links nach unten rechts
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 5; y >= 3; y--)
                {
                    if (Row(x, y, x + 1, y - 1, x + 2, y - 2, x + 3, y - 3)) return true;
                }
            }

            return false;
        }

        private bool Diagonal2() // von unten links nach oben rechts
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y <= 2; y++)
                {
                    if (Row(x, y, x + 1, y + 1, x + 2, y + 2, x + 3, y + 3)) return true;
                }
            }


            return false;
        }
        private bool win(int X, int Y)
        {
            //Horizontal prüfen
            for (int i = 0; i < 4; i++)
               
                if (Row(i, Y, i + 1, Y, i + 2, Y, i + 3, Y)) return true;
            //Vertikal prüfen
            for (int i = 0; i < 3; i++)
                if (Row(X, i, X, i + 1, X, i + 2, X, i + 3)) return true;

           
            if (Diagonal1()) return true;
            if (Diagonal2()) return true;
            return false;
        }

        public MainWindow()
        {
            InitializeComponent();



            grid.Background = Brushes.Blue;
            int index = 0;
            int locaX = 0;
           
            for (int X = 0; X < 7; X++)
            {
               
                for (int Y = 0; Y < 6; Y++)
                {
                    Ell[X, Y] = new Ellipse();
                    Ell[X, Y].Tag = locaX;
                    Ell[X, Y].Fill = Brushes.White;

                    Ell[X, Y].Height = 65;
                    Ell[X, Y].Width = 65;

                    Ell[X, Y].Name = "_" + index++;

                    Feld[X, Y] = new Label();
                    Feld[X, Y].Tag = locaX;
                    Feld[X, Y].Background = Brushes.White;
                    Feld[X, Y].Foreground = Brushes.White;
                    Feld[X, Y].Height = 45;
                    Feld[X, Y].Width = 45;
                    Feld[X, Y].Content = index++;
                    Feld[X, Y].Name = "_" + index++;


                   
                    
                    Feld[X, Y].MouseLeftButtonDown += new MouseButtonEventHandler(this.Label_MouseLeftButtonDown);
                    Grid.SetRow(Ell[X, Y], Y);
                    Grid.SetColumn(Ell[X, Y], X);
                    Grid.SetRow(Feld[X, Y], Y);
                    Grid.SetColumn(Feld[X, Y], X);
                    grid.Children.Add(this.Ell[X, Y]);
                    grid.Children.Add(this.Feld[X, Y]);
                   
                }

                locaX++;
            }

        }

       
        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int X = Convert.ToInt32((sender as Label).Tag);
            
           

            int Y = high[X]--;
            if (Y >= 0)
            {
                string st = string.Empty;
                Feld[X, Y].Background = (red ? Brushes.Red : Brushes.Yellow);
                Ell[X, Y].Fill = (red ? Brushes.Red : Brushes.Yellow);
                Feld[X, Y].Foreground = (red ? Brushes.Red : Brushes.Yellow);
                Feld[X,Y].Content = (red ? "Red" : "Yellow");
                if (Feld[X, Y].Background == Brushes.Red)
                    st = "red";
                if(Feld[X,Y].Background == Brushes.Yellow)
                    st = "yellow";
                
                if (win(X, Y)) Reset(st + " hat gewonnen");
                else if (moves == 0) Reset("Unentschieden.");
                else
                    red = !red;
            }
        }
    }
}
