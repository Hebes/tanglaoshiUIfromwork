using System.IO;
using UnityEditor;
using UnityEngine;

namespace Tool
{
    /// <summary>
    /// Excle转换工具文件
    /// </summary>
    public class ToolHelper : EditorWindow
    {
        /// <summary>
        /// 检查路径
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static void ChackPath(string path,string filePath)
        {
            //检查是否有这个路径的存在
            if (!Directory.Exists(path))
            {
                UnityEngine.Debug.Log("路径不存在,进行创建...");
                Directory.CreateDirectory(path);
                UnityEngine.Debug.Log("创建成功!");
            }
            //检查是否有这个文件的存在
            if (!File.Exists(filePath))
            {
                UnityEngine.Debug.Log("文件不存在,进行创建...");
            }
            else
            {
                UnityEngine.Debug.Log("文件已存在,进行删除...");
                File.Delete(filePath);
                UnityEngine.Debug.Log("删除成功!");
                UnityEngine.Debug.Log("文件重新创建...");

            }
            StreamWriter writer = File.CreateText(filePath);//生成文件
            writer.Close();
            UnityEngine.Debug.Log("创建成功!");
        }

        /// <summary>
        /// 通过路径检文件夹是否存在，如果不存在则创建
        /// </summary>
        /// <param name="folderPath"></param>
        public static void ChackFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))//是否存在这个文件
            {
                UnityEngine.Debug.Log("文件夹不存在,正在创建...");
                Directory.CreateDirectory(folderPath);//创建
                AssetDatabase.Refresh();//刷新编辑器
                UnityEngine.Debug.Log("创建成功!");
            }
        }

        /// <summary>
        /// 通过路径检查文件是否存在，如果不存在则创建
        /// </summary>
        /// <param name="filePath">文件存放的路径</param>
        public static void ChackFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                UnityEngine.Debug.Log("文件不存在,进行创建...");
            }
            else
            {
                UnityEngine.Debug.Log("文件已存在,进行删除...");
                File.Delete(filePath);
                UnityEngine.Debug.Log("删除成功!");
                UnityEngine.Debug.Log("文件重新创建...");
            }
            using (StreamWriter writer = File.CreateText(filePath))//生成文件
            {
                writer.Close();
            }
            UnityEngine.Debug.Log("创建成功!");
        }

        /// <summary>
        /// 创建文件并写入内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">要写入文件的内容</param>
        public static void ChackFileAndWriter(string filePath,string content)
        {
            if (!File.Exists(filePath))
            {
                UnityEngine.Debug.Log("文件不存在,进行创建...");
                using (StreamWriter writer = File.CreateText(filePath))//生成文件
                {
                    writer.Write(content);
                    UnityEngine.Debug.Log("内容写入成功!");
                }
            }
            else
            {
                UnityEngine.Debug.Log("文件已存在,进行删除...");
                File.Delete(filePath);
                UnityEngine.Debug.Log("删除成功!");
                UnityEngine.Debug.Log("文件重新创建...");
                UnityEngine.Debug.Log("内容写入中...");
                using (StreamWriter writer = File.CreateText(filePath))//生成文件
                {
                    writer.Write(content);
                    UnityEngine.Debug.Log("内容写入成功!");
                }
            }
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 文件以追加写入的方式
        /// https://wenku.baidu.com/view/a8fdb767fd4733687e21af45b307e87100f6f85b.html
        /// 显示IO异常请在创建文件的时候Close下
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="content">内容</param>
        public static void FileWriteContent(string path,string content)
        {
            byte[] myByte = System.Text.Encoding.UTF8.GetBytes(content);
            using (FileStream fsWrite = new FileStream(path, FileMode.Append,FileAccess.Write))
            {
                fsWrite.Write(myByte, 0, myByte.Length);
            }
        }
    }
}
