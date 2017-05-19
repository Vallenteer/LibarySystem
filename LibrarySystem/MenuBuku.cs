using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace Assignment2
{
	public class MenuBuku : ReadFiles
	{
		public static string fileName;
		public MenuBuku()
		{
			fileName = "book.txt";
			bool inBookMenu = true;
			while (inBookMenu)
			{
				Console.WriteLine("1 - Tampilkan hanya buku\n2 - Tampilkan buku yang bisa dipinjam\n3 - Tampilkan buku dengan judul tertentu\n4 - Tampilkan buku dengan pengarang tertentu\n5 - Kembali ke main menu");
				int bookMenu;
				lineForTryParse = Console.ReadLine();
				if (!int.TryParse(lineForTryParse, out bookMenu))
				{
					Console.Clear();
					Console.WriteLine("{0} bukan angka, coba lagi.", lineForTryParse);
				}
				else if (bookMenu == 1)
				{
					Console.Clear();
					readAll();

				}
				else if (bookMenu == 2)
				{
					Console.Clear();
					borrowableBooks();

				}

				else if (bookMenu == 3)
				{
					Console.Clear();
					searchByTitle();
				}
				else if (bookMenu == 4)
				{
					Console.Clear();
					searchByAuthor();

				}
				else if (bookMenu == 5)
				{
					inBookMenu = false;
					Console.Clear();
				}
				else
				{
					Console.Clear();
				}
			}
		}
		public void readAll()
		{
			int i = 0;
			while (i < author.Count)
			{
				Console.WriteLine("ID:{0}\nJudul Buku:{1}\nPengarang:{2}\nEdisi:{3}\nTanggal Kembali:{4}\nNIM Peminjam:{5}", id[i], title[i], author[i], edision[i], dueDate[i], nimForBook[i]);
				Console.WriteLine("============================================================================\n");
				i++;
			}
		}

		public void borrowableBooks()
		{
			for (int i = 0; i < title.Count; i++)
			{
				if ((id[i] == "-") || (dueDate[i] == "-"))
				{
					Console.WriteLine("ID:{0}\nJudul Buku:{1}\nPengarang:{2}\nEdisi:{3}\nTanggal Kembali:{4}\nNIM Peminjam:{5}", id[i], title[i], author[i], edision[i], dueDate[i], nimForBook[i]);
					Console.WriteLine("============================================================================\n");
				}
			}
		}

		public void searchByTitle()
		{
			int i = 0;
			Console.Write("Masukan judul buku: ");
			string titleInput = Console.ReadLine();
			while (i < id.Count)
			{
				if (title[i].Contains(titleInput))
				{
					Console.WriteLine("ID:{0}\nJudul Buku:{1}\nPengarang:{2}\nEdisi:{3}\nTanggal Kembali:{4}\nNIM Peminjam:{5}", id[i], title[i], author[i], edision[i], dueDate[i], nimForBook[i]);
					Console.WriteLine("==============================================================================\n");
				}
				else if (i == (id.Count - 1))
				{
					Console.WriteLine("Buku dengan judul {0} tidak ada", titleInput);
				}
				i++;
			}
		}

		public void searchByAuthor()
		{
			int i = 0;
			Console.Write("Masukan judul buku: ");
			string authorInput = Console.ReadLine();
			while (i < id.Count)
			{
				if (author[i] == authorInput)
				{
					Console.WriteLine("ID:{0}\nJudul Buku:{1}\nPengarang:{2}\nEdisi:{3}\nTanggal Kembali:{4}\nNIM Peminjam:{5}", id[i], title[i], author[i], edision[i], dueDate[i], nimForBook[i]);
					Console.WriteLine("================================================================================\n");
					break;
				}
				else if (i == (id.Count - 1))
				{
					Console.WriteLine("Buku dengan pengarang {0} tidak ada", authorInput);
					break;
				}
				i++;
			}
		}
	}
}
