﻿using Coravel.Invocable;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaDaemon {
    public class MaaChecker : IInvocable {
        public Task Invoke() {
            // 指定进程名
            string mumu = "MuMuVMMHeadless";
            var maa = GetProcessFileName("MAA");
            if (!string.IsNullOrEmpty(maa)) {
                if (CheckIfNeedKillAndKill(maa)) {
                    KillProcess(mumu);
                    Process.Start(maa);
                }
            }

            return Task.CompletedTask;
        }

        public bool CheckIfNeedKillAndKill(string  processName) {
            // 获取所有同名进程
            Process[] processes = Process.GetProcessesByName(processName);

            // 遍历进程并打印运行时间
            foreach (Process process in processes) {
                TimeSpan runTime = DateTime.Now - process.StartTime;
                if (runTime > TimeSpan.FromHours(2)) {
                    process.Kill();
                    Console.WriteLine($"{processName}运行了超过2小时，已杀死");
                    return true;
                }
            }
            return false;
        }
        public void KillProcess(string processName) {
            // 获取所有同名进程
            Process[] processes = Process.GetProcessesByName(processName);

            // 遍历进程并打印运行时间
            foreach (Process process in processes) {
                TimeSpan runTime = DateTime.Now - process.StartTime;
                process.Kill();
                Console.WriteLine($"已杀死{processName}");
            }
        }

        public string GetProcessFileName(string processName) {
            // 获取所有同名进程
            Process[] processes = Process.GetProcessesByName(processName);

            // 遍历进程并打印可执行文件路径
            foreach (Process process in processes) {
                try {
                    return process?.MainModule?.FileName ?? string.Empty;
                } catch (Exception ex) {
                    Console.WriteLine($"Error getting executable path for process {process.ProcessName}: {ex.Message}");
                }
            }
            return string.Empty;
        }
    }
}
