using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace mysqltest.Core.Providers
{
    public class EnumEntityBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Ignore if the type is neither nullable<enum> nor enum
            var targetType = context.Metadata.ModelType;
            if (false == targetType.IsEnum)
            {
                targetType = Nullable.GetUnderlyingType(targetType);
                if (targetType == null || false == targetType.IsEnum)
                    return null;
            }

            return new EnumModelBinder();
        }
    }

    public class EnumModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var targetType = bindingContext.ModelType;

            var isNullable = Nullable.GetUnderlyingType(targetType) != null;
            // In case it's a nullable enum, get the underlying type
            if (isNullable)
                targetType = Nullable.GetUnderlyingType(targetType);

            string rawData = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
            if (string.IsNullOrWhiteSpace(rawData))
            {
                if (isNullable)
                {
                    bindingContext.Result = ModelBindingResult.Success(null);
                    return Task.CompletedTask;
                }
                else
                {
                    bindingContext.Result = ModelBindingResult.Success(0);
                    return Task.CompletedTask;
                }
            }

            if (TryParseEnumFromAttribute(rawData, true, targetType, out var result, out var errMsg))
                bindingContext.Result = ModelBindingResult.Success(result);
            else
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, $"{errMsg} for '{bindingContext.ModelName}'.");

            return Task.CompletedTask;
        }

        protected static bool TryParseEnumFromAttribute(string rawValue, bool caseInsensitive, Type enumType, out Enum value, out string errMsg)
        {
            errMsg = null;

            var strValues = rawValue.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                    .Select(x => x.Trim())
                                    .ToList();

            var isFlagEnum = enumType.IsDefined(typeof(FlagsAttribute), false);
            List<Enum> flagValues = new List<Enum>();

            foreach (var str in strValues)
            {
                bool isParsed = false;
                foreach (var name in Enum.GetNames(enumType))
                {
                    var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name)
                                                                              .GetCustomAttributes(typeof(EnumMemberAttribute), true)).SingleOrDefault();

                    // Missing Enum Member Attribute? Revert to default Enum Parsing
                    if (enumMemberAttribute == null)
                    {
                        if (Enum.TryParse(enumType, str, caseInsensitive, out var parsedValue))
                        {
                            if (isFlagEnum)
                            {
                                flagValues.Add((Enum)parsedValue);
                                isParsed = true;
                                break;
                            }
                            else
                            {
                                value = (Enum)parsedValue;
                                return true;
                            }
                        }
                    }
                    else
                    if (caseInsensitive == true)
                    {
                        if (enumMemberAttribute.Value.ToUpperInvariant() == str?.ToUpperInvariant())
                        {
                            if (isFlagEnum)
                            {
                                flagValues.Add((Enum)Enum.Parse(enumType, name));
                                isParsed = true;
                                break;
                            }
                            else
                            {
                                value = (Enum)Enum.Parse(enumType, name);
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (enumMemberAttribute.Value == str)
                        {
                            if (isFlagEnum)
                            {
                                flagValues.Add((Enum)Enum.Parse(enumType, name));
                                isParsed = true;
                                break;
                            }
                            else
                            {
                                value = (Enum)Enum.Parse(enumType, name);
                                return true;
                            }
                        }
                    }
                }

                if (isParsed)
                    continue;

                // IF a value was not matched, return error
                errMsg = $"'{str}' is not a valid value";
                value = default;
                return false;
            }

            // No entry
            if (flagValues.Count == 0)
            {
                errMsg = "Empty value is not valid";
                value = null;
                return false;
            }

            // Only reaches this line if all parts have matched the flag
            value = flagValues.First();
            foreach (var entry in flagValues.Skip(1))
                value = EnumOr(value, entry);

            return true;
        }

        private static Enum EnumOr(Enum a, Enum b)
        {
            // consider adding argument validation here

            if (Enum.GetUnderlyingType(a.GetType()) != typeof(ulong))
                return (Enum)Enum.ToObject(a.GetType(), Convert.ToInt64(a) | Convert.ToInt64(b));
            else
                return (Enum)Enum.ToObject(a.GetType(), Convert.ToUInt64(a) | Convert.ToUInt64(b));
        }
    }
}