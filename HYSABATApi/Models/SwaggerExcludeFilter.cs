using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HYSABATApi.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SwaggerExcludeAttribute : Attribute
    {
    }
    public class SwaggerExcludeFilter : ISchemaFilter

    {

        //public void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type)
        //{
        //    //if (schema?.properties == null || type == null)
        //    //    return;

        //    //var excludedProperties = type.GetProperties()
        //    //                             .Where(t =>
        //    //                                    t.GetCustomAttribute<SwaggerExcludeAttribute>()
        //    //                                    != null);

        //    //foreach (var excludedProperty in excludedProperties)
        //    //{
        //    //    if (schema.properties.ContainsKey(excludedProperty.Name))
        //    //        schema.properties.Remove(excludedProperty.Name);
        //    //}
        //}

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null || context.Type == null)
                return;
         
            if(typeof(PurchasePlan) == context.Type)
            {
                var excludedProperties = context.Type.GetProperties()
                             .Where(t =>
                                    t.GetCustomAttribute<SwaggerExcludeAttribute>()
                                    != null);   
                foreach (var excludedProperty in excludedProperties)
                {
                    var _prop = schema.Properties.Where(x => x.Key.Equals(excludedProperty.Name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    if(!string.IsNullOrEmpty(_prop.Key))
                    {
                        schema.Properties.Remove(_prop.Key);
                    }

                }
            }
      
        }
    }
}
