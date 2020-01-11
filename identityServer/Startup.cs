// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace IdentityServer4
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }

        public Startup(IWebHostEnvironment environment)
        {
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // uncomment, if you want to add an MVC-based UI
            //services.AddControllersWithViews();

            var builder = services.AddIdentityServer()
                //.AddInMemoryIdentityResources(Config.Ids)
                .AddInMemoryApiResources(Config.Apis)
                .AddInMemoryClients(Config.Clients)
                .AddTestUsers(new List<IdentityServer4.Test.TestUser>() {
                    new IdentityServer4.Test.TestUser(){
                        Username="Erffun",
                        IsActive=true,
                        Password="123456",
                        SubjectId=Guid.NewGuid().ToString(),
                        Claims = new List<System.Security.Claims.Claim>(){
                            new System.Security.Claims.Claim("name","Erffun"),
                            new System.Security.Claims.Claim("lastName","Havaasi"),
                            new System.Security.Claims.Claim("email","erff@gas.sdf"),
                        }
                    },
                    new IdentityServer4.Test.TestUser(){
                        Username="admin",
                        IsActive=true,
                        Password="admin",
                        SubjectId=Guid.NewGuid().ToString(),
                        Claims = new List<System.Security.Claims.Claim>(){
                            new System.Security.Claims.Claim("name","admin"),
                            new System.Security.Claims.Claim("lastName","adminL"),
                            new System.Security.Claims.Claim("email","ad@min.sdf"),
                        }
                    }
                }); ;

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // uncomment if you want to add MVC
            //app.UseStaticFiles();
            //app.UseRouting();

            app.UseIdentityServer();

            // uncomment, if you want to add MVC
            //app.UseAuthorization();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapDefaultControllerRoute();
            //});
        }
    }
}
