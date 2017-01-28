using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace TextBackground
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
			GraphPane pane = zedGraph.GraphPane;

			DrawCurve (pane);


			// *** ������� ����� � ����� �� ��������� (� ����� �����) ***
			TextObj text1 = new TextObj ("Text 1", 0.0, 0.9);

			// �������� ����� ������ ������
			text1.FontSpec.Border.IsVisible = false;


			// *** ������� ����� � ���������� ����� ***
			TextObj text2 = new TextObj ("Text 2", 0.0, 0.8);

			// �������� ����� ������ ������
			text2.FontSpec.Border.IsVisible = false;

			// ���������� ����������� ������ Fill ��� ����������,
			// ����� ��� ��� ����������
			text2.FontSpec.Fill = new Fill ();


			// *** ������� ����� � ���������� ����� ***
			TextObj text3 = new TextObj ("Text 3", 0.0, 0.7);

			// �������� ����� ������ ������
			text3.FontSpec.Border.IsVisible = false;

			// ������������ ������ Fill �������� ��������� ������ Color
			text3.FontSpec.Fill = new Fill (Color.Yellow);


			// *** ������� ����� � ����������� ����� ***
			TextObj text4 = new TextObj ("Text 4", 0.0, 0.6);

			// �������� ����� ������ ������
			text4.FontSpec.Border.IsVisible = false;

			// ������������ ������ Fill �������� ��������� ������ Color
			text4.FontSpec.Fill = new Fill (Color.Yellow, Color.Red);
			

			// ������� ��������� ������� � ������ ������������ ��������
			pane.GraphObjList.Add (text1);
			pane.GraphObjList.Add (text2);
			pane.GraphObjList.Add (text3);
			pane.GraphObjList.Add (text4);

			// ��������� ������
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

		private void DrawCurve (GraphPane pane)
		{
			pane.CurveList.Clear ();

			PointPairList list = new PointPairList ();

			double xmin = -50;
			double xmax = 50;

			// ��������� ������ �����
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				list.Add (x, f (x));
			}
			LineItem myCurve = pane.AddCurve ("Sinc", list, Color.Blue, SymbolType.None);

			zedGraph.AxisChange ();
		}
	}
}