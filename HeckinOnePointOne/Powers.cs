using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeckinOnePointOne
{
    public class PowerCollection : ObservableCollection<Power>
    {
        public PowerCollection()
        {

        }

        public void CalculatePowers(int ToPower)
        {
            this.Add(Power.GetTheOne());
            if (ToPower >= 0)
            {
                for (int i = 0; i < ToPower; i++)
                {
                    Power thisPower = new Power();
                    thisPower = thisPower.CalculateFromPrevious(this[i]);
                    this.Add(thisPower);
                }
            }
        }

    }

    public class Power
    {
        private int totalDigitCount;
        private int wholeDigitCount;
        private int fractionDigitCount;
        private int decimalPointPosition;
        private int powerOf;

        private String powerOfString;
        private String wholeValueString;
        private String fractionValueString;
        private String valueString;

        private List<int> reverseValue = new List<int>();

        public int TotalDigitCount
        {
            get { return totalDigitCount; }
        }

        public int WholeDigitCount
        {
            get { return wholeDigitCount; }
        }

        public int FractionDigitCount
        {
            get { return fractionDigitCount; }
        }

        public int DecimalPointPosition
        {
            get { return decimalPointPosition; }
        }

        public int PowerOf
        {
            get { return powerOf; }
        }

        public String PowerOfString
        {
            get { return powerOfString; }
        }

        public String WholeValueString
        {
            get { return wholeValueString; }
        }

        public String FractionValueString
        {
            get { return fractionValueString; }
        }

        public String ValueString
        {
            get { return valueString; }
        }
 
        public List<int> ReverseValue
        {
            get { return reverseValue; }
        }

        public Power()
        {

        }

        public Power CalculateFromPrevious(Power previousPower)
        {
            Power newPower = new Power();

            bool carryOne = false;
            int sum = 0;

            newPower.reverseValue.Add(1);
            previousPower.reverseValue.Add(0);

            for (int i = 1; i <= previousPower.totalDigitCount; i++)
            {      
                if (carryOne == true)
                {
                    sum = previousPower.reverseValue[i - 1] + previousPower.reverseValue[i] + 1;
                }
                else
                {
                    sum = previousPower.reverseValue[i - 1] + previousPower.reverseValue[i];
                }             

                if (sum > 9)
                {
                    sum -= 10;              
                    carryOne = true;
                }
                else
                {
                    carryOne = false;
                }

                newPower.reverseValue.Add(sum);
            }

            if (carryOne == true)
            {
                newPower.reverseValue.Add(1);
                newPower.wholeDigitCount = previousPower.wholeDigitCount + 1;
                newPower.decimalPointPosition = previousPower.decimalPointPosition + 1;
            }
            else
            {
                newPower.wholeDigitCount = previousPower.wholeDigitCount;
                newPower.decimalPointPosition = previousPower.decimalPointPosition;
            }

            newPower.powerOf = previousPower.powerOf + 1;
            newPower.totalDigitCount = newPower.ReverseValue.Count();
            newPower.fractionDigitCount = newPower.reverseValue.Count() - newPower.wholeDigitCount;

            newPower.powerOfString = ("1.1^" + newPower.powerOf.ToString() + " = ");

            newPower._GetValueString();
            newPower._GetWholeValueString();
            newPower._GetFractionValueString();
            
            carryOne = false;

            return newPower;
        }

        public static Power GetTheOne()
        {
            Power theOne = new Power();

            theOne.powerOf = 0;
            theOne.decimalPointPosition = 1;
            theOne.totalDigitCount = 1;
            theOne.wholeDigitCount = 1;
            theOne.fractionDigitCount = 0;
            theOne.reverseValue.Add(1);
            theOne.powerOfString = "1.1^0 = ";
            theOne._GetValueString();
            theOne._GetWholeValueString();
            theOne._GetFractionValueString();
            

            return theOne;        
        }

        private void _GetValueString()
        {
            StringBuilder sb = new StringBuilder(this.totalDigitCount + 1);

            for (int i = this.reverseValue.Count() - 1; i >= 0; i--)
            {
                sb.Append(this.reverseValue[i]);
            }

            if (this.totalDigitCount > 1)
            {
                sb.Insert(this.decimalPointPosition, ".");
            }
            
            valueString = sb.ToString();
        }

        private void _GetWholeValueString()
        {
            StringBuilder sb = new StringBuilder(this.wholeDigitCount + 1);

            sb.Append(valueString.Substring(0, this.decimalPointPosition));

            wholeValueString = sb.ToString();
        }

        private void _GetFractionValueString()
        {
            StringBuilder sb = new StringBuilder(this.fractionDigitCount + 1);

            if (this.fractionDigitCount > 0)
            {
                sb.Append(valueString.Substring(this.decimalPointPosition + 1));
            }
            

            fractionValueString = sb.ToString();
        }
    }
}
