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
        char op;
        double a, b, result;
        int randOp, scoreApp = 0;

        //stop button also gives feedback to user
        private void stop_Click(object sender, RoutedEventArgs e)
        {
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
                question.Text = "Are you even trying?";
            }
        }

        String ans;

        public MainPage()
        {
            this.InitializeComponent();
        }
        
        private void Enter_Click(object sender, RoutedEventArgs e)
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
        public void generate_Random() 
        {
            //declare two new randoms between 0 and 100
            Random rand = new Random(); 
            a = rand.Next(0, 100);
            b = rand.Next(0, 100);
            randOp = rand.Next(1, 4);
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
                    //be nice to user and change division around if b > a
                    if (a > b)
                    {
                        result = a / b;
                    }
                    else 
                    {
                        double temp = 0;
                        a = temp;
                        b = a;
                        a = b;
                    }
                    //check for 0 and generate new random
                    if (a == 0 || b == 0)
                    {
                        generate_Random();
                    }
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
            do
            {
               generate_Random();
               question.Text = a.ToString() + op.ToString() + b.ToString();
            } while (stopApp != true);
        }   
    }
}
