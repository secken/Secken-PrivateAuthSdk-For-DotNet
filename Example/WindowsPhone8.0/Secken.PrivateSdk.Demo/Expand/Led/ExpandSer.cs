namespace Led
{
    internal static class ExpandSer
    {
        #region ExpandSer

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="objectToSerialize"></param>
        /// <returns></returns>
        public static string Serialize(this object objectToSerialize)
        {
            #region 序列化

            if (objectToSerialize != null)
            {
                using (var ms = new System.IO.MemoryStream())
                {
                    var serializer =
                        new System.Runtime.Serialization.Json.DataContractJsonSerializer(objectToSerialize.GetType());
                    serializer.WriteObject(ms, objectToSerialize);
                    ms.Position = 0;
                    using (var reader = new System.IO.StreamReader(ms))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            return null;

            #endregion
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T Deserialize<T>(this string jsonString)
        {
            #region 反序列化

            using (var ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonString)))
            {
                var deserializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                return (T)deserializer.ReadObject(ms);
            }

            #endregion
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="objectToSerialize"></param>
        public static string SerializeXml(this object objectToSerialize)
        {
            #region 序列化

            if (objectToSerialize != null)
            {
                using (var ms = new System.IO.MemoryStream())
                {
                    var serializer = new System.Xml.Serialization.XmlSerializer(objectToSerialize.GetType());
                    serializer.Serialize(ms, objectToSerialize);
                    ms.Position = 0;
                    using (var reader = new System.IO.StreamReader(ms))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            return null;

            #endregion
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static T DeserializeXml<T>(this string xmlString)
        {
            #region 反序列化

            using (var ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(xmlString)))
            {
                var deserializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                return (T)deserializer.Deserialize(ms);
            }

            #endregion
        } 
        #endregion
    }
}
