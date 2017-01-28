using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace MoveAxis
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			DrawGraph ();
		}


		private double f (double x)
		{
			return -x * x + 30;
		}


		private void DrawGraph ()
		{
			GraphPane pane = zedGraph.GraphPane;

			// ������� ������ ������
			pane.CurveList.Clear ();

			// �������� ������ �����
			PointPairList list = new PointPairList ();

			double xmin = -10;
			double xmax = 10;

			// ��������� ������ �����
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				// ������� � ������ �����
				list.Add (x, f (x));
			}

			// �������� ������
			LineItem myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.None);


			// !!!
			// ��� X ����� ������������ � ���� Y �� ������ Y = 0
			pane.XAxis.Cross = 0.0;

			// ��� Y ����� ������������ � ���� X �� ������ X = 0
			pane.YAxis.Cross = 0.0;

			// �������� ����������� ������ � ��������� ����� �� ����
			pane.XAxis.Scale.IsSkipFirstLabel = true;
			pane.XAxis.Scale.IsSkipLastLabel = true;

			// �������� ����������� ����� � ����� ����������� � ������ ����
			pane.XAxis.Scale.IsSkipCrossLabel = true;


			// �������� ����������� ������ � ��������� ����� �� ����
			pane.YAxis.Scale.IsSkipFirstLabel = true;

			// �������� ����������� ����� � ����� ����������� � ������ ����
			pane.YAxis.Scale.IsSkipLastLabel = true;
			pane.YAxis.Scale.IsSkipCrossLabel = true;

			// ������� ��������� ����
			pane.XAxis.Title.IsVisible = false;
			pane.YAxis.Title.IsVisible = false;

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			zedGraph.AxisChange ();


			// ��������� ������
			zedGraph.Invalidate ();
		}
	}

}