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
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MobileAppProject
{
    /// <summary>
    /// Mobile app development project by Ultan Kearns
    /// Simple arithmetic game which asks users to answer maths questions
    /// </summary>

    public sealed partial class MainPage : Page
    {
        //declare global variables
        Boolean stopApp = true;
        Boolean startApp = false;
        char op;
        int a, b, result, min = 40, max = 100, randOp, scoreApp = 0, highScore, previous;
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        //stop button also gives feedback to user
        private void stop_Click(object sender, RoutedEventArgs e)
        {
            stopApp = true;
            if (scoreApp > 0)
            {
                question.Text = "Nice job you got " + scoreApp + " point(s)!";
            }
            else if (scoreApp <= 0)
            {
                question.Text = "Sorry you got " + scoreApp + " Keep trying";
            }
            if (startApp == true)
            {
                startApp = false;
                stopApp = true;
                //if score is positive
                if (scoreApp > 0)
                {

                    if (scoreApp > highScore && hard.IsChecked == true)
                    {
                        highScore = scoreApp;
                        //store high score and inform user
                        localSettings.Values["hardHigh"] = highScore;
                        question.Text = "Congratulations you got " + scoreApp + " and the difficult high score!";
                        curDifficultHighScore.Text = "Difficult high score: " + localSettings.Values["hardHigh"].ToString();
                    }
                    else if (scoreApp > highScore && med.IsChecked == true)
                    {
                        highScore = scoreApp;
                        //store high score and inform user
                        localSettings.Values["high"] = highScore;
                        question.Text = "Congratulations you got " + scoreApp + " and the medium high score!\nNow try difficult!";
                        curHighScore.Text = "Medium high score: " + localSettings.Values["high"].ToString();
                    }
                    else if (scoreApp > highScore && easy.IsChecked == true)
                    {
                        highScore = scoreApp;
                        //store high score and inform user
                        localSettings.Values["easyHigh"] = highScore;
                        question.Text = "Congratulations you got " + scoreApp + " and the easy high score!\nNow try medium!";
                        curEasyHighScore.Text = "Easy high score: " + localSettings.Values["easyHigh"].ToString();
                    }
                    else
                    {
                        question.Text = "Nice job you got " + scoreApp + " points!";
                    }
                }
                //else if negative
                else
                {
                    question.Text = "Sorry you got " + scoreApp + " points";
                }
            }
            else
            {
                question.Text = "App not started!";
            }
            previousQuestion.Text = "Previous question and answer: ";
            scoreApp = 0;
        }
        public MainPage()
        {
            this.InitializeComponent();
            //Set high score for each difficulty when app is initialized
            if(localSettings.Values["high"] != null)
            {
                curHighScore.Text += localSettings.Values["high"].ToString();
            }
            else
            {
                curHighScore.Text = "No high scores yet!";
            }
            if (localSettings.Values["easyHigh"] != null)
            {
                curEasyHighScore.Text += localSettings.Values["easyHigh"].ToString();
            }
            else
            {
                curEasyHighScore.Text = "No high scores yet!";
            }
            if (localSettings.Values["hardHigh"] != null)
            {
                curDifficultHighScore.Text += localSettings.Values["hardHigh"].ToString();
            }
            else
            {
                curDifficultHighScore.Text = "No high scores yet!";
            }
        }

        private void Med_Tapped(object sender, TappedRoutedEventArgs e)
        {
            min = 40;
            max = 500;
            if (startApp == true)
            {
                Generate_Random();
                question.Text = a.ToString() + op.ToString() + b.ToString();
                previous = 1;
            }
            if (scoreApp != 0 && previous != 1)
            { 
                question.Text += "\nDifficulty changed resetting score to 0";
                scoreApp = 0;
                score.Text = "Your Score: " + scoreApp.ToString();  
            }
            else
            {
                question.Text = a.ToString() + op.ToString() + b.ToString() + "\nDifficulty already on medium";
            }
        }

        private void Hard_Tapped(object sender, TappedRoutedEventArgs e)
        {
            min = 1000;
            max = 100000;
            if (startApp == true)
            {
                Generate_Random();
                question.Text = a.ToString() + op.ToString() + b.ToString();
                previous = 2;
            }
            if (scoreApp != 0 && previous != 2)
            {
                question.Text += "\nDifficulty changed resetting score to 0";
                scoreApp = 0;
                score.Text = "Your Score: " + scoreApp.ToString();
            }
            else
            {
                question.Text = a.ToString() + op.ToString() + b.ToString() + "\nDifficulty already on difficult";
            }
        }

        private void Easy_Tapped(object sender, TappedRoutedEventArgs e)
        {
            min = 1;
            max = 40;
            if (startApp == true)
            {
                Generate_Random();
                question.Text = a.ToString() + op.ToString() + b.ToString();
                previous = 0;
            }
            if (scoreApp != 0 && previous != 0)
            {
               question.Text += "\nDifficulty changed resetting score to 0";
               scoreApp = 0;
               score.Text = "Your Score: " + scoreApp.ToString();
            }
            else
            {
                question.Text = a.ToString() + op.ToString() + b.ToString() + "\nDifficulty already on easy";
            }
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (startApp == true)
            {
                String ans;
                //set string answer = user answer
                ans = answer.Text;
                //try-catch to see if input is valid
                try
                {
                    //check if correect and increment or decrement score according to difficulty
                    if (result == Convert.ToInt32(ans))
                    {
                        if (min >= 40 && max <= 500)
                        {
                            scoreApp += 5;
                        }
                        else if (min >= 1000 && max <= 100000)
                        {
                            scoreApp += 10;
                        }
                        else
                        {
                            scoreApp++;
                        }
                    }
                    else
                    {
                        if (min >= 40 && max <= 500)
                        {
                            scoreApp -= 1 * 5;
                        }
                        else if (min >= 1000 && max <= 100000)
                        {
                            scoreApp -= 1 * 10;
                        }
                        else
                        {
                            scoreApp--;
                        }
                    }
                    previousQuestion.Text = "Previous question and answer: " + a.ToString() + op.ToString() + b.ToString() + " = " + result;
                    //change text of score
                    score.Text = "Your Score: " + scoreApp.ToString();
                    Generate_Random();
                    question.Text = a.ToString() + op.ToString() + b.ToString();
                    answer.Text = "";
                }
                catch
                {
                    question.Text = "invalid input please enter an integer\n" + a.ToString() + op.ToString() + b.ToString();
                }
            }
            else
            {
                question.Text = "App not started!";
            }
        }
        public void Generate_Random()
        {
            //declare two new randoms between 0 and 100
            Random rand = new Random();
            a = rand.Next(min, max);
            b = rand.Next(min, max);
            randOp = rand.Next(1,4);
            //switch so operator is also random
            switch (randOp)
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
                    //make sure numbers divide evenly and that answer is not 1
                    while (a % b != 0 || a / b == 1)
                    {
                        a++;
                    }
                    if(b > a)
                    {
                        int temp = a;
                        a = b;
                        b = a;
                    }
                    result = a / b;
                    op = '/';
                    break;
                case 4:
                    result = a * b;
                    op = '*';
                    break;
            }
        }
        private void start_Click(object sender, RoutedEventArgs e)
        {
            //check to see if app already started
            if (startApp == false)
            {
                //loop until stopApp = true
                do
                {
                    startApp = true;
                    score.Text = "Your Score: " + scoreApp.ToString();
                    Generate_Random();
                    question.Text = a.ToString() + op.ToString() + b.ToString();

                } while (stopApp != true);
            }
            else
            {
                question.Text = "App already started!\n" + a.ToString() + op.ToString() + b.ToString();
            }
        }
    }
}