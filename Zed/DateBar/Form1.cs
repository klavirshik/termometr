using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace DateBar
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

			Random rnd = new Random (100);

			// ����, ������� ����� ��������������� ��������
			XDate[] dates = new XDate[] { new XDate (2011, 02, 25), new XDate (2011, 02, 26),
				new XDate (2011, 02, 27), new XDate (2011, 03, 01), new XDate (2011, 03, 02),
				new XDate (2011, 03, 04), new XDate (2011, 03, 06)};

			// ��� ���������� ������� ���� ����� ������������� � Double
			double[] xvalues = new double[dates.Length];

			// ������ ���������
			double[] yvalues = new double[dates.Length];

			// �������� ������
			for (int i = 0; i < dates.Length; i++)
			{
				// �������� �� ��� X
				xvalues[i] = dates[i];

				// ������ ���������
				yvalues[i] = rnd.NextDouble ();
			}

			// �������� ������-�����������
			BarItem curve = pane.AddBar ("", xvalues, yvalues, Color.Blue);

			// !!! ��� ��� X ��������� ����������� ���
			pane.XAxis.Type = AxisType.Date;

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}