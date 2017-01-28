using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace DateOrdinal
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

			// ��������� ���� ��� ���������� �����������
			DateTime startDate = DateTime.Now;

			// ���������� ����, ��� ���������� ������
			int count = 30;

			// ��� ���������� ������� ���� ����� ������������� � Double
			List<double> xvalues = new List<double> ();

			// ������ ���������
			List<double> yvalues = new List<double> ();

			// �������� ������
			for (int i = 0; i < count; i++)
			{
				// ���� ��� ���������� ��������
				DateTime currentDate = startDate.AddDays (i);

				// ��� �������� ���� �������� ��������� �� �����
				if (currentDate.DayOfWeek != DayOfWeek.Saturday &&
					currentDate.DayOfWeek != DayOfWeek.Sunday)
				{
					// �������� �� ��� X (������� ����)
					xvalues.Add (new XDate (currentDate));

					// ������ ���������
					yvalues.Add (rnd.NextDouble ());
				}
			}

			// �������� �����������
			BarItem bar = pane.AddBar ("", xvalues.ToArray(), yvalues.ToArray(), Color.Blue);

			// !!! ��� ��� X ��������� ��� AxisType.DateAsOrdinal,
			// ������� ����������, ��� ���������� ���� ������������� ����������
			pane.XAxis.Type = AxisType.DateAsOrdinal;

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}