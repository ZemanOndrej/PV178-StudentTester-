using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL
{
	class Program
	{
		static void Main(string[] args)
		{

			Console.Write("Novy student: ");
			string newStudent = Console.ReadLine();

			using (var db = new AppDbContext())
			{
				

				Console.ReadKey();
			}
		}
	}
}
