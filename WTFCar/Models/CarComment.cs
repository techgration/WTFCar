using Newtonsoft.Json;
using System.Collections.Generic;

namespace WTFCar.Models
{
   
    public class CarComment
    {

       
        public CarComment()
        {

        }

        [JsonProperty("CarCommentTitle")]
        public string CarCommentTitle
        {
            get;
            set;
        }

        [JsonProperty("CarCommentText")]
        public string CarCommentText
        {
            get;
            set;
        }
    }
}