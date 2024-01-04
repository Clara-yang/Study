// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!")
using System.Xml;

class Program
{
    static void Main(string[] args)
    {
        string xmlFilePath = "XMLFile1.xml"; //Server.MapPath(@"相对路径如/xml/test.xml");
        XmlDocument doc = new XmlDocument();
        doc.Load(xmlFilePath);//加载XML文件
        //XmlNode root = doc.SelectSingleNode("//Activity");//当节点Workflow带有属性是，使用SelectSingleNode无法读取  
        //if (root != null)
        //{
        //    string ActivityId = (root.SelectSingleNode("ActivityId")).InnerText;
        //    string ActivityName = (root.SelectSingleNode("ActivityName")).InnerText;
        //    string ActivityLevel = root.SelectSingleNode("ActivityLevel").InnerText;
        //    Console.WriteLine("ActivityId:" + ActivityId + "/n"+"ActivityName:" + ActivityName + "/n"+"ActivityLevel:" + ActivityLevel);
        //}
        //else
        //{
        //    Console.WriteLine("the node  is not existed");
        //    //Console.Read();  
        //}
                
        XmlNodeList xnList = doc.SelectNodes("//Activity");
        foreach (XmlNode xn in xnList)
        {
            //无法使用xn["ActivityId"].InnerText  
            string ActivityId = (xn.SelectSingleNode("ActivityId")).InnerText;
            string ActivityName = xn.SelectSingleNode("ActivityName").InnerText;
            string ActivityLevel = xn.SelectSingleNode("ActivityLevel").InnerText;
            //  Console.WriteLine("ActivityId:" + ActivityId + "/nActivityName:" + ActivityName + "/nActivityLevel:" + ActivityLevel);  
            Console.WriteLine("ActivityId:  {0}/nActivityName:  {1}/nActivityLevel:  {2}", ActivityId, ActivityName, ActivityLevel);
        }
    }
}
