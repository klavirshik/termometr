using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace BarSimple
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

			int itemscount = 5;

			Random rnd = new Random ();

			// ������� ��� ����������
			string[] names = new string[itemscount];

			// ������ ���������
			double[] values = new double[itemscount];

			// �������� ������
			for (int i = 0; i < itemscount; i++)
			{
				names[i] = string.Format ("����� {0}", i);
				values[i] = rnd.NextDouble ();
			}

			// �������� ������-�����������
			// ������ �������� - �������� ������ ��� �������
			// ������ �������� - �������� ��� ��� X, �.�. � ��� �� ���� ��� ����� ���� �����, � ������� ������� ��� ��������� double[], �� ���� �������� null
			// ������ �������� - �������� ��� ��� Y
			// ��������� �������� - ����
			BarItem curve = pane.AddBar ("�����������", null, values, Color.Blue);

			// �������� ��� X ���, ����� ��� ���������� ��������� ������
			pane.XAxis.Type = AxisType.Text;

			// ������� ��� ��� ���� �������
			pane.XAxis.Scale.TextLabels = names;

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}