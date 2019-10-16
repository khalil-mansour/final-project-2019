﻿using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories
{
    public sealed class FileFetchAllRepoResponse : BaseGatewayResponse
    {
        public IEnumerable<File> Files { get; }

        public FileFetchAllRepoResponse(IEnumerable<File> files = null, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
            Files = files;
        }
    }
}