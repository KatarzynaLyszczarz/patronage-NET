//https://exceptionnotfound.net/using-middleware-to-log-requests-and-responses-in-asp-net-core/

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz.Middlewares
{
    public class Logger
    {
        private readonly RequestDelegate next;
        private readonly string path = "log.txt";
        private FileStream file;
        long maxsize = 1000 * 1024;

        public Logger(RequestDelegate next, string path, long maxsize)
        {
            this.path = path;
            this.next = next;
            this.maxsize = maxsize;
            file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            file.Seek(0, SeekOrigin.End);
        }

        ~Logger()
        {
            file.Close();
        }

        public async Task Invoke(HttpContext context)
        {
            //First, get the incoming request
            var request = await FormatRequest(context.Request);

            //Copy a pointer to the original response body stream
            var originalBodyStream = context.Response.Body;

            //Create a new memory stream...
            using (var responseBody = new MemoryStream())
            {
                //...and use that for the temporary response body
                context.Response.Body = responseBody;

                //Continue down the Middleware pipeline, eventually returning to this class
                await next(context);

                //Format the response from the server
                var response = await FormatResponse(context.Response);

                //TODO: Save log to chosen datastore
                if (file.Position >= maxsize)
                {
                    file.Position = 0;
                }
                using (StreamWriter w = new StreamWriter(file, Encoding.UTF8, 1024, true)) 
                {
                    w.WriteLine(DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss"));
                    w.WriteLine(request);
                    w.WriteLine(response);
                }

                //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;

            //This line allows us to set the reader for the request back at the beginning of its stream.
            request.EnableRewind();

            //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            //...Then we copy the entire request stream into the new buffer.
            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            //We convert the byte[] into a string using UTF8 encoding...
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            //..and finally, assign the read body back to the request body, which is allowed because of EnableRewind()
            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            string text = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);

            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return $"{response.StatusCode}: {text}";
        }
    }
}
