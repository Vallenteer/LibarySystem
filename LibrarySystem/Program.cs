using System;

namespace Assignment2
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			bool mainRun = true;
			while (mainRun)
			{
				Console.WriteLine("1 - Menu Murid\n2 - Menu Buku\n3 - Menu Peminjaman\n4 - Exit");
				int mainMenu;
				string lineForTryParse = Console.ReadLine();
				if (!int.TryParse(lineForTryParse, out mainMenu))
				{
					Console.Clear();
					Console.WriteLine("{0} bukan angka, coba lagi.", lineForTryParse);
				}
				else if (mainMenu == 1)
				{
					Console.Clear();
					MenuMurid ob1 = new MenuMurid();
				}
				else if (mainMenu == 2)
				{
					Console.Clear();
					MenuBuku ob2 = new MenuBuku();
				}
				else if (mainMenu == 3)
				{
					Console.Clear();
					MenuPeminjaman ob3 = new MenuPeminjaman();
				}
				else if (mainMenu == 4)
				{
					Console.Clear();
					Environment.Exit(0);
				}
				else
				{
					Console.Clear();
					Console.WriteLine("Wrong Input");
				}
			}
		}
	}
}
