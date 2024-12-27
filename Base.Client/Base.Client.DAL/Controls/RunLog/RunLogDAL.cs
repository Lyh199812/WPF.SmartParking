using Base.Client.Entity;
using Base.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using System.Windows;
using System.Windows.Media;

namespace Base.Client.DAL
{
    public class RunLogDAL : IRunLogDAL
    {
        public void AddRunLog(int type, string info, ObservableCollection<RunLogEntity> logs)
        {
            if (logs == null)
            {
                throw new ArgumentNullException(nameof(logs));
            }

            RunLogEntity runLog = new RunLogEntity
            {
                LogType = type,
                LogInfo = info,
                LogTime = DateTime.Now
            };

            switch (type)
            {
                case 0:
                    runLog.LogIcon = "\ue626";
                    runLog.IconColor = "Gray";
                    break;
                case 1:
                    runLog.LogIcon = "\ue638";
                    runLog.IconColor = "Green";
                    break;
                case 2:
                    runLog.LogIcon = "\ueb80";
                    runLog.IconColor = "Orange";
                    break;
                default:
                    runLog.LogIcon = "\ue610";
                    runLog.IconColor = "Red";
                    break;
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                logs.Insert(0, runLog);
               
                if (logs.Count > 100)
                {
                    CleanOldLogs(logs);
                }
            });
            SaveLog(runLog);
        }

        public void CleanOldLogs(ObservableCollection<RunLogEntity> logs)
        {
            if (logs == null || logs.Count <= 1)
            {
                return; // 如果日志为空或只有一条日志，直接返回
            }

            // 保留最后一条日志，清空其他日志
            RunLogEntity lastLog = logs[logs.Count - 1];
            logs.Clear();
            logs.Add(lastLog);
        }
        private static readonly object logLock = new object(); // 用于加锁，保证线程安全

        public virtual void SaveLog(RunLogEntity log)
        {
            // 获取当前应用程序的运行目录，并定位到上一级目录的 Logs 文件夹
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string logDirectory = Path.Combine(Directory.GetParent(Directory.GetParent(baseDirectory).FullName).FullName, "Logs");

            // 如果 Logs 文件夹不存在，则创建
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            // 根据当前日期创建一个子文件夹，格式为 yyyy-MM-dd
            string dateFolder = Path.Combine(logDirectory, DateTime.Now.ToString("yyyy-MM-dd"));
            if (!Directory.Exists(dateFolder))
            {
                Directory.CreateDirectory(dateFolder);
            }

            // 设置日志文件路径，每天生成一个 log.txt 文件
            string logFilePath = Path.Combine(dateFolder, "log.txt");

            // 准备要保存的日志内容
            string logEntry = $"{DateTime.Now:HH:mm:ss} [{log.LogType}] {log.LogInfo} ({log.LogIcon} - {log.IconColor})";

            try
            {
                // 使用锁确保文件写入是线程安全的
                lock (logLock)
                {
                    // 将日志内容追加到文件中
                    File.AppendAllText(logFilePath, logEntry + Environment.NewLine, Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                // 在 UI 线程上显示错误消息
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show($"写入日志文件时出错: {ex.ToString()}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                });
            }
        }
    }



}
