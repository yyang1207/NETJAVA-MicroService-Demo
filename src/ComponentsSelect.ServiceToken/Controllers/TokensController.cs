using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComponentsSelectTest.ServiceToken.Controllers
{
    /// <summary>
    /// token控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IConfiguration _Configuration;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置信息</param>
        public TokensController(IConfiguration configuration) 
        {
            _Configuration = configuration;
        }

        /// <summary>
        /// post方式获取token
        /// </summary>
        /// <param name="infos">client凭证模式参数</param>
        /// <returns>返回结果</returns>
        [HttpPost]
        public async Task Post([FromBody] ClientCredentialsRequest infos)
        {
            (bool success, string result) = await GetToken(infos.ClientId, infos.Secret, infos.Scopes);

            string respJson = JsonSerializer.Serialize(new AccessTokenResponse() { Success = success, ErrorMsg = success ? "" : result, AccessToken = success ? result : "" });

            await Response.BodyWriter.WriteAsync(new ReadOnlyMemory<byte>(System.Text.Encoding.UTF8.GetBytes(respJson)));
        }

        /// <summary>
        /// get方式获取token
        /// </summary>
        /// <param name="clientid">客户端id</param>
        /// <param name="secret">秘钥</param>
        /// <param name="scopes">需要的scopes</param>
        /// <returns>返回结果</returns>
        [HttpGet]
        public  async Task<string> Get(string clientid, string secret, string scopes)
        {
            (bool success,string result) = await GetToken(clientid, secret, scopes);

            return JsonSerializer.Serialize(new AccessTokenResponse() { Success=success,ErrorMsg=success?"":result,AccessToken=success?result:"" });
        }

        private async ValueTask<(bool,string)> GetToken(string clientid, string secret, string scopes)
        {
            string idsUrl = _Configuration.GetSection("IdentityServerUrl").Value;
            if (string.IsNullOrEmpty(idsUrl)) return (false,"身份认证服务不能为空");

            HttpClient client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(idsUrl);
            if (disco.IsError) return (false,$"获取tokenUrl失败:{disco.Error}");

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = clientid,
                ClientSecret = secret,
                Scope = scopes
            });

            if (tokenResponse.IsError) return (false, $"获取token失败:{tokenResponse.Error}");

            return (true, tokenResponse.AccessToken);
        }
    }
}
