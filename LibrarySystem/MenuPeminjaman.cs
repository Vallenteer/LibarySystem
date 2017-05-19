using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
namespace Assignment2
{
    public class MenuPeminjaman : ReadFiles
    {
        public bool toEditFile;
        public DateTime today = DateTime.Today;
        public int lineToEdit;
        public DateTime oDate;
        public int nimBorrow;
        public string edition;
        public int yesOrNo;
        public string titleBorrowInput;
        public MenuPeminjaman()
        {
            bool inBorrowMenu = true;
            while (inBorrowMenu)
            {
                Console.WriteLine("1 - Pinjam buku\n2 - Lihat buku yang overdue\n3 - Tagih buku yang overdue");
                Console.WriteLine("4 - Tagih mahasiswa tertentu\n5 - Kembali buku\n6 - Kembali ke main menu");
                int borrowMenu;
                lineForTryParse = Console.ReadLine();
                if (!int.TryParse(lineForTryParse, out borrowMenu))
                {
                    Console.Clear();
                    Console.WriteLine("{0} bukan angka, coba lagi", lineForTryParse);
                }
                else if (borrowMenu == 1)
                {
                    Console.Clear();
                    borrowBook();
                }
                else if (borrowMenu == 2)
                {
                    Console.Clear();
                    ShowOverdues();
                }
                else if (borrowMenu == 3)
                {
                    Console.Clear();
                    chargeOverduesByTitle();
                }
                else if (borrowMenu == 4)
                {
                    Console.Clear();
                    chargeOverduesByNim();
                }

                else if (borrowMenu == 5)
                {
					Console.Clear();
                    returnBook();
                }

                else if (borrowMenu == 6)
                {
                    Console.Clear();
                    inBorrowMenu = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Input tidak valid.");
                }

            }
        }

        public void borrowBook()
        {
            int five = 0;
            Console.Write("Masukan judul buku: ");
            titleBorrowInput = Console.ReadLine();
            if (title.Contains(titleBorrowInput))
            {
                Console.Write("Masukan edisi: ");
                edition = Console.ReadLine();
                for (int j = 0; j < nimForBook.Count; j++)
                {
                    if (titleBorrowInput == title[j])
                    {
                        if (edision[j] == edition)
                        {
                            if ((dueDate[j] == "-") && (nimForBook[j] == "-"))
                            {
                                Console.Write("Masukan NIM mahasiswa: ");
                                lineForTryParse = Console.ReadLine();
                                if (!int.TryParse(lineForTryParse, out nimBorrow))
                                {
                                    Console.Clear();
                                    Console.WriteLine("{0} bukan angka, coba lagi.", lineForTryParse);
                                }
                                else if (nimForStudent.Contains(nimBorrow.ToString()))
                                {
                                    for (int k = 0; k < nimForBook.Count; k++)
                                    {
                                        if (nimBorrow.ToString() == nimForBook[k])
                                        {
                                            five += 1;
                                            if (Convert.ToDateTime(dueDate[k]) <= today)
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Maaf, mahasiswa ini ada buku yang overdue belum di kembalikan.");
                                                return;
                                            }
                                            else if (five >= 5)
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Mahasiswa ini sudah meminjam 5 buku.");
                                                return;
                                            }
                                            else if (k == nimForBook.Count - 1)
                                            {
                                                Console.Clear();
                                                break;
                                            }
                                        }
                                    }
                                }
                                else{
                                    Console.WriteLine("Tidak ada mahasiswa dengan NIM "+nimBorrow);
                                }
                                    nimForBook[j] = nimBorrow.ToString();
                                    dueDate[j] = DateTime.Today.AddDays(21).ToString("MM/dd/yyy");
                                    using (StreamWriter sw = new StreamWriter(bookFile))
                                    {
                                        for (int l = 0; l < id.Count; l++)
                                        {
                                            sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", id[l], title[l], author[l], edision[l], dueDate[l], nimForBook[l]);
                                        }
                                    }
                                    Console.Clear();
                                    Console.WriteLine("Buku sudah di transaksi.");
                                    break;
                                }
                            else
                            {
                                Console.WriteLine("Buku tidak bisa di pinjam karena lagi di pinjam orang lain.");
                            }
                        }

                        else
                        {
                            Console.WriteLine("Tidak ada buku {0} dengan edisi {1}", titleBorrowInput, edition);
                        }
                    }
                }
            }

            else
            {
                Console.WriteLine("Tidak ada buku dengan judul " + titleBorrowInput);
            }
        }
              
                          
                       
        public void ShowOverdues()
        {
            for (int k = 0; k < dueDate.Count; k++)
            {
                if (dueDate[k] != "-")
                {
                    var convertedDate = Convert.ToDateTime(dueDate[k]).ToString("MM/dd/yyy");
                    DateTime convertedDate2 = Convert.ToDateTime(convertedDate);
                    if (convertedDate2 <= today)
                    {
                        Console.WriteLine("ID:{0}\nJudul Buku:{1}\nPengarang:{2}\nEdisi:{3}\nTanggal Kembali:{4}\nNIM Peminjam:{5}", id[k], title[k], author[k], edision[k], dueDate[k], nimForBook[k]);
                        Console.WriteLine("============================================================================\n");
                    }
                }
            }
        }

        public void chargeOverduesByTitle()
        {
            Console.Write("Masukan judul buku: ");
            string titleCharge = Console.ReadLine();
            for (int i = 0; i < title.Count; i++)
            {
                if (titleCharge == title[i])
                {
                    Console.Clear();
                    Console.WriteLine("Tagih buku {0}?\n1 - ya\n>1 - tidak", titleCharge);
                    lineForTryParse = Console.ReadLine();
                    if (!int.TryParse(lineForTryParse, out yesOrNo))
                    {
                        Console.Clear();
                        Console.WriteLine("{0} bukan angka, coba lagi.", lineForTryParse);
                    }
                    else if (yesOrNo == 1)
                    {
                        Console.Clear();
                        string oldValue = dueDate[i];
                        string oldValue2 = nimForBook[i];
                        dueDate[i] = "-";
                        nimForBook[i] = "-";
                        // write(replace) to book.txt
                        using (StreamWriter sw = new StreamWriter(bookFile))
                        {
                            for (int j = 0; j < id.Count; j++)
                            {
                                sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", id[j], title[j], author[j], edision[j], dueDate[j], nimForBook[j]);
                            }
                            Console.Clear();
                            Console.WriteLine("Buku sudah di kembalikan");
                            break;
                        }
                    }
                    else if (yesOrNo == 2)
                    {
                        Console.Clear();
                        break;
                    }
                }
                else if ((titleCharge != title[i]) && (i == title.Count - 1))
                {
                    Console.Clear();
                    Console.WriteLine("Tidak ada buku dengan judul {0}.", titleCharge);
                    break;
                }
            }
        }

        public void chargeOverduesByNim()
        {
            Console.Write("Masukan NIM mahasiswa: ");
            int nimCharge;
            lineForTryParse = Console.ReadLine();
            if (!int.TryParse(lineForTryParse, out nimCharge))
            {
                Console.Clear();
                Console.WriteLine("{0} bukan angka, coba lagi.", lineForTryParse);
            }
            else if ((!nimForBook.Contains(nimCharge.ToString())) && (nimForStudent.Contains(nimCharge.ToString())))
            {
                Console.Clear();
                Console.WriteLine("Mahasiswa ini tidak tercantum di data peminjaman");
            }
            else if (!nimForStudent.Contains(nimCharge.ToString()))
            {
                Console.Clear();
                Console.WriteLine("Tidak ada mahasiswa dengan NIM {0}" + nimCharge);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Tagih mahasiswa dengan NIM {0}?\n1 - ya\n>1 - tidak", nimCharge);
                lineForTryParse = Console.ReadLine();
                if (!int.TryParse(lineForTryParse, out yesOrNo))
                {
                    Console.Clear();
                    Console.WriteLine("{0} bukan angka, coba lagi.", lineForTryParse);
                }
                else if (yesOrNo == 1)
                {
                    for (int i = 0; i < dueDate.Count; i++)
                    {
                        if ((nimCharge.ToString() == nimForBook[i]) && (dueDate[i] != "-") && (nimForBook[i] != "-"))
                        {
                            dueDate[i] = "-";
                            nimForBook[i] = "-";
                        }
                    }
                    using (StreamWriter sw = new StreamWriter(bookFile))
                    {
                        for (int j = 0; j < id.Count; j++)
                        {
                            sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", id[j], title[j], author[j], edision[j], dueDate[j], nimForBook[j]);
                        }
                    }
                    Console.Clear();
                    Console.WriteLine("Buku sudah di kembalikan");
                }
                else if (yesOrNo > 1)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Input tidak valid.");
                }
            }
        }


        public void returnBook()
        {
            Console.Write("Masukan judul buku: ");
            string titleReturn = Console.ReadLine();
            if(title.Contains(titleReturn)){
                Console.Write("Masukan NIM mahasiswa: ");
                int nimReturn;
                lineForTryParse = Console.ReadLine();
                if (!int.TryParse(lineForTryParse, out nimReturn))
				{
					Console.Clear();
					Console.WriteLine("{0} bukan angka, coba lagi.", lineForTryParse);
				}
                else if(!nimForStudent.Contains(nimReturn.ToString())){
                    Console.Clear();
                    Console.WriteLine("Tidak ada mahasiswa dengan NIM "+nimReturn);
                }
                else{
                    for (int i = 0; i < nimForBook.Count;i++){
                        if((title[i]==titleReturn)&&(nimForBook[i]!=nimReturn.ToString())){
                            Console.WriteLine("Mahasiswa dengan NIM {0} tidak meminjam buku {1}",nimReturn,titleReturn);
                            return;
                        }
                    }
					Console.WriteLine("Buku sudah di kembalikan");
                    int index = title.IndexOf(titleReturn);
                    dueDate[index] = "-";
                    nimForBook[index] = "-";
					using (StreamWriter sw = new StreamWriter(bookFile))
					{
						for (int j = 0; j < id.Count; j++)
						{
							sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", id[j], title[j], author[j], edision[j], dueDate[j], nimForBook[j]);
						}
					}
                }
            }
            else{
                Console.Clear();
                Console.WriteLine("Tidak ada buku dengan judul "+titleReturn);
            }
        }
    }
}
