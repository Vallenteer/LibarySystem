using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
namespace Assignment2
{
	public class MenuMurid : ReadFiles
	{
		public int nimInput;
		public string nameInput, emailInput;
		public char genderInput;
		public MenuMurid()
		{
			bool inStudentMenu = true;
			while (inStudentMenu)
			{
				Console.WriteLine("1 - Tampilkan semua murid\n2 - Tampilkan murid laki-laki\n3 - Tampilkan murid wanita\n4 - Tambah murid\n5 - Kembali ke main menu");
				int studentMenu;
				lineForTryParse = Console.ReadLine();
				if (!int.TryParse(lineForTryParse, out studentMenu))
				{
					Console.Clear();
					Console.WriteLine("{0} bukan angka, coba lagi.", lineForTryParse);
				}
				else if (studentMenu == 1)
				{
					Console.Clear();
					ReadAllStudents();
				}
				else if (studentMenu == 2)
				{
					Console.Clear();
					ReadMaleStudent();
				}
				else if (studentMenu == 3)
				{
					Console.Clear();
					ReadFemaleStudent();
				}
				else if (studentMenu == 4)
				{
					Console.Clear();
					AddStudent();

				}
				else if (studentMenu == 5)
				{
					inStudentMenu = false;
					Console.Clear();
				}
			}
		}

		public void ReadAllStudents()
		{
			int i = 0;
			while (i < nimForStudent.Count)
			{
				Console.WriteLine("NIM : {0}\nName : {1}\nJenis Kelamin : {2}\nEmail : {3}", nimForStudent[i], name[i], gender[i], email[i]);
				Console.WriteLine("=======================================================\n");
				i++;
			}
		}

		public void ReadMaleStudent()
		{
			int i = 0;
			while (i < name.Count)
			{
				if (gender[i] == "L")
				{
					Console.WriteLine("NIM : {0}\nName : {1}\nJenis Kelamin : {2}\nEmail : {3}", nimForStudent[i], name[i], gender[i], email[i]);
					Console.WriteLine("=======================================================\n");
				}
				i++;
			}
		}

		public void ReadFemaleStudent()
		{
			ReadFiles ob = new ReadFiles();
			int i = 0;
			while (i < name.Count)
			{
				if (gender[i] == "P")
				{
					Console.WriteLine("NIM : {0}\nName : {1}\nJenis Kelamin : {2}\nEmail : {3}", nimForStudent[i], name[i], gender[i], email[i]);
					Console.WriteLine("=======================================================\n");
				}
				i++;
			}
		}

		public void getOtherData()
		{
			Console.Write("Masukan Nama: ");
			nameInput = Console.ReadLine();
			name.Add(nameInput);
			bool falseInput = true;
			while (falseInput)
			{
				Console.Write("Masukan Jenis Kelamin: ");
				genderInput = char.Parse(Console.ReadLine());
				if ((genderInput == 'p') || (genderInput == 'P') || (genderInput == 'l') || (genderInput == 'L'))
				{
					gender.Add(genderInput.ToString());
					break;
				}
				else
				{
					Console.WriteLine("Masukan P untuk perempuan & L untuk laki-laki");
				}
			}

			while (falseInput)
			{
				Console.Write("Masukan Email: ");
				emailInput = Console.ReadLine();
				string emailPattern = "(\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*)";
				Regex rgxEmail = new Regex(emailPattern);
				if (rgxEmail.IsMatch(emailInput))
				{
					Console.WriteLine("Menyimpan Data..................................");
					using (FileStream fs = new FileStream(studentFile, FileMode.Append, FileAccess.Write))
					using (StreamWriter sw = new StreamWriter(fs))
					{
						Console.Clear();
						sw.WriteLine("{0}\t{1}\t{2}\t{3}", nimInput, nameInput, genderInput, emailInput);
						Console.WriteLine("Data Tersimpan.");
						email.Add(emailInput);
						break;
					}
				}
				else
				{
					Console.WriteLine("Input tidak valid, masukan email dalam format [username@domain.com]");
				}
			}
		}


		public void getNim()
		{
			Console.Write("Masukan NIM: ");
			lineForTryParse = Console.ReadLine();
			if (int.TryParse(lineForTryParse, out nimInput))
			{
				for (int i = 0; i < nimForStudent.Count; i++)
				{
					if (nimInput.ToString() == nimForStudent[i])
					{
						Console.WriteLine("NIM sudah terpakai.");
						getNim();
						break;
					}
				}
				if (nimInput.ToString().Length < 6)
				{
					Console.WriteLine("Input tidak valid, masukan NIM 6 digit.");
					getNim();
					return;
				}
				else
				{
					nimForStudent.Add(nimInput.ToString());
					return;
				}

			}
			else
			{
				Console.WriteLine("{0} bukan angka, coba lagi.", lineForTryParse);
				getNim();
			}
		}


		public void AddStudent()
		{
			bool inAddMenu = true;
			while (inAddMenu)
			{
				Console.WriteLine("1 - Masukan data\n2 - Kembali ke menu murid");
				int addMenu;
				lineForTryParse = Console.ReadLine();
				if (!int.TryParse(lineForTryParse, out addMenu))
				{
					Console.WriteLine("{0} bukan angka, coba lagi.", lineForTryParse);
				}
				else if (addMenu == 2)
				{
					Console.Clear();
					inAddMenu = false;
				}
				else if (addMenu == 1)
				{
					// call getNim function here
					Console.Clear();
					getNim();
					getOtherData();
				}

				else

				{
					Console.Clear();
					Console.WriteLine("Input tidak valid.");

				}
			}
		}
	}
}
