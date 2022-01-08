using System;
using System.Collections.Generic;
using System.Text;

namespace RenewingShares
{
    public class Fraction
    {
		public int num;
		public int den;

		// A fraction consists of a
		// numerator and a denominator
		public Fraction(int n, int d)
		{
			num = n;
			den = d;
		}

		// If the fraction is not
		// in its reduced form
		// reduce it by dividing
		// them with their GCD
		public void ReduceFraction(Fraction f)
		{
			int gcd = GCD(f.num, f.den);
			f.num /= gcd;
			f.den /= gcd;
		}

		static int GCD(int a, int b)
		{
			return b == 0 ? a : GCD(b, a % b);
		}
		// Performing multiplication on the
		// fraction
		public static Fraction operator *(Fraction ImpliedObject, Fraction f)
		{
			
			Fraction temp = new Fraction(ImpliedObject.num * f.num, ImpliedObject.den * f.den);
			//ImpliedObject.ReduceFraction(temp);
			return temp;
		}

		// Performing addition on the
		// fraction
		public static Fraction operator +(Fraction ImpliedObject, Fraction f)
		{
			Fraction temp = new Fraction(ImpliedObject.num * f.den + ImpliedObject.den * f.num, ImpliedObject.den * f.den);

			//ImpliedObject.ReduceFraction(temp);
			return temp;
		}

	}
}
