﻿using System.Collections.Generic;
using RestSharp;
using static SeatsioDotNet.Util.RestUtil;

namespace SeatsioDotNet.Events
{
    public class EventReports
    {
        private readonly RestClient _restClient;

        public EventReports(RestClient restClient)
        {
            _restClient = restClient;
        }

        public Dictionary<string, IEnumerable<EventReportItem>> ByLabel(string eventKey)
        {
            return FetchReport("byLabel", eventKey);
        }  
        
        public IEnumerable<EventReportItem> ByLabel(string eventKey, string label)
        {
            return FetchReport("byLabel", eventKey, label);
        }

        public Dictionary<string, IEnumerable<EventReportItem>> ByStatus(string eventKey)
        {
            return FetchReport("byStatus", eventKey);
        }

        public IEnumerable<EventReportItem> ByStatus(string eventKey, string status)
        {
            return FetchReport("byStatus", eventKey, status);
        } 
        
        public Dictionary<string, IEnumerable<EventReportItem>> ByCategoryLabel(string eventKey)
        {
            return FetchReport("byCategoryLabel", eventKey);
        }

        public IEnumerable<EventReportItem> ByCategoryLabel(string eventKey, string categoryLabel)
        {
            return FetchReport("byCategoryLabel", eventKey, categoryLabel);
        }    
        
        public Dictionary<string, IEnumerable<EventReportItem>> ByCategoryKey(string eventKey)
        {
            return FetchReport("byCategoryKey", eventKey);
        }

        public IEnumerable<EventReportItem> ByCategoryKey(string eventKey, string categoryKey)
        {
            return FetchReport("byCategoryKey", eventKey, categoryKey);
        }   
        
        public Dictionary<string, IEnumerable<EventReportItem>> ByOrderId(string eventKey)
        {
            return FetchReport("byOrderId", eventKey);
        }

        public IEnumerable<EventReportItem> ByOrderId(string eventKey, string categoryKey)
        {
            return FetchReport("byOrderId", eventKey, categoryKey);
        }  
        
        public Dictionary<string, IEnumerable<EventReportItem>> BySection(string eventKey)
        {
            return FetchReport("bySection", eventKey);
        }

        public IEnumerable<EventReportItem> BySection(string eventKey, string section)
        {
            return FetchReport("bySection", eventKey, section);
        }

        private Dictionary<string, IEnumerable<EventReportItem>> FetchReport(string reportType, string eventKey)
        {
            var restRequest = new RestRequest("/reports/events/{key}/{reportType}", Method.GET)
                .AddUrlSegment("key", eventKey)
                .AddUrlSegment("reportType", reportType);
            return AssertOk(_restClient.Execute<Dictionary<string, IEnumerable<EventReportItem>>>(restRequest));
        }

        private IEnumerable<EventReportItem> FetchReport(string reportType, string eventKey, string filter)
        {
            var restRequest = new RestRequest("/reports/events/{key}/{reportType}/{filter}", Method.GET)
                .AddUrlSegment("key", eventKey)
                .AddUrlSegment("reportType", reportType)
                .AddUrlSegment("filter", filter);
            var report = AssertOk(_restClient.Execute<Dictionary<string, IEnumerable<EventReportItem>>>(restRequest));
            return report[filter];
        }
    }
}