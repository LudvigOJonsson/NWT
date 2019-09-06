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
        public string TaggString { get; set; }
        public int LoginStreak { get; set; }
        public int DailyLogin { get; set; }
        public int TutorialProgress { get; set; }
        public string Inventory { get; set; }
        public string Avatar { get; set; }
        public string Style { get; set; }
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
        public double Modifier { get; set; }
    }
    

    public class PlusRSSTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public long Article { get; set; }
        public int User { get; set; }
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
        public string Author { get; set; }
        public string Category { get; set; }
        public string Tag { get; set; }
        public int Plus { get; set; }
    }

    [Table("CNF")]
    public class CustomNewsfeedTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public long Article { get; set; }
        public int NewsScore { get; set; }
        public string Header { get; set; }
        public string Image { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Tag { get; set; }
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

    public class PicrossTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public string Left { get; set; }
        public string Top { get; set; }
        public string Gameboard { get; set; }
    }

    [Table("Items")]
    public class AvatarItemsTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public string Descriptions { get; set; }
        public string ImagePath { get; set; }
        public string InventorySlot { get; set; }
        public int Price { get; set; }
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
            DB.DropTable<NewsfeedTable>();
            DB.CreateTable<NewsfeedTable>();
            DB.DropTable<HistoryTable>();
            DB.CreateTable<HistoryTable>();
            DB.DropTable<CustomNewsfeedTable>();
            DB.CreateTable<CustomNewsfeedTable>();
            DB.DropTable<AvatarItemsTable>();
            DB.CreateTable<AvatarItemsTable>();
            AvatarItemInit();
        }




        public void Execute(string statement)
        {
            TCP(JsonConvert.SerializeObject(new JSONObj("User", "Execute", statement, App.LoggedinUser.ID)));       
        }
        public void LocalExecute(string statement)
        {
            DB.Execute(statement);
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
            if (JSONResult == "No")
            {
                return null;
            }
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<UserTable>>(Result.JSON);
        }
        public List<CommentTable> GetComments(int ID_, int LVL, int RNR) { 

            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Comments", "Query", "SELECT * FROM Comments WHERE Article = " + ID_ + " AND Replylvl = " + LVL + " AND (Replynr = " + RNR + " OR Replynr = -1) ORDER BY CommentNR", App.LoggedinUser.ID)));
            if (JSONResult == "No")
            {
                return null;
            }
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<CommentTable>>(Result.JSON);
        }


        public List<RSSTable> GetServerRSS(long ID_)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("RSS", "Query", "SELECT * FROM RSS WHERE ID = " + ID_, App.LoggedinUser.ID)));
            if (JSONResult == "No")
            {
                Console.WriteLine("Failed to Load Article");
                return null;
            }
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<RSSTable>>(Result.JSON);
        }
        public List<FavoritesTable> GetFavorites(int ID_)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Favorite", "Query", "SELECT * FROM Favorites WHERE User = " + ID_, App.LoggedinUser.ID)));
            if (JSONResult == "No")
            {
                return null;
            }
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<FavoritesTable>>(Result.JSON);
        }
        public List<HistoryTable> GetHistory(int ID_)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("History", "Query", "SELECT * FROM History WHERE User = " + ID_ + " ORDER BY Readat LIMIT 10 OFFSET(SELECT COUNT(*) FROM History  WHERE User = " + ID_ + ") - 10 ", App.LoggedinUser.ID)));
            if (JSONResult == "No")
            {
                return null;
            }
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<HistoryTable>>(Result.JSON);
        }
        public List<HistoryTable> GetAllHistory(int ID_)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("History", "Query", "SELECT * FROM History WHERE User = " + ID_ + " ORDER BY Readat", App.LoggedinUser.ID)));
            if (JSONResult == "No")
            {
                return null;
            }
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<HistoryTable>>(Result.JSON);
        }
        public List<StatsTable> GetUserStats(int ID)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Stats", "Query", "SELECT * FROM Stats WHERE User = " + ID.ToString(), App.LoggedinUser.ID)));
            if (JSONResult == "No")
            {
                return null;
            }
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<StatsTable>>(Result.JSON);
        }

        public List<NewsfeedTable> GetNF(int ID)
        {
            return DB.Query<NewsfeedTable>("SELECT * FROM NF LIMIT " + ID.ToString() + " OFFSET(SELECT COUNT(*) FROM NF) - " + ID.ToString());
        }
        public List<NewsfeedTable> GetCNF(int ID)
        {
            return DB.Query<NewsfeedTable>("SELECT * FROM CNF LIMIT " + ID.ToString() + " OFFSET(SELECT COUNT(*) FROM CNF) - " + ID.ToString());
        }
        public List<NewsfeedTable> GetNf(long ID)
        {
            return DB.Query<NewsfeedTable>("SELECT * FROM NF WHERE Article = ?", ID.ToString());
        }
        public List<AvatarItemsTable> GetAllItems()
        {
            return DB.Query<AvatarItemsTable>("SELECT * FROM Items");
        }
        public List<AvatarItemsTable> GetItemFromType(string ID)
        {
            return DB.Query<AvatarItemsTable>("SELECT * FROM Items WHERE InventorySlot = '" + ID +"'");
        }
        public List<AvatarItemsTable> GetItemFromID(long ID)
        {
            return DB.Query<AvatarItemsTable>("SELECT * FROM Items WHERE ID = ?", ID.ToString());
        }

        public int LoadNF(int start, int stop, string Filter, string Author, string Tag)
        {
            int Nr = 0;



            for (int x = start; x < stop; x++)
            {
                for (int i = 0; i < 10; i++)
                {
                    while (ComLock)
                    {

                    };
                    ComLock = true;
                    string JSONResult = "";
                    if ((Filter == "" || Filter == "All" && Filter != null) && (Author == "" || Author == "All" && Filter != null) && (Tag == "" || Tag == "All" && Tag != null))
                    {
                        JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Newsfeed", "Query", "SELECT * FROM Newsfeed LIMIT 1 OFFSET (SELECT COUNT(*) FROM Newsfeed) - " + (x + 1).ToString(), -1)));
                        Console.WriteLine("No Filter");
                    }
                    else
                    {

                        JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Newsfeed", "Query", "SELECT * FROM Newsfeed Where Category LIKE '%" + Filter + "%' AND Author LIKE '%" + Author + "%' AND Tag LIKE '%" + Tag + "%' LIMIT 1 OFFSET(SELECT COUNT(*) FROM Newsfeed WHERE Category LIKE '%" + Filter + "%' AND Author LIKE '%" + Author + "%' AND Tag LIKE '%" + Tag + "%') - " + (x + 1).ToString(), -1)));
                        Console.WriteLine("Filter");
                    }
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
        public int LoadCNF(int start, int stop, List<string> Filter_, List<string> Author_, List<string> Tag_)
        {
            int Nr = 0;



            for (int x = start; x < stop; x++)
            {
                for (int i = 0; i < 10; i++)
                {
                    while (ComLock)
                    {

                    };
                    ComLock = true;
                    string JSONResult = "";
                    if ((!Filter_.Any()) && (!Author_.Any()) && (!Tag_.Any()))
                    {
                        JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Newsfeed", "Query", "SELECT * FROM Newsfeed LIMIT 1 OFFSET (SELECT COUNT(*) FROM Newsfeed) - " + (x).ToString(), -1)));
                        Console.WriteLine("No Filter");
                    }
                    else
                    {
                        string C = "Category LIKE ";
                        string A = "Author LIKE ";
                        string T = "Tag LIKE ";
                        string X = "'%%'";
                        string Y = "'%XXXX%'";
                        bool CF = true;
                        bool AF = true;
                        bool TF = true;
                        bool Change = false;
                        foreach (string s in Filter_)
                        {
                            if (!CF)
                            {
                                C += " OR ";
                            }

                            C += " '%" + s + "%'";
                            Change = true;
                            CF = false;
                        }
                        foreach (string s in Author_)
                        {
                            if (!AF)
                            {
                                A += " OR ";
                            }

                            A += " '%" + s + "%'";
                            Change = true;
                            AF = false;
                        }
                        foreach (string s in Tag_)
                        {
                            if (!TF)
                            {
                                T += " OR ";
                            }

                            T += " '%" + s + "%'";
                            Change = true;
                            TF = false;
                        }

                        if (CF)
                        {
                            if (Change)
                            {
                                C += Y;
                            }
                            else
                            {
                                C += X;
                            }

                        }
                        if (AF)
                        {

                            if (Change)
                            {
                                A += Y;
                            }
                            else
                            {
                                A += X;
                            }
                        }
                        if (TF)
                        {

                            if (Change)
                            {
                                T += Y;
                            }
                            else
                            {
                                T += X;
                            }
                        }

                        JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Newsfeed", "Query", "SELECT * FROM Newsfeed WHERE " + C + " OR " + A + " OR " + T + " LIMIT 1 OFFSET(SELECT COUNT(*) FROM Newsfeed WHERE " + C + " OR " + A + " OR " + T + ") - " + (x).ToString(), -1)));
                        Console.WriteLine("Filter");
                    }
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

                        var Article = JsonConvert.DeserializeObject<List<CustomNewsfeedTable>>(Result.JSON).First();
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

        public void InsertComment(CommentTable Comment)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Comments", "Insert", JsonConvert.SerializeObject(Comment), App.LoggedinUser.ID)));
            if (JSONResult != "No")
            {
                App.LoggedinUser.MissionString = JsonConvert.DeserializeObject<JSONObj>(JSONResult).JSON;
            }
            else
            {
                
            }
            
        }     
        public void StatUpdate(string Statement)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Stats", "Update", Statement, App.LoggedinUser.ID)));
            if (JSONResult != "No")
            {
                App.LoggedinUser.MissionString = JsonConvert.DeserializeObject<JSONObj>(JSONResult).JSON;
            }
            else
            {

            }

        }
        public void InsertHistory(HistoryTable RSS)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("History", "Insert", JsonConvert.SerializeObject(RSS), App.LoggedinUser.ID)));
            if (JSONResult != "No")
            {
                App.LoggedinUser.MissionString = JsonConvert.DeserializeObject<JSONObj>(JSONResult).JSON;
            }
            else
            {

            }

        }
        public void InsertFavorite(FavoritesTable RSS)
        {
            TCP(JsonConvert.SerializeObject(new JSONObj("Favorite", "Insert", JsonConvert.SerializeObject(RSS), App.LoggedinUser.ID)));
        }
        public void DeleteFavorite(FavoritesTable RSS)
        {
            TCP(JsonConvert.SerializeObject(new JSONObj("Favorite", "Delete", JsonConvert.SerializeObject(RSS), App.LoggedinUser.ID)));
        }

        public void Registration(UserTable User)
        {
            TCP(JsonConvert.SerializeObject(new JSONObj("User", "Register", JsonConvert.SerializeObject(User), -1)));
        }
        public void Login(UserTable User)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Token", "Login", JsonConvert.SerializeObject(User), -1)));
            if (JSONResult == "No")
            {

            }
            else
            {
                var JSONObject = JsonConvert.DeserializeObject<JSONObj>(JSONResult);

                if (JSONObject.JSON != null)
                {
                    var Token = JsonConvert.DeserializeObject<List<TokenTable>>(JSONObject.JSON).First();
                    App.Token = Token;
                    var UserQuery = JsonConvert.DeserializeObject<JSONObj>(TCP(JsonConvert.SerializeObject(new JSONObj("User", "Query", "SELECT * FROM Users WHERE ID = " + Token.User, Token.User))));
                    App.LoggedinUser = JsonConvert.DeserializeObject<List<UserTable>>(UserQuery.JSON).First();
                }
            }


        }
        public void Logout()
        {
            if (App.Token != null)
            {
                TCP(JsonConvert.SerializeObject(new JSONObj("Token", "Logout", JsonConvert.SerializeObject(App.Token), App.LoggedinUser.ID)));
                App.Token = null;
                App.LoggedinUser = null;
                App.Startpage.Detail = new NavigationPage(App.Loginpage) { BarBackgroundColor = Color.FromHex("#649FD4"), BarTextColor = Color.FromHex("#FFFFFF"), };
            }
        }
        public bool TokenCheck()
        {
            if (App.Token != null)
            {

                var Token = new TokenTable
                {
                    User = App.Token.User,
                    Token = SHA256Hash(App.Token.Token + App.Token.User)
                };
                var Result = TCP(JsonConvert.SerializeObject(new JSONObj("Token", "TokenCheck", JsonConvert.SerializeObject(Token), App.LoggedinUser.ID)));

                if(Result != null && Result != "No")
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
        public bool Plustoken(UserTable User, int Balance)
        {
            var Pair = JsonConvert.SerializeObject(new KeyValuePair<UserTable, int>(User, Balance));
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("User", "Plustoken", Pair, App.LoggedinUser.ID)));
            if (JSONResult == "No")
            {
                return false;
            }
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);


            JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("User", "Query", "SELECT * FROM Users WHERE ID = " + App.Token.User, App.LoggedinUser.ID)));
            if (JSONResult == "No")
            {
                return false;
            }
            var UserQuery = JsonConvert.DeserializeObject<JSONObj>(JSONResult);

            App.LoggedinUser = JsonConvert.DeserializeObject<List<UserTable>>(UserQuery.JSON).First();

            return Convert.ToBoolean(Result.JSON);
        }
        public void MissionEvaluation()
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("User", "Mission", "", App.LoggedinUser.ID)));
            if (JSONResult != "No")
            {
                App.LoggedinUser.MissionString = JsonConvert.DeserializeObject<JSONObj>(JSONResult).JSON;
            }
            else
            {

            }

        }
        public void ChangePassword(string NewPass, string RepeatPass)
        {
            if (NewPass == RepeatPass)
            {
                var Statement = new KeyValuePair<UserTable, string>(App.LoggedinUser, NewPass);
                TCP(JsonConvert.SerializeObject(new JSONObj("User", "ChangePassword", JsonConvert.SerializeObject(Statement), App.LoggedinUser.ID)));
            }
        }
        public void UpdateInfo(UserTable Update)
        {
            TCP(JsonConvert.SerializeObject(new JSONObj("User", "UpdateInfo", JsonConvert.SerializeObject(Update), App.LoggedinUser.ID)));
        }
        public void UpdateChoices(UserTable Update)
        {
            TCP(JsonConvert.SerializeObject(new JSONObj("User", "UpdateChoices", JsonConvert.SerializeObject(Update), App.LoggedinUser.ID)));
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("User", "Query", "SELECT * FROM Users WHERE ID = " + App.Token.User, App.LoggedinUser.ID)));
            if (JSONResult != "No")
            {
                var UserQuery = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
                App.LoggedinUser = JsonConvert.DeserializeObject<List<UserTable>>(UserQuery.JSON).First();
            }
            else
            {


            }                      
        }
        public void UpdateAvatarItems(UserTable Update)
        {
            TCP(JsonConvert.SerializeObject(new JSONObj("User", "UpdateItems", JsonConvert.SerializeObject(Update), App.LoggedinUser.ID)));
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("User", "Query", "SELECT * FROM Users WHERE ID = " + App.Token.User, App.LoggedinUser.ID)));
            if (JSONResult != "No")
            {
                var UserQuery = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
                App.LoggedinUser = JsonConvert.DeserializeObject<List<UserTable>>(UserQuery.JSON).First();
            }
            else { 


            }
        }
        public void UpdateTutorialProgress(UserTable Update)
        {
            TCP(JsonConvert.SerializeObject(new JSONObj("User", "UpdateTutorial", JsonConvert.SerializeObject(Update), App.LoggedinUser.ID)));
        }
        public int CommentCount(int parm)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Comments", "Query", "SELECT Comment FROM Comments WHERE Article = " + parm, App.LoggedinUser.ID)));


            if (JSONResult == "No")
            {
                return -1;
            }

            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<CommentTable>>(Result.JSON).Count;
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
        public void AvatarItemInit()
        {
            LocalExecute("INSERT INTO Items (ID,Descriptions,ImagePath,InventorySlot,Price) VALUES " +
                "(1, 'Röd_Tröja', 'avatar_body1.png', 'Body', 5), " +
                "(2, 'Blå_Tröja', 'avatar_body2.png', 'Body', 5), " +
                "(3, 'Grön_Tröja', 'avatar_body3.png', 'Body', 5), " +
                "(4, 'Lila_Tröja', 'avatar_body4.png', 'Body', 12), " +
                "(5, 'Svart_Tröja', 'avatar_body5.png', 'Body', 12), " +
                "(6, 'LjusBlå_Tröja', 'avatar_body6.png', 'Body', 12), " +
                "(7, 'Lavender_Tröja', 'avatar_body7.png', 'Body', 12), " +
                "(8, 'Blåvit_Tröja', 'avatar_body8.png', 'Body', 25), " +
                "(9, 'Rödvit_Tröja', 'avatar_body9.png', 'Body', 25), " +
                "(10, 'Svartgrön_Tröja', 'avatar_body10.png', 'Body', 25), " +
                "(11, 'Brunt_Kort', 'avatar_hair1.png', 'Hair', 10), " +
                "(12, 'Svart_Kort', 'avatar_hair2.png', 'Hair', 10), " +
                "(13, 'Blått_Kort', 'avatar_hair3.png', 'Hair', 10), " +
                "(14, 'Lila_Kort', 'avatar_hair4.png', 'Hair', 10), " +
                "(15, 'Grått_Kort', 'avatar_hair5.png', 'Hair', 10), " +
                "(16, 'Svart_Långt', 'avatar_hair6.png', 'Hair', 10), " +
                "(17, 'Rött_Långt', 'avatar_hair7.png', 'Hair', 10), " +
                "(18, 'Blått_Långt', 'avatar_hair8.png', 'Hair', 10), " +
                "(19, 'Grått_Långt', 'avatar_hair9.png', 'Hair', 10), " +
                "(20, 'Blondt_Långt', 'avatar_hair10.png', 'Hair', 10), " +
                "(21, '#649FD4', '#649FD4', 'Style', 10), " +
                "(22, '#6fb110', '#6fb110', 'Style', 10), " +
                "(23, '#bb0066', '#bb0066', 'Style', 10), " +
                "(24, '#e0d8b3', '#e0d8b3', 'Style', 10), " +
                "(25, '#56E39F', '#56E39F', 'Style', 10), " +
                "(26, '#3B2C35', '#3B2C35', 'Style', 10), " +
                "(27, '#99C24D', '#99C24D', 'Style', 10), " +
                "(28, '#F18F01', '#F18F01', 'Style', 10), " +
                "(29, '#EF2648', '#EF2648', 'Style', 10), " +
                "(30, '#FFC818', '#FFC818', 'Style', 10), " +
                "(31, '#020202', '#020202', 'Style', 10), " +
                "(32, '#5EF4FE', '#5EF4FE', 'Style', 10), " +
                "(33, '#DF7782', '#DF7782', 'Style', 10), " +
                "(34, '#A84A5C', '#A84A5C', 'Style', 10), " +
                "(35, '#88382D', '#88382D', 'Style', 10), " +
                "(36, '#55134E', '#55134E', 'Style', 10), " +
                "(37, '#FFEC94', '#FFEC94', 'Style', 10), " +
                "(38, '#B0E57C', '#B0E57C', 'Style', 10), " +
                "(39, '#003366', '#003366', 'Style', 10), " +
                "(40, '#CCFF33', '#CCFF33', 'Style', 10), " +
                "(41, '#f27F60', '#f27F60', 'Style', 10), " +
                "(42, '#063336', '#063336', 'Style', 10), " +
                "(43, '#CC4B93', '#CC4B93', 'Style', 10), " +
                "(44, '#DADADA', '#DADADA', 'Style', 10), " +
                "(45, '#996699', '#996699', 'Style', 10); " +
             "");
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

                Console.WriteLine("JSON Error testing");
                var Result = JsonConvert.DeserializeObject<JSONObj>(Message).JSON;
                Console.WriteLine("JSON Error testing Complete");



                tcpclnt.Close();

                return Message;
            }

            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
                
                return "No";
            }
        }
        private void Storage()
        {
            /*
              
    public List<UserTable> GetUserByName(string Username)
    {

        var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("User", "Query", "SELECT * FROM Users WHERE Username = '" + Username + "'", App.LoggedinUser.ID)));

        if (JSONResult == "No")
        {
            return null;
        }
        var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
        return JsonConvert.DeserializeObject<List<UserTable>>(Result.JSON);
    }              

    public List<CommentTable> GetComment(int ID_)
    {
        var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Comments", "Query", "SELECT * FROM Comments WHERE ID = " + ID_ + " ORDER BY CommentNR", App.LoggedinUser.ID)));
        if (JSONResult == "No")
        {
            return null;
        }
        var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
        return JsonConvert.DeserializeObject<List<CommentTable>>(Result.JSON);
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

    public void InsertInsandare(NewsfeedTable RSS)
    {
    var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("UserRSS", "Insert", JsonConvert.SerializeObject(RSS), App.LoggedinUser.ID)));
    App.LoggedinUser.MissionString = JsonConvert.DeserializeObject<JSONObj>(JSONResult).JSON;
    }
    public void InsertQuestion(QuizTable Quiz)
    {
    var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Quiz", "Insert", JsonConvert.SerializeObject(Quiz), App.LoggedinUser.ID)));
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


    public void InsertPlus(PlusRSSTable RSS)
    {
    TCP(JsonConvert.SerializeObject(new JSONObj("Plus", "Insert", JsonConvert.SerializeObject(RSS), App.LoggedinUser.ID)));
    }

    public bool CheckPlus(int Article)
    {


    if (App.LoggedinUser != null)
    {


    var Plus = new PlusRSSTable
    {
        User = App.LoggedinUser.ID,
        Article = Article
    };
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


    public List<PicrossTable> LoadPicross()
    {
    var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Picross", "Query", "SELECT * FROM Picross WHERE ID = 1", App.LoggedinUser.ID)));
    var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
    return JsonConvert.DeserializeObject<List<PicrossTable>>(Result.JSON);
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


    public List<Task> MissionUpdate(UserTable User, string Operation)
    {


        var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("User", "Mission", JsonConvert.SerializeObject(new KeyValuePair<UserTable, string>(User, Operation)), App.LoggedinUser.ID)));
        if (JSONResult == "No")
        {
            return null;
        }
        var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
        JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("User", "Query", "SELECT * FROM Users WHERE ID = " + App.Token.User, App.LoggedinUser.ID)));
        if (JSONResult == "No")
        {
            return null;
        }
        var UserQuery = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
        App.LoggedinUser = JsonConvert.DeserializeObject<List<UserTable>>(UserQuery.JSON).First();
        return JsonConvert.DeserializeObject<List<Task>>(App.LoggedinUser.MissionString);
    }

    */
        }

    }
}
