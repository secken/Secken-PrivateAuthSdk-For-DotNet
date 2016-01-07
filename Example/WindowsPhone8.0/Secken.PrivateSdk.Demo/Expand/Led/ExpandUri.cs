using System;

namespace Led
{
    internal static class ExpandUri
    {
        /// <summary>
        /// PathStr -> ToUri
        /// </summary>
        /// <param name="pathStr"></param>
        /// <returns></returns>
        public static System.Uri ToHttpUri(this string pathStr)
        {
            #region PathStr -> ToUri

            System.Uri pathUri;
            try
            {
                if (pathStr.StartsWith("http://") || pathStr.StartsWith("https://") || pathStr.StartsWith("ms-appx:"))
                {
                    pathUri = new System.Uri(pathStr);
                }
                else
                {
                    pathUri = new System.Uri(String.Format("http://{0}", pathStr));
                }
            }
            catch
            {
                pathUri = null;
            }
            return pathUri;

            #endregion
        }

        /// <summary>
        /// PathStr -> ToRelativeUri
        /// </summary>
        /// <param name="pathStr"></param>
        /// <returns></returns>
        public static Uri ToRelativeUri(this string pathStr)
        {
            #region PathStr -> ToRelativeUri

            if (pathStr.IsNotNullOrEmpty())
            {
                Uri pathUri;
                try
                {
                    pathUri = new Uri(pathStr, UriKind.Relative);
                }
                catch
                {
                    pathUri = null;
                }
                return pathUri;
            }
            return null;
            #endregion
        }

        /// <summary>
        /// PathStr -> ToAbsoluteUri
        /// </summary>
        /// <param name="pathStr"></param>
        /// <returns></returns>
        public static Uri ToAbsoluteUri(this string pathStr)
        {
            #region PathStr -> ToAbsoluteUri
            if (pathStr.IsNotNullOrEmpty())
            {
                Uri pathUri;
                try
                {
                    pathUri = new Uri(pathStr, UriKind.Absolute);
                }
                catch
                {
                    pathUri = null;
                }
                return pathUri;
            }
            return null;
            #endregion
        }

        /// <summary>
        /// PathStr -> ToRelativeOrAbsoluteUri
        /// </summary>
        /// <param name="pathStr"></param>
        /// <returns></returns>
        public static Uri ToRelativeOrAbsoluteUri(this string pathStr)
        {
            #region PathStr -> ToRelativeOrAbsoluteUri
            if (pathStr.IsNotNullOrEmpty())
            {
                Uri pathUri;
                try
                {
                    pathUri = new Uri(pathStr, UriKind.RelativeOrAbsolute);
                }
                catch
                {
                    pathUri = null;
                }
                return pathUri;
            }
            return null;
            #endregion
        }
    }
}
