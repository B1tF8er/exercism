
using System;
using System.Collections.Generic;
using System.Linq;

public enum Bucket
{
    One,
    Two
}

public class TwoBucketResult
{
    public int Moves { get; set; }
    public Bucket GoalBucket { get; set; }
    public int OtherBucket { get; set; }
}

public class TwoBucket
{
    private int[] buckets;
    private int startBucket;

    public TwoBucket(int bucketOne, int bucketTwo, Bucket startBucket)
    {
        this.buckets = new[] { bucketOne, bucketTwo };
        this.startBucket = (int)startBucket;
    }

    public TwoBucketResult Measure(int goal)
    {
        var (moves, goalBucket, buckets) = GetBucketResult(goal);

        return new TwoBucketResult
        {
            Moves = moves,
            GoalBucket = (Bucket)goalBucket,
            OtherBucket = buckets[1 - goalBucket]
        };
    }

    public (int moves, int goalBucket, int[] buckets) GetBucketResult(int goal)
    {
        var invalid = new[] { 0, 0 };
        invalid[1 - startBucket] = this.buckets[1 - startBucket];

        var invalidStr = string.Join(",", invalid);
        var buckets = new[] { 0, 0 };

        buckets[startBucket] = this.buckets[startBucket];

        var toVisit = new Queue<(int[], int)>();
        var visited = new HashSet<string>();
        var count = 1;
        var goalBucket = Array.IndexOf(buckets, goal);

        while (goalBucket < 0)
        {
            var key = string.Join(",", buckets);

            if (!visited.Contains(key) && !key.Equals(invalidStr))
            {
                visited.Add(key);
                var nc = count + 1;

                for (int i = 0; i < 2; i++)
                {
                    if (buckets[i] != 0)
                        toVisit.Enqueue((Empty(buckets, i), nc));

                    if (buckets[i] != this.buckets[i])
                    {
                        toVisit.Enqueue((Fill(buckets, i), nc));
                        toVisit.Enqueue((Consolidate(buckets, i), nc));
                    }
                }
            }

            if (!toVisit.Any())
                throw new ArgumentException("no more moves!");

            (buckets, count) = toVisit.Dequeue();
            goalBucket = Array.IndexOf(buckets, goal);
        }

        return (count, goalBucket, buckets);
    }

    private int[] Empty(int[] buckets, int index) =>
        index == 0
            ? new[] { 0, buckets[1] }
            : new[] { buckets[0], 0 };

    private int[] Fill(int[] buckets, int index) =>
        index == 0
            ? new[] { this.buckets[0], buckets[1] }
            : new[] { buckets[0], this.buckets[1] };

    private int[] Consolidate(int[] buckets, int index)
    {
        var amount = (new[] { buckets[1 - index], this.buckets[index] - buckets[index] }).Min();
        var target = buckets[index] + amount;
        var source = buckets[1 - index] - amount;

        return index == 0
            ? new[] { target, source }
            : new[] { source, target };
    }
}