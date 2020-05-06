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
        public PathFinder pathFinder = null;
        public List<string> nodeNames = new List<string>();
        public List<Position> listOfPositions = new List<Position>();
        Dictionary<string, Path> result = new Dictionary<string, Path>();
        List<TextBox> textboxes = new List<TextBox>();

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

            textboxes.Add(tbxA);
            textboxes.Add(tbxB);
            textboxes.Add(tbxC);
            textboxes.Add(tbxD);
            textboxes.Add(tbxE);
            textboxes.Add(tbxF);
            textboxes.Add(tbxG);
            textboxes.Add(tbxH);
            textboxes.Add(tbxI);
            textboxes.Add(tbxJ);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DisplayNetwork(g);
            if (result.Count > 0)
            {
                DisplayQuickestPath(g);
            }
        }

        public void Connect(Position nodeOne, Position nodeTwo, Graphics g, Color color)
        {
            Pen pen = new Pen(color);
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
                    Connect(positions[element.Key], positions[element.Value[i].ToNode], g, Color.White);
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
            result.Clear();
            if (nodeNames.Count == 10)
            {
                network = new Network();
                network.CreateNetwork(nodeNames);
                pathFinder = new PathFinder(network);

                if (positions.Count != 10)
                {
                    positions.Clear();
                    for (int i = 0; i < nodeNames.Count; i++)
                    {
                        positions.Add(nodeNames[i], listOfPositions[i]);
                    }
                }

                foreach (var textbox in textboxes)
                {
                    textbox.Text = ListConnections(textbox.Tag.ToString());
                }

                this.Refresh();

            }
            else
            {
                MessageBox.Show("You need 10 nodes");
            }


            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnRandomize.PerformClick();

        }

        private void btnFindQuickest_Click(object sender, EventArgs e)
        {
            string fromNode = cbxFromLocation.Text;
            string toNode = cbxToLocation.Text;
            result = pathFinder.FindQuickestPath(fromNode, toNode, false);
            lblTotal.Text = result[toNode].QuickestTimeFromStart.ToString();
            this.Refresh();
        }

        private void DisplayQuickestPath(Graphics g)
        {
            string toNode = cbxToLocation.Text;
            for (int i = 0; i < result[toNode].NodesVisited.Count - 1; i++)
            {
                Connect(positions[result[toNode].NodesVisited[i]], positions[result[toNode].NodesVisited[i + 1]], g, Color.Gold);
            }
        }

        private string ListConnections(string currentNode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"Connected to:\r\n");
            foreach (var element in network.Nodes[currentNode].Connections)
            {
                builder.Append($"{element.Key} ({element.Value.TimeCost})    ");
            }
            return builder.ToString();
        }

        private void nodeA_Click(object sender, EventArgs e)
        {
            tbxA.Visible = !tbxA.Visible;
        }

        private void nodeB_Click(object sender, EventArgs e)
        {
            tbxB.Visible = !tbxB.Visible;
        }

        private void nodeC_Click(object sender, EventArgs e)
        {
            tbxC.Visible = !tbxC.Visible;
        }

        private void nodeD_Click(object sender, EventArgs e)
        {
            tbxD.Visible = !tbxD.Visible;
        }

        private void nodeE_Click(object sender, EventArgs e)
        {
            tbxE.Visible = !tbxE.Visible;
        }

        private void nodeF_Click(object sender, EventArgs e)
        {
            tbxF.Visible = !tbxF.Visible;
        }

        private void nodeG_Click(object sender, EventArgs e)
        {
            tbxG.Visible = !tbxG.Visible;
        }

        private void nodeH_Click(object sender, EventArgs e)
        {
            tbxH.Visible = !tbxH.Visible;
        }

        private void nodeI_Click(object sender, EventArgs e)
        {
            tbxI.Visible = !tbxI.Visible;
        }

        private void nodeJ_Click(object sender, EventArgs e)
        {
            tbxJ.Visible = !tbxJ.Visible;
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
