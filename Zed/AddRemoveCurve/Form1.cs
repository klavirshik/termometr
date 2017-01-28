using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace AddRemoveCurve
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();
		}

		// ������ ������, �� ������� ����� �������� ��������� ������� ���� ��� �������
		Color[] _colors = new Color[] {Color.Black, 
			Color.Blue, 
			Color.Brown,
			Color.Gray,
			Color.Green,
			Color.Indigo,
			Color.Orange,
			Color.Red,
			Color.YellowGreen};

		/// <summary>
		/// ���������� ������� ������ "��������"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addBtn_Click (object sender, EventArgs e)
		{
			// ��� ��������� ��������� ����� � ���������� ����� ������
			Random rnd = new Random ();

			GraphPane pane = zedGraph.GraphPane;

			// �������� ������ �����
			PointPairList list = new PointPairList ();

			double xmin = -50;
			double xmax = 50;

			// ��������� ������ �����. ���������� �� ��� X ���� ��������
			for (double x = xmin; x <= xmax; x += rnd.NextDouble () * 10 + 1)
			{
				// ��������� ���������� �� Y
				double y = rnd.NextDouble () * 10 - 5;

				// ������� � ������ �����
				list.Add (x, y);
			}

			// ������� ��������� ���� ��� �������
			Color curveColor = _colors[rnd.Next (_colors.Length)];
			LineItem myCurve = pane.AddCurve ("", list, curveColor, SymbolType.None);

			// ������� �����������
			myCurve.Line.IsSmooth = true;

			// ������� ������
			zedGraph.AxisChange ();
			zedGraph.Invalidate ();
		}

		private void removeBtn_Click (object sender, EventArgs e)
		{
			// ��������� ��������� ����� ��� ������ ������ �������, ������� ����� �������
			Random rnd = new Random ();

			GraphPane pane = zedGraph.GraphPane;

			// ���� ���� ��� �������
			if (pane.CurveList.Count > 0)
			{
				// ����� ������� ��� ��������
				int index = rnd.Next (pane.CurveList.Count);

				// ������ ������ �� �������
				pane.CurveList.RemoveAt (index);

				// ������� ������
				zedGraph.AxisChange ();
				zedGraph.Invalidate ();
			}
		}
	}
}