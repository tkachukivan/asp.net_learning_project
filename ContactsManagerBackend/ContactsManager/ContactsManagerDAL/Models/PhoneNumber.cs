namespace ContactsManagerDAL.Models
{
    public struct PhoneNumber
    {
        public string CountryCode;
        public string Number;

        public PhoneNumber(string countryCode, string number)
        {
            CountryCode = countryCode;
            Number = number;
        }
    }
}
