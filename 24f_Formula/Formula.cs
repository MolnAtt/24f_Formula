using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24f_Formula
{
	internal class Formula
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
			=> new Formula("-", new List<Formula> { A });
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

		/// <summary>
		/// Nem műveletekből áll össze, pl. p,q,r,...
		/// </summary>
		/// <returns></returns>
		public bool Atomi() => this.gyerekei.Count == 0;

		// literálok = atomok + tagadott atomok de ezen kívül semmi más!

		/// <summary>
		/// átirányít az atomi formulára: p,q,r,...
		/// </summary>
		/// <returns></returns>
		public bool Pozitív_literál() => Atomi();

		/// <summary>
		/// tagadott formula, amelynek egyetlen gyermeke egy atomi formula. pl.: -p, -q, -r
		/// </summary>
		/// <returns></returns>
		public bool Negatív_literál() => this.muvelet=="-" && this.gyerekei[0].Atomi();
		/// <summary>
		/// atomi formula vagy tagadott atomi formula, pl.: p, -q, r, -s,...
		/// </summary>
		/// <returns></returns>
		public bool Literál() => Pozitív_literál() || Negatív_literál();

		public static bool Kielégíthető(HashSet<Formula> formulahalmaz) => 0 < Modelljei(formulahalmaz).Count;
		public static bool Ellentmondás(HashSet<Formula> formulahalmaz) => !Kielégíthető(formulahalmaz);
		public bool Ellentmondás() => Ellentmondás(new HashSet<Formula> { this });
		public bool Logikai_igazság() => (-this).Ellentmondás();

		public static HashSet<Dictionary<Formula, bool>> Modelljei(HashSet<Formula> formulahalmaz)
		{
			Popper<Formula> formulák = new Popper<Formula>(formulahalmaz);
			HashSet<Formula> po = new HashSet<Formula>();
			HashSet<Formula> ne = new HashSet<Formula>();
			HashSet<Dictionary<Formula, bool>> modellek = new HashSet<Dictionary<Formula, bool>>();

			bool kielegitheto = AnFa(formulák, po, ne, modellek);

			// mire lefut az AnFa algoritmus, feltölti az eleinte üres modellhalmazt!

			return modellek;
		}
		static Dictionary<Formula, bool> Értékelés_literálokból(HashSet<Formula> pozitív_literálok, HashSet<Formula> negatív_literálok)
		{
			Dictionary<Formula, bool> modell = new Dictionary<Formula, bool>();

			foreach (Formula literal in pozitív_literálok)
				modell[literal] = true;
			foreach (Formula literal in negatív_literálok)
				modell[literal] = false;

			return modell;
		}

		public static void Modellek_kiirasa(HashSet<Dictionary<Formula, bool>> modellek)
		{
			int i = 1;
			foreach (Dictionary<Formula,bool> szotar in modellek)
			{
				Console.WriteLine($"{i}. modell:");
				foreach (Formula literal in szotar.Keys)
				{
					Console.WriteLine($"{literal}: {(szotar[literal]?"i":"h")}");
				}
				Console.WriteLine("-----------------------------------");
			}
		}

		static bool AnFa(
			Popper<Formula> formulák,
			HashSet<Formula> po,
			HashSet<Formula> ne,
			HashSet<Dictionary<Formula, bool>> modellek)
		{
			// Ha üres a popper
			if (formulák.Count == 0)
			{
				modellek.Add(Értékelés_literálokból(po, ne));
				return true;
			}

			// HA nem üres a popper, tehát van még tennivaló...

			Formula f = formulák.Pop();

			// Ha a formula egy p vagy -p alakú literál
			// ha pozitív literál (pl. p)
			if (f.Pozitív_literál())
			{
				if (ne.Contains(-f))
					return false;
				po.Add(f);

				return AnFa(
					new Popper<Formula>(formulák), 
					new HashSet<Formula>(po), 
					new HashSet<Formula>(ne), 
					modellek); // mivel nem újat hozunk létre, az analitikus fa összes csomópontja ugyanazt a halmazt fogja használni!
			}
			// ha negatív literál (pl. -p)
			if (f.Negatív_literál())
			{
				if (po.Contains(f.gyerekei[0]))
					return false;
				ne.Add(f);

				return AnFa(
					new Popper<Formula>(formulák),
					new HashSet<Formula>(po),
					new HashSet<Formula>(ne),
					modellek); // mivel nem újat hozunk létre, az analitikus fa összes csomópontja ugyanazt a halmazt fogja használni!
			}

			// Ha ezek egyike sem, de nem - jellel kezdődik (nem tagadott)

			if (f.muvelet == "&")
				return AnFa((f.gyerekei[0], f.gyerekei[1]) + formulák,
					new HashSet<Formula>(po),
					new HashSet<Formula>(ne),
					modellek);
			if (f.muvelet == "V")
				return AnFa(f.gyerekei[0] + formulák,
					new HashSet<Formula>(po),
					new HashSet<Formula>(ne),
					modellek)
					||
					AnFa(f.gyerekei[1] + formulák,
					new HashSet<Formula>(po),
					new HashSet<Formula>(ne),
					modellek);
			if (f.muvelet == "->")
				return AnFa(-f.gyerekei[0] + formulák,
					new HashSet<Formula>(po),
					new HashSet<Formula>(ne),
					modellek)
					||
					AnFa(f.gyerekei[1] + formulák,
					new HashSet<Formula>(po),
					new HashSet<Formula>(ne),
					modellek); 
			if (f.muvelet == "<->")
				return AnFa((f.gyerekei[0], f.gyerekei[1]) + formulák,
					new HashSet<Formula>(po),
					new HashSet<Formula>(ne),
					modellek)
					||
					AnFa((-f.gyerekei[0], -f.gyerekei[1]) + formulák,
					new HashSet<Formula>(po),
					new HashSet<Formula>(ne),
					modellek);
			if (f.muvelet == "><")
				return AnFa((-f.gyerekei[0], f.gyerekei[1]) + formulák,
					new HashSet<Formula>(po),
					new HashSet<Formula>(ne),
					modellek)
					||
					AnFa((f.gyerekei[0], -f.gyerekei[1]) + formulák,
					new HashSet<Formula>(po),
					new HashSet<Formula>(ne),
					modellek);

			// Ha viszont tagadva van...
			if (f.muvelet == "-")
			{
				Formula tf = f.gyerekei[0];
				// és azon belül...
				if (tf.muvelet == "-")
					return AnFa(tf.gyerekei[0] + formulák,
						new HashSet<Formula>(po),
						new HashSet<Formula>(ne),
						modellek);
				if (tf.muvelet == "&")
					return AnFa(-tf.gyerekei[0] + formulák,
						new HashSet<Formula>(po),
						new HashSet<Formula>(ne),
						modellek)
						||
						AnFa(-tf.gyerekei[1] + formulák,
						new HashSet<Formula>(po),
						new HashSet<Formula>(ne),
						modellek);
				if (tf.muvelet == "V")
					return AnFa((-tf.gyerekei[0], -tf.gyerekei[1]) + formulák,
						new HashSet<Formula>(po),
						new HashSet<Formula>(ne),
						modellek); if (tf.muvelet == "->")
					throw new NotImplementedException();
				if (tf.muvelet == "<->")
					return AnFa((-tf.gyerekei[0], tf.gyerekei[1]) + formulák,
						new HashSet<Formula>(po),
						new HashSet<Formula>(ne),
						modellek)
						||
						AnFa((tf.gyerekei[0], -tf.gyerekei[1]) + formulák,
						new HashSet<Formula>(po),
						new HashSet<Formula>(ne),
						modellek); 
				if (tf.muvelet == "><")
					return AnFa((tf.gyerekei[0], tf.gyerekei[1]) + formulák,
						new HashSet<Formula>(po),
						new HashSet<Formula>(ne),
						modellek)
						||
						AnFa((-tf.gyerekei[0], -tf.gyerekei[1]) + formulák,
						new HashSet<Formula>(po),
						new HashSet<Formula>(ne),
						modellek);
			}
			throw new NotImplementedException($"Ez a konnektívum nincs implementálva: {f.muvelet}");
		}
	}
}
