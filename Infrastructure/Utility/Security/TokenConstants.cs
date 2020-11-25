namespace PromoCodes_main.Infrastructure.Utility.Security
{
    public class TokenConstants
    {
         public static string SecurityKey
        {
            get
            {
                return "ThisKeyIsVeryImportantForAuthentication.HopeYouGotThat?";
            }
        }


        public static int TimeoutMinutes
        {
            get
            {

                return 60;
            }
        }

        public static int ClientTimeoutMinutes
        {
            get
            {

                return 30;
            }
        }

        public static int DateToleranceMinutes
        {
            get
            {

                return 2;
            }
        }


        public static string Issuer
        {
            get
            {


                return "busayo-demo";

            }
        }

        public static string Audience
        {
            get
            {
                return "busayo-demo";
            }
        }

        public static bool ClientKeyEnabled
        {
            get
            {
                return true;
            }
        }


        public static bool ValidateAudience
        {
            get
            {
                return true;
            }
        }

        public static bool ValidateIssuer
        {
            get
            {
                return true;
            }
        }

        public static bool ValidateSigningKey
        {
            get
            {
                return true;
            }
        }

        public static bool ValidateLifeTime
        {
            get
            {
                return true;
            }
        }

    }
}