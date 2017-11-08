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
        Boolean stopApp = true;
        Boolean startApp = false;
        char op;
        int a, b, result;
        int randOp, scoreApp = 0;

        //stop button also gives feedback to user
        private void stop_Click(object sender, RoutedEventArgs e)
        {
            startApp = false;
            stopApp = true;
            if(scoreApp > 0)
            {
                question.Text = "Nice job you got " + scoreApp + " right!";
            }
            else if(scoreApp < 0)
            {
                question.Text = "Sorry you got " + scoreApp + " Wrong but you can always improve!";
            }
            else
            {
                question.Text = "Sorry you got " + scoreApp + " keep trying";
            }
        }

        String ans;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void med_Tapped(object sender, TappedRoutedEventArgs e)
        { 
        }

        private void hard_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void easy_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (startApp == true)
            {
                //set string answer = user answer
                ans = answer.Text;
                //try-catch to see if input is valid
                try
                {
                    //check if correect and increment or decrement score
                    if (result == Convert.ToDouble(ans))
                    {
                        scoreApp++;
                    }
                    else
                    {
                        scoreApp--;
                    }
                    //change text of score
                    score.Text = "Score: " + scoreApp.ToString();
                    generate_Random();
                    question.Text = a.ToString() + op.ToString() + b.ToString();
                    answer.Text = "";
                }
                catch
                {
                    answer.Text = "invalid input";
                    question.Text = a.ToString() + op.ToString() + b.ToString();
                }
            }
            else
            {
                question.Text = "App not started!";
            }
        }
        public void generate_Random() 
        {
            //declare two new randoms between 0 and 100
            Random rand = new Random(); 
            a = rand.Next(1, 100);
            b = rand.Next(1, 100); 
            randOp = rand.Next(1,4);
            switch (randOp) //switch so operator is also random
            {
                case 1:
                    result = a + b;
                    op = '+';
                    break;
                case 2:
                    result = a - b;
                    op = '-';
                    break;
                case 3:
                    //make sure numbers divide evenly
                  while(a % b != 0 && b % a != 0) 
                    {
                        a++;
                    }
                    if (b > a)
                    {
                        int temp = a;
                        a = b;
                        b = temp;
                    }
                    result = a / b;
                    op = '/';
                    break;
                case 4:
                    result = a * b;
                    op = '*';
                    break;
                default:
                    result = a + b;
                    op = '+';
                    break;
            }
        }
        private void start_Click(object sender, RoutedEventArgs e)
        {
            //loop until stopApp = true
            if (startApp == false)
            {
                do
                {
                    startApp = true;
                    generate_Random();
                    question.Text = a.ToString() + op.ToString() + b.ToString();
                } while (stopApp != true);
            }
        }   
    }
}
