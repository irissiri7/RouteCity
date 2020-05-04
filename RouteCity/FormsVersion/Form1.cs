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
        public Form1()
        {
            InitializeComponent();
            
        }

        
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
    }
}
