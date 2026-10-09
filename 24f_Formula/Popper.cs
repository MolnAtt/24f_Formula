using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24f_Formula
{
	class Popper<T>
	{
		private List<T> tarolo;
		public int Count { get => tarolo.Count; }
		public Popper()
		{
			this.tarolo = new List<T>();
		}
		public Popper(Popper<T> popper)
		{
			this.tarolo = new List<T>(popper.tarolo);
		}

		public Popper(HashSet<T> formulahalmaz)
		{
			this.tarolo = formulahalmaz.ToList();
		}

		public void Push(T elem)
		{
			tarolo.Add(elem);
		}
		public T Pop()
		{
			T result = tarolo[tarolo.Count - 1];
			tarolo.RemoveAt(tarolo.Count - 1);
			return result;
		}
		public static Popper<T> operator +(T elem, Popper<T> popper)
		{
			Popper<T> p = new Popper<T>(popper);
			p.Push(elem);
			return p;
		}
		public static Popper<T> operator +((T, T) elempar, Popper<T> popper)
		{
			Popper<T> p = new Popper<T>(popper);
			(T egyik, T masik) = elempar;
			p.Push(egyik);
			p.Push(masik);
			return p;
		}

	}
}
