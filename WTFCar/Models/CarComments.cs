using Newtonsoft.Json;
using System.Collections.Generic;

namespace WTFCar.Models
{
   
    public class CarComments
    {

        List<CarComment> _carCommentsList = new List<CarComment>();


        public CarComments()
        {

        }

        [JsonProperty("CarComments")]
        public List<CarComment> CarCommentsList
        {
            get
            {
                return _carCommentsList;
            }
            set
            {
                _carCommentsList = value;
            }
        }


    }
}