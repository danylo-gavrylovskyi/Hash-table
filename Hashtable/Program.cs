using System.Runtime.CompilerServices;
using System.Xml.Linq;

string PathToFile = "C:\\Users\\danya\\Hash-table\\Hashtable\\dictionary.txt";
var Lines = File.ReadAllLines(PathToFile);
StringsDictionary Dictionary = new StringsDictionary();

for (int i = 0; i < Lines.Length; i++)
{
    var KeyValue = Lines[i].Split("; ");
    string Word = KeyValue[0];
    string Defenition = KeyValue[1];
    Dictionary.Add(Word, Defenition);
}

Console.WriteLine(Dictionary.Get("MUREXAN"));

public class StringsDictionary
{
    private const int InitialSize = 10;
    private int InitialSizeCheck = InitialSize;

    private LinkedList[] buckets = new LinkedList[InitialSize];

    private int NumItems = 0;

    private void RezisingArray()
    {
        int newSize = this.buckets.Length * 2;
        LinkedList[] newBuckets = new LinkedList[newSize];
        foreach (LinkedList bucket in this.buckets)
        {
            if (bucket == null) continue;

            LinkedListNode currentNode = bucket._first;

            while (currentNode != null)
            {
                int newIndex = CalculateHash(currentNode.Pair.Key, newSize);
                if (newBuckets[newIndex] == null)
                {
                    newBuckets[newIndex] = new LinkedList();
                }
                newBuckets[newIndex].Add(currentNode.Pair);
                currentNode = currentNode.Next;
            }
        }
        this.buckets = newBuckets;
        this.InitialSizeCheck = newSize;
    }

    public void Add(string key, string value)
    {
        this.NumItems++;
        double LoadFactor = this.NumItems / this.InitialSizeCheck;

        if (LoadFactor > 0.8)
        {
            this.RezisingArray();
        }

        int index = CalculateHash(key, this.buckets.Length);
        if (buckets[index] == null)
        {
            this.buckets[index] = new LinkedList();
        }
        this.buckets[index].Add(new KeyValuePair(key, value));
    }

    public void Remove(string key)
    {
        int index = CalculateHash(key, this.buckets.Length);
        if (this.buckets[index] == null)
        {
            throw new Exception("There is no such key to delete");
        }
        else
        {
            this.buckets[index].RemoveByKey(key);
        }
    }

    public string Get(string key)
    {
        int index = CalculateHash(key, this.buckets.Length);

        if (buckets[index] == null)
        {
            throw new Exception("Error! There is no such key to get");
        }
        else
        {
            return this.buckets[index].GetItemWithKey(key).Value;
        }
    }


    private int CalculateHash(string key, int tableLength)
    {
        int hash = 19;

        for (int i = 0; i < key.Length; i++)
        {
            hash += (15 * key[i]) ;
        }

        return hash % tableLength;
    }
}

public class KeyValuePair
{
    public string Key { get; }

    public string Value { get; }

    public KeyValuePair(string key, string value)
    {
        Key = key;
        Value = value;
    }
}

public class LinkedListNode
{
    public KeyValuePair Pair { get; }
        
    public LinkedListNode Next { get; set; }

    public LinkedListNode(KeyValuePair pair, LinkedListNode next = null)
    {
        Pair = pair;
        Next = next;
    }
}

public class LinkedList
{
    public LinkedListNode _first;

    public void Add(KeyValuePair pair)
    {
        var new_node = new LinkedListNode(pair, null);
        if (_first == null)
        {
            _first = new_node;
        }
        else
        {
            var cur_node = _first;
            while (cur_node.Next != null)
            {
                cur_node = cur_node.Next;
            }

            cur_node.Next = new_node; 
        }
            
    }

    public void RemoveByKey(string key)
    {
        var cur_node = _first;
        var prev_node = cur_node;
        while (cur_node.Next != null)
        {
            if (cur_node.Pair.Key == key)
            {
                prev_node.Next = cur_node.Next;
                break;
            }
            prev_node = cur_node;
            cur_node = cur_node.Next;
        }
    }

    public void Print()
    {
        var cur_node = _first;
        if (cur_node == null)
        {
            return;
        }
        while (cur_node.Next != null)
        {
            Console.WriteLine(cur_node.Pair.Key);
            cur_node = cur_node.Next;
        }
    }
    
    public KeyValuePair GetItemWithKey(string key)
    {
        LinkedListNode CurrentNode = _first;
        while (CurrentNode.Next != null)
        {
            if (CurrentNode.Pair.Key == key)
            {
                break;
            }
            CurrentNode = CurrentNode.Next;
        }

        return CurrentNode.Pair;
    }
}