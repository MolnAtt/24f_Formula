using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24f_Formula
{
	internal class Program
	{
		class Formula
		{
			string muvelet;
			List<Formula> gyerekei;

			public Formula(string muvelet, List<Formula> gyerekei)
			{
				this.muvelet = muvelet;
				this.gyerekei = gyerekei;
			}

			public Formula(string muvelet)
			{
				this.muvelet = muvelet;
				this.gyerekei = new List<Formula>();
			}

			public static Formula operator -(Formula A)
				=> new Formula("-", new List<Formula> { A});
			public static Formula operator *(Formula A, Formula B)
				=> new Formula("&", new List<Formula> { A, B });

			// C++-ban valami ilyesmi van: A*B valójában A.*(B) és nem static!
			// public Formula operator *(Formula B) => new Formula("&", new List<Formula> { this, B });

			public static Formula operator +(Formula A, Formula B)
				=> new Formula("V", new List<Formula> { A, B });
			public static Formula operator >(Formula A, Formula B)
				=> new Formula("->", new List<Formula> { A, B });
			public static Formula operator <(Formula A, Formula B)
				=> new Formula("<-", new List<Formula> { B, A });
			public static Formula operator ==(Formula A, Formula B)
				=> new Formula("<->", new List<Formula> { B, A });
			public static Formula operator !=(Formula A, Formula B)
				=> new Formula("><", new List<Formula> { B, A });

			public override string ToString()
			{
				if (this.gyerekei.Count == 0) // ha ez egy atom, akkor kiírjuk a "műveletet", ami most "A", "B", "C", "D", ...
					return this.muvelet;

				if (this.gyerekei.Count == 1) // Ha 1-argumentumú a művelet, akkor elé írjuk! (prefix)
					return this.muvelet + this.gyerekei[0].ToString();

				if (this.gyerekei.Count == 2) // Ha 2-argumentumú a művelet, akkor közé írjuk és ZÁRÓJELEZÜNK!
					return $"({this.gyerekei[0].ToString()} {this.muvelet} {this.gyerekei[1].ToString()})";
				
				// Ha több-argumentumú a művelet, akkor leírjuk a műveleti jelet, zárójelet nyitunk, és felsoroljuk az argumentumokat vesszővel elválasztva
				return $"{this.muvelet}({string.Join(", ", this.gyerekei)})";
			}

			public int Negaciok_szama()
			{
				if (this.gyerekei.Count == 0)
					return 0;

				int db = 0;
				foreach (Formula gyerek in this.gyerekei)
					db += gyerek.Negaciok_szama();

				return this.muvelet == "-" ? 1 + db : db;
			}
			public int Levelek_száma() // Hány különböző levélre bomlik a fa?
			{
				if (this.gyerekei.Count == 0)
					return 1;

				int db = 0;
				foreach (Formula gyerek in gyerekei)
				{
					db += gyerek.Levelek_száma();
				}
				return db;
			}

			//public int Levelek_száma() => this.gyerekei.Count == 0 ? 1 : gyerekei.Sum(x => x.Levelek_száma());

			public HashSet<Formula> Atomjai() // Hány különböző "betűből" épül fel! (ne legyen duplázódás)
			{

			}

			public int Mélység() // hány emelet magas a fa
			{

			}


			public HashSet<Formula> Részformulái() // A fában található összes részformula halmaza
			{

			}





		}




		static void Main(string[] args)
		{




			Formula A = new Formula("A");
			Formula B = new Formula("B");
			Formula C = new Formula("C");
			Formula D = new Formula("D");

			Formula aesb = A * B;

			// (A ∧ B ) →¬(C ∨ D)

			Formula f = (A * B) > -(C + D);


			Console.WriteLine(f);

			Console.WriteLine(f.Negaciok_szama());

		}
	}
}
