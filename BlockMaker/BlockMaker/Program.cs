using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BlockMaker
{
	class Program
	{
		static void Main(string[] args)
		{
			// 一枚絵を何分割するか指定する.
			int row = int.Parse(Console.ReadLine());
			int col = int.Parse(Console.ReadLine());

			BlockMaker bm = new BlockMaker(row, col);
			bm.makeBlock(2, 3);
			for (int i = 0; i < row; i++)
			{
				for (int j = 0; j < col; j++)
				{
					//bm.makeBlock(i, j);
				}
			}
		}
	}
	class BlockMaker
	{
		public int row;
		public int col;
		public BlockMaker(int row, int col)
		{
			this.row = row;
			this.col = col;
		}

		public void makeBlock(int row, int col)
		{
			StreamReader cReader = (new System.IO.StreamReader(@"template.x", System.Text.Encoding.Default));
			String fName = row.ToString() + "_" + col.ToString() + ".x";
			StreamWriter sw = new System.IO.StreamWriter(fName, false, System.Text.Encoding.GetEncoding("shift_jis"));
			double rowSize = 1.0 / this.row;
			double colSize = 1.0 / this.col;

			// テンプレートを読み込む.
			while (cReader.Peek() > 0)
			{
				sw.WriteLine(cReader.ReadLine());
			}
			Console.WriteLine(this.col);
			Console.WriteLine(colSize);
			// 一枚絵の一部をブロックのテクスチャとして使用する.
			double[] V = {colSize * col,
						 colSize * (col+1),
						 rowSize * row,
						 rowSize * (row+1),
						 };

			sw.WriteLine(V[0].ToString() + ";" + V[2].ToString() + ";,");
			sw.WriteLine(V[1].ToString() + ";" + V[2].ToString() + ";,");
			sw.WriteLine(V[1].ToString() + ";" + V[3].ToString() + ";,");
			sw.WriteLine(V[0].ToString() + ";" + V[3].ToString() + ";;");
			sw.WriteLine("}}");
			sw.Close();

		} 
	}
}
