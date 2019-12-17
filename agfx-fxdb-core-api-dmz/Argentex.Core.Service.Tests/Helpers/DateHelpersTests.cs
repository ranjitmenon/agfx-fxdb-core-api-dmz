using System;
using System.Collections.Generic;
using Argentex.Core.Service.Helpers;
using Xunit;

namespace Argentex.Core.Service.Tests.Helpers
{
    public class DateHelpersTests
    {
             
        public void GetDaysDifferencreBetween_Should_Give_The_Days_Difference_Between_Two_Dates(DateTime first, DateTime other, int expected)
        {
            // Given method parameters
            
            // When
            var result = DateHelpers.GetDaysDifferencreBetween(first, other);

            // Then
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> TestData => 
        new List<object[]>
        {
            new object[] {DateTime.Today, DateTime.Today.AddDays(-5), 5},
            new object[] {DateTime.Today, DateTime.Today.AddDays(5), 5},
            new object[] {DateTime.Today, DateTime.Today.AddYears(1), 365},
            new object[] {DateTime.Now, DateTime.Now, 0},
            new object[] {DateTime.Now, DateTime.Today, 0},
            new object[] {DateTime.Now, DateTime.Today.AddHours(18), 0}
        };
    }
}
