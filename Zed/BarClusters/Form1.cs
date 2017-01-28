using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace BarClusters
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
			// ������� ������ ��� ���������
			GraphPane pane = zedGraph.GraphPane;

			// ������� ������ ������ �� ��� ������, ���� �� ����� ������� ��� ���� ����������
			pane.CurveList.Clear ();

			int itemscount = 5;

			Random rnd = new Random ();

			// ������ ���������
			double[] YValues1 = new double[itemscount];
			double[] YValues2 = new double[itemscount];
			double[] YValues3 = new double[itemscount];

			double[] XValues = new double[itemscount];

			// �������� ������
			for (int i = 0; i < itemscount; i++)
			{
				XValues[i] = i + 1;

				YValues1[i] = rnd.NextDouble ();
				YValues2[i] = rnd.NextDouble ();
				YValues3[i] = rnd.NextDouble ();
			}

			// �������� ��� �����������
			// ��� ��� ��� ���� ���������� �� �������� ���������� ������� ��������� �� X,
			// �� �������� ����� �������������� � �������� � ���� ������.
			BarItem bar1 = pane.AddBar ("Values1", XValues, YValues1, Color.Blue);
			BarItem bar2 = pane.AddBar ("Values2", XValues, YValues2, Color.Red);
			BarItem bar3 = pane.AddBar ("Values3", XValues, YValues3, Color.Yellow);

			// !!! ���������� ����� ���������� � �������� (�������� ���������)
			pane.BarSettings.MinBarGap = 0.0f;

			// !!! �������� ���������� ����� ���������� � 2.5 ����
			pane.BarSettings.MinClusterGap = 2.5f;
			

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}