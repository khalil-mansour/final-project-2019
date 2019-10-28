using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Models.Response
{
    public class FileResponse
    {
        [JsonProperty("uid")]
        public string UserId { get; set; }

        [JsonProperty("document_type_id")]
        public int DocumentType { get; set; }

        [JsonProperty("storage_file_id")]
        public string StorageId { get; set; }

        [JsonProperty("created_date")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("visiable")]
        public bool Visible { get; set; }

        [JsonProperty("user_file_name")]
        public string FileName { get; set; }

        public FileResponse() { }

        public static string ToJson(File file)
        {
            var response = new FileResponse();
            response.UserId = file.UserId;
            response.FileName = file.FileName;
            response.StorageId = file.StorageId;
            response.Visible = file.Visible;
            response.CreatedDate = file.CreatedDate;
            response.DocumentType = file.DocumentType;

            return JsonConvert.SerializeObject(response);
        }

        public static string ToJson(IEnumerable<File> files)
        {
            List<FileResponse> responses = new List<FileResponse>();

            foreach (File file in files)
            {
                var response = new FileResponse();
                response.UserId = file.UserId;
                response.FileName = file.FileName;
                response.StorageId = file.StorageId;
                response.Visible = file.Visible;
                response.CreatedDate = file.CreatedDate;
                response.DocumentType = file.DocumentType;

                responses.Add(response);
            }

            return JsonConvert.SerializeObject(responses);
        }

    }
}
