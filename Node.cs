using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learning
{
    class Node
    {
        public int id;
        public static List<NodeStructure> Nodes = new List<NodeStructure>();

        public void AddStructure(int id, string text, string name)
        {
            var obj = new NodeStructure(id, text, name);
            Nodes.Add(obj);
        }
        public void ViewAllNode()
        {
            foreach (var obj in Nodes)
            {
                Console.WriteLine("Id: " + obj.id + ";" + " Name: " + obj.charName + ";" + " Text: " + obj.text + ";");
            }
        }

        public Node() { }
        public Node(int id) { this.id = id; }
    }

    class NodeStructure
    {
        public int id;
        public string text;
        public string charName;
        public List<StructureOptions> options = new List<StructureOptions>();

        public void AddOptions(int id, string text)
        {
            this.options.Add(new StructureOptions(id, text));
        }
        public NodeStructure() { }
        public NodeStructure(int id, string text, string charName)
        {
            this.id = id;
            this.text = text;
            this.charName = charName;
        }
    }

    class StructureOptions
    {
        public int toNodeId;
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

            node.AddStructure(1, "bla", "Jhon");
            node.ViewAllNode();
            Console.ReadKey();
        }
    }

}
