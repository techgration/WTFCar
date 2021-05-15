using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using WTFCar.Models;

namespace WTFCar
{
    public class Data
    {
        private readonly string _connectionString = WebConfigurationManager.ConnectionStrings["dbconnstring"].ConnectionString;

        public Data()
        {

        }

        public List<WTFC> GetWorstCarsAllTime(string page)
        {
            List<WTFC> lWTFC = new List<WTFC>();

            List<SqlParameter> lParams = new List<SqlParameter>();

            SqlParameter p = new SqlParameter("page", page);

            lParams.Add(p);

            DataTable dt = ExecuteStoredProcSQL("GetWorstCarsAllTime", true, lParams);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WTFC c = new WTFC
                {
                    year = dt.Rows[i][0].ToString(),
                    make = dt.Rows[i][1].ToString(),
                    model = dt.Rows[i][2].ToString(),
                    Totalpts = dt.Rows[i][3].ToString()
                };
                lWTFC.Add(c);
            }

            return lWTFC;
        }


        public List<WTFC> GetWorstCars(string page)
        {
            List<WTFC> lWTFC = new List<WTFC>();

            List<SqlParameter> lParams = new List<SqlParameter>();

            SqlParameter p = new SqlParameter("page", page);

            lParams.Add(p);

            DataTable dt = ExecuteStoredProcSQL("GetWorstNewCars", true, lParams);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WTFC c = new WTFC
                {
                    year = dt.Rows[i][0].ToString(),
                    make = dt.Rows[i][1].ToString(),
                    model = dt.Rows[i][2].ToString(),
                    Totalpts = dt.Rows[i][3].ToString()
                };
                lWTFC.Add(c);
            }

            return lWTFC;
        }

        public List<WTFC> GetCurrentCarInfo(string year, string make, string model)
        {
            //Data d = new Data();
            List<WTFC> lWTFC = new List<WTFC>();

            List<SqlParameter> lParams = new List<SqlParameter>();
            SqlParameter y = new SqlParameter("year", year);
            SqlParameter m = new SqlParameter("make", make);
            SqlParameter mo = new SqlParameter("model", model);

            lParams.Add(y);
            lParams.Add(m);
            lParams.Add(mo);

            DataTable dt = ExecuteStoredProcSQL("GetCurrentCarInfo", true, lParams);

            WTFC c = new WTFC();


            if (dt != null && dt.Rows.Count > 0)
            {
                c.year = dt.Rows[0][0].ToString();
                c.make = dt.Rows[0][1].ToString();
                c.model = dt.Rows[0][2].ToString();
            }


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][3] != null && dt.Rows[i][3].ToString() != "" && dt.Rows[i][3].ToString() != "0")
                {
                    c.EnginePts = dt.Rows[i][3].ToString();
                }
                if (dt.Rows[i][4] != null && dt.Rows[i][4].ToString() != "" && dt.Rows[i][4].ToString() != "0")
                {
                    c.ElectricalPts = dt.Rows[i][4].ToString();
                }
                if (dt.Rows[i][5] != null && dt.Rows[i][5].ToString() != "" && dt.Rows[i][5].ToString() != "0")
                {
                    c.AirbagPts = dt.Rows[i][5].ToString();

                }
                if (dt.Rows[i][6] != null && dt.Rows[i][6].ToString() != "" && dt.Rows[i][6].ToString() != "0")
                {
                    c.PowertrainPts = dt.Rows[i][6].ToString();

                }
                if (dt.Rows[i][7] != null && dt.Rows[i][7].ToString() != "" && dt.Rows[i][7].ToString() != "0")
                {
                    c.OtherComplaints = dt.Rows[i][7].ToString();

                }

                if (dt.Rows[i][11] != null && dt.Rows[i][11].ToString() != "" && dt.Rows[i][11].ToString() != "0")
                {
                    c.Totalsales = dt.Rows[i][11].ToString();
                }


                //averages

                if (dt.Rows[i][11] != null && dt.Rows[i][11].ToString() != "" && dt.Rows[i][11].ToString() != "0")
                {
                    c.AverageEnginePts = dt.Rows[i][11].ToString();
                }
                if (dt.Rows[i][12] != null && dt.Rows[i][12].ToString() != "" && dt.Rows[i][12].ToString() != "0")
                {
                    c.AverageElectricalPts = dt.Rows[i][12].ToString();
                }
                if (dt.Rows[i][13] != null && dt.Rows[i][13].ToString() != "" && dt.Rows[i][13].ToString() != "0")
                {
                    c.AverageAirbagPts = dt.Rows[i][13].ToString();

                }
                if (dt.Rows[i][14] != null && dt.Rows[i][14].ToString() != "" && dt.Rows[i][14].ToString() != "0")
                {
                    c.AveragePowertrainPts = dt.Rows[i][14].ToString();
                }
                if (dt.Rows[i][15] != null && dt.Rows[i][15].ToString() != "" && dt.Rows[i][15].ToString() != "0")
                {
                    c.AverageOtherComplaints = dt.Rows[i][15].ToString();
                }
                if (dt.Rows[i][16] != null && dt.Rows[i][16].ToString() != "" && dt.Rows[i][16].ToString() != "0")
                {
                    c.AverageYearPts = dt.Rows[i][16].ToString();
                }

                if (dt.Rows[i][17] != null && dt.Rows[i][17].ToString() != "" && dt.Rows[i][17].ToString() != "0")
                {
                    c.ComplaintCount = dt.Rows[i][17].ToString();
                }

                if (dt.Rows[i][18] != null && dt.Rows[i][18].ToString() != "" && dt.Rows[i][18].ToString() != "0")
                {
                    c.ProblemRatio = dt.Rows[i][18].ToString();
                }

                c.WordCloudList = GetWordClouds(year, make, model);

                c.ForumURL = GetForumUrl(make);

                lWTFC.Add(c);
            }

            return lWTFC;
        }

        public List<WTFC> GetWorstCarMakesLastFiveYears(ref string page)
        {
            //Data d = new Data();
            List<WTFC> lWTFC = new List<WTFC>();

            int result = -1;

            Int32.TryParse(page, out result);

            if (result > 0)
            {
                SqlParameter sqlP = new SqlParameter("page", Convert.ToInt32(page));
                List<SqlParameter> lSqlP = new List<SqlParameter>
                {
                    sqlP
                };

                DataTable dt = ExecuteStoredProcWithPage("GetWorstCarMakesLastFiveYears", lSqlP, ref page);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    WTFC c = new WTFC
                    {
                        make = dt.Rows[i][0].ToString(),
                        Totalpts = dt.Rows[i][1].ToString()
                    };
                    lWTFC.Add(c);
                }

            }

            return lWTFC;
        }

        internal string GetForumUrl(string make)
        {

            try
            {
                List<SqlParameter> lParams = new List<SqlParameter>();
                SqlParameter sqlp = new SqlParameter("make", make);

                lParams.Add(sqlp);

                DataTable dt = ExecuteStoredProcSQL("GetForumByMake", true, lParams);

                if (dt != null && dt.Rows != null && dt.Rows[0] != null)
                {
                    return dt.Rows[0][0].ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "";
            }



        }

        internal List<WordCloud> GetWordClouds(string year, string make, string model)
        {
            List<WordCloud> lWordCloud = new List<WordCloud>();


            List<SqlParameter> lParams = new List<SqlParameter>();
            SqlParameter y = new SqlParameter("year", year);
            SqlParameter m = new SqlParameter("make", make);
            SqlParameter mo = new SqlParameter("model", model);

            lParams.Add(y);
            lParams.Add(m);
            lParams.Add(mo);

            DataTable dt = ExecuteStoredProcSQL("GetCurrentCarWordCloud", true, lParams);

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                WordCloud wc = new WordCloud
                {
                    Word = dt.Rows[x][0].ToString(),
                    Weight = Convert.ToInt32(dt.Rows[x][1].ToString())
                };
                lWordCloud.Add(wc);
            }

            return lWordCloud;
        }

        internal List<WTFC> GetWorstCarsAllTime(string sortBy, string yearBegin, string yearEnd, string page)
        {
            List<WTFC> lWTFC = new List<WTFC>();

            List<SqlParameter> lParams = new List<SqlParameter>();

            SqlParameter p = new SqlParameter("page", page);
            lParams.Add(p);

            if (sortBy != "")
            {
                p = new SqlParameter("sortBy", sortBy);
                lParams.Add(p);
            }

            if (yearBegin != "")
            {
                p = new SqlParameter("yearBegin", yearBegin);
                lParams.Add(p);
            }


            if (yearEnd != "")
            {
                p = new SqlParameter("yearEnd", yearEnd);
                lParams.Add(p);
            }



            DataTable dt = ExecuteStoredProcSQL("GetWorstCarsAllTime", true, lParams);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WTFC c = new WTFC
                {
                    year = dt.Rows[i][0].ToString(),
                    make = dt.Rows[i][1].ToString(),
                    model = dt.Rows[i][2].ToString(),
                    Totalpts = dt.Rows[i][3].ToString()
                };
                lWTFC.Add(c);
            }

            return lWTFC;
        }

        public List<string> GetMakesFromYear(string year)
        {
            List<string> makes = new List<string>();

            List<SqlParameter> lParams = new List<SqlParameter>();
            SqlParameter m = new SqlParameter("yearid", year);
            lParams.Add(m);

            DataTable dt = ExecuteStoredProcSQL("GetMakesFromYear", true, lParams);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                makes.Add(dt.Rows[i][0].ToString());
            }

            return makes;
        }

        public List<string> GetModelsFromYearMake(string year, string make)
        {
            List<string> models = new List<string>();

            List<SqlParameter> lParams = new List<SqlParameter>();
            SqlParameter m = new SqlParameter("yearid", year);
            lParams.Add(m);
            m = new SqlParameter("make", make);
            lParams.Add(m);

            DataTable dt = ExecuteStoredProcSQL("GetModelsFromYearMake", true, lParams);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                models.Add(dt.Rows[i][0].ToString());
            }

            return models;
        }



        public CarComments GetIssuesByComponent(string page, string issue, string year, string make, string model)
        {
            CarComments lComplaints = new CarComments();

            List<SqlParameter> lParams = new List<SqlParameter>();
            SqlParameter m = new SqlParameter("component", issue);
            lParams.Add(m);
            m = new SqlParameter("year", year);
            lParams.Add(m);
            m = new SqlParameter("make", make);
            lParams.Add(m);
            m = new SqlParameter("model", model);
            lParams.Add(m);
            if (!string.IsNullOrEmpty(page))
            {
                m = new SqlParameter("page", page);
                lParams.Add(m);
            }
            else
            {
                m = new SqlParameter("page", "1");
                lParams.Add(m);
            }


            DataTable dt = ExecuteStoredProcSQL("GetIssuesByComponent", true, lParams);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CarComment cc = new CarComment
                {
                    CarCommentTitle = "Comment #" + i.ToString(),
                    CarCommentText = dt.Rows[i][1].ToString()
                };
                lComplaints.CarCommentsList.Add(cc);
            }

            return lComplaints;
        }

        public int GetComplaintCountTotal(string issue, string year, string make, string model)
        {
            //Data d = new Data();
            List<WTFC> lWTFC = new List<WTFC>();

            List<SqlParameter> lParams = new List<SqlParameter>();

            SqlParameter m = new SqlParameter("issue", issue);
            lParams.Add(m);

            m = new SqlParameter("year", year);
            lParams.Add(m);

            m = new SqlParameter("make", make);
            lParams.Add(m);

            m = new SqlParameter("model", model);
            lParams.Add(m);

            DataTable dt = ExecuteStoredProcSQL("GetComplaintCountTotal", true, lParams);

            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }

        }




        /// <summary>
        /// override for GetIssuesByComponent
        /// </summary>
        /// <param name="issue"></param>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public CarComments GetIssuesByComponent(string issue, string year, string make, string model)
        {
            return GetIssuesByComponent(issue, year, make, model, "1");
        }

        public List<WTFC> GetWorstCarsByMake(string make, ref string page)
        {
            //Data d = new Data();
            List<WTFC> lWTFC = new List<WTFC>();

            List<SqlParameter> lParams = new List<SqlParameter>();
            SqlParameter m = new SqlParameter("make", make);

            lParams.Add(m);

            SqlParameter p = new SqlParameter("page", Convert.ToInt32(page));

            lParams.Add(p);

            DataTable dt = ExecuteStoredProcWithPage("GetWorstCarsByMake", lParams, ref page);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WTFC c = new WTFC
                {
                    year = dt.Rows[i][0].ToString(),
                    make = dt.Rows[i][1].ToString(),
                    model = dt.Rows[i][2].ToString()
                };

                if (dt.Rows[i][8] != null)
                {
                    c.Totalpts = dt.Rows[i][8].ToString();
                }


                lWTFC.Add(c);
            }

            return lWTFC;
        }



        public List<WTFC> GetWorstCarMakes(ref string page)
        {
            //Data d = new Data();
            List<WTFC> lWTFC = new List<WTFC>();

            int result = -1;

            Int32.TryParse(page, out result);

            if (result > 0)
            {
                SqlParameter sqlP = new SqlParameter("page", Convert.ToInt32(page));
                List<SqlParameter> lSqlP = new List<SqlParameter>
                {
                    sqlP
                };

                DataTable dt = ExecuteStoredProcWithPage("GetWorstCarMakes", lSqlP, ref page);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    WTFC c = new WTFC
                    {
                        make = dt.Rows[i][0].ToString(),
                        Totalpts = dt.Rows[i][1].ToString()
                    };
                    lWTFC.Add(c);
                }


            }

            return lWTFC;
        }


        public List<WTFC> GetWorstCarByMake(string make)
        {
            //Data d = new Data();
            List<WTFC> lWTFC = new List<WTFC>();

            List<SqlParameter> lParams = new List<SqlParameter>
            {
                new SqlParameter("make", make)
            };

            DataTable dt = ExecuteStoredProcSQL("GetWorstCarsByMake", true, lParams);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WTFC c = new WTFC
                {
                    make = dt.Rows[i][0].ToString(),
                    Totalpts = dt.Rows[i][1].ToString()
                };
                lWTFC.Add(c);
            }

            return lWTFC;
        }

        public List<WTFC> GetWorstCarMakeModels()
        {
            //Data d = new Data();
            List<WTFC> lWTFC = new List<WTFC>();

            DataTable dt = ExecuteStoredProcSQL("GetWorstCarMakeModels", true, null);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WTFC c = new WTFC
                {
                    make = dt.Rows[i][0].ToString(),
                    model = dt.Rows[i][1].ToString(),
                    Totalpts = dt.Rows[i][2].ToString()
                };
                lWTFC.Add(c);
            }

            return lWTFC;
        }

        public List<WTFC> GetWorstCarsMakeModelYears(string make, string model, ref string page)
        {
            //Data d = new Data();
            List<WTFC> lWTFC = new List<WTFC>();

            List<SqlParameter> lParams = new List<SqlParameter>
            {
                new SqlParameter("make", make),
                new SqlParameter("model", model),
                new SqlParameter("page", page)
            };

            DataTable dt = ExecuteStoredProcWithPage("GetWorstCarsMakeModelYears", lParams, ref page);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WTFC c = new WTFC
                {
                    year = dt.Rows[i][0].ToString(),
                    make = dt.Rows[i][1].ToString(),
                    model = dt.Rows[i][2].ToString(),
                    Totalpts = dt.Rows[i][3].ToString()
                };
                lWTFC.Add(c);
            }

            return lWTFC;
        }

        public List<WTFC> GetWorstTrucksByMakeModel(ref string page)
        {
            if (string.IsNullOrEmpty(page))
            {
                page = "1";
            }

            //Data d = new Data();
            List<WTFC> lWTFC = new List<WTFC>();

            List<SqlParameter> lParams = new List<SqlParameter>
            {
                new SqlParameter("page", page)
            };

            DataTable dt = ExecuteStoredProcWithPage("GetWorstTrucksByMakeModel", lParams, ref page);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WTFC c = new WTFC
                {
                    //c.year = dt.Rows[i][0].ToString();
                    make = dt.Rows[i][0].ToString(),
                    model = dt.Rows[i][1].ToString(),
                    Totalpts = dt.Rows[i][2].ToString()
                };
                lWTFC.Add(c);
            }

            return lWTFC;
        }

        public List<WTFC> GetWorstTrucksByYearMakeModel(string make, string model, ref string page)
        {

            if (string.IsNullOrEmpty(page))
            {
                page = "1";
            }

            //Data d = new Data();
            List<WTFC> lWTFC = new List<WTFC>();

            List<SqlParameter> lParams = new List<SqlParameter>
            {
                new SqlParameter("page", page),
                new SqlParameter("make", make),
                new SqlParameter("model", model)
            };

            DataTable dt = ExecuteStoredProcWithPage("GetWorstTrucksByYearMakeModel", lParams, ref page);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WTFC c = new WTFC
                {
                    year = dt.Rows[i][0].ToString(),
                    make = dt.Rows[i][1].ToString(),
                    model = dt.Rows[i][2].ToString(),
                    Totalpts = dt.Rows[i][3].ToString()
                };
                lWTFC.Add(c);
            }

            return lWTFC;
        }

        public List<WTFC> GetWorstSUVsByMakeModel(ref string page)
        {
            if (string.IsNullOrEmpty(page))
            {
                page = "1";
            }

            //Data d = new Data();
            List<WTFC> lWTFC = new List<WTFC>();

            List<SqlParameter> lParams = new List<SqlParameter>
            {
                new SqlParameter("page", page)
            };

            DataTable dt = ExecuteStoredProcWithPage("GetWorstSUVsByMakeModel", lParams, ref page);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WTFC c = new WTFC
                {
                    //c.year = dt.Rows[i][0].ToString();
                    make = dt.Rows[i][0].ToString(),
                    model = dt.Rows[i][1].ToString(),
                    Totalpts = dt.Rows[i][2].ToString()
                };
                lWTFC.Add(c);
            }

            return lWTFC;
        }

        public List<WTFC> GetWorstSUVsByYearMakeModel(string make, string model, ref string page)
        {

            if (string.IsNullOrEmpty(page))
            {
                page = "1";
            }

            //Data d = new Data();
            List<WTFC> lWTFC = new List<WTFC>();

            List<SqlParameter> lParams = new List<SqlParameter>
            {
                new SqlParameter("page", page),
                new SqlParameter("make", make),
                new SqlParameter("model", model)
            };

            DataTable dt = ExecuteStoredProcWithPage("GetWorstSUVsByYearMakeModel", lParams, ref page);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WTFC c = new WTFC
                {
                    year = dt.Rows[i][0].ToString(),
                    make = dt.Rows[i][1].ToString(),
                    model = dt.Rows[i][2].ToString(),
                    Totalpts = dt.Rows[i][3].ToString()
                };
                lWTFC.Add(c);
            }

            return lWTFC;
        }

        public List<WTFC> GetWorstMinivansByMakeModel(ref string page)
        {
            if (string.IsNullOrEmpty(page))
            {
                page = "1";
            }

            //Data d = new Data();
            List<WTFC> lWTFC = new List<WTFC>();

            List<SqlParameter> lParams = new List<SqlParameter>
            {
                new SqlParameter("page", page)
            };

            DataTable dt = ExecuteStoredProcWithPage("GetWorstMinivansByMakeModel", lParams, ref page);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WTFC c = new WTFC
                {
                    //c.year = dt.Rows[i][0].ToString();
                    make = dt.Rows[i][0].ToString(),
                    model = dt.Rows[i][1].ToString(),
                    Totalpts = dt.Rows[i][2].ToString()
                };
                lWTFC.Add(c);
            }

            return lWTFC;
        }


        public List<WTFC> GetWorstMinivansByYearMakeModel(string make, string model, ref string page)
        {

            if (string.IsNullOrEmpty(page))
            {
                page = "1";
            }

            //Data d = new Data();
            List<WTFC> lWTFC = new List<WTFC>();

            List<SqlParameter> lParams = new List<SqlParameter>
            {
                new SqlParameter("page", page),
                new SqlParameter("make", make),
                new SqlParameter("model", model)
            };

            DataTable dt = ExecuteStoredProcWithPage("GetWorstMinivansByYearMakeModel", lParams, ref page);


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WTFC c = new WTFC
                {
                    year = dt.Rows[i][0].ToString(),
                    make = dt.Rows[i][1].ToString(),
                    model = dt.Rows[i][2].ToString(),
                    Totalpts = dt.Rows[i][3].ToString()
                };
                lWTFC.Add(c);
            }

            return lWTFC;
        }

        public List<WTFC> GetWorstCarsByMakeModel(ref string page)
        {
            if (string.IsNullOrEmpty(page))
            {
                page = "1";
            }

            //Data d = new Data();
            List<WTFC> lWTFC = new List<WTFC>();

            List<SqlParameter> lParams = new List<SqlParameter>
            {
                new SqlParameter("page", page)
            };

            DataTable dt = ExecuteStoredProcWithPage("GetWorstCarsByMakeModel", lParams, ref page);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WTFC c = new WTFC
                {
                    //c.year = dt.Rows[i][0].ToString();
                    make = dt.Rows[i][0].ToString(),
                    model = dt.Rows[i][1].ToString(),
                    Totalpts = dt.Rows[i][2].ToString()
                };
                lWTFC.Add(c);
            }

            return lWTFC;
        }


        public List<WTFC> GetWorstCarsByYearMakeModel(string make, string model, ref string page)
        {

            if (string.IsNullOrEmpty(page))
            {
                page = "1";
            }

            //Data d = new Data();
            List<WTFC> lWTFC = new List<WTFC>();

            List<SqlParameter> lParams = new List<SqlParameter>
            {
                new SqlParameter("page", page),
                new SqlParameter("make", make),
                new SqlParameter("model", model)
            };

            DataTable dt = ExecuteStoredProcWithPage("GetWorstCarsByYearMakeModel", lParams, ref page);


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WTFC c = new WTFC
                {
                    year = dt.Rows[i][0].ToString(),
                    make = dt.Rows[i][1].ToString(),
                    model = dt.Rows[i][2].ToString(),
                    Totalpts = dt.Rows[i][3].ToString()
                };
                lWTFC.Add(c);
            }

            return lWTFC;
        }

        /// <summary>
        /// if we are calling a stored proc using paging,
        /// check to see if we are returning an empty page
        /// and if the page # is greater than 1. If so, 
        /// return the next lowest page# where data exists
        /// </summary>
        /// <param name="storedProcNameOrSQL"></param>
        /// <param name="isStoredProc"></param>
        /// <param name="lParameters"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        private DataTable ExecuteStoredProcWithPage(string storedProcName, List<SqlParameter> lParameters, ref string page)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sqlcon = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(storedProcName, sqlcon))
                {
                    if (lParameters != null)
                    {

                        cmd.Parameters.Clear();

                        foreach (SqlParameter p in lParameters)
                        {
                            SqlParameter pnew = new SqlParameter
                            {
                                Value = p.Value,
                                ParameterName = p.ParameterName
                            };
                            cmd.Parameters.Add(pnew);
                        }

                    }

                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

            }

            if(dt==null || dt.Rows.Count == 0)
            {
                if(page != "1")
                {
                    int pageAsInt = Convert.ToInt32(page);
                    while(pageAsInt > 0 && dt == null || dt.Rows.Count == 0)
                    {
                        //keep trying to get a lower page count
                        pageAsInt = pageAsInt - 1;

                        List<SqlParameter> lParametersNew = new List<SqlParameter>();

                        foreach (SqlParameter p in lParameters)
                        {
                            if (p.ParameterName != "page")
                            {
                                SqlParameter pnew = new SqlParameter
                                {
                                    Value = p.Value,
                                    ParameterName = p.ParameterName
                                };
                                lParametersNew.Add(pnew);
                            }
                        }

                        //pass in the lower page value
                        SqlParameter pNew = new SqlParameter("page", pageAsInt.ToString());
                        lParametersNew.Add(pNew);

                        page = pageAsInt.ToString();

                        lParameters.Clear();

                        dt = ExecuteStoredProcWithPage(storedProcName, lParametersNew, ref page);

                    }
                }
            }

            return dt;
        }

        private DataTable ExecuteStoredProcSQL(string storedProcNameOrSQL, Boolean isStoredProc, List<SqlParameter> lParameters)
        {
            DataTable dt = new DataTable();


            if (isStoredProc)
            {
                using (SqlConnection sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcNameOrSQL, sqlcon))
                    {
                        if (lParameters != null)
                        {
                            foreach (SqlParameter p in lParameters)
                            {
                                cmd.Parameters.Add(p);
                            }

                        }

                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcNameOrSQL, sqlcon))
                    {
                        cmd.CommandType = CommandType.Text;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            return dt;
        }
    }
}