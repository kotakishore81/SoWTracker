using Microsoft.Extensions.Configuration;
using SoW.Tracker.WebAPI.Utility.OtherUtilities.OtherUtilitiesInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.Utility.OtherUtilities.OtherUtilitiesImplementation
{
    [ExcludeFromCodeCoverage]
    public class UtilityFunctions : IUtilityFunctions
    {

       // private readonly ICacheProvider _cache;
        public UtilityFunctions()
        {
            //_cache = cache;
        }

        /// <summary>
        /// To get Enum Description from Value
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        /// eg. to call : GetEnumDescription<MyEnum>(1);
        //public string GetEnumDescription<TEnum>(int value)
        //{
        //    return Convert.ToString((Enum)(object)((TEnum)(object)value));
        //}

     
    }
    }
