using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Security.Cryptography;
using Xamarin.Forms;

namespace NWT
{
    public class JSONObj
    {
        public string Type { get; set; }
        public string Operation { get; set; }
        public string JSON { get; set; }
        public int UserID { get; set; }
        public JSONObj(string Type_, string OP_, string JSON_, int UserID_)
        {
            Type = Type_;
            Operation = OP_;
            JSON = JSON_;
            UserID = UserID_;
        }
    }




    public class UserTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int Age { get; set; }
        public int Plustokens { get; set; }
        public string AchievementString { get; set; }
        public string MissionString { get; set; }
        public int LoginStreak { get; set; }
        public int DailyLogin { get; set; }
    }

    
    public class TokenTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public int User { get; set; }
        public string Token { get; set; }
        public string IP { get; set; }
        public string LastUse { get; set; }
    }
    
    public class CommentTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public long Article { get; set; }
        public int UserSubmitted { get; set; }
        public int CommentNR { get; set; }
        public int User { get; set; }
        public string Comment { get; set; }
        public int Point { get; set; }
        public int Replynr { get; set; }
        public int Replylvl { get; set; }
    }
    
    public class SudokuTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
        public int Placed { get; set; }
    }

    [Table("RSS")]
    public class RSSTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PubDate { get; set; }
        public string Link { get; set; }
        public string Source { get; set; }
        public int Plus { get; set; }
        public int NewsScore { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Tag { get; set; }
        public string Content { get; set; }
        public string ImgSource { get; set; }
        public string Ordning { get; set; }
        public string Text { get; set; }
        public string Images { get; set; }
        public string Imagetext { get; set; }
    }

    public class StatsTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public int User { get; set; }
        public int Logins { get; set; }
        public int UseTime { get; set; }
        public int ArticlesRead { get; set; }
        public int PlusArticlesUnlocked { get; set; }
        public int InsandareSubmitted { get; set; }
        public int InsandareRead { get; set; }
        public int GameFinished { get; set; }
        public int QuestionSubmitted { get; set; }
        public int QuestionAnswered { get; set; }
        public int VoteQuestionSubmitted { get; set; }
        public int VoteSubmitted { get; set; }
        public int CommentsPosted { get; set; }
        public int TokensCollected { get; set; }
    }

    public class Task
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public int Progress { get; set; }
        public int Goal { get; set; }
        public int Completed { get; set; }
        public string Type { get; set; }
        public int Mission { get; set; }
    }
    

    public class PlusRSSTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public long Article { get; set; }
        public int User { get; set; }
    }




    [Table("Insandare")]
    public class UserRSSTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public string Rubrik { get; set; }
        public string Ingress { get; set; }
        public string Brodtext { get; set; }
        public long Referat { get; set; }
        public DateTime PubDate { get; set; }
        public int Author { get; set; }
    }

    public class QuizTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public string Category { get; set; }
        public string QuestionText { get; set; }
        public string ChoiceA { get; set; }
        public string ChoiceB { get; set; }
        public string ChoiceC { get; set; }
        public string ChoiceD { get; set; }
        public string CorrectAnswer { get; set; }
    }

    public class FavoritesTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public long Article { get; set; }
        public int User { get; set; }
        public string Header { get; set; }
        public string Image { get; set; }
    }

    [Table("HistT")]
    public class HistoryTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public long Article { get; set; }
        public int User { get; set; }
        public string Header { get; set; }
        public string Image { get; set; }
        public DateTime Readat { get; set; }
    }

    [Table("NF")]
    public class NewsfeedTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public long Article { get; set; }
        public int NewsScore { get; set; }
        public string Header { get; set; }
        public string Image { get; set; }      
        public string Category { get; set; }
        public int Plus { get; set; }
    }
    public class VoteQuestionTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public string Question { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public DateTime Posted { get; set; }
        public int Stage { get; set; }
        public int Winner { get; set; }
        public int TotalVotes1 { get; set; }
        public int TotalVotes2 { get; set; }
        public int TotalVotes3 { get; set; }
        public int TotalVotes4 { get; set; }

    }
    public class VoteTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public int User { get; set; }
        public int Question { get; set; }
        public int ChoosenOption { get; set; }
    }

    public class DBHelper 
    {

        static SQLiteConnection DB;
        public static bool ComLock = false;
        public DBHelper(string dbPath)
        {
            DB = new SQLiteConnection(dbPath);
            DB.DropTable<RSSTable>();
            DB.CreateTable<RSSTable>();
            DB.DropTable<UserRSSTable>();
            DB.CreateTable<UserRSSTable>();
            DB.DropTable<NewsfeedTable>();
            DB.CreateTable<NewsfeedTable>();
            DB.DropTable<HistoryTable>();
            DB.CreateTable<HistoryTable>();



        }




        public void Execute(string statement)
        {
            TCP(JsonConvert.SerializeObject(new JSONObj("User", "Execute", statement, App.LoggedinUser.ID)));       
        }
        public void Insert<T>(T arg)
        {
            Console.WriteLine(arg.ToString());
            DB.Insert(arg);
        }
        public void Delete<T>(T arg)
        {
            Console.WriteLine(arg.ToString());
            DB.Delete(arg);
        }

        public List<UserTable> GetUser(int ID_)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("User", "Query", "SELECT * FROM Users WHERE ID = " + ID_, App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<UserTable>>(Result.JSON);
        }

        public List<UserTable> GetUserByName(string Username)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("User", "Query", "SELECT * FROM Users WHERE Username = '" + Username+ "'", App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<UserTable>>(Result.JSON);
        }

        public List<CommentTable> GetComments(int ID_ , int LVL, int RNR)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Comments", "Query", "SELECT * FROM Comments WHERE Article = " + ID_ + " AND Replylvl = " + LVL + " AND (Replynr = "+ RNR+ " OR Replynr = -1) ORDER BY CommentNR", App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<CommentTable>>(Result.JSON);
        }

        public List<CommentTable> GetComment(int ID_)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Comments", "Query", "SELECT * FROM Comments WHERE ID = " + ID_ + " ORDER BY CommentNR", App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<CommentTable>>(Result.JSON);
        }
        
        public List<RSSTable> GetServerRSS(long ID_)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("RSS", "Query", "SELECT * FROM RSS WHERE ID = " + ID_, App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<RSSTable>>(Result.JSON);
        }

        public List<FavoritesTable> GetFavorites(int ID_)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Favorite", "Query", "SELECT * FROM Favorites WHERE User = " + ID_, App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<FavoritesTable>>(Result.JSON);
        }

        public List<HistoryTable> GetHistory(int ID_)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("History", "Query", "SELECT * FROM History WHERE User = "+ID_+" ORDER BY Readat LIMIT 10 OFFSET(SELECT COUNT(*) FROM History ) - 10 ", App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<HistoryTable>>(Result.JSON);
        }

        public List<HistoryTable> GetAllHistory(int ID_)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("History", "Query", "SELECT * FROM History WHERE User = " + ID_ + " ORDER BY Readat", App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<HistoryTable>>(Result.JSON);
        }

        public List<VoteQuestionTable> GetVoteQuestions(int ID)
        {           
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("VoteQuestion", "Query", "SELECT * FROM VoteQuestions WHERE Stage = " + ID, App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<VoteQuestionTable>>(Result.JSON);
        }

        public List<VoteTable> VoteCheck(int ID, int Q)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Vote", "Query", "SELECT * FROM Votes WHERE User = " + ID + " AND Question = " + Q, App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<VoteTable>>(Result.JSON);
            
        }

        public List<StatsTable> GetUserStats(int ID)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Stats", "Query", "SELECT * FROM Stats WHERE User = " + ID.ToString(), App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<StatsTable>>(Result.JSON);
        }



        public int LoadNF(int start, int stop)
        {
            int Nr = 0;
            for (int x = start; x < stop; x++)
            {
                for(int i = 0; i < 10; i++)
                {
                    while (ComLock)
                    {

                    };
                    ComLock = true;

                    var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Newsfeed", "Query", "SELECT * FROM Newsfeed LIMIT 1 OFFSET (SELECT COUNT(*) FROM Newsfeed) - " + (x+1).ToString(),-1)));
                    //Console.WriteLine(JSONResult.Length);
                    ComLock = false;

                    if (JSONResult != "No")
                    {

                        //Console.WriteLine("JSON Object Found");
                        var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);

                        //Console.WriteLine(Result.JSON);
                        if (Result.JSON == "[]")
                        {
                            break;
                        }

                        var Article = JsonConvert.DeserializeObject<List<NewsfeedTable>>(Result.JSON).First();
                        //Console.WriteLine("JSON Deserialized");
                        DB.Insert(Article);
                        //Console.WriteLine("Article Inserted");
                        break;
                    }
                    else
                    {

                        //ParseRssFile();
                        //App.Online = false;
                        
                    }                    
                }
                Nr = x;
            }
            return Nr;
        }

        public int LoadUserRSS(int start, int stop)
        {
            int Nr = 0;
            for (int x = start; x < stop; x++)
            {
                var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("UserRSS", "Query", "SELECT * FROM Insandare WHERE ID = " + x.ToString(), App.LoggedinUser.ID)));
                

                

                if (JSONResult != "No")
                {
                    var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
                    Console.WriteLine(Result.JSON);
                    if (Result.JSON == "[]")
                    {
                        break;
                    }

                    var Article = JsonConvert.DeserializeObject<List<UserRSSTable>>(Result.JSON).First();
                    DB.Insert(Article);
                }
                else
                {
                    x = -1;
                    App.Online = false;
                    break;
                }
                Nr = x;
            }
            return Nr;
        }

        public void InsertPlus(PlusRSSTable RSS)
        {
            TCP(JsonConvert.SerializeObject(new JSONObj("Plus", "Insert", JsonConvert.SerializeObject(RSS), App.LoggedinUser.ID)));
        }

        public bool CheckPlus(int Article)
        {
            

            if (App.LoggedinUser != null)
            {

                var Plus = new PlusRSSTable();
                Plus.User = App.LoggedinUser.ID;
                Plus.Article = Article;
                var Result = TCP(JsonConvert.SerializeObject(new JSONObj("Plus", "PlusCheck", JsonConvert.SerializeObject(Plus), App.LoggedinUser.ID)));

                if (Result != null)
                {
                    var JSON = JsonConvert.DeserializeObject<JSONObj>(Result).JSON;
                    var Test = JsonConvert.DeserializeObject<Boolean>(JSON);

                    if (Test)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            return false;
        }

        public void InsertComment(CommentTable Comment)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Comments", "Insert", JsonConvert.SerializeObject(Comment), App.LoggedinUser.ID)));
            App.LoggedinUser.MissionString = JsonConvert.DeserializeObject<JSONObj>(JSONResult).JSON;
        }
        public void InsertInsandare(UserRSSTable RSS)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("UserRSS", "Insert", JsonConvert.SerializeObject(RSS), App.LoggedinUser.ID)));
            App.LoggedinUser.MissionString = JsonConvert.DeserializeObject<JSONObj>(JSONResult).JSON;
        }
        public void StatUpdate(string Statement)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Stats", "Update", Statement, App.LoggedinUser.ID)));
            App.LoggedinUser.MissionString = JsonConvert.DeserializeObject<JSONObj>(JSONResult).JSON;
        }
        public void InsertQuestion(QuizTable Quiz)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Quiz", "Insert", JsonConvert.SerializeObject(Quiz), App.LoggedinUser.ID)));
            App.LoggedinUser.MissionString = JsonConvert.DeserializeObject<JSONObj>(JSONResult).JSON;
        }
        public void InsertHistory(HistoryTable RSS)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("History", "Insert", JsonConvert.SerializeObject(RSS), App.LoggedinUser.ID)));
            App.LoggedinUser.MissionString = JsonConvert.DeserializeObject<JSONObj>(JSONResult).JSON;
        }
        public void InsertVoteQuestion(VoteQuestionTable VQ)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("VoteQuestion", "Insert", JsonConvert.SerializeObject(VQ), App.LoggedinUser.ID)));
            App.LoggedinUser.MissionString = JsonConvert.DeserializeObject<JSONObj>(JSONResult).JSON;
        }
        public void InsertVote(VoteTable Vote)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Vote", "Insert", JsonConvert.SerializeObject(Vote), App.LoggedinUser.ID)));
            App.LoggedinUser.MissionString = JsonConvert.DeserializeObject<JSONObj>(JSONResult).JSON;
        }

 


        public void InsertFavorite(FavoritesTable RSS)
        {
            TCP(JsonConvert.SerializeObject(new JSONObj("Favorite", "Insert", JsonConvert.SerializeObject(RSS), App.LoggedinUser.ID)));
        }
        public void DeleteFavorite(FavoritesTable RSS)
        {
            TCP(JsonConvert.SerializeObject(new JSONObj("Favorite", "Delete", JsonConvert.SerializeObject(RSS), App.LoggedinUser.ID)));
        }

        public List<NewsfeedTable> GetNF(int ID)
        {          
            return DB.Query<NewsfeedTable>("SELECT * FROM NF LIMIT " + ID.ToString() + " OFFSET(SELECT COUNT(*) FROM NF) - " + ID.ToString());     
        }

        public List<NewsfeedTable> GetNf(long ID)
        {
            return DB.Query<NewsfeedTable>("SELECT * FROM NF WHERE Article = ?" , ID.ToString());
        }

        public List<UserRSSTable> GetUserRSS(int ID)
        {                     
            return DB.Query<UserRSSTable>("SELECT * FROM Insandare WHERE ID < ? ORDER BY PubDate DESC", ID.ToString());
        }

        public List<UserRSSTable> GetUserRss(int ID)
        {
            return DB.Query<UserRSSTable>("SELECT * FROM Insandare WHERE ID = ?", ID.ToString());
        }



        public void Registration(UserTable User)
        {
            TCP(JsonConvert.SerializeObject(new JSONObj("User", "Register", JsonConvert.SerializeObject(User),-1)));           
        }

        public void Login(UserTable User)
        {
            var JSONObject = JsonConvert.DeserializeObject<JSONObj>(TCP(JsonConvert.SerializeObject(new JSONObj("Token", "Login", JsonConvert.SerializeObject(User),-1))));
            
            if(JSONObject.JSON != null)
            {
                var Token = JsonConvert.DeserializeObject<List<TokenTable>>(JSONObject.JSON).First();
                App.Token = Token;
                var UserQuery = JsonConvert.DeserializeObject<JSONObj>(TCP(JsonConvert.SerializeObject(new JSONObj("User", "Query", "SELECT * FROM Users WHERE ID = " + Token.User,Token.User))));
                App.LoggedinUser = JsonConvert.DeserializeObject<List<UserTable>>(UserQuery.JSON).First();
            }
            
        }
     
        public void Logout()
        {
            if (App.Token != null)
            {
                TCP(JsonConvert.SerializeObject(new JSONObj("Token", "Logout", JsonConvert.SerializeObject(App.Token), App.LoggedinUser.ID)));
                App.Token = null;
                App.LoggedinUser = null;
                App.Startpage.Detail = new NavigationPage(App.Loginpage) { BarBackgroundColor = Color.FromHex("#2f6e83"), BarTextColor = Color.FromHex("#FFFFFF"), };
            }
        }

        public bool TokenCheck()
        {
            if(App.Token != null)
            {
             
                var Token = new TokenTable();
                Token.User = App.Token.User;
                Token.Token = SHA256Hash(App.Token.Token + App.Token.User);            
                var Result = TCP(JsonConvert.SerializeObject(new JSONObj("Token", "TokenCheck", JsonConvert.SerializeObject(Token), App.LoggedinUser.ID)));
                if(Result != null)
                {                                     
                    var JSON = JsonConvert.DeserializeObject<JSONObj>(Result).JSON;
                    var Test = JsonConvert.DeserializeObject<Boolean>(JSON);
                    
                    if (Test)
                    {
                        
                        return true;
                    }
                    else
                    {
                        Logout();
                        return false;
                    }
                }
                
            }
            return false;
        }

        public bool Plustoken(UserTable User,int Balance)
        {
            var Pair = JsonConvert.SerializeObject(new KeyValuePair<UserTable, int>(User, Balance)); 
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("User", "Plustoken", Pair, App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);

            var UserQuery = JsonConvert.DeserializeObject<JSONObj>(TCP(JsonConvert.SerializeObject(new JSONObj("User", "Query", "SELECT * FROM Users WHERE ID = " + App.Token.User, App.LoggedinUser.ID))));
            App.LoggedinUser = JsonConvert.DeserializeObject<List<UserTable>>(UserQuery.JSON).First();

            return Convert.ToBoolean(Result.JSON);
        }


        public List<Task> MissionUpdate(UserTable User, string Operation)
        {

            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("User", "Mission", JsonConvert.SerializeObject(new KeyValuePair<UserTable, string>(User,Operation)), App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);


            var UserQuery = JsonConvert.DeserializeObject<JSONObj>(TCP(JsonConvert.SerializeObject(new JSONObj("User", "Query", "SELECT * FROM Users WHERE ID = " + App.Token.User, App.LoggedinUser.ID))));
            App.LoggedinUser = JsonConvert.DeserializeObject<List<UserTable>>(UserQuery.JSON).First();
            return JsonConvert.DeserializeObject<List<Task>>(App.LoggedinUser.MissionString);
        }

        public void MissionEvaluation()
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("User", "Mission", "", App.LoggedinUser.ID)));
            App.LoggedinUser.MissionString = JsonConvert.DeserializeObject<JSONObj>(JSONResult).JSON;
        }



        public void ChangePassword(string NewPass,string RepeatPass)
        {
            if (NewPass == RepeatPass)
            {
                var Statement = new KeyValuePair<UserTable, string>(App.LoggedinUser,NewPass);
                TCP(JsonConvert.SerializeObject(new JSONObj("User", "ChangePassword", JsonConvert.SerializeObject(Statement), App.LoggedinUser.ID)));
            }
        }

        public void UpdateInfo(UserTable Update)
        {        
                TCP(JsonConvert.SerializeObject(new JSONObj("User", "UpdateInfo", JsonConvert.SerializeObject(Update), App.LoggedinUser.ID)));      
        }



        public int CommentCount(int parm)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Comments", "Query", "SELECT Comment FROM Comments WHERE Article = " + parm, App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<CommentTable>>(Result.JSON).Count;
        }






        public List<SudokuTable> GetTile (int x , int y)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Sudoku", "Query", "SELECT * FROM Sudoku WHERE X = " + x + " AND Y = " + y, App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<SudokuTable>>(Result.JSON);      
        }





        public List<QuizTable> GetQuestion(int ID)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Quiz", "Query", "SELECT * FROM Questions WHERE ID = " + ID, App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<QuizTable>>(Result.JSON);
        }



        public string SHA256Hash(string input)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        
        public static string TCP(string JSON)
        {
            string Message = "";


            
            try
            {
                
                TcpClient tcpclnt = new TcpClient();
                //Console.WriteLine("Connecting.....");

                tcpclnt.Connect("109.228.152.124", 1518);
                // use the ipaddress as in the server program

                //Console.WriteLine("Connected");
                var Client = tcpclnt.Client;
                //Stream stm = tcpclnt.GetStream();
                Client.ReceiveTimeout = 3000;
                Client.SendTimeout = 3000;
                Encoding asen = Encoding.GetEncoding("iso-8859-1");
                byte[] ba = asen.GetBytes(JSON);
                //Console.WriteLine("Transmitting.....");

                //stm.Write(ba, 0, ba.Length);
                Client.Send(ba, ba.Length, SocketFlags.None);

                byte[] bb = new byte[100];

                int Length = Client.Receive(bb);

                for (int i = 0; i < Length; i++)
                {
                    Message += Convert.ToChar(bb[i]);
                }

                Length = Convert.ToInt32(Message);
                //Console.WriteLine("Length of Message is: " + Length);


                byte[] bc = asen.GetBytes("OK");
                Client.Send(bc, bc.Length, SocketFlags.None);


                byte[] bd = new byte[100000];
                int byteCount = 0;
                //Console.WriteLine("Recieved Bytecount is: " + byteCount);
                for (int i = 0; i < byteCount; i++)
                {
                    Message += Convert.ToChar(bd[i]);
                }

                int BR = 0;
                int Break = 0;
                Message = "";
                do
                {
                    
                    byteCount = Client.Receive(bd);
                    
                   // Console.WriteLine("Recieved Bytecount is: " + byteCount);
                    for (int i = 0; i < byteCount; i++)
                    {
                        Message += Convert.ToChar(bd[i]);
                    }
                    BR += byteCount;
                    //Console.WriteLine("Current Bytes Read: " + BR);

                    Break++;

                    if (Break > 100) 
                    {
                        Message = "No";
                        break;
                    }
                } while (BR < Length);

                




                //Console.WriteLine(Message);
                tcpclnt.Close();
                
                return Message;
            }

            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
                
                return "No";
            }
        }



    }
}
