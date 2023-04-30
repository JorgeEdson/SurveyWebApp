using Newtonsoft.Json;
using Survey.WebApp.Domain;
using Survey.WebApp.Domain.Enuns;
using Survey.WebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Survey.WebApp.Services
{
    public static class DatabaseOperationsService
    {
        public readonly static string _connectionString = "Data Source=SQL5109.site4now.net; Initial Catalog=db_9ab8b7_123dda8563; User Id=db_9ab8b7_123dda8563_admin; Password=vY4EtDRZ;";

        public static Question GetQuestionById(int questionId)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = _connectionString;
            SqlCommand myCommand;
            try
            {                
                // Prepare the query
                //myCommand = new SqlCommand("SELECT * FROM TabUser ORDER BY UserLevel", myConnection);
                myCommand = new SqlCommand("SELECT * FROM questions " +
                    "WHERE id = @id", myConnection);
                myCommand.Parameters.AddWithValue("@id", questionId);

                // Open the connection
                myConnection.Open();

                // Execute query
                SqlDataReader myReader = myCommand.ExecuteReader();

                // Read data from database
                if (myReader.Read())
                {
                    Question myQuestion = new Question();
                    List<Option> options = new List<Option>();
                    myQuestion.Id = Convert.ToInt32(myReader["id"]);
                    myQuestion.Text = Convert.ToString(myReader["text"]);
                    var stringType = Convert.ToString(myReader["type"]);

                    if (stringType.Equals("radio"))
                        myQuestion.Type = QuestionType.Radio;
                    else if (stringType.Equals("text"))
                        myQuestion.Type = QuestionType.Text;
                    else if (stringType.Equals("checkbox"))
                        myQuestion.Type = QuestionType.Checkbox;
                    else if (stringType.Equals("dropdown"))
                        myQuestion.Type = QuestionType.Dropdown;

                    myQuestion.Required = Convert.ToInt32(myReader["required"]);

                    if (String.IsNullOrEmpty(myReader["next_id"].ToString()))
                        myQuestion.NextId = null;
                    else
                        myQuestion.NextId = Convert.ToInt32(myReader["next_id"]);

                    Convert.ToString(myReader["options"]).Split(',')
                    .ToList().ForEach(text => {
                        Option option = new Option
                        {
                            Text = text,
                            QuestionId = myQuestion.Id
                        };

                        options.Add(option);
                    });
                    myQuestion.Options = options;
                    return myQuestion;
                }

                // Close DB connection
                myConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public static bool Login(string userName, string password) 
        {
            try
            {
                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = _connectionString;
                SqlCommand myCommand;

                myCommand = new SqlCommand("SELECT * FROM Users WHERE uname = @userName AND Pwd = @password", myConnection);
                myCommand.Parameters.AddWithValue("@userName", userName);
                myCommand.Parameters.AddWithValue("@password", password);

                myConnection.Open();

                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.HasRows)
                {
                    myConnection.Close();
                    return true;
                }
                else
                {
                    myConnection.Close();
                    return false;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<SearchListViewModel> SearchFilter(string gender, string age, string stateOrTerritory, string homeSuburb, string postCode, string email, 
            string bankUsed, string additionalService, string newspaper, string sectionRead, string favoriteSports, string travelDestination, string username)
        {
            try
            {
                List<SearchListViewModel> list = new List<SearchListViewModel>();
                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = _connectionString;
                SqlCommand myCommand;

                var query = $@"SELECT   Registers.given_name AS Name,
                                        Registers.last_name AS LastName,
		                                Questions.Text AS Question,
		                                Answers.text AS Answer
                            FROM Answers
                            INNER JOIN Respondents ON Respondents.Id = Answers.respondent_id
                            INNER JOIN Questions ON Questions.id = Answers.question_id
                            LEFT JOIN Registers ON Registers.respondent_id = Respondents.id
                            WHERE Respondents.id > 0";

                if(gender != "")
                {
                    query += " AND Questions.Text LIKE 'Gender' AND Answers.text = @gender";
                }

                if(age != "")
                {
                    query += " AND Questions.Text LIKE 'Age' AND Answers.text = @age";
                }

                if (stateOrTerritory != "")
                {
                    query += " AND Questions.Text LIKE 'State or Territory of Australia' AND Answers.text = @stateOrTerritory";
                }

                if(homeSuburb != "")
                {
                    query += " AND Questions.Text LIKE 'Home Suburb' AND Answers.text = @homeSuburb";
                }

                if (postCode != "")
                {
                    query += " AND Questions.Text LIKE 'Home PostCode' AND Answers.text = @postCode";
                }

                if (email != "")
                {
                    query += " AND Questions.Text LIKE 'Email' AND Answers.text = @email";
                }

                if (bankUsed != "")
                {
                    query += " AND Questions.Text LIKE 'What Bank do you use?' AND Answers.text = @bankUsed";
                }

                if (additionalService != "")
                {
                    query += " AND Questions.Text LIKE 'Do you use additional services?' AND Answers.text = @additionalService";
                }

                if (newspaper != "")
                {
                    query += " AND Questions.Text LIKE 'Which newspaper do you read?' AND Answers.text = @newspaper";
                }

                if (sectionRead != "")
                {
                    query += " AND Questions.Text LIKE 'What section do you usually read more?' AND Answers.text = @sectionRead";
                }

                if (favoriteSports != "")
                {
                    query += " AND Questions.Text LIKE 'What sort of sports do you like?' AND Answers.text = @favoriteSports";
                }

                if (travelDestination != "")
                {
                    query += " AND Questions.Text LIKE 'What usually is the destinations that you like to travel?' AND Answers.text = @travelDestination";
                }

                if (username != "")
                {
                    query += " AND Registers.given_name = @username";
                }

                query += " ORDER BY Respondents.Id";

                myCommand = new SqlCommand(query, myConnection);
                myCommand.Parameters.AddWithValue("@gender", gender);
                myCommand.Parameters.AddWithValue("@age", age);
                myCommand.Parameters.AddWithValue("@stateOrTerritory", stateOrTerritory);
                myCommand.Parameters.AddWithValue("@homeSuburb", homeSuburb);
                myCommand.Parameters.AddWithValue("@postCode", postCode);
                myCommand.Parameters.AddWithValue("@email", email);
                myCommand.Parameters.AddWithValue("@bankUsed", bankUsed);
                myCommand.Parameters.AddWithValue("@additionalService", additionalService);
                myCommand.Parameters.AddWithValue("@newspaper", newspaper);
                myCommand.Parameters.AddWithValue("@sectionRead", sectionRead);
                myCommand.Parameters.AddWithValue("@favoriteSports", favoriteSports);
                myCommand.Parameters.AddWithValue("@travelDestination", travelDestination);
                myCommand.Parameters.AddWithValue("@username", username);

                myConnection.Open();

                SqlDataReader myReader = myCommand.ExecuteReader();

                var datatable = new DataTable();
                datatable.Load(myReader);

                if(datatable.Rows.Count > 0)
                {
                    var serializeObject = JsonConvert.SerializeObject(datatable);

                    list = (List<SearchListViewModel>)JsonConvert.DeserializeObject(serializeObject, typeof(List<SearchListViewModel>));
                }

                myConnection.Close();

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void AddRespondent(Respondent paramRespodent)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = _connectionString;
                SqlCommand myCommand;

                myCommand = new SqlCommand("INSERT INTO respondents (id,ip_address,respondend_at) " +
                                                                  "VALUES (@id,@ipAddres,@respondentAt)", myConnection);
                myCommand.Parameters.AddWithValue("@id", paramRespodent.Id);
                myCommand.Parameters.AddWithValue("@ipAddres", paramRespodent.IpAddres);
                myCommand.Parameters.AddWithValue("@respondentAt", paramRespodent.RespondendAt);

                myConnection.Open();
                int affectRows = myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception ex)
            { 
                
            }
        }

        private static int GetLastId(string TableName)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = _connectionString;
                SqlCommand myCommand;
                myCommand = new SqlCommand("SELECT TOP 1 id FROM " + TableName + " ORDER BY ID DESC", myConnection);
                myConnection.Open();
                SqlDataReader myReader = myCommand.ExecuteReader();

                int returnId = 0;
                while (myReader.Read())
                {
                    returnId = (int)myReader["id"];
                }
                myConnection.Close();
                return returnId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static int IncrementId(string TableName) 
        {
            return 1+GetLastId(TableName);
        }
    }
}