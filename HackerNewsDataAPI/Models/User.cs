using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackerNewsDataAPI.Models
{
    [Serializable]
    public class User
    {
        /// <summary>
        /// id The user's unique username. Case-sensitive. Required. 
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// delay Delay in minutes between a comment's creation and its visibility to other users. 
        /// </summary>
        public int? Delay { get; set; }

        /// <summary>
        /// created Creation date of the user, in Unix Time.
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// karma The user's karma. 
        /// </summary>
        public int Karma { get; set; }

        /// <summary>
        /// about The user's optional self-description. HTML. 
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// submitted List of the user's stories, polls and comments. 
        /// </summary>
        public List<int> Submitted { get; set; }
    }
}