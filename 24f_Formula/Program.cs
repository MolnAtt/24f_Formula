using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24f_Formula
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Formula p = new Formula("p");
			Formula q = new Formula("q");
			Formula r = new Formula("r");

			Formula f1 = p > (-q * r);
			Formula f2 = (p + q) * -(-p + r);
			Formula f3 = r == (p+-q);

			//HashSet<Dictionary<Formula, bool>> modellek = Formula.Modelljei(new HashSet<Formula> { f1, f2, f3 });
			HashSet<Dictionary<Formula, bool>> modellek = Formula.Modelljei(new HashSet<Formula> { f1, f2 });


			Formula.Modellek_kiirasa(modellek);

			return;

			//Popper<int> popper = new Popper();
			//Popper<int> ujpopper = 4 + popper;
			//Popper<int> vadiujpopper = (4,5) + popper;

			Formula A = new Formula("A");
			Formula B = new Formula("B");
			Formula C = new Formula("C");
			Formula D = new Formula("D");

			HashSet<Formula> formulahalmaz = new HashSet<Formula> { A, B, C, D };

			Formula.Kielégíthető(formulahalmaz);

			Formula aesb = A * B;

			// (A ∧ B ) →¬(C ∨ D)

			Formula f = (A * B) > -(A + D);


			Console.WriteLine(f);

			Console.WriteLine(f.Negaciok_szama());

			Console.WriteLine(string.Join(", ", f.Atomjai()));
			Console.WriteLine(string.Join(", ", f.Részformulái()));
			Console.WriteLine(string.Join(", ", f.Részformulái2()));


			Formula i = new Formula("I");
			Formula j = new Formula("J");
			Formula k = new Formula("K");
			Formula a = new Formula("A");
			Formula e = new Formula("E");
			Formula o = new Formula("Ó");

			Formula legfeljebb_egy_igaz = (-i * -j * -k) + (i * -j * -k) + (-i * j * -k) + (-i * -j * k);
			Formula valahol_van = (a * -e * -o) + (-a * e * -o) + (-a * -e * o);
			Formula elso = i == a;
			Formula masodik = j == -e;
			Formula harmadik = k == -a;

			Formula együtt = legfeljebb_egy_igaz * valahol_van * elso * masodik * harmadik;

			//együtt.Hogyan_lehet_igaz();



		}
	}
}
