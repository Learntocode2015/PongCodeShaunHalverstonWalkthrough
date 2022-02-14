using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PongCodeShaunHalverston
{
    public partial class Pong : Form
    {
        int cpuDirection = 5;
        int ballXCoordinate = 5;
        int ballYCoordinate = 5;
        int playerScore2 =0;
        int cpuScore2 = 0;
        int bottomBoundary;
        int centerPoint;
        int xMidpoint;
        int yMidpoint;
        bool playerDetectedUp;
        bool playerDetectedDown;
        int spaceBarClicked = 0;
        public Pong()
        {
            InitializeComponent();
            bottomBoundary = ClientSize.Height - player1.Height;
            
            xMidpoint = ClientSize.Width / 2;
            yMidpoint = ClientSize.Height / 2;   
        }

        private void pong_Load(object sender, EventArgs e)
        { 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random newBallSpot = new Random();
            int newSpot = newBallSpot.Next(100, ClientSize.Height - 100);
            //Adjust where the ball is every tick
            pongBall.Top -= ballYCoordinate;
            pongBall.Left -= ballXCoordinate;
            //make the CPU move
            cpuPlayer.Top += cpuDirection;
            //check if CPU has reached the top or the bottom
            if (cpuPlayer.Top < 0 || cpuPlayer.Top > bottomBoundary)
                {
                cpuDirection = -cpuDirection;
                }
            // Check if the ball has exited the left side of the screen
            if (pongBall.Left < 0)
            {
                pongBall.Left = xMidpoint;
                pongBall.Top = newSpot;
                ballXCoordinate = -ballXCoordinate;
                cpuScore2++;
                cpuScoreLabel.Text = cpuScore2.ToString();
            }

            // Check if the ball has exited the right side of the screen
            if (pongBall.Left + pongBall.Width > ClientSize.Width)
            {
                pongBall.Left = xMidpoint;
                pongBall.Top = newSpot;
                ballXCoordinate = -ballXCoordinate;
                playerScore2++;
                playerScoreLabel.Text = playerScore2.ToString();
            }
            // Ensure the ball is within the boundaries of the screen
            if (pongBall.Top < 0 || pongBall.Top + pongBall.Height > ClientSize.Height) { ballYCoordinate = -ballYCoordinate; }

            // Check if the ball hits the player or CPU paddle
            if (pongBall.Bounds.IntersectsWith(player1.Bounds) || pongBall.Bounds.IntersectsWith(cpuPlayer.Bounds))
            {
                // Send ball opposite direction
                ballXCoordinate = -ballXCoordinate;
            }

            // Move player up

            if (playerDetectedUp == true && player1.Top > 0) { player1.Top -= 10; }
            // Move player down
            if (playerDetectedDown == true && player1.Top < bottomBoundary) { player1.Top += 10; }
            // Check for winner
            if (playerScore2 >= 10) { timer1.Stop(); }
        }


        private void Pong_KeyDown(object sender, KeyEventArgs e)
        {
            // If player presses the up arrow, move paddle upwards
            if (e.KeyCode == Keys.Up) { playerDetectedUp = true; }
            // If player presses the down arrow, move paddle downwards
            if (e.KeyCode == Keys.Down) { playerDetectedDown = true; }
            // If player presses the C key, open the character selection screen

            // If player presses space bar, pause the game
            if (e.KeyCode == Keys.Space)
            {
                if (spaceBarClicked % 2 == 0)
                {
                    timer1.Stop();
                }
                else
                {
                    timer1.Start();
                }
            }
            spaceBarClicked++;
        }

        private void Pong_KeyUp(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Up) { playerDetectedUp = false; }
            if (e.KeyCode == Keys.Down) { playerDetectedDown = false; }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
