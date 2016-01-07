namespace Led
{
    internal static class ExpandTurn
    {
        #region ExpandTurn

        /// <summary>
        /// String -> Int
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt(this string s)
        {
            #region String -> Int

            int a;
            if (System.Int32.TryParse(s, out a))
            {
                return a;
            }
            return -1;

            #endregion
        }

        /// <summary>
        /// String -> Int64
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long ToInt64(this string s)
        {
            #region String -> Int

            long a;
            if (System.Int64.TryParse(s, out a))
            {
                return a;
            }
            return -1;

            #endregion
        }

        /// <summary>
        ///  String -> Boolean
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ToBool(this object s)
        {
            #region String -> Boolean

            return System.Boolean.Parse(s.ToString());

            #endregion
        }

        /// <summary>
        /// String -> Int
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static float ToFloat(this string s)
        {
            #region String -> Int

            float a;
            if (System.Single.TryParse(s, out a))
            {
                return a;
            }
            return -1;

            #endregion
        }

        ///// <summary>
        ///// 二进制转换成图片
        ///// </summary>
        ///// <param name="bytes"></param>
        ///// <returns></returns>
        //public static System.Drawing.Image ToImage(this byte[] bytes)
        //{
        //    #region 二进制转换成图片

        //    try
        //    {
        //        return System.Drawing.Image.FromStream(bytes.ToStream());
        //    }
        //    catch
        //    {
        //        return null;
        //    }

        //    #endregion
        //}

        ///// <summary>
        ///// Stream 转换为 image 图片
        ///// </summary>
        ///// <returns></returns>
        //public static System.Drawing.Image ToImage(this System.IO.Stream stream)
        //{
        //    var img = new System.Drawing.Bitmap(stream);
        //    return img;
        //}



        /// <summary>
        /// 将 Stream 转成 byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this System.IO.Stream stream)
        {
            #region 将 Stream 转成 byte[]

            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            return bytes;

            #endregion
        }

        /// <summary>
        /// 将 byte[] 转成 Stream
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static System.IO.Stream ToStream(this byte[] bytes)
        {
            #region 将 byte[] 转成 Stream

            var stream = new System.IO.MemoryStream(bytes);
            // 设置当前流的位置为流的开始 
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            return stream;

            #endregion
        }

        /// <summary>
        /// Bytes -> String
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToStr(this byte[] bytes)
        {
            #region Bytes -> String

            return System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

            #endregion
        }

        /// <summary>
        /// Stream  -> String
        /// </summary>
        /// <returns></returns>
        public static string ToStr(this System.IO.Stream stream)
        {
            #region Stream  -> String

            return System.Text.Encoding.UTF8.GetString(stream.ToBytes(), 0, (int)stream.Length);

            #endregion
        }

        /// <summary>
        /// string 转换为 byte[] 
        /// </summary>
        /// <returns></returns>
        public static byte[] ToBytes(this string str)
        {
            return System.Text.Encoding.UTF8.GetBytes(str);
        }

        /// <summary>
        /// string 转换为 Stream 
        /// </summary>
        /// <returns></returns>
        public static System.IO.Stream ToStream(this string str)
        {
            System.IO.Stream stream = new System.IO.MemoryStream(str.ToBytes());
            // 设置当前流的位置为流的开始 
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            return stream;
        }

        ///// <summary>
        ///// String -> Path -> Ext
        ///// </summary>
        ///// <param name="path"></param>
        ///// <returns></returns>
        //public static string ToExt(this string path)
        //{
        //    #region String -> Path -> Ext

        //    if (!path.IsNullOrEmptyOrSpace() && path.IsExistFile())
        //    {
        //        var ext = System.IO.Path.GetExtension(path);
        //        return !ext.IsNullOrEmptyOrSpace() ? ext : null;
        //    }
        //    return null;

        //    #endregion
        //}

        ///// <summary>
        ///// String -> Path -> NameWithExt
        ///// </summary>
        ///// <param name="path"></param>
        ///// <returns></returns>
        //public static string ToNameWithExt(this string path)
        //{
        //    #region String -> Path -> NameWithExt

        //    if (!path.IsNullOrEmptyOrSpace())
        //    {
        //        if (path.IsExistDir())
        //        {
        //            var name = System.IO.Path.GetDirectoryName(path);
        //            return !name.IsNullOrEmptyOrSpace() ? name : null;
        //        }
        //        if (path.IsExistFile())
        //        {
        //            var name = System.IO.Path.GetFileName(path);
        //            return !name.IsNullOrEmptyOrSpace() ? name : null;
        //        }
        //    }
        //    return null;

        //    #endregion
        //}

        ///// <summary>
        ///// String -> Path -> NameNoExt
        ///// </summary>
        ///// <param name="path"></param>
        ///// <returns></returns>
        //public static string ToNameNoExt(this string path)
        //{
        //    #region String -> Path -> ToNameNoExt

        //    if (!path.IsNullOrEmptyOrSpace())
        //    {
        //        if (path.IsExistDir())
        //        {
        //            var name = System.IO.Path.GetDirectoryName(path);
        //            return !name.IsNullOrEmptyOrSpace() ? name : null;
        //        }
        //        if (path.IsExistFile())
        //        {
        //            var name = System.IO.Path.GetFileNameWithoutExtension(path);
        //            return !name.IsNullOrEmptyOrSpace() ? name : null;
        //        }
        //    }
        //    return null;

        //    #endregion
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        public static System.Collections.Generic.List<object> ToSafeObjects(
            this System.Collections.Generic.List<object> objects)
        {
            return objects.IsNotEmptyObjectList()
                ? objects
                : new System.Collections.Generic.List<object>();
        }

        /// <summary>
        /// String -> Check Value -> GetBack -> String
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToSafeValue(this string value)
        {
            #region String -> Check Value -> GetBack -> String

            return value.IsNullOrEmptyOrSpace() ? "" : value;

            #endregion
        }

        ///// <summary>
        ///// String -> ToHtmlDecode
        ///// </summary>
        ///// <param name="code"></param>
        ///// <returns></returns>
        //public static string ToHtmlDecode(this string code)
        //{
        //    #region String -> ToHtmlDecode

        //    return System.Web.HttpUtility.HtmlDecode(code);

        //    #endregion
        //}

        ///// <summary>
        ///// String -> ToHtmlEncode
        ///// </summary>
        ///// <param name="code"></param>
        ///// <returns></returns>
        //public static string ToHtmlEncode(this string code)
        //{
        //    #region String -> ToHtmlEncode

        //    return System.Web.HttpUtility.HtmlEncode(code);

        //    #endregion
        //}

        ///// <summary>
        ///// String -> ToUrlDecode
        ///// </summary>
        ///// <param name="code"></param>
        ///// <returns></returns>
        //public static string ToUrlDecode(this string code)
        //{
        //    #region String -> ToUrlDecode

        //    return System.Web.HttpUtility.UrlDecode(code);

        //    #endregion
        //}

        ///// <summary>
        ///// String -> ToUrlEncode
        ///// </summary>
        ///// <param name="code"></param>
        ///// <returns></returns>
        //public static string ToUrlEncode(this string code)
        //{
        //    #region String -> ToUrlEncode

        //    return System.Web.HttpUtility.UrlEncode(code, System.Text.Encoding.Default);

        //    #endregion
        //}

        #endregion
    }
}
