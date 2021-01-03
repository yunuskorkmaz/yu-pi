using System;
using System.Linq;
using System.Reflection;
using Core.Validations;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions
{
    public static class ValidationExtension
    {
        public static IMvcBuilder AddValidation(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddFluentValidation(fv => {
               fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>();
            });
            return mvcBuilder;
        }
    }
}