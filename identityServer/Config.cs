// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer4
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            { new ApiResource("erffapi","Erff Api") };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client(){
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "erffapi" }
                },
                new Client{
                    ClientId="api",
                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    },
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    RequireConsent=false,
                    RequirePkce=true,
                    //RedirectUris={ }
                    AllowedScopes = new List<string>
            {"erffapi",
                IdentityServerConstants.StandardScopes.Profile
            }


                }
            };

    }
}