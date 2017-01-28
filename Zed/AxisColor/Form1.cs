using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;


namespace AxisColor
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
			if (x == 0)
			{
				return 1;
			}

			return Math.Sin (x) / x;
		}


		private void DrawGraph ()
		{
			GraphPane pane = zedGraph.GraphPane;

			// ��������� ������ ���������� � ������ SetColors()
			SetColors (pane);			

			// ������� ������ ������
			pane.CurveList.Clear ();

			// �������� ������ �����
			PointPairList list = new PointPairList ();

			double xmin = -50;
			double xmax = 50;

			// ��������� ������ �����
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				// ������� � ������ �����
				list.Add (x, f (x));
			}

			// �������� ������
			LineItem myCurve = pane.AddCurve ("", list, Color.Yellow, SymbolType.None);

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}


		private static void SetColors (GraphPane pane)
		{
			// !!!
			// ��������� ���� ����� ��� ����� ����������
			pane.Border.Color = Color.Red;

			// ��������� ���� ����� ������ �������
			pane.Chart.Border.Color = Color.Green;

			// �������� ��� ����� ���������� ZedGraph
			// ������� ����� ��������
			pane.Fill.Type = FillType.Solid;
			pane.Fill.Color = Color.Silver;

			// �������� ������� ������� (��� ���) � ������ ����
			pane.Chart.Fill.Type = FillType.Solid;
			pane.Chart.Fill.Color = Color.Black;

			// ������� ����� ��� �� ������ X = 0 � Y = 0, ����� ������ ���� ����
			pane.XAxis.MajorGrid.IsZeroLine = true;
			pane.YAxis.MajorGrid.IsZeroLine = true;
			// ��������� ���� ����
			pane.XAxis.Color = Color.Gray;
			pane.YAxis.Color = Color.Gray;

			// ������� �����
			pane.XAxis.MajorGrid.IsVisible = true;
			pane.YAxis.MajorGrid.IsVisible = true;
			// ��������� ���� ��� �����
			pane.XAxis.MajorGrid.Color = Color.Cyan;
			pane.YAxis.MajorGrid.Color = Color.Cyan;

			// ��������� ���� ��� �������� ����� � �����
			pane.XAxis.Title.FontSpec.FontColor = Color.White;
			pane.YAxis.Title.FontSpec.FontColor = Color.White;

			// ��������� ���� �������� ��� �������
			pane.XAxis.Scale.FontSpec.FontColor = Color.GreenYellow;
			pane.YAxis.Scale.FontSpec.FontColor = Color.GreenYellow;

			// ��������� ���� ��������� ��� ��������
			pane.Title.FontSpec.FontColor = Color.Khaki;
		}
	}
}