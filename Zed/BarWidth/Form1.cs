using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace BarWidth
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

			int itemscount = 19;

			Random rnd = new Random ();

			// ������ ���������
			double[] values = new double[itemscount];

			// �������� ������
			for (int i = 0; i < itemscount; i++)
			{
				values[i] = rnd.NextDouble ();
			}

			// �������� ������-�����������
			// ������ �������� - �������� ������ ��� �������
			// ������ �������� - �������� ��� ��� X
			// ������ �������� - �������� ��� ��� Y
			// ��������� �������� - ����
			BarItem bar = pane.AddBar ("�����������", null, values, Color.Blue);

			// !!! ���������� ����� ���������� (�������� ���������) ����������� = 0.0
			// � ��� � �������� ������ ���� �������.
			pane.BarSettings.MinClusterGap = 0.0f;

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}