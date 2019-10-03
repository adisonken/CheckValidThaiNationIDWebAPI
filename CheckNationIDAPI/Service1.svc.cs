using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CheckNationIDAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public bool IsValidCheckPersonID(string pid)
        {
            char[] numberChars = pid.ToCharArray();

            int total = 0;
            int mul = 13;
            int mod = 0, mod2 = 0;
            int nsub = 0;
            int numberChars12 = 0;

            for (int i = 0; i < numberChars.Length - 1; i++)
            {
                int num = 0;
                int.TryParse(numberChars[i].ToString(), out num);

                total = total + num * mul;
                mul = mul - 1;

                //Debug.Log(total + " - " + num + " - "+mul);
            }

            mod = total % 11;
            nsub = 11 - mod;
            mod2 = nsub % 10;

            //Debug.Log(mod);
            //Debug.Log(nsub);
            //Debug.Log(mod2);


            int.TryParse(numberChars[12].ToString(), out numberChars12);

            //Debug.Log(numberChars12);

            if (mod2 != numberChars12)
                return false;
            else
                return true;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
