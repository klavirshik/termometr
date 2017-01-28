using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace BarHorizontal
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
			pane.CurveList.Clear ();

			// ���������� ��������
			int itemscount = 9;

			// ����������� ������ ��� ��� X (���� ��������)
			double[] barLength = GenerateData (itemscount);

			double[] barPosition = new double[itemscount];

			// �������� ������ �� ��� Y (��������� ��������)
			for (int i = 0; i < itemscount; i++)
			{
				barPosition[i] = i + 1;
			}

			// !!! �������� �����������. 
			// �������� �������� �� ������� ���������� ��������: 
			// ������� ���� ������ �� ��� X (����� �������), ����� �� ��� Y (��������� ��������)
			// ��� ������������ ���������� �������� �� ���� X � Y ����� ��������������� ��������.
			pane.AddBar ("", barLength, barPosition, Color.Blue);

			// ���� �������� ���������, ��� ������� ���� ��� ����������� ����� ��� Y,
			// �� ���� ��������� �������� ������������� ��������� �� ��� Y.
			pane.BarSettings.Base = BarBase.Y;

			// ������� ������ �� ����
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
		

		/// <summary>
		/// ������������� ��������� ������ ��� �������
		/// </summary>
		/// <param name="itemscount">���������� ��������</param>
		/// <returns></returns>
		private double[] GenerateData (int itemscount)
		{
			Random rnd = new Random ();

			double[] values = new double[itemscount];

			// �������� ������
			for (int i = 0; i < itemscount; i++)
			{
				values[i] = rnd.NextDouble ();
			}

			return values;
		}
	}
}