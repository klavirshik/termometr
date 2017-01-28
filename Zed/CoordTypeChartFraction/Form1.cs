using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace CoordTypeChartFraction
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


		/// <summary>
		/// !!! ���������� ����������� �������
		/// </summary>
		/// <param name="pane"></param>
		private void DrawObjects (GraphPane pane)
		{
			// �������� ��������� ������, ���������� �������� ��������
			// �� � ����������� �������, � � ����������� ������������ �������
			TextObj text = new TextObj ("���� ����� ������ ��������� � ����",
				0.0, 1,                      // ���������� ���������� �������
				CoordType.ChartFraction,     // ���������� �������� ������������ �������
				AlignH.Left,                 // ���������� �� X ������ ��������� ����� ������� �������
				AlignV.Bottom);              // ���������� �� Y ������ ��������� ������ ������� �������

			// ������� ����� � ������ ����������� ��������
			pane.GraphObjList.Add (text);

			// �������� �����������, ������� ������ ����� ���������� �������
			
			// �������� �������������� �����
			LineObj cross_hor = new LineObj (Color.Black, 0.48, 0.5, 0.52, 0.5);
			// �� ���������� �������������� ������������ �������
			cross_hor.Location.CoordinateFrame = CoordType.ChartFraction;
			// ���������� ������ ����������� ����� � �� �����������, � �� ���������
			cross_hor.Location.AlignH = AlignH.Center;
			cross_hor.Location.AlignV = AlignV.Center;

			// ���������� ������� ������������ ����� �����������
			LineObj cross_ver = new LineObj (Color.Black, 0.5, 0.48, 0.5, 0.52);
			cross_ver.Location.CoordinateFrame = CoordType.ChartFraction;
			cross_ver.Location.AlignH = AlignH.Center;
			cross_ver.Location.AlignV = AlignV.Center;

			// ������� ������ ����������� � ������ ����������� ��������
			pane.GraphObjList.Add (cross_hor);
			pane.GraphObjList.Add (cross_ver);
		}


		private void DrawGraph ()
		{
			GraphPane pane = zedGraph.GraphPane;

			// �������� ����������� �������
			DrawObjects (pane);

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
			LineItem myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.None);

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}