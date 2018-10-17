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

namespace NWT
{
    public class JSONObj
    {
        public string Type { get; set; }
        public string Operation { get; set; }
        public string JSON { get; set; }
        public JSONObj(string Type_, string OP_, string JSON_)
        {
            Type = Type_;
            Operation = OP_;
            JSON = JSON_;
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
        public int Article { get; set; }
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
        public string Link{ get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string ImgSource { get; set; }
        public string Category { get; set; }
        public DateTime PubDate { get; set; }
        public int Plus { get; set; }
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
    
    public class UpvoteTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public int UserSubmitted { get; set; }
        public int Article { get; set; }
        public int CommentNR { get; set; }
        public int User { get; set; }
        public int Point { get; set; }
    }

    public class PlusRSSTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public int Article { get; set; }
        public int User { get; set; }
    }

    [Table("ReadArticles")]
    public class RAL // Read Article List
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public int Article { get; set; }
        public DateTime Date { get; set; }
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
        public int Referat { get; set; }
        public DateTime PubDate { get; set; }
        public int Author { get; set; }
    }

    public class StatsTable
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public int User { get; set; }
        public int Startups { get; set; }
        public int Logins { get; set; }
        public int UseTime { get; set; }
        public int ArticlesClicked { get; set; }
        public int ArticlesRead { get; set; }
        public int PlusArticlesClicked { get; set; }
        public int PlusArticlesUnlocked { get; set; }
        public int InsandareSubmitted { get; set; }
        public int InsandareRead { get; set; }
        public int GameStarted { get; set; }
        public int GameFinished { get; set; }
    }

    [Table("LS")]
    public class LocalStatsTable
    {
        [PrimaryKey, Unique]
        public int ID { get; set; }
        public int Startups { get; set; }
        public int UseTime { get; set; }
        public int ArticlesClicked { get; set; }
        public int PlusArticlesClicked { get; set; }
        public int InsandareRead { get; set; }
        public int GameStarted { get; set; } 
        public int GameFinished { get; set; }
    }

    public class DBHelper 
    {

        static SQLiteConnection DB;

        public DBHelper(string dbPath)
        {
            DB = new SQLiteConnection(dbPath);
            DB.DropTable<RSSTable>();
            DB.CreateTable<RSSTable>();
            DB.DropTable<UserRSSTable>();
            DB.CreateTable<UserRSSTable>();

            DB.CreateTable<LocalStatsTable>();
            if(DB.Query<LocalStatsTable>("Select * From LS Where ID = 0").Count == 0)
            {
                Console.WriteLine("Reseting Statistics");
                RCLS();
            }

            
            //DB.DropTable<RAL>();
            DB.CreateTable<RAL>();
        }

        public void RCLS()
        {
            DB.DropTable<LocalStatsTable>();
            DB.CreateTable<LocalStatsTable>();
            var LS = new LocalStatsTable();
            LS.ID = 0;
            LS.Startups = 0;
            LS.UseTime = 0;
            LS.ArticlesClicked = 0;
            LS.PlusArticlesClicked = 0;
            LS.InsandareRead = 0;
            LS.GameStarted = 0;
            LS.GameFinished = 0;
            DB.Insert(LS);
        }


        public void Execute(string statement)
        {
            TCP(JsonConvert.SerializeObject(new JSONObj("User", "Execute", statement)));       
        }

        public List<UserTable> GetUser(int ID_)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("User", "Query", "SELECT * FROM Users WHERE ID = " + ID_)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<UserTable>>(Result.JSON);
        }

        public List<UserTable> GetUserByName(string Username)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("User", "Query", "SELECT * FROM Users WHERE Username = '" + Username+ "'")));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<UserTable>>(Result.JSON);
        }

        public List<CommentTable> GetComments(int ID_ , int LVL, int RNR)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Comments", "Query", "SELECT * FROM Comments WHERE Article = " + ID_ + " AND Replylvl = " + LVL + " AND (Replynr = "+ RNR+ " OR Replynr = -1) ORDER BY CommentNR")));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<CommentTable>>(Result.JSON);
        }

        public List<CommentTable> GetComment(int ID_)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Comments", "Query", "SELECT * FROM Comments WHERE ID = " + ID_ + " ORDER BY CommentNR")));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<CommentTable>>(Result.JSON);
        }
        
        public List<UpvoteTable> GetUpvote(int CommentNR,int Article, int US)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Upvote", "Query", "SELECT * FROM Upvotes WHERE Article = " + Article + " AND CommentNR = "+ CommentNR + " AND UserSubmitted = " + US)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            Console.WriteLine(Result.JSON);
            return JsonConvert.DeserializeObject<List<UpvoteTable>>(Result.JSON);
        }
       
        public int LoadRSS(int start, int stop)
        {
            int Nr = 0;
            for (int x = start; x < stop; x++)
            {
                var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("RSS", "Query", "SELECT * FROM RSS WHERE ID = " + x.ToString())));
                Console.WriteLine(JSONResult);

                

                if (JSONResult != "No")
                {
                    var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
                    Console.WriteLine(Result.JSON);
                    if (Result.JSON == "[]")
                    {
                        break;
                    }
                    var Article = JsonConvert.DeserializeObject<List<RSSTable>>(Result.JSON).First();
                    DB.Insert(Article);
                }
                else
                {
                    ParseRssFile();
                    App.Online = false;
                    break;
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
                var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("UserRSS", "Query", "SELECT * FROM Insandare WHERE ID = " + x.ToString())));
                

                

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
            TCP(JsonConvert.SerializeObject(new JSONObj("Plus", "Insert", JsonConvert.SerializeObject(RSS))));
        }

        public bool CheckPlus(int Article)
        {
            

            if (App.LoggedinUser != null)
            {

                var Plus = new PlusRSSTable();
                Plus.User = App.LoggedinUser.ID;
                Plus.Article = Article;
                var Result = TCP(JsonConvert.SerializeObject(new JSONObj("Plus", "PlusCheck", JsonConvert.SerializeObject(Plus))));

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

        public void InsertInsandare(UserRSSTable RSS)
        {
            TCP(JsonConvert.SerializeObject(new JSONObj("UserRSS", "Insert", JsonConvert.SerializeObject(RSS))));
        }

        public void InsertUpvote(UpvoteTable RSS)
        {
            TCP(JsonConvert.SerializeObject(new JSONObj("Upvote", "Insert", JsonConvert.SerializeObject(RSS))));
        }

        public void DeleteUpvote(UpvoteTable RSS)
        {
            TCP(JsonConvert.SerializeObject(new JSONObj("Upvote", "Delete", JsonConvert.SerializeObject(RSS))));
        }

        public List<RSSTable> GetRSS(int ID)
        {           
            return DB.Query<RSSTable>("SELECT * FROM RSS WHERE ID < ? ORDER BY PubDate DESC", ID.ToString());     
        }

        public List<RSSTable> GetRss(int ID)
        {
            return DB.Query<RSSTable>("SELECT * FROM RSS WHERE ID = ?" , ID.ToString());
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
            TCP(JsonConvert.SerializeObject(new JSONObj("User", "Register", JsonConvert.SerializeObject(User))));           
        }

        public void Login(UserTable User)
        {
            var JSONObject = JsonConvert.DeserializeObject<JSONObj>(TCP(JsonConvert.SerializeObject(new JSONObj("Token", "Login", JsonConvert.SerializeObject(User)))));
            
            if(JSONObject.JSON != null)
            {
                var Token = JsonConvert.DeserializeObject<List<TokenTable>>(JSONObject.JSON).First();
                App.Token = Token;
                var UserQuery = JsonConvert.DeserializeObject<JSONObj>(TCP(JsonConvert.SerializeObject(new JSONObj("User", "Query", "SELECT * FROM Users WHERE ID = " + Token.User))));
                App.LoggedinUser = JsonConvert.DeserializeObject<List<UserTable>>(UserQuery.JSON).First();
            }
            
        }
     
        public void Logout()
        {
            if (App.Token != null)
            {
                TCP(JsonConvert.SerializeObject(new JSONObj("Token", "Logout", JsonConvert.SerializeObject(App.Token))));
                App.Token = null;
                App.LoggedinUser = null;
                App.Mainpage.Children[2] = new LoginPage();
                App.Mainpage.CurrentPage = App.Mainpage.Children[2];
            }
        }

        public bool TokenCheck()
        {
            if(App.Token != null)
            {
             
                var Token = new TokenTable();
                Token.User = App.Token.User;
                Token.Token = SHA256Hash(App.Token.Token + App.Token.User);            
                var Result = TCP(JsonConvert.SerializeObject(new JSONObj("Token", "TokenCheck", JsonConvert.SerializeObject(Token))));
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
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("User", "Plustoken", Pair)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);

            var UserQuery = JsonConvert.DeserializeObject<JSONObj>(TCP(JsonConvert.SerializeObject(new JSONObj("User", "Query", "SELECT * FROM Users WHERE ID = " + App.Token.User))));
            App.LoggedinUser = JsonConvert.DeserializeObject<List<UserTable>>(UserQuery.JSON).First();

            return Convert.ToBoolean(Result.JSON);
        }

        public bool PointUpdate(CommentTable Comment, int Value)
        {
            var Pair = JsonConvert.SerializeObject(new KeyValuePair<CommentTable, int>(Comment, Value));
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Comments", "Point", Pair)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);

            

            return Convert.ToBoolean(Result.JSON);
        }

        public List<Task> MissionUpdate(UserTable User, string Operation)
        {

            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("User", "Mission", JsonConvert.SerializeObject(new KeyValuePair<UserTable, string>(User,Operation)))));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);


            var UserQuery = JsonConvert.DeserializeObject<JSONObj>(TCP(JsonConvert.SerializeObject(new JSONObj("User", "Query", "SELECT * FROM Users WHERE ID = " + App.Token.User))));
            App.LoggedinUser = JsonConvert.DeserializeObject<List<UserTable>>(UserQuery.JSON).First();
            return JsonConvert.DeserializeObject<List<Task>>(App.LoggedinUser.MissionString);
        }

        public void ChangePassword(string NewPass,string RepeatPass)
        {
            if (NewPass == RepeatPass)
            {
                var Statement = new KeyValuePair<UserTable, string>(App.LoggedinUser,NewPass);
                TCP(JsonConvert.SerializeObject(new JSONObj("User", "ChangePassword", JsonConvert.SerializeObject(Statement))));
            }
        }

        public void UpdateInfo(UserTable Update)
        {        
                TCP(JsonConvert.SerializeObject(new JSONObj("User", "UpdateInfo", JsonConvert.SerializeObject(Update))));      
        }

        public void InsertComment(CommentTable Comment)
        {
            TCP(JsonConvert.SerializeObject(new JSONObj("Comments", "Insert", JsonConvert.SerializeObject(Comment))));
        }

        public int CommentCount(int parm)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Comments", "Query", "SELECT Comment FROM Comments WHERE Article = " + parm)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<CommentTable>>(Result.JSON).Count;
        }

        public void InsertStats(StatsTable RSS)
        {
            TCP(JsonConvert.SerializeObject(new JSONObj("Stats", "Insert", JsonConvert.SerializeObject(RSS))));
        }

        public void UpdateStats(string Value)
        {
            if(App.LoggedinUser != null)
            {
                var statement = "UPDATE Stats SET " + Value + " = " + Value + " + 1 WHERE User = " + App.LoggedinUser.ID;
                TCP(JsonConvert.SerializeObject(new JSONObj("Stats", "Execute", statement)));
            }
            else
            {
                var statement = "UPDATE LS SET " + Value + " = " + Value + " + 1 WHERE ID = 0";
                Console.WriteLine(statement);
            }
        }

        public void LocalStatDump()
        {
            var Query = DB.Query<LocalStatsTable>("SELECT * FROM LS WHERE ID = '0'");

            Console.WriteLine("LSQuery: "+ Query.Count());
            if (Query.Any())
            {
                var LS = Query.First();
                Console.WriteLine("Current Local Stats: " + LS);

                var statement = "UPDATE Stats SET"
                    + "  Startups = Startups + " + LS.Startups
                    + ", UseTime = UseTime + " + LS.UseTime
                    + ", ArticlesClicked = ArticlesClicked + " + LS.ArticlesClicked
                    + ", PlusArticlesClicked = PlusArticlesClicked + " + LS.PlusArticlesClicked
                    + ", InsandareRead = InsandareRead + " + LS.InsandareRead
                    + ", GameStarted = GameStarted + " + LS.GameStarted
                    + ", GameFinished = GameFinished + " + LS.GameFinished
                    + " WHERE User = " + App.LoggedinUser.ID;
                TCP(JsonConvert.SerializeObject(new JSONObj("Stats", "Execute", statement)));
                RCLS();

            }

            
        }

        public List<SudokuTable> GetTile (int x , int y)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Sudoku", "Query", "SELECT * FROM Sudoku WHERE X = " + x + " AND Y = " + y)));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<SudokuTable>>(Result.JSON);      
        }

        public void ReadArticle(RAL Article)
        {
            DB.Insert(Article);
        }

        public int ReadArticleCount(UserTable User)
        {
            var Query = DB.Query<RAL>("SELECT * FROM ReadArticles WHERE User = " + User.ID);
            return Query.Count;
        }

        public List<RAL> GetReadArticle(int ID)
        {
            return DB.Query<RAL>("SELECT * FROM ReadArticles WHERE Article = ?", ID.ToString());
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

        public void ParseRssFile()
        {
            //
            string[] RssSource = { "SLA" };
            string[] RssSite = { "https://www.sla.se/feed/" };
            List<KeyValuePair<string, string>> RSSList = new List<KeyValuePair<string, string>>();


            for (int i = 0; i < RssSite.Length; i++)
            {
                RSSList.Add(new KeyValuePair<string, string>(RssSource[i], RssSite[i]));
            }

            foreach (KeyValuePair<string, string> T in RSSList)
            {
                var Compare = DB.Query<RSSTable>("SELECT * FROM RSS");
                XmlDocument rssXmlDoc = new XmlDocument();

                // Load the RSS file from the RSS URL
                rssXmlDoc.Load(T.Value);

                // Parse the Items in the RSS file
                XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");

                StringBuilder rssContent = new StringBuilder();

                XmlNamespaceManager nmspc = new XmlNamespaceManager(rssXmlDoc.NameTable);
                nmspc.AddNamespace("media", "http://search.yahoo.com/mrss/");

                // Iterate through the items in the RSS file


                foreach (XmlNode rssNode in rssNodes)
                {
                    var RSS = new RSSTable();

                    RSS.Source = T.Key;




                    XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                    RSS.Title = rssSubNode != null ? rssSubNode.InnerText : "";

                    rssSubNode = rssNode.SelectSingleNode("link");
                    RSS.Link = rssSubNode != null ? rssSubNode.InnerText : "";

                    rssSubNode = rssNode.SelectSingleNode("category");
                    RSS.Category = rssSubNode != null ? rssSubNode.InnerText : "";

                    rssSubNode = rssNode.SelectSingleNode("source");
                    RSS.Source = rssSubNode != null ? rssSubNode.InnerText : "";

                    rssSubNode = rssNode.SelectSingleNode("media:content", nmspc);
                    RSS.ImgSource = rssSubNode != null ? rssSubNode.Attributes["url"].Value : "";

                    rssSubNode = rssNode.SelectSingleNode("pubDate");
                    var date = rssSubNode != null ? rssSubNode.InnerText : "";

                    while (true)
                    {
                        if (Char.IsDigit(date[0]))
                            break;
                        else
                        {
                            date = date.Remove(0, 1);
                        }

                    }
                    Random rnd = new Random();
                    int test = rnd.Next(21);

                    if (test > 13)
                    {

                        RSS.Plus = 1;
                    }
                    else
                    {
                        RSS.Plus = 0;
                    }


                    RSS.PubDate = DateTime.Parse(date);
                    Console.WriteLine(RSS.PubDate);
                    Boolean Noinsert = false;

                    foreach (RSSTable rss in Compare)
                    {
                        if (RSS.Title == rss.Title)
                            Noinsert = true;
                    }

                    if (!Noinsert)
                        DB.Insert(RSS);
                }

            }

        }

        public static string TCP(string JSON)
        {
            string Message = "";
            try
            {
                
                TcpClient tcpclnt = new TcpClient();
                Console.WriteLine("Connecting.....");

                tcpclnt.Connect("109.228.152.124", 1508);
                // use the ipaddress as in the server program

                Console.WriteLine("Connected");

                Stream stm = tcpclnt.GetStream();

                Encoding asen = Encoding.Default;
                byte[] ba = asen.GetBytes(JSON);
                Console.WriteLine("Transmitting.....");

                stm.Write(ba, 0, ba.Length);

                byte[] bb = new byte[100000000];
                int k = stm.Read(bb, 0, 16384);
                Console.WriteLine("Read Complete");
                for (int i = 0; i < k; i++)
                    Message += Convert.ToChar(bb[i]);

                Console.WriteLine(Message);
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
