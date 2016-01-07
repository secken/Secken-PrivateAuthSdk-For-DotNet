using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Led.Plugin.FileIo
{
    internal static class ExpandForFileIo
    {
        #region ExpandForFileIo

        /// <summary>
        /// String + FilePath -> WriteFile
        /// </summary>
        /// <param name="thisStr"></param>
        /// <param name="thisFilePath"></param>
        /// <returns></returns>
        public static bool WriteFile(this string thisStr, string thisFilePath)
        {
            #region String + FilePath -> WriteFile
            try
            {
                using (var myIsolatedStorage = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (var myIsolatedStorageFileStream = new System.IO.IsolatedStorage.IsolatedStorageFileStream(thisFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write, myIsolatedStorage))
                    {
                        using (var writer = new System.IO.StreamWriter(myIsolatedStorageFileStream))
                        {
                            writer.Write(thisStr);
                            return true;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            #endregion
        }

        /// <summary>
        /// FilePath -> ReadFile -> string
        /// </summary>
        /// <param name="thisFilePath"></param>
        /// <returns></returns>
        public static string ReadFile(this string thisFilePath)
        {
            #region FilePath -> ReadFile -> string
            try
            {
                using (var myIsolatedStorage = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (var fileStream = myIsolatedStorage.OpenFile(thisFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var reader = new System.IO.StreamReader(fileStream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
            #endregion
        }

        /// <summary>
        /// FilePath -> DeleteFile -> string
        /// </summary>
        /// <param name="thisFilePath"></param>
        /// <returns></returns>
        public static bool DeleteFile(this string thisFilePath)
        {
            #region FilePath -> DeleteFile -> string

            if (!thisFilePath.IsExistFile()) return false;
            try
            {
                using (var myIsolatedStorage = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
                {
                    myIsolatedStorage.DeleteFile(thisFilePath);
                    return true;
                }
            }
            catch
            {
                return false;
            }

            #endregion
        }

        /// <summary>
        /// String -> Dir -> ToDelete
        /// </summary>
        /// <param name="dir"></param>
        public static bool DeleteDir(this string dir)
        {
            #region String -> Dir -> ToDelete

            if (!dir.IsExistDir()) return false;
            try
            {
                using (var myIsolatedStorage = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
                {
                    myIsolatedStorage.DeleteDirectory(dir);
                    return true;
                }
            }
            catch
            {
                return false;
            }

            #endregion
        }

        /// <summary>
        /// DirPath -> ToSafeDirectory -> Bool
        /// </summary>
        /// <param name="thisDirPath"></param>
        /// <returns></returns>
        public static bool CreatDir(this string thisDirPath)
        {
            #region DirPath -> ToSafeDirectory -> Bool
            return thisDirPath.IsExistDir() || thisDirPath.ToCreateDirectory();
            #endregion
        }

        /// <summary>
        /// DirPath -> CreateDirectory -> Bool
        /// </summary>
        /// <param name="thisDirPath"></param>
        /// <returns></returns>
        public static bool ToCreateDirectory(this string thisDirPath)
        {
            #region DirPath -> CreateDirectory -> Bool
            try
            {
                using (var isolatedStorageFile = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
                {
                    isolatedStorageFile.CreateDirectory(thisDirPath);
                    return true;
                }
            }
            catch
            {
                return false;
            }
            #endregion
        }

        /// <summary>
        /// FilePath -> IsExistFile -> Bool
        /// </summary>
        /// <param name="thisFilePath"></param>
        /// <returns></returns>
        public static bool IsExistFile(this string thisFilePath)
        {
            #region FilePath -> IsExistFile -> Bool
            try
            {
                using (var isolatedStorageFile = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
                {
                    return isolatedStorageFile.FileExists(thisFilePath);
                }
            }
            catch
            {
                return false;
            }
            #endregion
        }

        /// <summary>
        /// DirPath -> IsExistDir -> Bool
        /// </summary>
        /// <param name="thisDirPath"></param>
        /// <returns></returns>
        public static bool IsExistDir(this string thisDirPath)
        {
            #region DirPath -> IsExistDir -> Bool
            try
            {
                using (var isolatedStorageFile = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
                {
                    return isolatedStorageFile.DirectoryExists(thisDirPath);
                }
            }
            catch
            {
                return false;
            }
            #endregion
        }

        /// <summary>
        /// Stream + FilePath -> SaveStream -> Bool
        /// </summary>
        /// <param name="thisStream"></param>
        /// <param name="thisFilePath"></param>
        /// <returns></returns>
        public static bool SaveStream(this System.IO.Stream thisStream, string thisFilePath)
        {
            #region Stream + FilePath -> SaveStream -> Bool
            if (thisStream != null)
            {
                try
                {
                    using (var isolatedStorageFile = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        using (var fileStream = isolatedStorageFile.OpenFile(thisFilePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write))
                        {
                            thisStream.Position = 0;
                            thisStream.CopyTo(fileStream);
                            return true;
                        }
                    }

                }
                catch
                {
                    return false;
                }
            }
            return false;
            #endregion
        }

        /// <summary>
        /// FilePath -> ReadStream -> Stream
        /// </summary>
        /// <param name="thisFilePath"></param>
        /// <returns></returns>
        public static System.IO.Stream ReadStream(this string thisFilePath)
        {
            #region FilePath -> ReadStream -> Stream
            if (thisFilePath.IsExistFile())
            {
                try
                {
                    using (var isolatedStorageFile = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        var stream = isolatedStorageFile.OpenFile(thisFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        return stream;
                    }
                }
                catch
                {
                    return null;
                }
            }
            return null;
            #endregion
        }

        /// <summary>
        /// AllByte - > FilePath -> Bool
        /// </summary>
        /// <param name="allByte"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool WriteBytes(this byte[] allByte, string filePath)
        {
            #region AllByte - > FilePath -> Bool

            try
            {
                return allByte.ToStream().SaveStream(filePath);
            }
            catch
            {
                return false;
            }

            #endregion
        }


        /// <summary>
        /// FilePath -> ToReadBytes
        /// </summary>
        /// <param name="thisFilePath"></param>
        /// <returns></returns>
        public static byte[] ReadBytes(this string thisFilePath)
        {
            #region FilePath -> ToReadBytes

            if (thisFilePath.IsExistFile())
            {
                try
                {
                    using (var isolatedStorageFile = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        using (var fileStream = isolatedStorageFile.OpenFile(thisFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                        {
                            var bytes = new byte[fileStream.Length];
                            fileStream.Read(bytes, 0, (int)fileStream.Length);
                            return bytes;
                        }
                    }
                }
                catch
                {
                    return null;
                }
            }
            return null;

            #endregion
        }

        /// <summary>
        /// String -> Path -> NameNoExt
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ToNameNoExt(this string path)
        {
            #region String -> Path -> ToNameNoExt

            if (!path.IsNullOrEmptyOrSpace())
            {
                if (path.IsExistDir())
                {
                    var name = System.IO.Path.GetDirectoryName(path);
                    return !name.IsNullOrEmptyOrSpace() ? name : null;
                }
                if (path.IsExistFile())
                {
                    var name = System.IO.Path.GetFileNameWithoutExtension(path);
                    return !name.IsNullOrEmptyOrSpace() ? name : null;
                }
            }
            return null;

            #endregion
        }

        /// <summary>
        /// 二进制转换成图片
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static BitmapImage ToImage(this byte[] bytes)
        {
            #region 二进制转换成图片
            try
            {
                var bmp = new BitmapImage();
                bmp.SetSource(bytes.ToStream());
                return bmp;
            }
            catch
            {
                return null;
            }

            #endregion
        }

        /// <summary>
        /// Stream 转换为 image 图片
        /// </summary>
        /// <returns></returns>
        public static BitmapImage ToImage(this System.IO.Stream stream)
        {
            #region Stream 转换为 image 图片
            try
            {
                var bmp = new BitmapImage();
                bmp.SetSource(stream);
                return bmp;
            }
            catch
            {
                return null;
            }
            #endregion
        }

        /// <summary>
        /// 读取本地图片
        /// </summary>
        /// <param name="thisFilePath"></param>
        /// <returns></returns>
        public static BitmapImage ReadImage(this string thisFilePath)
        {
            #region 读取本地图片
            if (thisFilePath.IsExistFile())
            {
                try
                {
                    var bitmapImage = new BitmapImage();
                    bitmapImage.SetSource(thisFilePath.ReadStream());
                    return bitmapImage;
                }
                catch
                {
                    return null;
                }
            }
            return null;
            #endregion
        }


        /// <summary>
        /// 保存本地图片
        /// </summary>
        /// <param name="thisBitmapImage"></param>
        /// <param name="thisFilePath"></param>
        /// <returns></returns>
        public static bool WriteImage(this BitmapSource thisBitmapImage, string thisFilePath)
        {
            #region 保存本地图片
            using (var memoryStream = new System.IO.MemoryStream())
            {
                var wp = new WriteableBitmap(thisBitmapImage);
                wp.SaveJpeg(memoryStream, wp.PixelWidth, wp.PixelHeight, 0, 100);
                memoryStream.SaveStream(thisFilePath);
                return true;
            }
            #endregion
        }

        /// <summary>
        /// ResourceAddress + AssemblyNamespace -> ToResourceStream
        /// </summary>
        /// <param name="thisResourceAddress"></param>
        /// <param name="thisAssemblyNamespace"></param>
        /// <returns></returns>
        public static System.IO.Stream ToResourceStream(this string thisResourceAddress, string thisAssemblyNamespace = null)
        {
            #region ResourceAddress + AssemblyNamespace -> ToResourceStream
            //1.BuildAction属性常用的状态有三种，即Content|Resource|None。
            // BuildAction属性设置为Conten的文件将被作为独立文件直接打包在xap文件中
            // BuildAction属性设置为Resource的文件将被嵌入到xap包中的dll文件内
            // BuildAction属性设置为None的文件，将不会存以任何形式在于xap包中
            // 微软给出的解释是使用”Content”要比“Resource”性能上好一些。因为Windows Phone 7是为文件和网络流做了优化处理，但是Memory流却没有。
            // 设置成Content类型，这些文件将会作为独立的文件存在xap包中，如果设置为resource，他们会被编译到dll中。
            // 如果把这些文件设置为Resource方式的话，它们实际上在回放的时候还需要读取出来放到文件中，反而降低了性能。
            // 所以，如果你的程序中有大量的媒体文件的话，要想性能好，还是把它们的BuildAction设置为“Content”比较好一些。
            // 1.资源文件是只读的，无法进行写操作。
            if (thisResourceAddress.IsNotNullOrEmpty())
            {
                thisResourceAddress = thisResourceAddress.Replace("\\", "/");
                // 先按内容的方式读取
                var thisAddress = thisResourceAddress;
                var thisStreamResourceInfo = Application.GetResourceStream(thisAddress.ToRelativeUri());
                if (thisStreamResourceInfo != null && thisStreamResourceInfo.Stream != null)
                {
                    return thisStreamResourceInfo.Stream;
                }
                // 再按内嵌Resource的方式读取
                if (thisAssemblyNamespace.IsNullOrEmptyOrSpace())
                {
                    var declaringType = MethodBase.GetCurrentMethod().DeclaringType;
                    if (declaringType != null)
                    {
                        thisAssemblyNamespace = declaringType.Namespace;
                    }
                }
                thisAddress = string.Format("/{0};component/{1}", thisAssemblyNamespace, thisResourceAddress);
                thisStreamResourceInfo = Application.GetResourceStream(thisAddress.ToRelativeUri());
                if (thisStreamResourceInfo != null && thisStreamResourceInfo.Stream != null)
                {
                    return thisStreamResourceInfo.Stream;
                }
            }
            return null;
            #endregion
        }

        /// <summary>
        /// ResourceAddress + thisDeclaringType -> ToResourceStr
        /// </summary>
        /// <param name="thisResourceAddress"></param>
        /// <param name="thisDeclaringType"></param>
        /// <returns></returns>
        public static string ToResourceStr(this string thisResourceAddress, Type thisDeclaringType)
        {
            #region ResourceAddress + thisDeclaringType -> ToResourceStr

            if (thisDeclaringType != null)
            {
                if (thisDeclaringType.Assembly.FullName.IsNotNullOrEmpty() && thisDeclaringType.Assembly.FullName.Contains(","))
                {
                    var thisAssemblyNamespace = thisDeclaringType.Assembly.FullName.Split(',')[0];
                    if (thisAssemblyNamespace.IsNotNullOrEmpty())
                    {
                        var thisConfigKeyStream = thisResourceAddress.ToResourceStream(thisAssemblyNamespace);
                        if (thisConfigKeyStream != null)
                        {
                            var thisResourceStr = thisConfigKeyStream.ToStr();
                            if (thisResourceStr.IsNotNullOrEmpty())
                            {
                                return thisResourceStr;
                            }
                        }
                    }
                }
            }
            return null;

            #endregion
        }
        #endregion
    }
}
