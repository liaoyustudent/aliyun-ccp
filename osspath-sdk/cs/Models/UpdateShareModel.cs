// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections.Generic;
using System.IO;

using Tea;

namespace Aliyun.SDK.CCP.OSSClient.Models
{
    /**
     * 列举指定用户的共享
     * @tags share
     * @error InvalidParameter The input parameter {parameter_name} is not valid.
     * @error AccessTokenInvalid AccessToken is invalid. {message}
     * @error ForbiddenNoPermission No Permission to access resource {resource_name}.
     * @error InternalError The request has been failed due to some unknown error.
     * @error ServiceUnavailable The request has failed due to a temporary failure of the server.
     */
    public class UpdateShareModel : TeaModel {
        [NameInMap("headers")]
        [Validation(Required=false)]
        public Dictionary<string, string> Headers { get; set; }

        [NameInMap("body")]
        [Validation(Required=true)]
        public UpdateShareResponse Body { get; set; }

    }

}