using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace learning
{
    [Serializable]
    [XmlRoot("NodeTree")]
    public class Node
    {
        [XmlElement("id")]
        public int id;

        [XmlArray("Node")]
        [XmlArrayItem("NodeStructure")]
        public List<NodeStructure> Nodes;

        public void AddStructure(int id, string text, string name)
        {
            Nodes.Add(new NodeStructure(id, text, name));
        }

        public void InsertOptions(int nodeId, string text, int toNodeId)
        {

            var options = Nodes.Find(x => x.id == nodeId);
            options.AddOptions(toNodeId, text);
        }

        public void AddDialogueText(int node_id, string text)
        {
            var conver = Nodes.Find(x => x.id == node_id);
            conver.AddConversations(text);
        }

        public void ViewAllNode()
        {
            foreach (var obj in Nodes)
            {
                Console.WriteLine("Id: " + obj.id + ";" + " Name: " + obj.charName + ";" + " Text: " + obj.text + ";\r\n");
                foreach (var element in obj.options)
                {
                    Console.WriteLine("toNodeID: " + element.toNodeId + " Text: " + element.text + "");
                }
            }

        }

        public void ToNode(int node_id)
        {
            var node = Nodes.Find(x => x.id == node_id);
            Console.WriteLine("Id: " + node.id + ";" + " Name: " + node.charName + ";" + " Text: " + node.text + ";\r\n");
            foreach (var text in node.conversations)
            {
                Console.WriteLine(text);
            }
            foreach (var elem in node.options)
            {
                Console.WriteLine("toNodeID: " + elem.toNodeId + " Text: " + elem.text + "");
            }
        }


        public Node() { }
        public Node(int id)
        {
            this.id = id;
            this.Nodes = new List<NodeStructure>();
        }
    }

    [XmlType("NodeStructure")]
    public class NodeStructure
    {
        [XmlAttribute]
        public int id;
        [XmlElement("text")]
        public string text;
        [XmlArray("Conversations")]
        [XmlArrayItem("Conversation")]
        public List<string> conversations;
        [XmlAttribute]
        public string charName;
        [XmlArray("Options")]
        [XmlArrayItem("Option")]
        public List<StructureOptions> options;

        public void AddOptions(int id, string text)
        {
            this.options.Add(new StructureOptions(id, text));
        }
        public void AddConversations(string text)
        {
            conversations.Add(text);
        }
        public NodeStructure() { }
        public NodeStructure(int id, string text, string charName)
        {
            this.id = id;
            this.text = text;
            this.charName = charName;
            this.options = new List<StructureOptions>();
            this.conversations = new List<string>();
        }
    }

    [XmlType("StructureOptions")]
    public class StructureOptions
    {
        //[XmlElement("toNodeId")]
        [XmlAttribute]
        public int toNodeId;
        [XmlElement("text")]
        public string text;

        public StructureOptions() { }
        public StructureOptions(int toId, string textopt)
        {
            toNodeId = toId;
            text = textopt;
        }
    }

    class Manager
    {
        static void Main(string[] args)
        {
            Node node = new Node(1);

            node.AddStructure(1, "Hello im node text", "Jhon");

            node.AddDialogueText(1, "Как твои дела?");
            node.AddDialogueText(1, "Что делаешь??");

            node.AddStructure(2, "Hi, me to, yes", "Micel");
            node.AddStructure(0, "Bye", "Jhon");
            node.AddStructure(0, "Bye", "Micel");

            node.InsertOptions(1, "U want some water?", 2);
            node.InsertOptions(1, "Bye", 4);

            node.InsertOptions(2, "[Take water]", 3);
            node.InsertOptions(2, "Bye", 4);

            node.ViewAllNode();

            XmlSerializer formatter = new XmlSerializer(typeof(Node));//, nodeType);
            using (FileStream fs = new FileStream(@"C:\Work\nodes.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, node);

                Console.WriteLine("Объект сериализован");
            }


            Console.WriteLine("-----------------------");
            node.ToNode(1);
            while (true)
            {

                Console.WriteLine("Write node id \r\n");
                var putted = Console.ReadLine();
                if (Int32.Parse(putted) > 4)
                {
                    Console.WriteLine("End of Dialog");
                }
                else
                {
                    node.ToNode(Int32.Parse(putted));
                }

            }

            Console.ReadKey();
        }
    }

}
