namespace ResultCommunication
{
    public static class Enums
    {

        public enum ErrorType
        {

            InvalidField = 10001,
            MandatoryField = 10002,
            RelatedRecord = 10004,
            RepeatedRecord = 10005,
            NotFound = 10007,
            GeneralException = 11001,
            SqlException = 11002

        }

    }
}
