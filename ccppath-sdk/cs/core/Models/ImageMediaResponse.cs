// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections.Generic;
using System.IO;

using Tea;

namespace Aliyun.SDK.CCP.CCPClient.Models
{
    /**
     * 
     */
    public class ImageMediaResponse : TeaModel {
        [NameInMap("height")]
        [Validation(Required=false)]
        public long Height { get; set; }

        [NameInMap("time")]
        [Validation(Required=false)]
        public string Time { get; set; }

        [NameInMap("width")]
        [Validation(Required=false)]
        public long Width { get; set; }

    }

}
