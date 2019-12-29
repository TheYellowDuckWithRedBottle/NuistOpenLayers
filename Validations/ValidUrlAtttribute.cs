using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XinYiThree.Validations
{
    /// <summary>
    /// 自定义的校验url的类
    /// </summary>
    public class ValidUrlAtttribute : Attribute, IModelValidator//
    {
        public string ErrorMessage { get; set; }
        public IEnumerable<ModelValidationResult> Validate(
            ModelValidationContext context)
        {
            var url = context.Model as string;
            if (url != null && Uri.IsWellFormedUriString(url, UriKind.Absolute))//绝对路路径
            {
                return Enumerable.Empty<ModelValidationResult>();
            }
            return new List<ModelValidationResult>
            {
                new ModelValidationResult(string.Empty,ErrorMessage)
            };
        }
    }
}
