using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;

namespace DatabaseService
{
    /// <summary>
    /// Summary description for WebServiceDatabase
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //[System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]

    [WebService(Description = "A Web Service for WorkshopsManagement Database")]
    public class WebServiceDatabase : System.Web.Services.WebService
    {
        private SqlConnection myCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\WorkshopDatabase.mdf;Integrated Security=True");
        private SqlDataAdapter daUsers, daQuestions, daWorkshop, daAnswers;
        private DataSet dsUsers, dsQuestions, dsWorkshop, dsAnswers;


        [WebMethod(Description = "Counts the number of users")]
        private int returnNumberOfUsers()
        {
            int nr = 0;
            myCon.Open();
            dsUsers = new DataSet();
            daUsers = new SqlDataAdapter("SELECT * FROM Users", myCon);
            daUsers.Fill(dsUsers, "Users");
            foreach (DataRow dr in dsUsers.Tables["Users"].Rows) nr++;
            myCon.Close();
            return nr;
        }


        [WebMethod(Description = "Returns a message for login Check")]
        public string login(string username, string password)
        {

            myCon.Open();
            dsUsers = new DataSet();
            daUsers = new SqlDataAdapter("SELECT * FROM Users", myCon);

            daUsers.Fill(dsUsers, "Users");
            foreach (DataRow dr in dsUsers.Tables["Users"].Rows)
            {
                if (dr.ItemArray.GetValue(1).ToString().Equals(username))
                {
                    if (dr.ItemArray.GetValue(2).ToString().Equals(password))
                    {
                        myCon.Close();
                        return "Ok";
                    }
                    else
                    {
                        myCon.Close();
                        return "Incorrect Password!";
                    }
                }
            }

            myCon.Close();
            return "User Not Found!";
        }


        [WebMethod(Description = "Return the full list of users")]
        public List<string> GetUsers()
        {
            List<string> temp = new List<string>();

            myCon.Open();
            dsUsers = new DataSet();
            daUsers = new SqlDataAdapter("SELECT * FROM Users", myCon);
            daUsers.Fill(dsUsers, "Users");

            foreach (DataRow dr in dsUsers.Tables["Users"].Rows)
            {
                temp.Add(dr.ItemArray.GetValue(0).ToString() + " " +
                    dr.ItemArray.GetValue(1).ToString() + " " +
                    dr.ItemArray.GetValue(2).ToString() + " " +
                    dr.ItemArray.GetValue(3).ToString() + " " +
                    dr.ItemArray.GetValue(4).ToString() + " " +
                    dr.ItemArray.GetValue(5).ToString());
            }

            return temp;
        }

        [WebMethod(Description = "Return the full list of questions")]
        public List<string> getQuestions()
        {
            List<string> temp = new List<string>();
            myCon.Open();
            dsQuestions = new DataSet();
            daQuestions = new SqlDataAdapter("SELECT * FROM Questions", myCon);
            daQuestions.Fill(dsQuestions, "Questions");

            foreach (DataRow dr in dsQuestions.Tables["Questions"].Rows)
            {
                temp.Add(dr.ItemArray.GetValue(0).ToString() + " " +
                    dr.ItemArray.GetValue(1).ToString() + " " +
                    dr.ItemArray.GetValue(2).ToString() + " " +
                    dr.ItemArray.GetValue(3).ToString());
            }
            return temp;
        }

        [WebMethod(Description = "Return the full list of answers")]
        public List<string> getAnswers()
        {
            List<string> temp = new List<string>();
            myCon.Open();
            dsAnswers = new DataSet();
            daAnswers = new SqlDataAdapter("SELECT * FROM Answers", myCon);
            daAnswers.Fill(dsAnswers, "Answers");

            foreach (DataRow dr in dsAnswers.Tables["Answers"].Rows)
            {
                temp.Add(dr.ItemArray.GetValue(0).ToString() + " " +
                    dr.ItemArray.GetValue(1).ToString() + " " +
                    dr.ItemArray.GetValue(2).ToString() + " " +
                    dr.ItemArray.GetValue(3).ToString());
            }

            return temp;
        }

        [WebMethod(Description = "Return the full list of workshops")]
        public List<string> getWorkshops()
        {
            List<string> temp = new List<string>();
            myCon.Open();
            dsWorkshop = new DataSet();
            daWorkshop = new SqlDataAdapter("SELECT * FROM Workshops", myCon);
            daWorkshop.Fill(dsWorkshop, "Workshops");

            foreach (DataRow dr in dsWorkshop.Tables["Workshops"].Rows)
            {
                temp.Add(dr.ItemArray.GetValue(0).ToString() + " " +
                    dr.ItemArray.GetValue(1).ToString() + " " +
                    dr.ItemArray.GetValue(2).ToString() + " " +
                    dr.ItemArray.GetValue(3).ToString() + " " +
                    dr.ItemArray.GetValue(4).ToString());
            }
            return temp;
        }

        [WebMethod(Description = "Delete user from database")]
        public void deleteUser(string username)
        {
            myCon.Open();
            dsUsers = new DataSet();
            daUsers = new SqlDataAdapter("DELETE FROM Users WHERE UserName=@user", myCon);

            daUsers.SelectCommand
                .Parameters.AddWithValue("@user", username);
            daUsers.Fill(dsUsers, "Users");
            myCon.Close();
        }

        [WebMethod(Description = "Add user to database")]
        public void addUser(int id, string username, string password, string firstName, string lastName, string email)
        {
            myCon.Open();
            dsUsers = new DataSet();
            daUsers = new SqlDataAdapter("INSERT INTO Users VALUES(@Id, @user, @pass, @first, @last, @email)", myCon);

            daUsers.SelectCommand
                .Parameters.AddWithValue("@Id", id);
            daUsers.SelectCommand
                .Parameters.AddWithValue("@user", username);
            daUsers.SelectCommand
                .Parameters.AddWithValue("@pass", password);
            daUsers.SelectCommand
                .Parameters.AddWithValue("@first", firstName);
            daUsers.SelectCommand
                .Parameters.AddWithValue("@last", lastName);
            daUsers.SelectCommand
                .Parameters.AddWithValue("@email", email);

            daUsers.Fill(dsUsers, "Users");
            myCon.Close();
        }




        [WebMethod(Description = "Update user information")]
        public void updateUser(string findUsername, string username, string password, string firstName, string lastName, string email)
        {
            myCon.Open();
            dsUsers = new DataSet();
            daUsers = new SqlDataAdapter("UPDATE Users" +
                " SET UserName=@user,Password=@pass,FirstName=@first,LastName=@last,Email=@email" +
                " WHERE UserName=@findUser", myCon);

            daUsers.SelectCommand
                .Parameters.AddWithValue("@findUser", findUsername);
            daUsers.SelectCommand
                .Parameters.AddWithValue("@user", username);
            daUsers.SelectCommand
                .Parameters.AddWithValue("@pass", password);
            daUsers.SelectCommand
                .Parameters.AddWithValue("@first", firstName);
            daUsers.SelectCommand
                .Parameters.AddWithValue("@last", lastName);
            daUsers.SelectCommand
                .Parameters.AddWithValue("@email", email);

            daUsers.Fill(dsUsers, "Users");
            myCon.Close();
        }

        [WebMethod(Description = "Add Question")]
        public void addQuestion(int id, string question, string username, int qid)
        {
            myCon.Open();
            dsQuestions = new DataSet();
            daQuestions = new SqlDataAdapter("INSERT INTO Questions VALUES(@Id,@question,@user,@qid)", myCon);

            daQuestions.SelectCommand.Parameters.AddWithValue("@Id", id);
            daQuestions.SelectCommand.Parameters.AddWithValue("@question", question);
            daQuestions.SelectCommand.Parameters.AddWithValue("@user", username);
            daQuestions.SelectCommand.Parameters.AddWithValue("@qid", qid);

            daQuestions.Fill(dsQuestions, "Questions");
            myCon.Close();
        }

        [WebMethod(Description = "Delete Question")]
        public void deleteQuestion(int qid)
        {
            myCon.Open();
            dsAnswers = new DataSet();
            daAnswers = new SqlDataAdapter("DELETE FROM Answers Where QuestionId=@qid", myCon);
            daAnswers.SelectCommand.Parameters.AddWithValue("@qid", qid);
            daAnswers.Fill(dsAnswers, "Answers");


            dsQuestions = new DataSet();
            daQuestions = new SqlDataAdapter("DELETE FROM Questions WHERE QuestionId=@qid", myCon);
            daQuestions.SelectCommand.Parameters.AddWithValue("@qid", qid);
            daQuestions.Fill(dsQuestions, "Questions");
            myCon.Close();
        }

        [WebMethod(Description = "Update Question")]
        public void updateQuestion(int id, string question, string user)
        {
            myCon.Open();
            dsQuestions = new DataSet();
            daQuestions = new SqlDataAdapter("UPDATE Questions SET QuestionAsked=@question, UserName=@user" +
                " WHERE Id=@id", myCon);

            daQuestions.SelectCommand.Parameters.AddWithValue("@question", question);
            daQuestions.SelectCommand.Parameters.AddWithValue("@user", user);
            daQuestions.SelectCommand.Parameters.AddWithValue("@id", id);

            daQuestions.Fill(dsQuestions, "Questions");
            myCon.Close();
        }

        [WebMethod(Description = "Add Answer")]
        public void addAnswer(int id, string answer, string user, int qid)
        {
            myCon.Open();
            dsAnswers = new DataSet();
            daAnswers = new SqlDataAdapter("INSERT INTO Answers VALUES(@Id,@answer,@user,@qid)", myCon);

            daAnswers.SelectCommand.Parameters.AddWithValue("@Id", id);
            daAnswers.SelectCommand.Parameters.AddWithValue("@answer", answer);
            daAnswers.SelectCommand.Parameters.AddWithValue("@user", user);
            daAnswers.SelectCommand.Parameters.AddWithValue("@qid", qid);


            daAnswers.Fill(dsAnswers, "Answers");
            myCon.Close();
        }

        [WebMethod(Description = "Delete Answer")]
        public void deleteAnswer(int id)
        {
            myCon.Open();
            dsAnswers = new DataSet();
            daAnswers = new SqlDataAdapter("DELETE FROM Answers WHERE Id=@id", myCon);

            daAnswers.SelectCommand.Parameters.AddWithValue("@id", id);

            daAnswers.Fill(dsAnswers, "Answers");
            myCon.Close();
        }

        [WebMethod(Description = "Update Answer")]
        public void updateAnswer(int id, string answer, string user)
        {
            myCon.Open();
            dsAnswers = new DataSet();
            daAnswers = new SqlDataAdapter("UPDATE Answers SET Answer=@answer, UserName=@user" +
                " WHERE Id=@id", myCon);

            daAnswers.SelectCommand.Parameters.AddWithValue("@answer", answer);
            daAnswers.SelectCommand.Parameters.AddWithValue("@user", user);
            daAnswers.SelectCommand.Parameters.AddWithValue("@id", id);

            daAnswers.Fill(dsAnswers, "Answers");
            myCon.Close();
        }

        [WebMethod(Description = "Add workshop")]
        public void addWorkshop(string name, string speakerName, DateTime date, string description, string imagePath)
        {
            myCon.Open();
            dsWorkshop = new DataSet();
            daWorkshop = new SqlDataAdapter("INSERT INTO Workshops VALUES(@Name,@SpeakerName,@Date,@Description,@ImagePath)", myCon);

            daWorkshop.SelectCommand.Parameters.AddWithValue("@Name", name);
            daWorkshop.SelectCommand.Parameters.AddWithValue("@SpeakerName", speakerName);
            daWorkshop.SelectCommand.Parameters.AddWithValue("@Date", date);
            daWorkshop.SelectCommand.Parameters.AddWithValue("@Description", description);
            daWorkshop.SelectCommand.Parameters.AddWithValue("@ImagePath", imagePath);

            daWorkshop.Fill(dsWorkshop, "Workshops");

            myCon.Close();
        }

        [WebMethod(Description = "delete workshop")]
        public void deleteWorkshop(string name)
        {
            myCon.Open();
            dsWorkshop = new DataSet();
            daWorkshop = new SqlDataAdapter("DELETE FROM Workshops WHERE Name=@Name", myCon);

            daWorkshop.SelectCommand.Parameters.AddWithValue("@Name", name);

            daWorkshop.Fill(dsWorkshop, "Workshops");
            myCon.Close();
        }

        [WebMethod(Description = "delete workshop")]
        public void updateWorkshop(string name, string speakerName, DateTime date, string description, string imagePath)
        {
            myCon.Open();
            dsWorkshop = new DataSet();
            daWorkshop = new SqlDataAdapter("UPDATE Workshops SET SpeakerName=@SpeakerName , Date=@Date, Description=@Description, ImagePath=@ImagePath" +
                " WHERE Name=@Name", myCon);

            daWorkshop.SelectCommand.Parameters.AddWithValue("@Name", name);
            daWorkshop.SelectCommand.Parameters.AddWithValue("@SpeakerName", speakerName);
            daWorkshop.SelectCommand.Parameters.AddWithValue("@Date", date);
            daWorkshop.SelectCommand.Parameters.AddWithValue("@Description", description);
            daWorkshop.SelectCommand.Parameters.AddWithValue("@ImagePath", imagePath);

            daWorkshop.Fill(dsWorkshop, "Workshops");
            myCon.Close();
        }
    }
}
