using System;

namespace HackerNewsAPI.Models
{
    /// <summary>
    /// The item returned from the /item/ call.  All fields except ID are optional
    /// </summary>
    [Serializable]
    public class BestStoryInfo
    {
        /// <summary>
        /// id The item's unique id. 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// by The username of the item's author. 
        /// </summary>
        public string By { get; set; }

        /// <summary>
        /// title The title of the story, poll or job.
        /// </summary>
        public string Title { get; set; }
    }
}