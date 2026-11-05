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

			//Stack<string> v = Formula.Tolatóudvar("-(A*B)>-A+-B");

			Formula f = Formula.Parse("-(A*B)>-A+-B");
			Console.WriteLine(f);

			//Formula p = new Formula("p");
			//Formula q = new Formula("q");
			//Formula r = new Formula("r");
			//Formula t = new Formula("t");

			// Itt az első kettőből kijön egy ellentmondás
			//Formula f1 = p > (-q * r);
			//Formula f2 = (p + q) * -(-p + r);
			//Formula f3 = r == (p + -q);

			// Ez nem ellentmondásos: 
			//Formula f1 = -p + (r == (q * -t));
			//Formula f2 = -((p + r) > -(q + t));
			//Formula f3 = -(p == (-q > r));

			//HashSet<Dictionary<Formula, bool>> modellek = Formula.Modelljei(new HashSet<Formula> { f1, f2 });
			//HashSet<Dictionary<Formula, bool>> modellek = Formula.Modelljei(new HashSet<Formula> { f1, f2, f3 });

			//Formula.Modellek_kiirasa(modellek);




			//Formula i = new Formula("I"); // Az arany ládika IGAZAT mond
			//Formula j = new Formula("J"); // Az ezüst ládika IGAZAT mond
			//Formula k = new Formula("K"); // Az ólom ládika IGAZAT mond
			//Formula a = new Formula("A"); // a kép az arany ládikában van
			//Formula e = new Formula("E"); // a kép az ezüst ládikában van
			//Formula o = new Formula("Ó"); // a kép az ólom ládikában van

			//Formula legfeljebb_egy_igaz = (-i * -j * -k) + (i * -j * -k) + (-i * j * -k) + (-i * -j * k); // ezt mondta Portia
			//Formula valahol_van = (a * -e * -o) + (-a * e * -o) + (-a * -e * o); // mert 1 kép van
			//Formula elso = i == a; // akkor és csak akkor mond igazat az arany ládika, ha igaz, amit mond: azaz ott van a kép
			//Formula masodik = j == -e;
			//Formula harmadik = k == -a;

			//HashSet<Dictionary<Formula, bool>> modellek = Formula.Modelljei(new HashSet<Formula> {
			//	legfeljebb_egy_igaz,
			//	valahol_van,
			//	elso,
			//	masodik,
			//	harmadik,
			//});

			//Formula.Modellek_kiirasa(modellek);



			//Formula a = new Formula("a"); // Az arany ládikában van
			//Formula e = new Formula("e"); // Az ezüstben van
			//Formula o = new Formula("o"); // Az ólomban van

			//Formula l1 = (-a * e) * -(-a + o) * -(-o == a);// a csupa igaz, e csupa hamis, o vegyes
			//Formula l2 = (-a * e) * -(-a == o) * -(-o + a);// a csupa igaz, e vegyes, o csupa hamis
			//Formula l3 = -(-a + e) * (-a * o) * -(-o == a);// a csupa hamis, e csupa igaz, o vegyes
			//Formula l4 = -(-a + e) * -(-a == o) * (-o * a);// a csupa hamis, e vegyes, o csupa igaz
			//Formula l5 = -(-a == e) * (-a * o) * -(-o + a);// a vegyes, e csupa igaz, o csupa hamis
			//Formula l6 = -(-a == e) * -(-a + o) * (-o * a);// a vegyes, e csupa hamis, o csupa igaz
			//Formula egész = l1 + l2 + l3 + l4 + l5 + l6;
			//Formula valahol_van = (a * -e * -o) + (-a * e * -o) + (-a * -e * o); // mert 1 kép van

			//HashSet<Dictionary<Formula, bool>> modellek = Formula.Modelljei(new HashSet<Formula> {
			//	egész,
			//	valahol_van,
			//});

			//Formula.Modellek_kiirasa(modellek);


			//Formula a = new Formula("a"); // A bűnös
			//Formula b = new Formula("b"); // B bűnös
			//Formula c = new Formula("c"); // C bűnös

			//HashSet<Formula> formulák = new HashSet<Formula>
			//{
			//	(a * -b) > c,
			//	c > (a + b),
			//	-(a * c),
			//	a + b + c,
			//};


			//HashSet<Dictionary<Formula, bool>> modellek = Formula.Modelljei(formulák);

			//Formula.Modellek_kiirasa(modellek);



			//Formula a = new Formula("a");
			//Formula e = new Formula("b");
			//Formula o = new Formula("c");
			//Formula i = new Formula("i");
			//Formula j = new Formula("j");
			//Formula k = new Formula("k");

			//HashSet<Formula> formulák = new HashSet<Formula>
			//{
			//	i==a,
			//	j==-e,
			//	k == ((-i*-j*-k) + (i*-j*-k) + (-i*j*-k) + (-i*-j*k)),
			//	a+e+o,
			//	(a*-e*-o) + (-a*e*-o) + (-a*-e*o),
			//};


			//HashSet<Dictionary<Formula, bool>> modellek = Formula.Modelljei(formulák);

			//Formula.Modellek_kiirasa(modellek);



			Formula m = new Formula("m"); // A dokumentumot megnyitották
			Formula r = new Formula("r"); // A rendszergazda bejelentkezett
			Formula k = new Formula("k"); // Valaki megszerezte a hozzáférési kódot

			HashSet<Formula> formulák = new HashSet<Formula>
			{
				m>(-r==k),
				(-r * k) > m,
				(m*r) > -k,
				-r+m,
				-(k>m),
			};

			HashSet<Dictionary<Formula, bool>> modellek = Formula.Modelljei(formulák);

			Formula.Modellek_kiirasa(modellek);




			//Formula e = new Formula("e"); // Esik az eső
			//Formula h = new Formula("h"); // A kirándulást elhalasztják
			//Formula m = new Formula("m"); // A diákok múzeumba mennek

			//HashSet<Formula> formulák = new HashSet<Formula>
			//{
			//	e > (h + m),
			//	h > -m,
			//	-e > -h,
			//	h + m,
			//	-e,
			//};

			//HashSet<Dictionary<Formula, bool>> modellek = Formula.Modelljei(formulák);

			//Formula.Modellek_kiirasa(modellek);




			return;

			//Popper<int> popper = new Popper();
			//Popper<int> ujpopper = 4 + popper;
			//Popper<int> vadiujpopper = (4,5) + popper;

			//Formula A = new Formula("A");
			//Formula B = new Formula("B");
			//Formula C = new Formula("C");
			//Formula D = new Formula("D");

			//HashSet<Formula> formulahalmaz = new HashSet<Formula> { A, B, C, D };

			//Formula.Kielégíthető(formulahalmaz);

			//Formula aesb = A * B;

			//// (A ∧ B ) →¬(C ∨ D)

			//Formula f = (A * B) > -(A + D);


			//Console.WriteLine(f);

			//Console.WriteLine(f.Negaciok_szama());

			//Console.WriteLine(string.Join(", ", f.Atomjai()));
			//Console.WriteLine(string.Join(", ", f.Részformulái()));
			//Console.WriteLine(string.Join(", ", f.Részformulái2()));





			////együtt.Hogyan_lehet_igaz();



		}
	}
}
