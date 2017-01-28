using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace MasterPane2
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			DrawGraph ();
		}

		private void DrawGraph ()
		{
			// ������� ��������� ������ MasterPane, ������� ������������ ����� �������, 
			// �� ������� "�����" ��� ������� (���������� ������ GraphPane)
			ZedGraph.MasterPane masterPane = zedGraph.MasterPane;

			// �� ��������� � MasterPane ���������� ���� ��������� ������ GraphPane 
			// (������� ����� �������� �� �������� zedGraph.GraphPane)
			// ������� ���� ������, ��� ��� ����� �� ����� ��������� ������� �������
			masterPane.PaneList.Clear ();

			// ������� ��� �������
			for (int i = 0; i < 9; i++)
			{
				// ������� ��������� ������ GraphPane, �������������� ����� ���� ������
				GraphPane pane = new GraphPane ();

				// ���������� ������� ������� �� ����������, 
				// ������� ������� ���������� ����� � ��������� ����� DrawSingleGraph()
				DrawSingleGraph (pane);

				// ������� ����� ������ � MasterPane
				masterPane.Add (pane);
			}

			// ����� ��������� ����������� ������� � MasterPane
			using (Graphics g = CreateGraphics ())
			{
				// ������� ����� ��������� � ��� ������.
				// � ������ ����� 4 �������,
				// �� ������ - 2
				// � ������ - 3
				// ���� �� ������ �������� ��� ����� false, �� ������ �� �������� ���������
				// ������� �� �� ���������� �������� � ������ ������,
				// � ���������� ����� � ������ �������
				masterPane.SetLayout (g, true, new int[] {4, 2, 3 } );
			}

			// ������� ��� � ���������� ������
			zedGraph.AxisChange ();
			zedGraph.Invalidate ();
		}

		private double f (double x)
		{
			if (x == 0)
			{
				return 1;
			}

			return Math.Sin (x) / x;
		}

		private void DrawSingleGraph (GraphPane pane)
		{
			pane.CurveList.Clear ();

			PointPairList list = new PointPairList ();

			double xmin = -40;
			double xmax = 40;

			for (double x = xmin; x <= xmax; x += 0.01)
			{
				list.Add (x, f (x));
			}

			LineItem myCurve = pane.AddCurve ("Sinc", list, Color.Blue, SymbolType.None);
		}
	}
}