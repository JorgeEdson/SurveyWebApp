using Survey.WebApp.Domain;
using Survey.WebApp.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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

        public static void Login() 
        { 
        
        }
    }
}