using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary;

namespace FormsVersion
{
    public partial class Form1 : Form
    {
        public Dictionary<string, Position> positions = new Dictionary<string, Position>();
        public Network network = new Network();
        public List<string> nodeNames = new List<string>();
        public List<Position> listOfPositions = new List<Position>();

        public Form1()
        {
            InitializeComponent();

            listOfPositions.Add(new Position(nodeA));
            listOfPositions.Add(new Position(nodeB));
            listOfPositions.Add(new Position(nodeC));
            listOfPositions.Add(new Position(nodeD));
            listOfPositions.Add(new Position(nodeE));
            listOfPositions.Add(new Position(nodeF));
            listOfPositions.Add(new Position(nodeG));
            listOfPositions.Add(new Position(nodeH));
            listOfPositions.Add(new Position(nodeI));
            listOfPositions.Add(new Position(nodeJ));

            nodeNames.Add("A");
            nodeNames.Add("B");
            nodeNames.Add("C");
            nodeNames.Add("D");
            nodeNames.Add("E");
            nodeNames.Add("F");
            nodeNames.Add("G");
            nodeNames.Add("H");
            nodeNames.Add("I");
            nodeNames.Add("J");
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DisplayNetwork(g);
        }

        public void Connect(Position nodeOne, Position nodeTwo, Graphics g)
        {
            Pen pen = new Pen(Color.White);
            pen.Width = 2;
            Point nodeOneLocation = nodeOne.Location;
            Point nodeTwoLocation = nodeTwo.Location;
            g.DrawLine(pen, nodeOneLocation.X, nodeOneLocation.Y, nodeTwoLocation.X, nodeTwoLocation.Y);
        }

        private void DisplayNetwork(Graphics g)
        {
            foreach (var element in network.connectionPath)
            {
                for (int i = 0; i < element.Value.Count; i++)
                {
                    Connect(positions[element.Key], positions[element.Value[i].ToNode], g);
                }
            }
        }

        private void nodeC_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                nodeC.Location = new Point(this.PointToClient(Cursor.Position).X - 50, this.PointToClient(Cursor.Position).Y - 50);
                this.Refresh();
            }
        }

        private void btnRandomize_Click(object sender, EventArgs e)
        {
            if (nodeNames.Count == 10)
            {
                network = new Network();
                network.CreateNetwork(nodeNames);

                if (positions.Count != 10)
                {
                    positions.Clear();
                    for (int i = 0; i < nodeNames.Count; i++)
                    {
                        positions.Add(nodeNames[i], listOfPositions[i]);
                    }
                }
                
                this.Refresh();

            }
            else
            {
                MessageBox.Show("You need to 10 nodes");
            }


            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnRandomize.PerformClick();
        }

        private void btnFindQuickest_Click(object sender, EventArgs e)
        {
            string test = cbxToLocation.Text;
        }
    }

    public class Position
    {
        internal PictureBox PB { get; set; }
        internal Point Location 
        { get 
            {
                double xEnd = 479;
                double yEnd = 254;
                double middleOfNodeX = PB.Location.X + (PB.Size.Width / 2);
                double middleOfNodeY = PB.Location.Y + (PB.Size.Height / 2);

                double angle = Math.Atan2((yEnd - middleOfNodeY), (xEnd - middleOfNodeX)) * (180 / Math.PI);
                double radius = 40;

                double x1 = middleOfNodeX + radius * Math.Cos(angle * (Math.PI / 180));
                double y1 = middleOfNodeY + radius * Math.Sin(angle * (Math.PI / 180));
                return new Point((int)x1, (int)y1);
                }
        }

        public Position(PictureBox pB)
        {
            PB = pB;
        }
    }
}
