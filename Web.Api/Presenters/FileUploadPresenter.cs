﻿using Newtonsoft.Json;
using System.Net;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Presenters
{
    public class FileUploadPresenter : IOutputPort<FileUploadResponse>
    {
        public JsonContentResult ContentResult { get; }

        public FileUploadPresenter()
        {
            ContentResult = new JsonContentResult();
        }


        public void Handle(FileUploadResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonConvert.SerializeObject(response);
        }
    }
}
