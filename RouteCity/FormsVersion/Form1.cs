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
        // Essential variables
        public Dictionary<string, Position> nodePositionCoupling = new Dictionary<string, Position>();
        public Network network = new Network();
        public PathFinder pathFinder = null;
        public List<string> nodeNames = new List<string>();
        public List<Position> listOfAnchorPointPositions = new List<Position>();
        Dictionary<string, Path> resultsFromPathFinder = new Dictionary<string, Path>();
        List<TextBox> nodeTextBoxes = new List<TextBox>();

        public Form1()
        {
            InitializeComponent();

            // Gathering the positions of each circle on the Forms-window. 
            listOfAnchorPointPositions.Add(new Position(nodeA));
            listOfAnchorPointPositions.Add(new Position(nodeB));
            listOfAnchorPointPositions.Add(new Position(nodeC));
            listOfAnchorPointPositions.Add(new Position(nodeD));
            listOfAnchorPointPositions.Add(new Position(nodeE));
            listOfAnchorPointPositions.Add(new Position(nodeF));
            listOfAnchorPointPositions.Add(new Position(nodeG));
            listOfAnchorPointPositions.Add(new Position(nodeH));
            listOfAnchorPointPositions.Add(new Position(nodeI));
            listOfAnchorPointPositions.Add(new Position(nodeJ));

            // Creating a list of names for the nodes. 
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

            // Gathering a list of all textboxes that displays information about each connection. 
            // This will be useful in a loop later. 
            nodeTextBoxes.Add(tbxA);
            nodeTextBoxes.Add(tbxB);
            nodeTextBoxes.Add(tbxC);
            nodeTextBoxes.Add(tbxD);
            nodeTextBoxes.Add(tbxE);
            nodeTextBoxes.Add(tbxF);
            nodeTextBoxes.Add(tbxG);
            nodeTextBoxes.Add(tbxH);
            nodeTextBoxes.Add(tbxI);
            nodeTextBoxes.Add(tbxJ);
        }

        // This method is called when the Form-window is redrawn, for example when it's refreshed. When that happens we want it to
        // also regenerate the connections displayed. 
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DisplayNetwork(g);
            if (resultsFromPathFinder.Count > 0)
            {
                DisplayQuickestPath(g);
            }
        }

        private void DrawLineBetweenPositions(Position nodeOne, Position nodeTwo, Graphics g, Color color)
        {
            Pen pen = new Pen(color);
            pen.Width = 2;
            Point nodeOneLocation = nodeOne.Location;
            Point nodeTwoLocation = nodeTwo.Location;
            g.DrawLine(pen, nodeOneLocation.X, nodeOneLocation.Y, nodeTwoLocation.X, nodeTwoLocation.Y);
        }

        private void DisplayNetwork(Graphics g)
        {
            // Since "connectionpath" uses strings to informs how the nodes were connected and "positions" is a dictionary connecting 
            // a name to a position on the form, we can combine these two datastructures to draw the right lines between the right nodes. 
            foreach (var element in network.ConnectionPath)
            {
                for (int i = 0; i < element.Value.Count; i++)
                {
                    Position fromPosition = nodePositionCoupling[element.Key];
                    Position toPosition = nodePositionCoupling[element.Value[i].TargetName];
                    DrawLineBetweenPositions(fromPosition, toPosition, g, Color.White);
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
            // Clearing the result results in the line drawn using the latest quickest path based on the previous network is also removed. 
            resultsFromPathFinder.Clear();

            if (nodeNames.Count == 10)
            {
                // Creating a new network and a new PathFinder based on that network. 
                network = new Network();
                network.CreateNetwork(nodeNames);
                pathFinder = new PathFinder(network);

                if (nodePositionCoupling.Count != 10)
                {
                    // Connecting the nodes their position on the form. 
                    nodePositionCoupling.Clear();
                    for (int i = 0; i < nodeNames.Count; i++)
                    {
                        nodePositionCoupling.Add(nodeNames[i], listOfAnchorPointPositions[i]);
                    }
                }

                // Update the information in the textBoxes
                foreach (var textbox in nodeTextBoxes)
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
            try
            {
                resultsFromPathFinder = pathFinder.FindQuickestPath(fromNode, toNode, false);
                lblTotal.Text = resultsFromPathFinder[toNode].QuickestTimeFromStart.ToString();

            }
            catch(ArgumentException error)
            {
                MessageBox.Show(error.Message);
                lblTotal.Text = "N/A";
                resultsFromPathFinder.Clear();
                network.ResetNodes();
            }

            this.Refresh();

        }

        private void DisplayQuickestPath(Graphics g)
        {
            // Draws a gold line between nodes, showing the quickest path between the nodes the user chose. 
            string toNode = cbxToLocation.Text;
            for (int i = 0; i < resultsFromPathFinder[toNode].NodesVisited.Count - 1; i++)
            {
                Position fromPosition = nodePositionCoupling[resultsFromPathFinder[toNode].NodesVisited[i]];
                Position toPosition = nodePositionCoupling[resultsFromPathFinder[toNode].NodesVisited[i + 1]];
                DrawLineBetweenPositions(fromPosition, toPosition, g, Color.Gold);
            }
        }

        // Returns a string of what node the current node is connected to. 
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

        // Each circle has a position that the lines shoulld be drawn to. These positions is based on what position on the edge of the
        // circle is closest to the middle of the form. This calculates that. 
        internal Point Location 
        { get 
            {
                double middleOfFormX = 479;
                double middleOfFormY = 254;
                double middleOfNodeX = PB.Location.X + (PB.Size.Width / 2);
                double middleOfNodeY = PB.Location.Y + (PB.Size.Height / 2);

                // Calculates the angle from the cordinates at the middle of the node to the cordinates at middle of the form. 
                double angle = Math.Atan2((middleOfFormY - middleOfNodeY), (middleOfFormX - middleOfNodeX)) * (180 / Math.PI);
                double radiusOfNode = 40;

                // Calculates the cordinates at the edge of a circle based on the angle. 
                double x = middleOfNodeX + radiusOfNode * Math.Cos(angle * (Math.PI / 180));
                double y = middleOfNodeY + radiusOfNode * Math.Sin(angle * (Math.PI / 180));
                return new Point((int)x, (int)y);
                }
        }

        public Position(PictureBox pB)
        {
            PB = pB;
        }
    }
}
