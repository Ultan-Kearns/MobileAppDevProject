using System;
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
using System.Threading;
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MobileAppProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Boolean stopApp = true, startApp = false, right;
        char op;
        int a, b, result,randOp,scoreApp = 0;

        public MainPage()
        {
            this.InitializeComponent();
        }

    

        private void start_Click(object sender, RoutedEventArgs e)
        {
            for(int i = 60; i < 0; i--)
            {
                timer.Text = "Timer: " + i.ToString();
            }
            do
            {
                Random rand = new Random();
                a = rand.Next(0,100);
                b = rand.Next(0, 100);
                randOp = rand.Next(1, 4);
                switch (randOp)
                {
                    case 1:
                        op = '+';
                        break;
                    case 2:
                        op = '-';
                        break;
                    case 3:
                        op = '/';
                        break;
                    case 4:
                        op = '*';
                        break;
                    default:
                        op = '+';
                        break;
                }
                result = a + (char)op + b;
                question.Text = a.ToString() + op.ToString() + b.ToString();
                if(Convert.ToInt32(answer) == result)
                {
                 
                }
            } while (stopApp != true);
    }       
 

        private void score_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
