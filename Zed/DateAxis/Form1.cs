using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace DateAxis
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

			// �������� ������ �����
			PointPairList list = new PointPairList ();

			// ������ ���� �� ��� X
			DateTime startDate = new DateTime (2011, 02, 25);

			// ���������� �������� �� ��� X (���������� ����)
			int daysCount = 20;

			Random rnd = new Random();

			// ��������� ������ �����
			for (int i = 0; i < daysCount; i++)
			{
				// ������� ����� �� ��� X. ���� ������ - ���� ����
				DateTime currentDate = startDate.AddDays (i);

				// ������� �������� �� ��� Y
				double yValue = rnd.NextDouble () * 0.2 + 0.9;

				// ���������� ����� XDate ��� �������������� DateTime � ���� Double.
				// ������������ ������� ���������� � ���� Double.
				// ������ ���� �������� � ���� ������� �����.
				// ������ DateTime ����� ���� �� ����� ������������ ��� XDate
				list.Add (new XDate (currentDate), yValue);
			}

			// ������� ������,
			// ������� ����� ���������� ������� ������ (Color.Blue),
			// ������� ����� ���������� �� ����� (SymbolType.None)
			// �������� ������ � �������������� ��� ����� �� ���������� �� �������� ������ ������
			LineItem myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.None);

			// ���������, ��� �� ��� X � ��� ������������� ����,
			pane.XAxis.Type = AxisType.Date;

			// ��� ����������� ��������� ������� �� ��� Y
			pane.YAxis.Scale.Min = 0;
			pane.YAxis.Scale.Max = 1.4;

			// ������ ��������� �������� �� ���, ��� ������������ ����
			pane.XAxis.Scale.Min = new XDate (2011, 02, 20);
			pane.XAxis.Scale.Max = new XDate (2011, 03, 20);

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}
