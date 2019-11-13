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
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("uid")]
        public string UserId { get; set; }

        [JsonProperty("document_type_id")]
        public int DocumentType { get; set; }

        [JsonProperty("storage_file_id")]
        public string StorageId { get; set; }

        [JsonProperty("created_date")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }

        [JsonProperty("user_file_name")]
        public string FileName { get; set; }

        [JsonProperty("signed_url")]
        public string SignedUrl { get; set; }


        public FileResponse() { }

        public static string ToJson(File file)
        {
            var response = new FileResponse
            {
                Id = file.Id,
                UserId = file.UserId,
                FileName = file.FileName,
                StorageId = file.StorageId,
                Visible = file.Visible,
                CreatedDate = file.CreatedDate,
                DocumentType = file.DocumentType,
                SignedUrl = file.Url
            };

            return JsonConvert.SerializeObject(response);
        }

        public static string ToJson(IEnumerable<File> files)
        {
            List<FileResponse> responses = new List<FileResponse>();

            foreach (File file in files)
            {
                var response = new FileResponse
                {
                    Id = file.Id,
                    UserId = file.UserId,
                    FileName = file.FileName,
                    StorageId = file.StorageId,
                    Visible = file.Visible,
                    CreatedDate = file.CreatedDate,
                    DocumentType = file.DocumentType,
                    SignedUrl = file.Url
                };

                responses.Add(response);
            }

            return JsonConvert.SerializeObject(responses);
        }

    }
}
