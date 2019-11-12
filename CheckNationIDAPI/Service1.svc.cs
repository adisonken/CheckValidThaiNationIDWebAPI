using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

        public bool IsValidPassword(string password)
        {
            PasswordPolicy policy = new PasswordPolicy();
            bool t = policy.IsValid(password);
            return t;
        }

        public string UpdateStatus(string user)
        {
            string conn = ConfigurationManager.ConnectionStrings["constrk2"].ToString();
            SqlConnection objsqlconn = new SqlConnection(conn);
            string sql2 = "";
            try
            {
                objsqlconn.Open();
                sql2 = string.Format(
                       "	 Update [K2].[dbo].[UserPolicyPassword] SET [IsWarning] = '0',[IsExpire] = '0' " +
                       "	 Where [UserName] = '{0}'  "
                    , user);
                SqlCommand objcmd2 = new SqlCommand(sql2, objsqlconn);
                SqlDataAdapter objAdp2 = new SqlDataAdapter(objcmd2);
                objcmd2.ExecuteNonQuery();

            }
            catch (Exception ex)
            {


            }
            finally
            {
                objsqlconn.Close();
            }

            return "OK";
        }

        public bool IsWarning(string user)
        {
            bool chkresult = false;
            string conn = ConfigurationManager.ConnectionStrings["constrk2"].ToString();
            SqlConnection objsqlconn = new SqlConnection(conn);
            string sql2 = "";
            try
            {
                objsqlconn.Open();
                sql2 = string.Format(
                       "	 SELECT [IsWarning] FROM [UserPolicyPassword]" +
                       "	 Where [UserName] = '{0}' AND IsWarning = '1' "
                    , user);
                SqlCommand objcmd2 = new SqlCommand(sql2, objsqlconn);
                SqlDataAdapter objAdp = new SqlDataAdapter(objcmd2);
                DataTable dt = new DataTable();
                objAdp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    chkresult = true;

                }
                else
                {
                    chkresult = false;
                }

            }
            catch (Exception ex)
            {


            }
            finally
            {
                objsqlconn.Close();
            }

            return chkresult;
        }

        public bool IsExpire(string user)
        {
            bool chkresult = false;
            string conn = ConfigurationManager.ConnectionStrings["constrk2"].ToString();
            SqlConnection objsqlconn = new SqlConnection(conn);
            string sql2 = "";
            try
            {
                objsqlconn.Open();
                sql2 = string.Format(
                       "	 SELECT [IsExpire] FROM [UserPolicyPassword]" +
                       "	 Where [UserName] = '{0} AND IsExpire = '1' "
                    , user);
                SqlCommand objcmd2 = new SqlCommand(sql2, objsqlconn);
                SqlDataAdapter objAdp = new SqlDataAdapter(objcmd2);
                DataTable dt = new DataTable();
                objAdp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    chkresult = true;

                }
                else
                {
                    chkresult = false;
                }

            }
            catch (Exception ex)
            {


            }
            finally
            {
                objsqlconn.Close();
            }

            return chkresult;
        }
    }
}
