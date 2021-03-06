﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OakIdeas.AppHarbor.Api.Core.Models
{
    public class Build : BaseObject
    {
        private string _status;
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private DateTime? _created;
        public DateTime? Created
        {
            get { return _created; }
            set { _created = value; }
        }

        private DateTime? _deployed;
        public DateTime? Deployed
        {
            get { return _deployed; }
            set { _deployed = value; }
        }

        private Branch _branch;
        public Branch Branch
        {
            get { return _branch; }
            set { _branch = value; }
        }

        private Commit _commit;
        public Commit Commit
        {
            get
            {
                if (_commit == null)
                {
                    _commit = new Commit();
                }

                return _commit;
            }
            set { _commit = value; }
        }
        //download_url
        private string _downloadUrl;
        [JsonProperty("download_url")]
        public string DownloadUrl
        {
            get { return _downloadUrl; }
            set { _downloadUrl = value; }
        }
        
        private string _testsUrl;
        [JsonProperty("tests_url")]
        public string TestsUrl
        {
            get { return _testsUrl; }
            set { _testsUrl = value; }
        }

        private List<LogEntry> _logEntries;
        [JsonProperty("log_entries")]
        public List<LogEntry> LogEntries
        {
            get { return _logEntries ?? (_logEntries = new List<LogEntry>()); }
            set { _logEntries = value; }
        }

        private string _url;
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        
        public Build()
        {
            LogEntries = new List<LogEntry>();
        }
    }
}
