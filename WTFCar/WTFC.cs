using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace WTFCar
{
    public class WTFC
    {
        string _year;
        string _make;
        string _model;
        string _totalpts;

        public WTFC()
        {

        }

        public WTFC(string year, string make, string model, string totalpts)
        {
            _year = year;
            _make = make;
            _model = model;
            _totalpts = totalpts;
        }

        [JsonProperty("year")]
        public string year
        {
            get
            {
                return _year;
            }
            set
            {
                _year = value;
            }
        }

        [JsonProperty("make")]
        public string make
        {
            get
            {
                return _make;
            }
            set
            {
                _make = value;
            }
        }

        [JsonProperty("model")]
        public string model
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
            }
        }

        [JsonProperty("Totalpts")]
        public string Totalpts
        {
            get
            {
                if(EnginePts==null && AirbagPts == null && PowertrainPts == null && ElectricalPts == null && OtherComplaints == null)
                {
                    return _totalpts;
                }
                else
                {
                    return (ConvertPropInt(EnginePts) + ConvertPropInt(AirbagPts)
                   + ConvertPropInt(PowertrainPts) + ConvertPropInt(ElectricalPts)
                   + ConvertPropInt(OtherComplaints)).ToString();
                }
               
            }
            set
            {
                _totalpts = value;
            }
        }

        public string EnginePts
        {
            get;
            set;
        }
        public string ElectricalPts { get; internal set; }
        public string AirbagPts { get; internal set; }
        public string PowertrainPts { get; internal set; }
        public string OtherComplaints { get; internal set; }

        public string AverageEnginePts
        {
            get;
            set;
        }
        public string AverageElectricalPts { get; internal set; }
        public string AverageAirbagPts { get; internal set; }
        public string AveragePowertrainPts { get; internal set; }
        public string AverageOtherComplaints { get; internal set; }


        public string Totalsales { get; internal set; }
        public string ProblemRatio { get; internal set; }
        public string AverageYearPts { get; internal set; }
        public string ComplaintCount { get; internal set; }

        public List<WordCloud> WordCloudList { get; internal set; }

        public string ForumURL { get; internal set; }

        private int ConvertPropInt(string s)
        {
            int intvalue = 0;
            try
            {
                intvalue = Convert.ToInt32(s);
            }
            catch
            {
                intvalue = 0;
            }
            return intvalue;
        }
    }
}