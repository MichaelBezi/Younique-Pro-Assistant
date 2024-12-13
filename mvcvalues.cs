using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace Younique_Pro_Assistant
{
    public class mvcvalues
    {
        private static string _fname, _uname, _pword, _bal, _constr, _phone, _lev;


        public static string fname
        {
            get
            {
                // Reads are usually simple 
                return _fname;
            }
            set
            {
                // You can add logic here for race conditions, 
                // or other measurements 
                _fname = value;
            }
        }
        public static string uname
        {
            get
            {
                // Reads are usually simple 
                return _uname;
            }
            set
            {
                // You can add logic here for race conditions, 
                // or other measurements 
                _uname = value;
            }
        }
        public static string pword
        {
            get
            {
                // Reads are usually simple 
                return _pword;
            }
            set
            {
                // You can add logic here for race conditions, 
                // or other measurements 
                _pword = value;
            }
        }
        public static string bal
        {
            get
            {
                // Reads are usually simple 
                return _bal;
            }
            set
            {
                // You can add logic here for race conditions, 
                // or other measurements 
                _bal = value;
            }
        }

        public static string constr
        {
            get
            {
                // Reads are usually simple 
                return _constr;
            }
            set
            {
                // You can add logic here for race conditions, 
                // or other measurements 
                _constr = value;
            }
        }

        public static string phone
        {
            get
            {
                // Reads are usually simple 
                return _phone;
            }
            set
            {
                // You can add logic here for race conditions, 
                // or other measurements 
                _phone = value;
            }
        }

        public static string lev
        {
            get
            {
                // Reads are usually simple 
                return _lev;
            }
            set
            {
                // You can add logic here for race conditions, 
                // or other measurements 
                _lev = value;
            }
        }
    }

}

