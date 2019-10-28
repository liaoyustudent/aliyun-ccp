using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using Aliyun.Credentials;
using Aliyun.SDK.CCP.Credentials;
using Aliyun.SDK.CCP.Utils;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Tea;

namespace Aliyun.SDK.CCP
{
    public class BaseClient
    {
        public static string URL_ENCODING = "UTF-8";
        private static string ALGORITHM_NAME = "HmacSHA1";
        protected string _domainId;
        protected string _protocol;
        protected string _endpoint;
        protected string _userId;
        protected Dictionary<string, object> _config;
        protected AccessKeyCredential _accessKeyCredential;
        protected AccessTokenCredential _accessTokenCredential;

        public BaseClient(Dictionary<string, object> config)
        {
            _domainId = DictUtils.GetDicValue(config, "DomainId").ToSafeString();
            _protocol = DictUtils.GetDicValue(config, "Protocol").ToSafeString();
            _endpoint = DictUtils.GetDicValue(config, "Endpoint").ToSafeString();
            _userId=DictUtils.GetDicValue(config, "UserId").ToSafeString();
            SetCredential(config);
            this._config = config;
        }

        private void SetCredential(Dictionary<string, object> config)
        {
            string accessKeyId = DictUtils.GetDicValue(config, "AccessKeyId").ToSafeString();
            string accessKeySecret = DictUtils.GetDicValue(config, "AccessKeySecret").ToSafeString();
            string refreshToken = DictUtils.GetDicValue(config, "RefreshToken").ToSafeString();
            if (!string.IsNullOrWhiteSpace(accessKeyId) && !string.IsNullOrWhiteSpace(accessKeySecret))
            {
                _accessKeyCredential = new AccessKeyCredential(accessKeyId, accessKeySecret);
            }
            else if (!string.IsNullOrWhiteSpace(refreshToken))
            {
                _accessTokenCredential = new AccessTokenCredential(config);
            }
        }

        protected string _getAccessKeyId()
        {
            if (_accessKeyCredential != null)
            {
                return _accessKeyCredential.AccessKeyId;
            }
            else
            {
                return null;
            }

        }

        protected string _getAccessKeySecret()
        {
            if (_accessKeyCredential != null)
            {
                return _accessKeyCredential.AccessKeySecret;
            }
            else
            {
                return null;
            }

        }

        protected string _getAccessToken()
        {
            string accessToken = null;
            if (null != _accessTokenCredential)
            {
                accessToken = this._accessTokenCredential.AccessToken;
            }
            return accessToken;
        }

        protected string _getProtocol(string _protocol, string protocol)
        {
            if (_notEmpty(_protocol))
            {
                return _protocol;
            }
            return protocol;
        }

        protected string _getHost(string _endpoint, string endpoint)
        {
            if (_notEmpty(_endpoint))
            {
                return _endpoint;
            }
            return endpoint;
        }

        protected bool _notEmpty(string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        protected string _getRFC2616Date()
        {
            return DateTime.Now.ToUniversalTime().GetDateTimeFormats('r') [0];
        }

        protected string _getSignature(TeaRequest request)
        {
            string method = request.Method;
            string pathname = request.Pathname;
            Dictionary<string, string> headers = request.Headers;
            Dictionary<string, string> query = request.Query;
            string accept = headers.ContainsKey("accept") ? headers["accept"] : "";
            string contentMD5 = headers.ContainsKey("content-md5") ? headers["content-md5"] : "";
            string contentType = headers.ContainsKey("content-type") ? headers["content-type"] : "";
            string date = headers.ContainsKey("date") ? headers["date"] : "";
            string header = method + "\n" + accept + "\n" + contentMD5 + "\n" + contentType + "\n" + date;
            string canonicalizedHeaders = getCanonicalizedHeaders(headers);
            string canonicalizedResource = getCanonicalizedResource(pathname, query);
            string stringToSign = header + "\n" + canonicalizedHeaders + "\n" + canonicalizedResource;
            byte[] signData;
            using(KeyedHashAlgorithm algorithm = CryptoConfig.CreateFromName("HMACSHA1") as KeyedHashAlgorithm)
            {
                algorithm.Key = Encoding.UTF8.GetBytes(_getAccessKeySecret());
                signData = algorithm.ComputeHash(Encoding.UTF8.GetBytes(stringToSign.ToCharArray()));
            }
            return Convert.ToBase64String(signData);
        }

        protected bool _isStatusCode(TeaResponse response, int code)
        {
            return response.StatusCode == code;
        }

        protected string _toJSONString(TeaModel request)
        {
            return JsonConvert.SerializeObject(request);
        }

        protected Dictionary<string, object> _readAsJSON(TeaResponse response)
        {
            string body = TeaCore.GetResponseBody(response);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            Dictionary<string, object> dicBody = JsonConvert.DeserializeObject<Dictionary<string, object>>(body);
            dic = ObjToDictionary(dicBody);
            return dic;
        }

        protected Dictionary<string, string> _toQuery(TeaModel request)
        {
            Dictionary<string, object> dicRequest = request.ToMap();
            Dictionary<string, string> dicQuery = new Dictionary<string, string>();
            foreach (KeyValuePair<string, object> item in dicRequest)
            {
                if (item.Value != null)
                {
                    dicQuery.Add(item.Key, item.Value.ToString());
                }
                else
                {
                    dicQuery.Add(item.Key, null);
                }
            }
            return dicQuery;
        }
        public String getCanonicalizedHeaders(Dictionary<String, String> headers)
        {
            String prefix = "x-acs-";
            Dictionary<string, string>.KeyCollection keys = headers.Keys;
            List<string> canonicalizedKeys = new List<string>();
            canonicalizedKeys = headers.Where(p => p.Key.StartsWith(prefix))
                .Select(p => p.Key).ToList();
            canonicalizedKeys.Sort();
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < canonicalizedKeys.Count; i++)
            {
                if (i > 0)
                {
                    result.Append("\n");
                }
                String key = canonicalizedKeys[i];
                result.Append(key);
                result.Append(":");
                result.Append(headers[key].Trim());
            }
            return result.ToString();
        }

        public String getCanonicalizedResource(String pathname, Dictionary<String, String> query)
        {
            if (query == null || query.Count <= 0)
            {
                return pathname;
            }
            List<string> keys = query.Keys.ToList();
            StringBuilder result = new StringBuilder(pathname);
            result.Append("?");
            String key;
            for (int i = 0; i < keys.Count; i++)
            {
                key = keys[i];
                result.Append(key);
                result.Append("=");
                result.Append(query[key]);
            }
            return result.ToString();
        }

        public Dictionary<string, object> ObjToDictionary(Dictionary<string, object> dicObj)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (string key in dicObj.Keys)
            {
                if (dicObj[key] is JArray)
                {
                    List<Dictionary<string, object>> dicObjList = ((JArray) dicObj[key]).ToObject<List<Dictionary<string, object>>>();
                    List<Dictionary<string, object>> dicList = new List<Dictionary<string, object>>();
                    foreach (Dictionary<string, object> objItem in dicObjList)
                    {
                        dicList.Add(ObjToDictionary(objItem));
                    }
                    dic.Add(key, dicList);
                }
                else if (dicObj[key] is JObject)
                {
                    Dictionary<string, object> dicJObj = ((JObject) dicObj[key]).ToObject<Dictionary<string, object>>();
                    dic.Add(key, dicJObj);
                }
                else
                {
                    dic.Add(key, dicObj[key]);
                }
            }
            return dic;
        }

        public string _default(string strValue, string strDefault)
        {
            if (string.IsNullOrWhiteSpace(strValue))
            {
                return strDefault;
            }
            return strValue;
        }

        public int _defaultNumber(int numValue, int numDefault)
        {
            if (numValue > 0)
            {
                return numValue;
            }
            return numDefault;
        }
    }
}