using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace FontSize
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
			// ������� ������ ��� ���������
			GraphPane pane = zedGraph.GraphPane;

			// ������� ������� ��� ������ ��������� �������
			int labelsXfontSize = 25;
			int labelsYfontSize = 20;

			int titleXFontSize = 25;
			int titleYFontSize = 20;

			int legendFontSize = 15;

			int mainTitleFontSize = 30;

			// ��������� ������� ������� ��� ����� ����� ����
			pane.XAxis.Scale.FontSpec.Size = labelsXfontSize;
			pane.YAxis.Scale.FontSpec.Size = labelsYfontSize;

			// ��������� ������� ������� ��� �������� �� ����
			pane.XAxis.Title.FontSpec.Size = titleXFontSize;
			pane.YAxis.Title.FontSpec.Size = titleYFontSize;

			// ��������� ������� ������ ��� �������
			pane.Legend.FontSpec.Size = legendFontSize;

			// ��������� ������� ������ ��� ������ ���������
			pane.Title.FontSpec.Size = mainTitleFontSize;
			pane.Title.FontSpec.IsUnderline = true;


			// ������� ������ ������ �� ��� ������, ���� �� ����� ������� ��� ���� ����������
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

			// �������� ������ � ��������� "Sinc", 
			// ������� ����� ���������� ������� ������ (Color.Blue),
			// ������� ����� ���������� �� ����� (SymbolType.None)
			LineItem myCurve = pane.AddCurve ("Sinc", list, Color.Blue, SymbolType.None);

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			// � ��������� ������ �� ������� ����� �������� ������ ����� �������, 
			// ������� ��������� � ��������� �� ����, ������������� �� ���������
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}