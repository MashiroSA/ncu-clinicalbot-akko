using System;
using System.Data.SQLite;
using ncu_clinicalbot_akko.Core;

namespace ncu_clinicalbot_akko.Extensions
{
    public class MedicalQuestionBasicExtension
    {
        /*
         * 数据库查询内容的参数
         */
        private int _id;
        private string _question;
        private string _a;
        private string _b;
        private string _c;
        private string _d;
        private string _answer;
        
        /*
         * 
         */
        private string _message;
        
        public MedicalQuestionBasicExtension(){} // 构造方法，为空

        public string GetRandomMedicalQuestion()
        {
            GetRandomMedicalQuestionInfoDB();
            return _message;
        }
        
        public string GetMedicalQuestion(int tID)
        {
            GetMedicalQuestionInfoDB(tID);
            return _message;
        }
        
        public string GetMedicalAnswer(int tID)
        {
            GetMedicalAnswerInfoDB(tID);
            return _message;
        }
        
        public string GetMedicalQuestionNumber()
        {
            GetMedicalQuestionNumberDB();
            return _message;
        }
        
        private int GetRandomMedicalQuestionInfoDB() // 巡查database中的随机题目及相关信息
        {
            // 基本信息
            string dbPath = AppDomain.CurrentDomain.BaseDirectory + @"/db/";
            string dbQAFile = dbPath + @"QA.db";
            
            SQLiteConnection sqliteConn = new SQLiteConnection("data source=" + dbQAFile);
            if (sqliteConn.State != System.Data.ConnectionState.Open)
            {
                sqliteConn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = sqliteConn;
                
                // 巡查数据库中的题目数量
                cmd.CommandText = "SELECT count(*) FROM Basic";
                SQLiteDataReader c = cmd.ExecuteReader();
                c.Read();
                int totalNumber = c.GetInt32(0);
                c.Close();
                
                //生成范围随机数
                Random rand = new Random();
                int targetID = rand.Next(1, totalNumber);

                cmd.CommandText = $"SELECT * FROM Basic WHERE ID= {targetID}"; // 查询数据
                
                SQLiteDataReader sr = cmd.ExecuteReader(); // 读取结果集
                while (sr.Read())
                {
                    this._id = sr.GetInt32(0);
                    this._question = sr.GetString(1);
                    this._a = sr.GetString(2);
                    this._b = sr.GetString(3);
                    this._c = sr.GetString(4);
                    this._d = sr.GetString(5);
                    this._answer = sr.GetString(6);
                    this._message =
                        $"已从数据库中获取到信息，题组：常规，模式：非答题模式，题目ID:{this._id}\n" +
                        $"{this._question}:\n" +
                        $"A:{this._a}\n" +
                        $"B:{this._b}\n" +
                        $"C:{this._c}\n" +
                        $"D:{this._d}\n" +
                        $"输入'查常规答案 id'获取相关题目的正确答案";
                }

                TraceLog.Log("", "常规医学题目:有人调用了随机常规题并从数据库中获取信息");
                sr.Close();
                cmd.ExecuteNonQuery();
            }
            sqliteConn.Close();

            return 0;
        }

        private int GetMedicalAnswerInfoDB(int tID)
        {
            // 基本信息
            string dbPath = AppDomain.CurrentDomain.BaseDirectory + @"/db/";
            string dbQAFile = dbPath + @"QA.db";
            
            SQLiteConnection sqliteConn = new SQLiteConnection("data source=" + dbQAFile);
            if (sqliteConn.State != System.Data.ConnectionState.Open)
            {
                sqliteConn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = sqliteConn;
                
                // 巡查数据库中的题目数量
                cmd.CommandText = "SELECT count(*) FROM Basic";
                SQLiteDataReader c = cmd.ExecuteReader();
                c.Read();
                int totalNumber = c.GetInt32(0);
                c.Close();
                
                // 确权输入的id是否合法，否则返回警告信息，todo:尚未设计异常 @Author MashiroSA
                if (tID <= 0 || tID > totalNumber)
                {
                    this._message = "不合法的输入，ID越界";
                }
                
                // 查询目的题目的答案
                cmd.CommandText = $"SELECT * FROM Basic WHERE ID= {tID}";
                
                SQLiteDataReader sr = cmd.ExecuteReader();
                while (sr.Read())
                {
                    this._id = sr.GetInt32(0);
                    this._answer = sr.GetString(6);
                    this._message =
                        $"已从数据库中获取到信息，题组：常规，模式：非答题模式\n" +
                        $"题目ID:{this._id} 的 答案为:{this._answer}";
                }

                TraceLog.Log("", "常规医学题目:有人调用了查答案并从数据库中获取信息");
                sr.Close();
                cmd.ExecuteNonQuery();
            }
            sqliteConn.Close();

            return 0;
        } // 获取database中的题目答案信息
        
        private int GetMedicalQuestionInfoDB(int tID) // 巡查database中的指定题目及相关信息
        {
            // 基本信息
            string dbPath = AppDomain.CurrentDomain.BaseDirectory + @"/db/";
            string dbQAFile = dbPath + @"QA.db";
            
            SQLiteConnection sqliteConn = new SQLiteConnection("data source=" + dbQAFile);
            if (sqliteConn.State != System.Data.ConnectionState.Open)
            {
                sqliteConn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = sqliteConn;
                
                // 巡查数据库中的题目数量
                cmd.CommandText = "SELECT count(*) FROM Basic";
                SQLiteDataReader c = cmd.ExecuteReader();
                c.Read();
                int totalNumber = c.GetInt32(0);
                c.Close();
                
                // 确权输入的id是否合法，否则返回警告信息，todo:尚未设计异常 @Author MashiroSA
                if (tID <= 0 || tID > totalNumber)
                {
                    this._message = "不合法的输入，ID越界";
                }
                else
                {
                    cmd.CommandText = "SELECT * FROM Basic WHERE ID= 1";
                
                    SQLiteDataReader sr = cmd.ExecuteReader();
                    while (sr.Read())
                    {
                        this._id = sr.GetInt32(0);
                        this._question = sr.GetString(1);
                        this._a = sr.GetString(2);
                        this._b = sr.GetString(3);
                        this._c = sr.GetString(4);
                        this._d = sr.GetString(5);
                        this._answer = sr.GetString(6);
                        this._message =
                            $"已从数据库中获取到信息，题组：常规，模式：非答题模式（查询模式），题目ID:{this._id}\n" +
                            $"{this._question}:\n" +
                            $"A:{this._a}\n" +
                            $"B:{this._b}\n" +
                            $"C:{this._c}\n" +
                            $"D:{this._d}\n" +
                            $"输入'查常规答案 id'获取相关题目的正确答案";
                    }
                    
                    TraceLog.Log("", "常规医学题目:有人调用了查题目并从数据库中获取信息");
                    sr.Close();
                }
                cmd.ExecuteNonQuery();
            }
            sqliteConn.Close();

            return 0;
        }
        
        private int GetMedicalQuestionNumberDB() // 巡查database中所有题目数量
        {
            // 基本信息
            string dbPath = AppDomain.CurrentDomain.BaseDirectory + @"/db/";
            string dbQAFile = dbPath + @"QA.db";

            int totalNumber = 0;
            SQLiteConnection sqliteConn = new SQLiteConnection("data source=" + dbQAFile);
            if (sqliteConn.State != System.Data.ConnectionState.Open)
            {
                sqliteConn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = sqliteConn;

                // 巡查数据库中的题目数量
                cmd.CommandText = "SELECT count(*) FROM Basic";
                SQLiteDataReader c = cmd.ExecuteReader();
                c.Read();
                totalNumber = c.GetInt32(0);
                c.Close();

                this._message = $"\n常规题库中，已查询到共{totalNumber}条题目";
                TraceLog.Log("", "常规医学题目:有人调用了查询题目数量并从数据库中获取信息");
                
            }
            return 0;
        }
    }
}