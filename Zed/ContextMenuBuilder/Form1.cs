using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;


namespace ContextMenuBuilder
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			// !!! ���������� �� �������, ������� ����� ��������� ����� ���, 
			// ��� ����� �������� ����������� ����.
			zedGraph.ContextMenuBuilder += 
				new ZedGraphControl.ContextMenuBuilderEventHandler (zedGraph_ContextMenuBuilder);

			DrawGraph ();
		}


		/// <summary>
		/// ���������� �������, ������� ����������, ����� ������� ������������ ����
		/// </summary>
		/// <param name="sender">��������� ZedGraph</param>
		/// <param name="menuStrip">����������� ����, ������� ����� ��������</param>
		/// <param name="mousePt">���������� ������� ����</param>
		/// <param name="objState">��������� ������������ ����. ��������� ������, �� ������� ��������.</param>
		void zedGraph_ContextMenuBuilder (ZedGraphControl sender, 
			ContextMenuStrip menuStrip, 
			Point mousePt, 
			ZedGraphControl.ContextMenuObjectState objState)
		{
			// !!! 
			// ����������� (��������� �� ������� ����) ��������� ������ ������������ ����
			menuStrip.Items[0].Text = "����������";
			menuStrip.Items[1].Text = "��������� ��� ��������";
			menuStrip.Items[2].Text = "��������� ���������";
			menuStrip.Items[3].Text = "�������";
			menuStrip.Items[4].Text = "���������� �������� � �������";
			menuStrip.Items[7].Text = "���������� ������� �� ���������";

			// ��������� ������ ������
			menuStrip.Items.RemoveAt (5);
			menuStrip.Items.RemoveAt (5);

			// ������� ���� ����� ����
			ToolStripItem newMenuItem = new ToolStripMenuItem ("���� ����� ���� ������ �� ������");
			menuStrip.Items.Add (newMenuItem);
		}


		private double f (double x)
		{
			if (x == 0)
			{
				return 1;
			}

			return Math.Sin (x) / x;
		}


		private void DrawGraph ()
		{
			GraphPane pane = zedGraph.GraphPane;

			// ������� ������ ������
			pane.CurveList.Clear ();

			// �������� ������ �����
			PointPairList list = new PointPairList ();

			double xmin = -50;
			double xmax = 50;

			// ��������� ������ �����
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				// ������� � ������ �����
				list.Add (x, f (x));
			}

			// �������� ������
			LineItem myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.None);

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}