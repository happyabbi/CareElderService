using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using ReflectSoftware.Facebook.Messenger.Client;
using ReflectSoftware.Facebook.Messenger.Common.Models.Client;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Facebook;
using ReflectSoftware.Facebook.Messenger.Common.Models;
using Newtonsoft.Json.Linq;
using FileUploadsInAspNetMvc.Models;
using ReflectSoftware.Facebook.Messenger.Common.Interfaces;

namespace FileUploadsInAspNetMvc.Helper
{
    public class MyClientMessenger : ClientMessenger
    {

        private readonly string _apiVersion;

        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public MyClientMessenger(string accessToken, string version = "2.8") : base(accessToken)
        {

            _apiVersion = version;

            SetJsonSerializers();

            _jsonSerializerSettings = new JsonSerializerSettings();
        }


        /// <summary>
        /// Sets the json serializer.
        /// </summary>
        private void SetJsonSerializers()
        {

            SetJsonSerializers((obj) =>
            {

                var value = JsonConvert.SerializeObject(obj, _jsonSerializerSettings);

                return value;

            },
            (value, type) =>
            {

                return type == null ? JsonConvert.DeserializeObject(value, _jsonSerializerSettings) : JsonConvert.DeserializeObject(value, type, _jsonSerializerSettings);

            });

        }



        private ResultError CreateResultError(JObject value)

        {

            var result = (ResultError)null;

            if (value.Property("error") != null)

            {

                var error = (JObject)value.Property("error").Value;

                result = new ResultError()

                {

                    Message = error.Value<string>("message"),

                    Code = error.Value<int>("code"),

                    ErrorSubcode = error.Value<int>("error_subcode"),

                    FBTraceId = error.Value<string>("fbtrace_id"),

                    Type = error.Value<string>("type")

                };

            }

            return result;

        }

        public async Task<MessageResult> SendTemplateAttachmentAsync(string userId, IAttachment attachment)
        {

            var result = new MessageResult();




            try
            {

                using (var client = new HttpClient())

                {

                    using (var content = new MultipartFormDataContent())

                    {

                        var recipient = JsonConvert.SerializeObject(new Identity(userId));
                        var message = JsonConvert.SerializeObject(new AttachmentMessage(attachment));



                        content.Add(new StringContent(recipient), "recipient");

                        content.Add(new StringContent(message), "message");









                        using (var response = await client.PostAsync($"{"https"}://graph.facebook.com/v{_apiVersion}/me/messages?access_token={AccessToken}", content))
                        {

                            var returnValue = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());


                            result.Error = CreateResultError(returnValue);

                            if (result.Error == null)

                            {

                                result.RecipientId = returnValue.Value<string>("recipient_id");

                                result.MessageId = returnValue.Value<string>("message_id");

                                result.Success = true;

                            }

                        }

                    }

                }

            }

            catch (WebExceptionWrapper ex)

            {

                result.Message = ex.Message;

                result.Error = new ResultError

                {

                    Type = "Bad connection",

                    Code = -1000,

                };

            }

            catch (Exception ex)

            {

                result.Message = ex.Message;

            }

            return result;

        }

    }



}