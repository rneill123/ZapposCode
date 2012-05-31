using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            double bookHeight = 50;
            double bookWidth = 200;
            double bookLeft = 0;
            double bookTop = 0;
            int bookRow = 1;
            int totalBooks = 20;
            double booksPerRow = bookShelf.Width / bookWidth;
            Random randNum = new Random(2);
            Label[] books = new Label[totalBooks];
            String[] bookTitles = new String[]
            {
                "Ninja Acadamy 4",
                "Ninja on The Roof Top",
                "American Ninja in Vegas",
                "All the Ninjas Work at Zappos",
                "Ninjageddon",
                "OMGNOTANOTHERNINJA",
                "Pirate Ganking 101",
                "Chuck Noris, Who?",
                "Hack Like a Ninja",
                "Ninjas For Dummies",
                "You too can Ninja",
                "Ninja Hacking++",
                "Ninjas have all the fun",
                "Ninjas > Pirates",
                "Project Management for Ninjas",
                "Not another Ninja Book",
                "Ask a Ninja",
                "Urban Ninja",
                "NinjaSort for Dummies",
                "Teenage Mutant Ninja Hackers",
            };

            //Sort Array using custom QuickSort method
            NinjaSort(bookTitles, 0, bookTitles.Length - 1);

            for (int b = 0; b < totalBooks; b++)
            {
                books[b] = new Label();
                books[b].Name = ("Book" + b);
                books[b].Content = bookTitles[b];
                books[b].Height = bookHeight;
                books[b].Width = bookWidth;
                books[b].BorderThickness = new Thickness(1);
                books[b].VerticalContentAlignment = VerticalAlignment.Center;
                books[b].HorizontalContentAlignment = HorizontalAlignment.Left;
                

                //Rotate book
                RotateTransform rotateBook = new RotateTransform(90);
                books[b].RenderTransform = rotateBook;

                //Apply random color to book       
                Color bookColor = GetRandomColor(randNum);
                books[b].Background = new SolidColorBrush(bookColor);
                
                // Posistion book
                Canvas.SetLeft(books[b], (bookLeft + bookHeight));
                Canvas.SetTop(books[b], bookTop);
                 
                // Add new book to bookshelf canvas
                bookShelf.Children.Add(books[b]);
                
                //Check to make sure enough room to fit next book in canvas, else start new row
                if ((bookLeft + bookHeight) >= (booksPerRow * bookWidth))
                {
                    bookLeft = 0;
                    bookTop = bookWidth * bookRow;
                    bookRow++;
                }
                else 
                {
                    bookLeft += bookHeight;
                }
            }       
        }

        //Create and return random color
        private Color GetRandomColor(Random randNum)
        {
            Color color = new Color(); 
            byte[] cBytes = new byte[3];
            cBytes[0] = (byte)(randNum.Next(128) + 127);
            cBytes[1] = (byte)(randNum.Next(128) + 127);
            cBytes[2] = (byte)(randNum.Next(128) + 127);

            
            // make the color opaque
            color.A = 255;
            color.R = cBytes[0];
            color.B = cBytes[1];
            color.G = cBytes[2];
  
            return color;
        }

        //Custom QuickSort
        public static void NinjaSort(IComparable[] items, int left , int right)
        {
            int l = left, r= right;
            //Find item to Pivot on
            IComparable pivot = items[(left + right) / 2];
            while (l <= r)
            {
                // starting at left side look for first item that needs moved, if compareTo returns <0 then items needs to be sorted to the right
                while (items[l].CompareTo(pivot) < 0)
                {
                    l++;
                }

                //starting at right side look for last item that needs moved, if compareTo returns >0 then items needs to be sorted to the left
                while (items[r].CompareTo(pivot) > 0)
                {
                    r--;
                }

                if (l <= r)
                {
                    //Swap items
                    IComparable tmp = items[l];
                    items[l] = items[r];
                    items[r] = tmp;
                    l++; r--;
                }
            }

            if (left < r)
            {
                NinjaSort(items, left, r);
            }
            if (l < right)
            {
                NinjaSort(items, l, right);
            }
        }
    }
}
