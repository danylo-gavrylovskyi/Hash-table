public class StringsDictionary
{
    private const int InitialSize = 10;

    private LinkedList[] buckets = new LinkedList[InitialSize];

    private int NumItems = 0;

    public void Add(string key, string value)
    {
        this.NumItems++;
        double LoadFactor = this.NumItems/ this.InitialSize;

        if (LoadFactor > .8)
        {
            //resize array
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