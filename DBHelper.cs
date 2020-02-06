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
        public string Difficulty { get; set; }
        public string ValueList { get; set; }
        public string PlacedList { get; set; }
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
        public int StylesChanged { get; set; }
        public int AvatarChanged { get; set; }
        public int BoughtItems { get; set; }
        public int BoughtStyles { get; set; }
        public int CoinsSpent { get; set; }
        public int ArticlesClicked { get; set; }
        public int SubjectClicked { get; set; }
        public int SubjectAdded { get; set; }
        public int SubjectRemoved { get; set; }
        public int GenericStat1 { get; set; }
        public int GenericStat2 { get; set; }
        public int GenericStat3 { get; set; }
        public int GenericStat4 { get; set; }
        public int GenericStat5 { get; set; }
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
        public string Ingress { get; set; }
        public string Image { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Tag { get; set; }
        public DateTime DatePosted { get; set; }
        public int Plus { get; set; }
        public string ArtikelReactions { get; set; }
        public string ReactionSum { get; set; }
    }

    [Table("CNF")]
    public class CustomNewsfeedTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public long Article { get; set; }
        public int NewsScore { get; set; }
        public string Header { get; set; }
        public string Ingress { get; set; }
        public string Image { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Tag { get; set; }
        public DateTime DatePosted { get; set; }
        public int Plus { get; set; }
        public string ArtikelReactions { get; set; }
        public string ReactionSum { get; set; }
    }
    [Table("Reaction")]
    public class ReactionTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public long Article { get; set; }
        public int User { get; set; }
        public int Reaktion { get; set; }
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
        public string Category { get; set; }
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
        public List<AvatarItemsTable> GetItemFromCategory(string ID)
        {
            return DB.Query<AvatarItemsTable>("SELECT * FROM Items WHERE Category = '" + ID + "'");
        }
        public List<AvatarItemsTable> GetItemFromID(long ID)
        {
            return DB.Query<AvatarItemsTable>("SELECT * FROM Items WHERE ID = ?", ID.ToString());
        }

        public List<VoteQuestionTable> GetVoteQuestions(int ID)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("VoteQuestion", "Query", "SELECT * FROM VoteQuestions WHERE Stage = " + ID, App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<VoteQuestionTable>>(Result.JSON);
        }
        public List<VoteTable> GetVotes(int ID)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Vote", "Query", "SELECT * FROM Votes WHERE Question = " + ID, App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<VoteTable>>(Result.JSON);
        }
        public List<QuizTable> GetQuestion(int ID)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Quiz", "Query", "SELECT * FROM Questions WHERE ID = " + ID, App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<QuizTable>>(Result.JSON);
        }
        public int LoadNF(int start, int stop, string Filter, string Author, string Tag)
        {
            int Nr = 0;



            for (int x = start; x < stop; x++)
            {
                for (int i = 0; i < 2; i++)
                {
                    while (ComLock)
                    {

                    };
                    ComLock = true;
                    string JSONResult = "";
                    if ((Filter == "" || Filter == "All" && Filter != null) && (Author == "" || Author == "All" && Filter != null) && (Tag == "" || Tag == "All" && Tag != null))
                    {
                        JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Newsfeed", "Query", "SELECT * FROM Newsfeed LIMIT 1 OFFSET (SELECT COUNT(*) FROM Newsfeed) - " + (x).ToString(), -1)));
                        Console.WriteLine("No Filter");
                    }
                    else
                    {

                        JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Newsfeed", "Query", "SELECT * FROM Newsfeed Where Category LIKE '%" + Filter + "%' AND Author LIKE '%" + Author + "%' AND Tag LIKE '%" + Tag + "%' LIMIT 1 OFFSET(SELECT COUNT(*) FROM Newsfeed WHERE Category LIKE '%" + Filter + "%' AND Author LIKE '%" + Author + "%' AND Tag LIKE '%" + Tag + "%') - " + (x).ToString(), -1)));
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
                Device.BeginInvokeOnMainThread(() =>
                {


                    App.LS.LoadingText.Text = "Laddar in Dina Val. " + x + " artiklar inladdade.";

                });
                

                Nr = x;
            }
            return Nr;
        }
        public int BatchLoadNF(int start, int stop, string Filter, string Author, string Tag)
        {
            int Nr = 0;

            int BS = stop - start;

            
                for (int i = 0; i < 5; i++)
                {
                    while (ComLock)
                    {

                    };
                    ComLock = true;
                    string JSONResult = "";
                    if ((Filter == "" || Filter == "All" && Filter != null) && (Author == "" || Author == "All" && Filter != null) && (Tag == "" || Tag == "All" && Tag != null))
                    {
                        JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Newsfeed", "Query", "SELECT * FROM Newsfeed LIMIT "+BS+" OFFSET (SELECT COUNT(*) FROM Newsfeed) - " + (stop).ToString(), -1)));
                        Console.WriteLine("No Filter");
                    }
                    else
                    {

                        JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Newsfeed", "Query", "SELECT * FROM Newsfeed Where Category LIKE '%" + Filter + "%' AND Author LIKE '%" + Author + "%' AND Tag LIKE '%" + Tag + "%' LIMIT " + BS + " OFFSET(SELECT COUNT(*) FROM Newsfeed WHERE Category LIKE '%" + Filter + "%' AND Author LIKE '%" + Author + "%' AND Tag LIKE '%" + Tag + "%') - " + (stop).ToString(), -1)));
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

                        var ArticleList = JsonConvert.DeserializeObject<List<NewsfeedTable>>(Result.JSON);

                        foreach (NewsfeedTable Article in ArticleList)
                        {
                            DB.Insert(Article);
                        }
                        //Console.WriteLine("JSON Deserialized");
                        
                        //Console.WriteLine("Article Inserted");
                        break;
                    }
                    else
                    {

                        //ParseRssFile();
                        //App.Online = false;

                    }
                }
                Device.BeginInvokeOnMainThread(() =>
                {


                    App.LS.LoadingText.Text = "Laddar in Dina Val. Artiklar inladdade.";

                });


                Nr = stop;
            
            return Nr;
        }
        public int LoadCNF(int start, int stop, List<string> Filter_, List<string> Author_, List<string> Tag_)
        {
            int Nr = 0;



            for (int x = start; x < stop; x++)
            {
                for (int i = 0; i < 2; i++)
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
                Device.BeginInvokeOnMainThread(() =>
                {


                    App.LS.LoadingText.Text = "Laddar in det samlade Nyhetsflödet. " + x + " artiklar inladdade.";


                });
                
                Nr = x;
            }
            return Nr;
        }
        public int BatchLoadCNF(int start, int stop, List<string> Filter_, List<string> Author_, List<string> Tag_)
        {
            int Nr = 0;

            int BS = stop - start;

            
                for (int i = 0; i < 2; i++)
                {
                    while (ComLock)
                    {

                    };
                    ComLock = true;
                    string JSONResult = "";
                    if ((!Filter_.Any()) && (!Author_.Any()) && (!Tag_.Any()))
                    {
                        JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Newsfeed", "Query", "SELECT * FROM Newsfeed LIMIT " + BS + " OFFSET (SELECT COUNT(*) FROM Newsfeed) - " + (stop).ToString(), -1)));
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

                        JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Newsfeed", "Query", "SELECT * FROM Newsfeed WHERE " + C + " OR " + A + " OR " + T + " LIMIT " + BS + " OFFSET(SELECT COUNT(*) FROM Newsfeed WHERE " + C + " OR " + A + " OR " + T + ") - " + (stop).ToString(), -1)));
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

                        var ArticleList = JsonConvert.DeserializeObject<List<CustomNewsfeedTable>>(Result.JSON);
                        Nr = ArticleList.Count;
                        foreach (CustomNewsfeedTable Article in ArticleList)
                        {
                            DB.Insert(Article);
                        }
                    //Console.WriteLine("Article Inserted");
                    break;
                    }
                    else
                    {

                        //ParseRssFile();
                        //App.Online = false;

                    }
                }
                Device.BeginInvokeOnMainThread(() =>
                {


                    App.LS.LoadingText.Text = "Laddar in det samlade Nyhetsflödet. Artiklar inladdade.";


                });

                
            
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
        public List<PicrossTable> LoadPicross()
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Picross", "Query", "SELECT * FROM Picross WHERE ID = 1", App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<PicrossTable>>(Result.JSON);
        }
        public List<SudokuTable> LoadSudoku()
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Sudoku", "Query", "SELECT * FROM Sudoku WHERE ID = 1", App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<SudokuTable>>(Result.JSON);
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

        public List<VoteTable> VoteCheck(int ID, int Q)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Vote", "Query", "SELECT * FROM Votes WHERE User = " + ID + " AND Question = " + Q, App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<VoteTable>>(Result.JSON);

        }

        public void InsertReaction(ReactionTable RSS)
        {
            TCP(JsonConvert.SerializeObject(new JSONObj("Reaction", "Insert", JsonConvert.SerializeObject(RSS), App.LoggedinUser.ID)));
        }
        public void DeleteReaction(ReactionTable RSS)
        {
            TCP(JsonConvert.SerializeObject(new JSONObj("Reaction", "Delete", JsonConvert.SerializeObject(RSS), App.LoggedinUser.ID)));
        }

        public List<ReactionTable> GetReactionsFromUser(int ID)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Reaction", "Query", "SELECT * FROM Reactions WHERE Article = " + ID, App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<ReactionTable>>(Result.JSON);
        }
        public List<ReactionTable> GetReactionsFromArticle(int ID)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("VoteQuestion", "Query", "SELECT * FROM Reactions WHERE User = " + ID, App.LoggedinUser.ID)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<ReactionTable>>(Result.JSON);
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
            LocalExecute("INSERT INTO Items (ID,Descriptions,ImagePath,InventorySlot,Price,Category) VALUES " +
                "(1, 'Röd_Tröja', 'avatar_body1.png', 'Body', 5, 'Ct1'), " +
                "(2, 'Blå_Tröja', 'avatar_body2.png', 'Body', 5, 'Ct1'), " +
                "(3, 'Grön_Tröja', 'avatar_body3.png', 'Body', 5, 'Ct1'), " +
                "(4, 'Lila_Tröja', 'avatar_body4.png', 'Body', 12, 'Ct1'), " +
                "(5, 'Svart_Tröja', 'avatar_body5.png', 'Body', 12, 'Ct1'), " +
                "(6, 'Frölunda_Tröja', 'avatar_body6.png', 'Body', 12, 'Ct1'), " +
                "(7, 'HV71_Tröja', 'avatar_body7.png', 'Body', 12, 'Ct1'), " +
                "(8, 'Blåvit_Tröja', 'avatar_body8.png', 'Body', 25, 'Ct1'), " +
                "(9, 'Rödvit_Tröja', 'avatar_body9.png', 'Body', 25, 'Ct1'), " +
                "(10, 'Svartgrön_Tröja', 'avatar_body10.png', 'Body', 25, 'Ct1'), " +
                "(11, 'Brunt_Kort', 'avatar_hair1.png', 'Hair', 10, 'Hair'), " +
                "(12, 'Svart_Kort', 'avatar_hair2.png', 'Hair', 10, 'Hair'), " +
                "(13, 'Blått_Kort', 'avatar_hair3.png', 'Hair', 10, 'Hair'), " +
                "(14, 'Lila_Kort', 'avatar_hair4.png', 'Hair', 10, 'Hair'), " +
                "(15, 'Grått_Kort', 'avatar_hair5.png', 'Hair', 10, 'Hair'), " +
                "(16, 'Svart_Långt', 'avatar_hair6.png', 'Hair', 10, 'Hair'), " +
                "(17, 'Rött_Långt', 'avatar_hair7.png', 'Hair', 10, 'Hair'), " +
                "(18, 'Blått_Långt', 'avatar_hair8.png', 'Hair', 10, 'Hair'), " +
                "(19, 'Grått_Långt', 'avatar_hair9.png', 'Hair', 10, 'Hair'), " +
                "(20, 'Blondt_Långt', 'avatar_hair10.png', 'Hair', 10, 'Hair'), " +
                "(21, '#649FD4', '#649FD4', 'Style', 10, 'Style'), " +
                "(22, '#6fb110', '#6fb110', 'Style', 10, 'Style'), " +
                "(23, '#bb0066', '#bb0066', 'Style', 10, 'Style'), " +
                "(24, '#e0d8b3', '#e0d8b3', 'Style', 10, 'Style'), " +
                "(25, '#56E39F', '#56E39F', 'Style', 10, 'Style'), " +
                "(26, '#3B2C35', '#3B2C35', 'Style', 10, 'Style'), " +
                "(27, '#99C24D', '#99C24D', 'Style', 10, 'Style'), " +
                "(28, '#F18F01', '#F18F01', 'Style', 10, 'Style'), " +
                "(29, '#EF2648', '#EF2648', 'Style', 10, 'Style'), " +
                "(30, '#FFC818', '#FFC818', 'Style', 10, 'Style'), " +
                "(31, '#020202', '#020202', 'Style', 10, 'Style'), " +
                "(32, '#5EF4FE', '#5EF4FE', 'Style', 10, 'Style'), " +
                "(33, '#DF7782', '#DF7782', 'Style', 10, 'Style'), " +
                "(34, '#A84A5C', '#A84A5C', 'Style', 10, 'Style'), " +
                "(35, '#88382D', '#88382D', 'Style', 10, 'Style'), " +
                "(36, '#55134E', '#55134E', 'Style', 10, 'Style'), " +
                "(37, '#FFEC94', '#FFEC94', 'Style', 10, 'Style'), " +
                "(38, '#B0E57C', '#B0E57C', 'Style', 10, 'Style'), " +
                "(39, '#003366', '#003366', 'Style', 10, 'Style'), " +
                "(40, '#CCFF33', '#CCFF33', 'Style', 10, 'Style'), " +
                "(41, '#f27F60', '#f27F60', 'Style', 10, 'Style'), " +
                "(42, '#063336', '#063336', 'Style', 10, 'Style'), " +
                "(43, '#CC4B93', '#CC4B93', 'Style', 10, 'Style'), " +
                "(44, '#DADADA', '#DADADA', 'Style', 10, 'Style'), " +
                "(45, '#996699', '#996699', 'Style', 10, 'Style'), " +
                "(46, 'Röd_Hat', 'avatar_hair11.png', 'Hair', 10, 'Hat'), " +
                "(47, 'Röd_Keps', 'avatar_hair12.png', 'Hair', 10, 'Hat'), " +
                "(48, 'Blå_Keps', 'avatar_hair13.png', 'Hair', 10, 'Hat'), " +
                "(49, 'Brun_Pompador', 'avatar_hair14.png', 'Hair', 10, 'Hair'), " +
                "(50, 'Grått_Pompador', 'avatar_hair15.png', 'Hair', 10, 'Hair'), " +
                "(51, 'Neutral', 'avatar_expr1.png', 'Expr', 5, 'Expr'), " +
                "(52, 'Frågande', 'avatar_expr2.png', 'Expr', 5, 'Expr'), " +
                "(53, 'Ledsen', 'avatar_expr3.png', 'Expr', 5, 'Expr'), " +
                "(54, 'Glad', 'avatar_expr4.png', 'Expr', 5, 'Expr'), " +
                "(55, 'Blinkar', 'avatar_expr5.png', 'Expr', 5, 'Expr'), " +
                "(56, 'Svart Helt', 'avatar_beard1.png', 'Beard', 10, 'Beard'), " +
                "(57, 'Brunt Helt', 'avatar_beard2.png', 'Beard', 10, 'Beard'), " +
                "(58, 'Rött Helt', 'avatar_beard3.png', 'Beard', 10, 'Beard'), " +
                "(59, 'Grått Helt', 'avatar_beard4.png', 'Beard', 10, 'Beard'), " +
                "(60, 'Blondt Helt', 'avatar_beard5.png', 'Beard', 10, 'Beard'), " +
                "(61, 'Röd_Tröja', 'avatar_body11.png', 'Body', 5, 'Ct2'), " +
                "(62, 'Blå_Tröja', 'avatar_body12.png', 'Body', 5, 'Ct2'), " +
                "(63, 'Grön_Tröja', 'avatar_body13.png', 'Body', 5, 'Ct2'), " +
                "(64, 'Lila_Tröja', 'avatar_body14.png', 'Body', 12, 'Ct2'), " +
                "(65, 'Svart_Tröja', 'avatar_body15.png', 'Body', 12, 'Ct2'), " +
                "(66, 'Frölunda_Tröja', 'avatar_body16.png', 'Body', 12, 'Ct2'), " +
                "(67, 'HV71_Tröja', 'avatar_body17.png', 'Body', 12, 'Ct2'), " +
                "(68, 'Blåvit_Tröja', 'avatar_body18.png', 'Body', 25, 'Ct2'), " +
                "(69, 'Rödvit_Tröja', 'avatar_body19.png', 'Body', 25, 'Ct2'), " +
                "(70, 'Svartgrön_Tröja', 'avatar_body20.png', 'Body', 25, 'Ct2'), " +
                "(71, 'Röd_Kostym', 'avatar_body21.png', 'Body', 5, 'Ct1'), " +
                "(72, 'Blå_Kostym', 'avatar_body22.png', 'Body', 5, 'Ct1'), " +
                "(73, 'Grön_Kostym', 'avatar_body23.png', 'Body', 5, 'Ct1'), " +
                "(74, 'Lila_Kostym', 'avatar_body24.png', 'Body', 12, 'Ct1'), " +
                "(75, 'Svart_Kostym', 'avatar_body25.png', 'Body', 12, 'Ct1'), " +
                "(76, 'Röd_V-Ring', 'avatar_body26.png', 'Body', 12, 'Ct2'), " +
                "(77, 'Blå_V-Ring', 'avatar_body27.png', 'Body', 12, 'Ct2'), " +
                "(78, 'Grön_V-Ring', 'avatar_body28.png', 'Body', 25, 'Ct2'), " +
                "(79, 'Lila_V-Ring', 'avatar_body29.png', 'Body', 25, 'Ct2'), " +
                "(80, 'Svart_V-Ring', 'avatar_body30.png', 'Body', 25, 'Ct2'), " +
                "(81, 'Röd_Sleeveless', 'avatar_body31.png', 'Body', 5, 'Ct3'), " +
                "(82, 'Blå_Sleeveless', 'avatar_body32.png', 'Body', 5, 'Ct3'), " +
                "(83, 'Grön_Sleeveless', 'avatar_body33.png', 'Body', 5, 'Ct3'), " +
                "(84, 'Lila_Sleeveless', 'avatar_body34.png', 'Body', 12, 'Ct3'), " +
                "(85, 'Svart_Sleeveless', 'avatar_body35.png', 'Body', 12, 'Ct3'), " +
                "(86, 'Röd_Skjorta', 'avatar_body36.png', 'Body', 12, 'Ct3'), " +
                "(87, 'Blå_Skjorta', 'avatar_body37.png', 'Body', 12, 'Ct3'), " +
                "(88, 'Grön_Skjorta', 'avatar_body38.png', 'Body', 25, 'Ct3'), " +
                "(89, 'Lila_Skjorta', 'avatar_body39.png', 'Body', 25, 'Ct3'), " +
                "(90, 'Svart_Skjorta', 'avatar_body40.png', 'Body', 25, 'Ct3'), " +
                "(91, 'Röd_T-Shirt', 'avatar_body41.png', 'Body', 5, 'Ct4'), " +
                "(92, 'Blå_T-Shirt', 'avatar_body42.png', 'Body', 5, 'Ct4'), " +
                "(93, 'Grön_T-Shirt', 'avatar_body43.png', 'Body', 5, 'Ct4'), " +
                "(94, 'Lila_T-Shirt', 'avatar_body44.png', 'Body', 12, 'Ct4'), " +
                "(95, 'Svart_T-Shirt', 'avatar_body45.png', 'Body', 12, 'Ct4'), " +
                "(96, 'Röd_Shoulder-Top', 'avatar_body46.png', 'Body', 12, 'Ct4'), " +
                "(97, 'Blå_Shoulder-Top', 'avatar_body47.png', 'Body', 12, 'Ct4'), " +
                "(98, 'Grön_Shoulder-Top', 'avatar_body48.png', 'Body', 25, 'Ct4'), " +
                "(99, 'Lila_Shoulder-Top', 'avatar_body49.png', 'Body', 25, 'Ct4'), " +
                "(100, 'Svart_Shoulder-Top', 'avatar_body50.png', 'Body', 25, 'Ct4'), " +
                "(101, 'Rustning', 'avatar_body51.png', 'Body', 50, 'Ct1'), " +
                "(102, 'Rustning', 'avatar_body52.png', 'Body', 50, 'Ct2'), " +
                "(103, 'Svart och Kort', 'avatar_hair61.png', 'Hair', 10, 'Hair'), " +
                "(104, 'Brunt och Kort', 'avatar_hair62.png', 'Hair', 10, 'Hair'), " +
                "(105, 'Rött och Kort', 'avatar_hair63.png', 'Hair', 10, 'Hair'), " +
                "(106, 'Grått och Kort', 'avatar_hair64.png', 'Hair', 10, 'Hair'), " +
                "(107, 'Blond och Kort', 'avatar_hair65.png', 'Hair', 10, 'Hair'), " +
                "(108, 'Svart med Keps', 'avatar_hair16.png', 'Hair', 15, 'Hat'), " +
                "(109, 'Brunt med Keps', 'avatar_hair17.png', 'Hair', 15, 'Hat'), " +
                "(110, 'Rött med Keps', 'avatar_hair18.png', 'Hair', 15, 'Hat'), " +
                "(111, 'Grått med Keps', 'avatar_hair19.png', 'Hair', 15, 'Hat'), " +
                "(112, 'Blond med Keps', 'avatar_hair20.png', 'Hair', 10, 'Hat'), " +
                "(113, 'Svart Fauxhawk', 'avatar_hair21.png', 'Hair', 10, 'Hair'), " +
                "(114, 'Brunt Fauxhawk', 'avatar_hair22.png', 'Hair', 10, 'Hair'), " +
                "(115, 'Röd Fauxhawk', 'avatar_hair23.png', 'Hair', 10, 'Hair'), " +
                "(116, 'Gray Fauxhawk', 'avatar_hair24.png', 'Hair', 10, 'Hair'), " +
                "(117, 'Blond Fauxhawk', 'avatar_hair25.png', 'Hair', 10, 'Hair'), " +
                "(118, 'Snitsig Svart', 'avatar_hair26.png', 'Hair', 10, 'Hair'), " +
                "(119, 'Snitsig  Brun', 'avatar_hair27.png', 'Hair', 10, 'Hair'), " +
                "(120, 'Snitsig  Röd', 'avatar_hair28.png', 'Hair', 10, 'Hair'), " +
                "(121, 'Snitsig  Grå', 'avatar_hair29.png', 'Hair', 10, 'Hair'), " +
                "(122, 'Snitsig  Blond', 'avatar_hair30.png', 'Hair', 10, 'Hair'), " +
                "(123, 'Hästsvans Svart', 'avatar_hair31.png', 'Hair', 10, 'Hair'), " +
                "(124, 'Hästsvans Brun', 'avatar_hair32.png', 'Hair', 10, 'Hair'), " +
                "(125, 'Hästsvans Röd', 'avatar_hair33.png', 'Hair', 10, 'Hair'), " +
                "(126, 'Hästsvans Grå', 'avatar_hair34.png', 'Hair', 10, 'Hair'), " +
                "(127, 'Hästsvans Blond', 'avatar_hair35.png', 'Hair', 10, 'Hair'), " +
                "(128, 'Lång Svart med Keps', 'avatar_hair36.png', 'Hair', 15, 'Hat'), " +
                "(129, 'Lång Brun med Keps', 'avatar_hair37.png', 'Hair', 15, 'Hat'), " +
                "(130, 'Lång Röd med Keps', 'avatar_hair38.png', 'Hair', 15, 'Hat'), " +
                "(131, 'Lång Grå med Keps', 'avatar_hair39.png', 'Hair', 15, 'Hat'), " +
                "(132, 'Lång Blond med Keps', 'avatar_hair40.png', 'Hair', 15, 'Hat'), " +
                "(133, 'Sidan Svart', 'avatar_hair41.png', 'Hair', 10, 'Hair'), " +
                "(134, 'Sidan Brun', 'avatar_hair42.png', 'Hair', 10, 'Hair'), " +
                "(135, 'Sidan Röd', 'avatar_hair43.png', 'Hair', 10, 'Hair'), " +
                "(136, 'Sidan Grå', 'avatar_hair44.png', 'Hair', 10, 'Hair'), " +
                "(137, 'Sidan Blond', 'avatar_hair45.png', 'Hair', 10, 'Hair'), " +
                "(138, 'Spikig Svart', 'avatar_hair46.png', 'Hair', 10, 'Hair'), " +
                "(139, 'Spikig Brun', 'avatar_hair47.png', 'Hair', 10, 'Hair'), " +
                "(140, 'Spikig Röd', 'avatar_hair48.png', 'Hair', 10, 'Hair'), " +
                "(141, 'Spikig Grå', 'avatar_hair49.png', 'Hair', 10, 'Hair'), " +
                "(142, 'Spikig Blond', 'avatar_hair50.png', 'Hair', 10, 'Hair'), " +
                "(143, 'Vågig Svart', 'avatar_hair51.png', 'Hair', 10, 'Hair'), " +
                "(144, 'Vågig Brun', 'avatar_hair52.png', 'Hair', 10, 'Hair'), " +
                "(145, 'Vågig Röd', 'avatar_hair53.png', 'Hair', 10, 'Hair'), " +
                "(146, 'Vågig Grå', 'avatar_hair54.png', 'Hair', 10, 'Hair'), " +
                "(147, 'Vågig Blond', 'avatar_hair55.png', 'Hair', 10, 'Hair'), " +
                "(148, 'Ordentlig Svart', 'avatar_hair56.png', 'Hair', 10, 'Hair'), " +
                "(149, 'Ordentlig Brun', 'avatar_hair57.png', 'Hair', 10, 'Hair'), " +
                "(150, 'Ordentlig Röd', 'avatar_hair58.png', 'Hair', 10, 'Hair'), " +
                "(151, 'Ordentlig Grå', 'avatar_hair59.png', 'Hair', 10, 'Hair'), " +
                "(152, 'Ordentlig Blond', 'avatar_hair60.png', 'Hair', 10, 'Hair'), " +
                "(153, 'Svart Goatee', 'avatar_beard6.png', 'Beard', 10, 'Beard'), " +
                "(154, 'Brun Goatee', 'avatar_beard7.png', 'Beard', 10, 'Beard'), " +
                "(155, 'Röd Goatee', 'avatar_beard8.png', 'Beard', 10, 'Beard'), " +
                "(156, 'Grå Goatee', 'avatar_beard9.png', 'Beard', 10, 'Beard'), " +
                "(157, 'Blond Goatee', 'avatar_beard10.png', 'Beard', 10, 'Beard'), " +
                "(158, 'Svart Anchor', 'avatar_beard11.png', 'Beard', 10, 'Beard'), " +
                "(159, 'Svart Anchor', 'avatar_beard12.png', 'Beard', 10, 'Beard'), " +
                "(160, 'Röd Anchor', 'avatar_beard13.png', 'Beard', 10, 'Beard'), " +
                "(161, 'Grå Anchor', 'avatar_beard14.png', 'Beard', 10, 'Beard'), " +
                "(162, 'Blond Anchor', 'avatar_beard15.png', 'Beard', 10, 'Beard'), " +
                "(163, 'Svart After Eight', 'avatar_beard16.png', 'Beard', 10, 'Beard'), " +
                "(164, 'Brun After Eight', 'avatar_beard17.png', 'Beard', 10, 'Beard'), " +
                "(165, 'Röd After Eight', 'avatar_beard18.png', 'Beard', 10, 'Beard'), " +
                "(166, 'Grå After Eight', 'avatar_beard19.png', 'Beard', 10, 'Beard'), " +
                "(167, 'Blond After Eight', 'avatar_beard20.png', 'Beard', 10, 'Beard'), " +
                "(168, 'Svart Connoisseur', 'avatar_beard21.png', 'Beard', 10, 'Beard'), " +
                "(169, 'Brun Connoisseur', 'avatar_beard22.png', 'Beard', 10, 'Beard'), " +
                "(170, 'Röd Connoisseur', 'avatar_beard23.png', 'Beard', 10, 'Beard'), " +
                "(171, 'Grå Connoisseur', 'avatar_beard24.png', 'Beard', 10, 'Beard'), " +
                "(172, 'Blond Connoisseur', 'avatar_beard25.png', 'Beard', 10, 'Beard'), " +
                "(173, 'Svart Fancy', 'avatar_beard26.png', 'Beard', 20, 'Beard'), " +
                "(174, 'Brun Fancy', 'avatar_beard27.png', 'Beard', 20, 'Beard'), " +
                "(175, 'Röd Fancy', 'avatar_beard28.png', 'Beard', 20, 'Beard'), " +
                "(176, 'Grå Fancy', 'avatar_beard29.png', 'Beard', 20, 'Beard'), " +
                "(177, 'Blond Fancy', 'avatar_beard30.png', 'Beard', 20, 'Beard'), " +
                "(178, 'Svart The Regent', 'avatar_beard31.png', 'Beard', 10, 'Beard'), " +
                "(179, 'Brun The Regent', 'avatar_beard32.png', 'Beard', 10, 'Beard'), " +
                "(180, 'Röd The Regent', 'avatar_beard33.png', 'Beard', 10, 'Beard'), " +
                "(181, 'Grå The Regent', 'avatar_beard34.png', 'Beard', 10, 'Beard'), " +
                "(182, 'Blond The Regent', 'avatar_beard35.png', 'Beard', 10, 'Beard'), " +
                "(183, 'Svart Tjock Fancy', 'avatar_beard36.png', 'Beard', 20, 'Beard'), " +
                "(184, 'Brun Tjock Fancy', 'avatar_beard37.png', 'Beard', 20, 'Beard'), " +
                "(185, 'Röd Tjock Fancy', 'avatar_beard38.png', 'Beard', 20, 'Beard'), " +
                "(186, 'Grå Tjock Fancy', 'avatar_beard39.png', 'Beard', 20, 'Beard'), " +
                "(187, 'Blond Tjock Fancy', 'avatar_beard40.png', 'Beard', 20, 'Beard'), " +
                "(189, 'Svart Brett', 'avatar_beard41.png', 'Beard', 20, 'Beard'), " +
                "(190, 'Brun Brett', 'avatar_beard42.png', 'Beard', 20, 'Beard'), " +
                "(191, 'Röd Brett', 'avatar_beard43.png', 'Beard', 20, 'Beard'), " +
                "(192, 'Grå Brett', 'avatar_beard44.png', 'Beard', 20, 'Beard'), " +
                "(193, 'Blond Brett', 'avatar_beard45.png', 'Beard', 20, 'Beard'), " +
                "(194, '(Sportutstyrsel1)', 'avatar_body53.png', 'Body', '20', 'Ct1'), " +
                "(195, '(Sportutstyrsel2)', 'avatar_body54.png', 'Body', '20', 'Ct1'), " +
                "(196, '(Sportutstyrsel3)', 'avatar_body55.png', 'Body', '20', 'Ct1'), " +
                "(197, '(Sportutstyrsel4)', 'avatar_body56.png', 'Body', '20', 'Ct1'), " +
                "(198, '(Sportutstyrsel5)', 'avatar_body57.png', 'Body', '20', 'Ct1'), " +
                "(199, '(Sportutstyrsel6)', 'avatar_body58.png', 'Body', '20', 'Ct1'), " +
                "(200, '(Sportutstyrsel7)', 'avatar_body59.png', 'Body', '20', 'Ct1'), " +
                "(201, '(Sportutstyrsel8)', 'avatar_body61.png', 'Body', '20', 'Ct1'), " +
                "(202, '(Sportutstyrsel9)', 'avatar_body62.png', 'Body', '20', 'Ct1'), " +
                "(203, '(Sportutstyrsel10)', 'avatar_body63.png', 'Body', '20', 'Ct1'), " +
                "(204, '(Sportutstyrsel11)', 'avatar_body64.png', 'Body', '20', 'Ct1'), " +
                "(205, '(Sportutstyrsel12)', 'avatar_body65.png', 'Body', '20', 'Ct2'), " +
                "(206, '(Sportutstyrsel13)', 'avatar_body66.png', 'Body', '20', 'Ct2'), " +
                "(207, '(Sportutstyrsel14)', 'avatar_body67.png', 'Body', '20', 'Ct2'), " +
                "(208, '(Sportutstyrsel15)', 'avatar_body68.png', 'Body', '20', 'Ct2'), " +
                "(209, '(Sportutstyrsel16)', 'avatar_body69.png', 'Body', '20', 'Ct2'), " +
                "(210, '(Sportutstyrsel17)', 'avatar_body70.png', 'Body', '20', 'Ct2'), " +
                "(211, '(Sportutstyrsel18)', 'avatar_body71.png', 'Body', '20', 'Ct2'), " +
                "(212, '(Sportutstyrsel19)', 'avatar_body72.png', 'Body', '20', 'Ct2'), " +
                "(213, '(Sportutstyrsel20)', 'avatar_body73.png', 'Body', '20', 'Ct2'), " +
                "(214, '(Sportutstyrsel21)', 'avatar_body74.png', 'Body', '20', 'Ct2'), " +
                "(215, '(Sportutstyrsel22)', 'avatar_body75.png', 'Body', '20', 'Ct2'), " +
                "(216, '(Sportutstyrsel23)', 'avatar_body76.png', 'Body', '20', 'Ct2'); " +
             "");
        }
        public static string TCP(string JSON)
        {
            string Message = "";



            try
            {

                TcpClient tcpclnt = new TcpClient();
                //Console.WriteLine("Connecting.....");

                tcpclnt.Connect("35.228.169.200", 1518);
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
