using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;

namespace setup
{
  public class IniConfig
  {
    protected List<string> lineList = new List<string>();

    /// <summary>
    /// 从字符串解析Ini
    /// </summary>
    /// <param name="iniText"></param>
    public void ParseFromText(string iniText)
    {
      string[] lines = Regex.Split(iniText, Environment.NewLine);
      ParseFromStrings(lines);
    }

    /// <summary>
    /// 返回Ini字符串
    /// </summary>
    /// <returns></returns>
    public string ToIniText()
    {
      StringBuilder sb = new StringBuilder();
      foreach (string line in lineList)
      {
        sb.AppendLine(line);
      }

      return sb.ToString();
    }

    /// <summary>
    /// 从数组解析
    /// </summary>
    /// <param name="lineAry"></param>
    public void ParseFromStrings(string [] lineAry)
    {
      lineList.Clear();
      foreach (string line in lineAry)
      {
        lineList.Add(line);
      }
    }

    /// <summary>
    /// 获取Ini文件数据
    /// </summary>
    /// <returns></returns>
    public string[] ToIniStrings()
    {
      return lineList.ToArray();
    }    

    /// <summary>
    /// 获取所有的Section
    /// </summary>
    /// <returns></returns>
    public string[] GetSectios()
    {
      List<string> secList = new List<string>();
      foreach (string line in lineList)
      {
        if (line.StartsWith("[") && line.EndsWith("]"))
        {
          secList.Add(line.Substring(1, line.Length - 2));
        }
      }
      return secList.ToArray();
    }

    /// <summary>
    /// 获取section下的所有ident
    /// </summary>
    /// <param name="section"></param>
    /// <returns></returns>
    public string[] GetIdents(string section)
    {
      List<string> identList = new List<string>();
      int secIndex = GetSectionIndex(section);
      if (secIndex >= 0)
      {
        int secNextIndex = GetNextSection(secIndex + 1);
        int secEndIndex = secNextIndex - 1;
        if (secNextIndex < 0)
        {
          secEndIndex = lineList.Count - 1;
        }

        for (int i = secIndex + 1; i <= secEndIndex; ++i)
        {
          string line = lineList[i];
          string ident = "";
          if (GetIdent(line, ref ident))
          {
            identList.Add(ident);
          }
        }
      }
      return identList.ToArray();
    }

    /// <summary>
    /// 读取一个Ident
    /// </summary>
    /// <param name="section"></param>
    /// <param name="ident"></param>
    /// <param name="defaultvalue"></param>
    /// <returns></returns>
    public string ReadIdent(string section, string ident, string defaultvalue)
    {
      int identIndex = GetIdentIndex(section, ident);
      if (identIndex >= 0)
      {
        string value = "";
        if (GetValue(lineList[identIndex], ref value))
        {
          return value;
        }
      }
      return defaultvalue;
    }

    /// <summary>
    /// 添加或者替换一个ident
    /// </summary>
    /// <param name="section"></param>
    /// <param name="ident"></param>
    /// <param name="value"></param>
    public void AddOrReplace(string section, string ident, string value)
    {
      int secIndex = GetSectionIndex(section);
      if (secIndex < 0)
      {
        lineList.Add("[" + section + "]");
        lineList.Add(ident + "=" + value);
        return;
      }

      int identIndex = GetIdentIndex(section, ident);
      if (identIndex >= 0)
      {
        lineList[identIndex] = ident + "=" + value;
      }
      else
      {
        string identvalue = ident + "=" + value;
        lineList.Insert(secIndex + 1, identvalue);
      }
    }

    public bool IdentExists(string section, string ident)
    {
      if (GetIdentIndex(section, ident) < 0)
      {
        return false;
      }
      return true;
    }

    public bool SectionExists(string section)
    {
      if (GetSectionIndex(section) < 0)
      {
        return false;
      }
      return true;
    }

    /// <summary>
    /// 删除一个section
    /// </summary>
    /// <param name="section"></param>
    public void DelSection(string section)
    {
      int secIndex = GetSectionIndex(section);
      if (secIndex < 0)
      {
        return;
      }

      int secNextIndex = GetNextSection(secIndex + 1);
      int secEndIndex = secNextIndex - 1;
      if (secNextIndex < 0)
      {
        secEndIndex = lineList.Count - 1;
      }
     
      for (int i = secIndex; i <= secEndIndex; ++i)
      {
        if (!lineList[i].Trim().StartsWith(";"))
        {
          lineList[i] = "";
        }
      }

      DelBlankLine();
    }

    public void DelIdent(string section, string ident)
    {
      int secIndex = GetIdentIndex(section, ident);
      if (secIndex >= 0)
      {
        lineList.RemoveAt(secIndex);
      }
    }

    /// <summary>
    /// 删除连续的两行空白行
    /// </summary>
    private void DelBlankLine()
    {
      for (int i = 1; i < lineList.Count; )
      {
        string preLine = lineList[i - 1].Trim();
        string line = lineList[i].Trim();

        if (preLine.Length <= 0 && line.Length <= 0)
        {
          lineList.RemoveAt(i);
        }
        else
        {
          ++i;
        }
      }
    }

    public bool GetIdent(string identvalue, ref string ident)
    {
      int eqIndex = identvalue.IndexOf("=");
      if (eqIndex < 0)  //在sec之后发现非法的行,则抛异常
      {
        ident = "";
        return false;
      }
      ident = identvalue.Substring(0, eqIndex);
      return true;
    }

    public  bool GetValue(string identvalue, ref string value)
    {
      int eqIndex = identvalue.IndexOf("=");
      if (eqIndex < 0)
      {
        value = "";
        return false;
      }
      value = identvalue.Substring(eqIndex + 1);
      return true;
    }

    /// <summary>
    /// 找出section在列表中的下标号，如果没有找到，则返回-1.
    /// </summary>
    /// <param name="startIndex"></param>
    /// <param name="section"></param>
    /// <returns></returns>
    private int GetSectionIndex(string section)
    {
      for (int i = 0; i < lineList.Count; ++i)
      {
        string line = lineList[i];
        if (line.StartsWith("[") && line.EndsWith("]"))
        {
          if (line.Substring(1, line.Length - 2).Equals(section))
          {
            return i;
          }
        }
      }
      return -1;
    }

    /// <summary>
    /// 从startIndex下标开始，找到下一个section的位置。如果没有找到，就返回-1。
    /// </summary>
    /// <param name="startIndex"></param>
    /// <returns></returns>
    private int GetNextSection(int startIndex)
    {
      for (int i = startIndex; i < lineList.Count; ++i)
      {
        string line = lineList[i];
        if (line.StartsWith("[") && line.EndsWith("]"))
        {
          return i;
        }
      }
      return -1;
    }

    /// <summary>
    /// 获取Ident在列表中的下标,如果没有找到，则返回-1
    /// </summary>
    /// <param name="section"></param>
    /// <param name="ident"></param>
    /// <returns></returns>
    private int GetIdentIndex(string section, string ident)
    {
      int secIndex = GetSectionIndex(section);
      if (secIndex < 0)
      {
        return -1;
      }

      int secNextIndex = GetNextSection(secIndex + 1);
      int secEndIndex = secNextIndex - 1;
      if (secNextIndex < 0)
      {
        secEndIndex = lineList.Count - 1;
      }

      for (int i = secIndex; i <= secEndIndex; ++i)
      {
        string line = lineList[i];
        string strIdent = "";
        if (GetIdent(line, ref strIdent))
        {
          if (strIdent.Equals(ident))
          {
            return i;
          }
        }
      }

      return -1;
    }
        /// <summary>
        /// 保存数据到文件
        /// </summary>
        /// <param name="pathfile"></param>
        public void SaveToFile(string pathfile)
        {
            File.WriteAllLines(pathfile, ToIniStrings(), Encoding.Default);
        }
        /// <summary>
        /// 从文件中加载数据
        /// </summary>
        /// <param name="pathfile"></param>
        public void LoadFromFile(string pathfile)
        {
            string[] lines = File.ReadAllLines(pathfile, Encoding.UTF8);
            ParseFromStrings(lines);
        }
        public void WriteInt(string section, string ident, int value)
        {
            AddOrReplace(section, ident, value.ToString());
        }
        public void WriteStr(string section, string ident, string value)
        {
            AddOrReplace(section, ident, value);
        }



    }
}
