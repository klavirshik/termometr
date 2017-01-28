using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;


namespace BarOverlay
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
			// ������ ������������ ������ � ��������
			zedGraph.MasterPane.PaneList.Clear ();

			// �������� ��� ������ ��� �������, ��� ����� ������������
			// ���������� ������, �� � ������� ���������� BarType
			GraphPane pane1 = new GraphPane ();
			GraphPane pane2 = new GraphPane ();

			// ���������� ��������
			int itemscount = 9;
			Random rnd = new Random ();

			// ����������� ������ ��� ����� ��������
			double[] YValues1 = GenerateData (itemscount, rnd);
			double[] YValues2 = GenerateData (itemscount, rnd);
			double[] YValues3 = GenerateData (itemscount, rnd);

			double[] XValues = new double[itemscount];

			// �������� ������
			for (int i = 0; i < itemscount; i++)
			{
				XValues[i] = i + 1;
			}

			// �� ���������� ������ �������� ��� �����������
			CreateBars (pane1, XValues, YValues1, YValues2, YValues3);
			CreateBars (pane2, XValues, YValues1, YValues2, YValues3);

			// !!! � ������� ������� ������� ������������� ���� �� ������
			// ������ � ���������� ������������������:
			// ������� �����, ����� �������, ����� ������
			pane1.BarSettings.Type = BarType.Overlay;
			pane1.Title.Text = "BarType.Overlay";

			// !!! � ������� ������� ������� ��������� �������� �����, ����� ��� ��� ���� �����
			pane2.BarSettings.Type = BarType.SortedOverlay;
			pane2.Title.Text = "BarType.SortedOverlay";

			// ������� ��������� ������ � MasterPane
			zedGraph.MasterPane.Add (pane1);
			zedGraph.MasterPane.Add (pane2);

			// ������� ������������ ��������
			using (Graphics g = CreateGraphics ())
			{
				// ������� ����� ��������� � ���� ������� ���� ��� ������
				zedGraph.MasterPane.SetLayout (g, PaneLayout.SingleColumn);
			}

			// ������� ������ �� ����
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
		

		/// <summary>
		/// ������������� ��������� ������ ��� �������
		/// </summary>
		/// <param name="itemscount"></param>
		/// <param name="rnd"></param>
		/// <returns></returns>
		private double[] GenerateData (int itemscount, Random rnd)
		{
			double[] values = new double[itemscount];

			// �������� ������
			for (int i = 0; i < itemscount; i++)
			{
				values[i] = rnd.NextDouble ();
			}

			return values;
		}


		/// <summary>
		/// ������� �������� �� ������
		/// </summary>
		/// <param name="pane">������, ���� ����������� �������</param>
		/// <param name="XValues">���������� �� ��� X</param>
		/// <param name="YValues1">������ �� ��� Y ��� ������� ������ ��������</param>
		/// <param name="YValues2">������ �� ��� Y ��� ������� ������ ��������</param>
		/// <param name="YValues3">������ �� ��� Y ��� �������� ������ ��������</param>
		private static void CreateBars (GraphPane pane, 
			double[] XValues, 
			double[] YValues1, double[] YValues2, double[] YValues3)
		{
			pane.CurveList.Clear ();

			// �������� ��� �����������
			pane.AddBar ("", XValues, YValues1, Color.Blue);
			pane.AddBar ("", XValues, YValues2, Color.Red);
			pane.AddBar ("", XValues, YValues3, Color.Yellow);
		}
	}
}