using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
namespace Assignment2
{
	public class ReadFiles
	{
		public string lineForTryParse;
        public List<string> id;
        public List<string> title;
        public List<string> author;
        public List<string> edision;
        public List<string> dueDate;
        public List<string> nimForStudent;
        public List<string> nimForBook;
        public List<string> name;
        public List<string> gender;
        public List<string> email;

        public static string bookFile = "book.txt";
        public static string studentFile = "student.txt";
		public ReadFiles()
		{
            id = new List<string>();
            title = new List<string>();
            author = new List<string>();
            edision = new List<string>();
            dueDate = new List<string>();
            nimForStudent = new List<string>();
            nimForBook = new List<string>();
            name = new List<string>();
            gender = new List<string>();
            email = new List<string>();

			// Read & Store data into list from the files
			readBookFile();
			readStudentFile();
		}

		public void readBookFile()
		{
			{
				using (StreamReader sr = new StreamReader(bookFile))
				{
					string Line;
					string pattern = @"\t+";
					Regex rgx = new Regex(pattern);
					while ((Line = sr.ReadLine()) != null)
					{
						string[]result = rgx.Split(Line);
                        id.Add(result[0].ToString());
                        title.Add(result[1].ToString());
                        author.Add(result[2].ToString());
                        edision.Add(result[3].ToString());
                        dueDate.Add(result[4].ToString());
                        nimForBook.Add(result[5].ToString());
                    }
				}
			}
		}

		public void readStudentFile()
		{
			using (StreamReader sr = new StreamReader(studentFile))
			{
				string LineStudent;
				string pattern = @"\t+";
				Regex rgx = new Regex(pattern);
				while ((LineStudent = sr.ReadLine()) != null)
				{
					string[] result = rgx.Split(LineStudent);
					nimForStudent.Add(result[0]);
					name.Add(result[1]);
					gender.Add(result[2]);
					email.Add(result[3]);
				}
			}
		}
	}
}
