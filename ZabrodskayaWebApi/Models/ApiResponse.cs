using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ZabrodskayaWebApi.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public string Json { get; set; }

        public ApiResponse(bool success = false, string message = "", object jsonData = null)
        {
            this.Success = success;
            this.Message = message;
            if (jsonData != null)
            {
                var options = new JsonSerializerOptions()
                {
                    MaxDepth = 10000,
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                this.Json = JsonSerializer.Serialize(jsonData, options);
            }
        }
    }
}
