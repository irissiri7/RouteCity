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
        public Dictionary<PictureBox, Point> positions = new Dictionary<PictureBox, Point>();
        public Network network = new Network();
        public List<string> nodeNames = new List<string>();

        public Form1()
        {
            InitializeComponent();
            
        }

        // <string,node>

        /*
         public class Display
         {
            int middle;
            <string, node> Nodes - Eller dictionary <namn, namn> en för nodens namn och vilken picturebox den är kopplad till. Kanske själva pb?
            eller <namnetpåPB, noden>
            <pb, noden>
            CalculateAnchorPoint()
            pictureboxes? or <string, point> dictionary

            Objekt med picturebox och positionen?
            Calculate iff där med?

            <sträng, position>
            Behövs egentligen bara två positioner för att göra koppling. 
            Men det måste vara kopplat till faktiska kopplingar. 

            Från nod = Namnet och listan på kopplingar
            (Istället: Ha vilka kopplingar som gjordes till vilken som en lista i network (Och tidskostnad)? Då blir de unika också)
            (Skapa lista med dessa namn och vilka de kopplades till? (Infon är då inte vilka som har kopplingar till vilka, utan
            bara hur kopplingar gjorts vilket är det enda jag behöver) Sedan loopa det?)
            (Hur ge varje nod en position? En annan dictionary med samma namn men en position?) - Objekt med picturebox som räknar ut position och har det som en property?***
            (Det kan redan finnas ett objekt för varje picturebox som har en position som sen bara sätts in i dictionary?)
            Get calculerar värdet varjegång det ska gettas. Då Get tar Pictureboxvärden osv. 
            (Listorna i egen klass så att den kan ha propertyn middle en gång?)

            (Måste nog isf ha info om vilket namn som ska vara kopplat till vilken picturebox? Ha lista med pictureboxes så att i i listan blir samma som 
            namnet i den andra? och gör kalkultionen?) - Måste också ha det för att veta vilken position som ska ändras när den flyttas? Döpa om pb?
            (Funkar nog bara om de finns i en lista så jag vet vilken som är den första osv)
            
            Från Picturebox = Positionen och storleken (Kanske radien)
            A ska kopplas till ett namn
            För kopplingar startpunkt och slutpunkt
            skapa objekt av detta och det ger en lista med start och slutpositioner? (Vill väl ha en lista med unika positioner) (Använda hashset?) 
            


         }
             */

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            positions.Clear();
            positions.Add(nodeA, CalculateAnchorPoint(new Point(479, 254), nodeA));
            positions.Add(nodeB, CalculateAnchorPoint(new Point(479, 254), nodeB));
            positions.Add(nodeC, CalculateAnchorPoint(new Point(479, 254), nodeC));
            positions.Add(nodeD, CalculateAnchorPoint(new Point(479, 254), nodeD));
            positions.Add(nodeE, CalculateAnchorPoint(new Point(479, 254), nodeE));
            positions.Add(nodeF, CalculateAnchorPoint(new Point(479, 254), nodeF));
            positions.Add(nodeG, CalculateAnchorPoint(new Point(479, 254), nodeG));
            positions.Add(nodeH, CalculateAnchorPoint(new Point(479, 254), nodeH));
            positions.Add(nodeI, CalculateAnchorPoint(new Point(479, 254), nodeI));
            positions.Add(nodeJ, CalculateAnchorPoint(new Point(479, 254), nodeJ));

            Connect(nodeA, nodeC, g);
            Connect(nodeA, nodeD, g);
            Connect(nodeA, nodeI, g);

        }

        public void Connect(PictureBox nodeOne, PictureBox nodeTwo, Graphics g)
        {
            Pen pen = new Pen(Color.White);
            pen.Width = 2;
            g.DrawLine(pen, positions[nodeOne].X, positions[nodeOne].Y, positions[nodeTwo].X, positions[nodeTwo].Y);
        }

        public Point CalculateAnchorPoint(Point middle, PictureBox node)
        {
            double xEnd = middle.X;
            double yEnd = middle.Y;
            double middleOfNodeX = node.Location.X + (node.Size.Width / 2);
            double middleOfNodeY = node.Location.Y + (node.Size.Height / 2);

            double angle = Math.Atan2((yEnd - middleOfNodeY), (xEnd - middleOfNodeX)) * (180 / Math.PI);
            double radius = 40;

            double x1 = middleOfNodeX + radius * Math.Cos(angle * (Math.PI / 180));
            double y1 = middleOfNodeY + radius * Math.Sin(angle * (Math.PI / 180));
            return new Point((int)x1, (int)y1);
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
                network.CreateNetwork(nodeNames);

            }
            else
            {
                MessageBox.Show("You need to 10 nodes");
            }


            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            nodeNames.Add(tbxName.Text);
            lblTotal.Text = $"Total {nodeNames.Count}";

            if (nodeNames.Count >= 10)
            {
                btnAdd.Enabled = false;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            nodeNames.Clear();
            lblTotal.Text = $"Total {nodeNames.Count}";
        }
    }
}
