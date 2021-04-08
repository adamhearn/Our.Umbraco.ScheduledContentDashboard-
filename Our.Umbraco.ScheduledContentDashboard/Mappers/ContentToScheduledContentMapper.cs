﻿//-----------------------------------------------------------------------------
// 2021 Our Umbraco
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.ScheduledContentDashboard.Contracts;
using Our.Umbraco.ScheduledContentDashboard.Models;
using Umbraco.Core.Models;

namespace Our.Umbraco.ScheduledContentDashboard.Mappers
{
    /// <summary>
    /// Implementation of an <see cref="IObjectMapper{TFrom, TTo}"/> in support of mapping between Umbraco content and Scheduled Results
    /// </summary>
    public class ContentToScheduledContentMapper : IObjectMapper<Tuple<ContentScheduleAction, IEnumerable<IContent>>, IEnumerable<ScheduledContentModel>>
    {
        /// <summary>
        /// Map from one instance of an object to another
        /// </summary>
        /// <param name="from">Object instance to convert from</param>
        /// <returns>Mapped object</returns>
        public IEnumerable<ScheduledContentModel> Map( Tuple<ContentScheduleAction, IEnumerable<IContent>> from )
        {
            // Project the results based on the request into the required model
            return from.Item2.Select( x => new ScheduledContentModel()
            {
                ContentId = x.Id,
                Name = x.Name,
                Action = from.Item1.ToString(),
                ScheduledDate = DateTime.SpecifyKind( x.ContentSchedule?.FullSchedule?.FirstOrDefault( a => a.Action == from.Item1 )?.Date ?? default, DateTimeKind.Local )
            } );
        }
    }
}