﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace HackerNewsAPI.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class User
    {
        /// <summary>
        /// Initializes a new instance of the User class.
        /// </summary>
        public User() { }

        /// <summary>
        /// Initializes a new instance of the User class.
        /// </summary>
        public User(string id = default(string), int? delay = default(int?), DateTime? created = default(DateTime?), int? karma = default(int?), string about = default(string), IList<int?> submitted = default(IList<int?>))
        {
            ID = id;
            Delay = delay;
            Created = created;
            Karma = karma;
            About = about;
            Submitted = submitted;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ID")]
        public string ID { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Delay")]
        public int? Delay { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Created")]
        public DateTime? Created { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Karma")]
        public int? Karma { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "About")]
        public string About { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Submitted")]
        public IList<int?> Submitted { get; set; }

    }
}
