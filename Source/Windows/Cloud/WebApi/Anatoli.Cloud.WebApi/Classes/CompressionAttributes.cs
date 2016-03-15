﻿using System.IO;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Anatoli.Cloud.WebApi
{
    public class CompressionHelper
    {
        public static byte[] DeflateByte(byte[] str)
        {
            if (str == null)
                return null;

            using (var output = new MemoryStream())
            {
                using (var compressor = new Ionic.Zlib.DeflateStream(output, Ionic.Zlib.CompressionMode.Compress, Ionic.Zlib.CompressionLevel.BestCompression))
                {
                    compressor.Write(str, 0, str.Length);
                }

                return output.ToArray();
            }
        }
        public static byte[] GzipByte(byte[] str)
        {
            if (str == null)
                return null;

            using (var output = new MemoryStream())
            {
                using (var compressor = new Ionic.Zlib.GZipStream(output, Ionic.Zlib.CompressionMode.Compress, Ionic.Zlib.CompressionLevel.BestCompression))
                {
                    compressor.Write(str, 0, str.Length);
                }

                return output.ToArray();
            }
        }
    }

    public class DeflateCompressionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actContext)
        {
            var content = actContext.Response.Content;
            var bytes = content == null ? null : content.ReadAsByteArrayAsync().Result;
            var zlibbedContent = bytes == null ? new byte[0] :

            CompressionHelper.DeflateByte(bytes);

            actContext.Response.Content = new ByteArrayContent(zlibbedContent);

            actContext.Response.Content.Headers.Remove("Content-Type");

            actContext.Response.Content.Headers.Add("Content-encoding", "deflate");

            actContext.Response.Content.Headers.Add("Content-Type", "application/json");

            base.OnActionExecuted(actContext);
        }
    }
    public class GzipCompressionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actContext)
        {
            var content = actContext.Response.Content;
            var bytes = content == null ? null : content.ReadAsByteArrayAsync().Result;
            var zlibbedContent = bytes == null ? new byte[0] :

            CompressionHelper.GzipByte(bytes);

            actContext.Response.Content = new ByteArrayContent(zlibbedContent);

            actContext.Response.Content.Headers.Remove("Content-Type");

            actContext.Response.Content.Headers.Add("Content-encoding", "gzip");

            actContext.Response.Content.Headers.Add("Content-Type", "application/json");

            base.OnActionExecuted(actContext);
        }
    }

}