using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace BarView
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

			// ������� ������ ������
			pane.CurveList.Clear ();

			// ���������� �������� � �����������
			int itemscount = 5;

			Random rnd = new Random ();

			// ������ ��������
			double[] values = new double[itemscount];

			// �������� ������
			for (int i = 0; i < itemscount; i++)
			{
				values[i] = rnd.NextDouble ();
			}

			// �������� ������-�����������
			BarItem curve = pane.AddBar ("�����������", null, values, Color.Blue);

			// !!!
			// ��������� ���� ��� �������� �����������
			curve.Bar.Fill.Color = Color.YellowGreen;

			// �������� ����������� �������
			curve.Bar.Fill.Type = FillType.Solid;

			// ������� ������� �������� ����������
			curve.Bar.Border.IsVisible = false;

			// �������� ������ �� ����
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}