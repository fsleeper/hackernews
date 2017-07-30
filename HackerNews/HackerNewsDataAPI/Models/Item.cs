using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Utilities;

namespace HackerNewsDataAPI.Models
{
    /// <summary>
    /// The item returned from the /item/ call.  All fields except ID are optional
    /// </summary>
    [Serializable]
    public class Item
    {
        /// <summary>
        /// id The item's unique id. 
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// deleted  true  if the item is deleted.
        /// </summary>
        public string Deleted { get; set; }

        /// <summary>
        /// type The type of item.One of "job", "story", "comment", "poll", or "pollopt". 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// by The username of the item's author. 
        /// </summary>
        public string By { get; set; }

        /// <summary>
        /// time Creation date of the item, in Unix Time.
        /// </summary>
        [JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime? Time { get; set; }

        /// <summary>
        /// text The comment, story or poll text. HTML.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// dead  true  if the item is dead.
        /// </summary>
        public string Dead { get; set; }

        /// <summary>
        /// parent The comment's parent: either another comment or the relevant story. 
        /// </summary>
        public int? Parent { get; set; }

        /// <summary>
        /// poll The pollopt's associated poll. 
        /// </summary>
        public int? Poll { get; set; }

        /// <summary>
        /// kids The ids of the item's comments, in ranked display order. 
        /// </summary>
        public List<int> Kids { get; set; }

        /// <summary>
        /// url The URL of the story. 
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// score The story's score, or the votes for a pollopt. 
        /// </summary>
        public int? Score { get; set; }

        /// <summary>
        /// title The title of the story, poll or job.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// parts A list of related pollopts, in display order. 
        /// </summary>
        public List<int> Parts { get; set; }

        /// <summary>
        /// descendants In the case of stories or polls, the total comment count. 
        /// </summary>
        public int? Descendants { get; set; }
    }
}