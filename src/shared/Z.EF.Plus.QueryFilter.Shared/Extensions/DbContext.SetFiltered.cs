﻿// Description: Entity Framework Bulk Operations & Utilities (EF Bulk SaveChanges, Insert, Update, Delete, Merge | LINQ Query Cache, Deferred, Filter, IncludeFilter, IncludeOptimize | Audit)
// Website & Documentation: https://github.com/zzzprojects/Entity-Framework-Plus
// Forum & Issues: https://github.com/zzzprojects/EntityFramework-Plus/issues
// License: https://github.com/zzzprojects/EntityFramework-Plus/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.


#if EFCORE
using System;
using System.Linq;
#if EF5 || EF6
using System.Data.Entity;

#elif EFCORE
using Microsoft.EntityFrameworkCore;

#endif

#if EF6
using AliasBaseQueryFilter = Z.EntityFramework.Plus.BaseQueryDbSetFilter;
using AliasBaseQueryFilterQueryable = Z.EntityFramework.Plus.BaseQueryDbSetFilterQueryable;
using AliasQueryFilterContext = Z.EntityFramework.Plus.QueryDbSetFilterContext;
using AliasQueryFilterManager = Z.EntityFramework.Plus.QueryDbSetFilterManager;
using AliasQueryFilterSet = Z.EntityFramework.Plus.QueryDbSetFilterSet;
#else
using AliasBaseQueryFilter = Z.EntityFramework.Plus.BaseQueryFilter;
using AliasBaseQueryFilterQueryable = Z.EntityFramework.Plus.BaseQueryFilterQueryable;
using AliasQueryFilterContext = Z.EntityFramework.Plus.QueryFilterContext;
using AliasQueryFilterManager = Z.EntityFramework.Plus.QueryFilterManager;
using AliasQueryFilterSet = Z.EntityFramework.Plus.QueryFilterSet;
#endif

namespace Z.EntityFramework.Plus
{
#if EF6
    public static partial class QueryDbSetFilterExtensions
#else
    public static partial class QueryFilterExtensions
#endif
    {
#if EF6
        public static IQueryable<T> WithDbSetFiltered<T>(this DbContext context) where T : class
#else
        public static IQueryable<T> SetFiltered<T>(this DbContext context) where T : class
#endif
        {
            var filterContext = AliasQueryFilterManager.AddOrGetFilterContext(context);

            if (filterContext.FilterSetByType.ContainsKey(typeof(T)))
            {
                var set = filterContext.FilterSetByType[typeof(T)];

                if (set.Count == 1)
                {
                    return (IQueryable<T>) set[0].DbSetProperty.GetValue(context);
                }
                throw new Exception(ExceptionMessage.QueryFilter_SetFilteredManyFound);
            }

            throw new Exception(ExceptionMessage.QueryFilter_SetFilteredNotFound);
        }
    }
}

#endif