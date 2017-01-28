using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;


namespace FilteredList
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			DrawCurves ();
		}

		/// <summary>
		/// ��������� ���� ������
		/// </summary>
		private void DrawCurves ()
		{
			// ���������� ������� ������� ������ �� ���� X � Y
			double xmin = -20;
			double xmax = 20;

			// ���������� ����� � ������ �������
			int count = 10000;

			// ������� � ������������ ����� �� X � Y
			double[] xlist = new double[count];
			double[] ylist = new double[count];

			// ��� �������������
			double dx = (xmax - xmin) / count;

			// �������� ������ �� �������� ���������� ���������
			for (int i = 0; i < count; i++)
			{
				double currx = xmin + i * dx;

				xlist[i] = currx;
				ylist[i] = f(currx);
			}

			// ������� �������� ������ �� ����������� ��������
			PointPairList fullList = new PointPairList (xlist, ylist);
			DrawGraph (fullList, Color.Red);

			// ������ ������������� ������� FilteredPointList, 
			// ����� ��������� ���������� ������������ �����.
			// � ����������� ������ ���������� ����������� �������
			FilteredPointList filteredList = new FilteredPointList (xlist, ylist);

			// ��������� ���������� �����
			// ��� ���������� ������ �������� �� -15 �� 15
			double filteredXMin = -15;
			double filteredXMax = 15;

			// ��� ���������� 20-�� �����
			int filteredCount = 20;

			// ��������� ��������� ����������
			filteredList.SetBounds (filteredXMin, filteredXMax, filteredCount);

			// �������� ������ �� ��������������� ������
			DrawGraph (filteredList, Color.Blue);
		}


		/// <summary>
		/// ��������� ����� ������
		/// </summary>
		/// <param name="points">����� ��� ������</param>
		/// <param name="color">���� ��� ������</param>
		private void DrawGraph (IPointList points, Color color)
		{
			// ������� ������ ��� ���������
			GraphPane pane = zedGraph.GraphPane;

			// �������� ������, � �������� ������� ���������� 
			// ���������� ���������� ������������ �����
			pane.AddCurve (points.Count.ToString() + " �����", 
				points, 
				color, 
				SymbolType.None);

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			zedGraph.AxisChange ();

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
	}
}