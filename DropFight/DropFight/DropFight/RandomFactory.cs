using System;

namespace DropFight
{
	/// <summary>
	/// シード値が異なるRandomクラスのインスタンスを作成するクラス
	/// 参照：http://www.atmarkit.co.jp/fdotnet/dotnettips/035random/random.html
	/// </summary>
	public class RandomFactory
	{
		private int seed;

		public RandomFactory(int seed)
		{
			this.seed = seed;
		}

		public RandomFactory()
			: this(Environment.TickCount)
		{
		}

		public Random CreateRandom()
		{
			return new Random(seed++);
		}
	}
}
