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
        public int CommentNR { get; set; }
        public int User { get; set; }
        public string Comment { get; set; }
        public int Point { get; set; }
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
        public string PubDate { get; set; }
    }


    public class DBHelper 
    {

        static SQLiteConnection DB;

        public DBHelper(string dbPath)
        {
            DB = new SQLiteConnection(dbPath);
            DB.DropTable<RSSTable>();
            DB.CreateTable<RSSTable>();
        }


        public void Execute(string statement)
        {
            TCP(JsonConvert.SerializeObject(new JSONObj("User", "Execute", JsonConvert.SerializeObject(statement))));       
        }
        public List<UserTable> GetUsers()
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("User", "Query", JsonConvert.SerializeObject("SELECT * FROM Users"))));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<UserTable>>(Result.JSON);
        }
        public List<UserTable> GetUser(int ID_)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("User", "Query", JsonConvert.SerializeObject("SELECT * FROM Users WHERE ID = " + ID_))));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<UserTable>>(Result.JSON);
        }
        public List<CommentTable> GetComments(int ID_)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Comments", "Query", JsonConvert.SerializeObject("SELECT * FROM Comments WHERE Article = " + ID_ + " ORDER BY CommentNR"))));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<CommentTable>>(Result.JSON);
        }
        public List<CommentTable> GetComment(int ID_)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Comments", "Query", JsonConvert.SerializeObject("SELECT * FROM Comments WHERE ID = " + ID_ + " ORDER BY CommentNR"))));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<CommentTable>>(Result.JSON);
        }
        public void LoadRSS(int start, int stop)
        {
            for(int x = start; x < stop; x++)
            {
                var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("RSS", "Query", JsonConvert.SerializeObject("SELECT * FROM RSS WHERE ID = " + x.ToString()))));
                var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
                var Article = JsonConvert.DeserializeObject<List<RSSTable>>(Result.JSON).First();
                DB.Insert(Article);
            }
        }
        public List<RSSTable> GetRSS(int ID)
        {           
            return DB.Query<RSSTable>("SELECT * FROM RSS WHERE ID < " + ID.ToString() + " ORDER BY PubDate");     
        }
        public List<RSSTable> GetRss(int ID)
        {
            return DB.Query<RSSTable>("SELECT * FROM RSS WHERE ID = " + ID.ToString());
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
                var UserQuery = JsonConvert.DeserializeObject<JSONObj>(TCP(JsonConvert.SerializeObject(new JSONObj("User", "Query", JsonConvert.SerializeObject("SELECT * FROM Users WHERE ID = " + Token.User)))));
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
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Comments", "Query", JsonConvert.SerializeObject("SELECT Comment FROM Comments WHERE Article = " + parm))));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<CommentTable>>(Result.JSON).Count;
        }
   
        public List<SudokuTable> GetTile (int x , int y)
        {
            var JSONResult = TCP(JsonConvert.SerializeObject(new JSONObj("Sudoku", "Query", JsonConvert.SerializeObject("SELECT * FROM Sudoku WHERE X = " + x + " AND Y = " + y))));
            var Result = JsonConvert.DeserializeObject<JSONObj>(JSONResult);
            return JsonConvert.DeserializeObject<List<SudokuTable>>(Result.JSON);      
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
                Console.WriteLine("Connecting.....");

                tcpclnt.Connect("81.170.199.32", 1508);
                // use the ipaddress as in the server program

                Console.WriteLine("Connected");

                Stream stm = tcpclnt.GetStream();

                Encoding asen = Encoding.Default;
                byte[] ba = asen.GetBytes(JSON);
                Console.WriteLine("Transmitting.....");

                stm.Write(ba, 0, ba.Length);

                byte[] bb = new byte[1000000000];
                int k = stm.Read(bb, 0, 1024);

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
