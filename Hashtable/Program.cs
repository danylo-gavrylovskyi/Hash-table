using System.Runtime.CompilerServices;

public class StringsDictionary
{
    private int InitialSize = 10;

    private LinkedList[] buckets = new LinkedList[InitialSize];

    private int NumItems = 0;

    private void RezisingArray()
    {
        this.InitialSize *= 2;
        LinkedList[] NewBuckets = new LinkedList[InitialSize];

        foreach (var Array in NewBuckets)
        {
            if (Array)
            {
                foreach (var item in Array)
                {
                    int index = CalculateHash(item, NewBuckets.Length);
                    if (NewBuckets[index])
                    {
                        NewBuckets[index].Push([item[0], item[1]]);
                    }
                    else
                    {
                        NewBuckets[index] = [[item[0], item[1]]];
                    }
                }
            }
        }

        this.buckets = NewBuckets;
    }

    public void Add(string key, string value)
    {
        this.NumItems++;
        double LoadFactor = this.NumItems/ this.InitialSize;

        if (LoadFactor > 0.8)
        {
            this.RezisingArray();
        }

        int index = CalculateHash(key, this.buckets.Length());
        if (buckets[index])
        {
            this.buckets[index].Push([key, value]);
        }
        else
        {
            this.buckets[index] = [[key, value]];
        }
    }

    public void Remove(string key)
    {

    }

    public string Get(string key)
    {
        int index = CalculateHash(key, this.buckets.Length());

        if (!buckets[index])
        {
            return "Error! There is no such key";
        }

        for (int i = 0; i < buckets[index].Length(); i++)
        {
            if (i[0] == key)
            {
                return buckets[index][i][1];
            }
        }
    }


    private int CalculateHash(string key, int tableLength)
    {
        int hash = 19;

        for (int i = 0; i < key.Length(); i++)
        {
            hash *= (15 * hash * key[i]) % tableLength;
        }

        return hash
    }
}