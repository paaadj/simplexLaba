using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simplexLaba
{
    public class Fraction
    {
        public Int64 numerator { get; set; }
        public Int64 denominator { get; set; }

        public Fraction(Int64 numerator, Int64 denominator)
        {
            if (denominator == 0)
            {
                throw new ArgumentException("Знаменатель не может быть равен нулю", nameof(denominator));
            }
            BigInteger gcd = BigInteger.GreatestCommonDivisor(numerator, denominator);
            numerator =  numerator / (Int64)gcd;
            denominator = denominator / (Int64)gcd;

            this.numerator = numerator;
            this.denominator = denominator;

            if(this.denominator < 0)
            {
                this.numerator *= (-1);
                this.denominator *= (-1);
            }
        }

        public override string ToString() {
            if (numerator == 0)
                return "0";
            else if ((numerator) % denominator == 0)
                return (numerator / denominator).ToString();
            return this.numerator + "/" + this.denominator; 
        }

        public static Fraction operator +(Fraction a) => a;
        public static Fraction operator -(Fraction a) => new Fraction(-a.numerator, a.denominator);
        public static Fraction operator +(Fraction a, Fraction b) => new Fraction(a.numerator * b.denominator + b.numerator * a.denominator, a.denominator * b.denominator);

        public static Fraction operator -(Fraction a, Fraction b) => a + (-b);

        public static Fraction operator *(Fraction a, Fraction b) => new Fraction(a.numerator * b.numerator, a.denominator * b.denominator);

        public static Fraction operator *(Fraction a, int b) => new Fraction(a.numerator * b, a.denominator);
        public static Fraction operator /(Fraction a, Fraction b){
            if(b.numerator == 0)
            {
                throw new DivideByZeroException();
            }
            return new Fraction(a.numerator * b.denominator, a.denominator * b.numerator);
        }

        public double Value()
        {
            return (double)(numerator) / (double)(denominator);
        }
        public Fraction turnOver() => new Fraction(denominator, numerator);
    }
}
