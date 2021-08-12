﻿using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace Demo.IdentityServer4.MVC
{
    public class Config
    {
        /// <summary>
        /// 1、微服务API资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api", "api需要被保护",new List<string> {"role","admin" })
            };
        }

        /// <summary>
        /// 2、客户端
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    // 没有交互性用户，使用 clientid/secret 实现认证。
                    // 1、client认证模式
                    // 2、client用户密码认证模式
                    // 3、授权码认证模式(code)
                    // 4、简单认证模式(js)
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // 用于认证的密码
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    // 客户端有权访问的范围（Scopes）
                    AllowedScopes = { "ServiceA", "ServiceB", "ServiceF" }
                },
                new Client
                {
                    ClientId = "client-password",
	                // 使用用户名密码交互式验证
	                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

	                // 用于认证的密码
	                ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
	                // 客户端有权访问的范围（Scopes）
	                AllowedScopes = { "TeamService" }
                },
                // openid客户端
                new Client
                {
                    ClientId="client-code",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.Code,
                    RequireConsent=false,
                    RequirePkce=true,

                    RedirectUris={ "https://localhost:5003/signin-oidc"}, // 1、客户端地址

                    PostLogoutRedirectUris={ "https://localhost:5003/signout-callback-oidc"},// 2、登录退出地址

                    AllowedScopes=new List<string>{
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api" // 启用服务授权支持
                    },

                    // 增加授权访问
                    AllowOfflineAccess=true,

                    //增加跨域
                    AllowedCorsOrigins={ "https://localhost:5003", "http://localhost:5002" }
                }
            };
        }

        /// <summary>
        /// 2.1 openid身份资源
        /// </summary>
        public static IEnumerable<IdentityResource> Ids => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

        /// <summary>
        /// 3、测试用户
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
                new TestUser
                {
                    SubjectId="1",
                    Username="tony",
                    Password="123456"
                },
                // openid 身份验证
                new TestUser{SubjectId = "818727", Username = "tony-1", Password = "123456",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "tony-1"),
                        new Claim(JwtClaimTypes.GivenName, "tony-1"),
                        new Claim(JwtClaimTypes.FamilyName, "tony-1"),
                        new Claim(JwtClaimTypes.Email, "tony-1@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://tony-1.com"),
                      //  new Claim(JwtClaimTypes.Address, @"{ '城市': '杭州', '邮政编码': '310000' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }
                }
            };
        }

        public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("ServiceA", "微服务A"),
            new ApiScope("ServiceB", "微服务B"),
            new ApiScope("ServiceD", "微服务D"),
            new ApiScope("ServiceF", "微服务F")
        };
    }
}
