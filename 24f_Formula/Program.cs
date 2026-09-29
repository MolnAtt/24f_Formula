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
				if (this.gyerekei.Count == 0)
					return new HashSet<Formula> { this };

				HashSet<Formula> result = this.gyerekei[0].Atomjai();
				for (int i = 1; i < this.gyerekei.Count; i++)
					result.UnionWith(this.gyerekei[i].Atomjai());

				return result;
			}

			public int Mélység() // hány emelet magas a fa
			{
				if (this.gyerekei.Count == 0)
					return 0;

				int max = this.gyerekei[0].Mélység();

				for (int i = 1; i < this.gyerekei.Count; i++)
				{
					int n = this.gyerekei[i].Mélység();
					if (max < n)
					{
						max = n;
					}
				}

				return 1 + max;
			}

			public int Mélység2() => gyerekei.Count == 0 ? 0 : 1 + gyerekei.Max(x => x.Mélység2());

			public HashSet<Formula> Részformulái() // A fában található összes részformula halmaza
			{
				if (this.gyerekei.Count == 0)
					return new HashSet<Formula> { this };

				HashSet<Formula> result = this.gyerekei[0].Részformulái();
				for (int i = 1; i < this.gyerekei.Count; i++)
					result.UnionWith(this.gyerekei[i].Részformulái());

				result.Add(this);
				return result;
			}

			public HashSet<Formula> Részformulái2() // A fában található összes részformula halmaza
			{
				HashSet<Formula> result = new HashSet<Formula> { this };
				foreach (Formula gyerek in this.gyerekei)
					result.UnionWith(gyerek.Részformulái2());

				return result;
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

			együtt.Hogyan_lehet_igaz();



		}
	}
}
