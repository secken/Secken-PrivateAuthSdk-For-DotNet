namespace Led
{
    internal static class ExpandIs
    {
        #region ExpandIs

        /// <summary>
        /// List -> IsEmptyObjectList
        /// </summary>
        /// <param name="iList"></param>
        /// <returns></returns>
        public static bool IsNotEmptyObjectList(this System.Collections.Generic.List<object> iList)
        {
            #region List -> IsEmptyObjectList

            return iList != null && iList.Count > 0;

            #endregion
        }

        /// <summary>
        /// List -> IsNotEmptyList
        /// </summary>
        /// <param name="iList"></param>
        /// <returns></returns>
        public static bool IsNotEmptyList(this System.Collections.Generic.List<string> iList)
        {
            #region List -> IsNotEmptyList

            return iList != null && iList.Count > 0;

            #endregion
        }

        /// <summary>
        /// String[] -> IsNotEmptyStrings
        /// </summary>
        /// <param name="iString"></param>
        /// <returns></returns>
        public static bool IsNotEmptyStrings(this string[] iString)
        {
            #region String[] -> IsNotEmptyStrings

            return iString != null && iString.Length > 0;

            #endregion
        }

        /// <summary>
        /// Byte[] -> IsNotEmptyBytes
        /// </summary>
        /// <param name="iBytes"></param>
        /// <returns></returns>
        public static bool IsNotEmptyBytes(this byte[] iBytes)
        {
            #region Byte[] -> IsNotEmptyBytes

            return iBytes != null && iBytes.Length > 0;

            #endregion
        }

        /// <summary>
        /// Object -> IsNotEmpty
        /// </summary>
        /// <param name="iO"></param>
        /// <returns></returns>
        public static bool IsNotEmpty(this object iO)
        {
            #region Object -> IsNotEmpty

            return iO != null;

            #endregion
        }

        /// <summary>
        /// String -> Check Value -> IsNotNullOrEmpty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string value)
        {
            #region String -> Check Value -> IsNotNullOrEmpty

            return !System.String.IsNullOrEmpty(value) && !System.String.IsNullOrWhiteSpace(value);

            #endregion
        }

        /// <summary>
        /// String -> Check Value -> IsNullOrEmptyOrSpace
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrSpace(this string value)
        {
            #region String -> Check Value -> IsNullOrEmptyOrSpace

            return System.String.IsNullOrEmpty(value) && System.String.IsNullOrWhiteSpace(value);

            #endregion
        } 
        #endregion
    }
}
