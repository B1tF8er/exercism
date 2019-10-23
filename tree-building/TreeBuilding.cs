using System;
using System.Collections.Generic;
using System.Linq;

public class TreeBuildingRecord
{
    public int ParentId { get; set; }
    public int RecordId { get; set; }
}

public class Tree
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public List<Tree> Children { get; set; }
    public bool IsLeaf => Children.Count == 0;
}

public static class TreeBuilder
{
    public static Tree BuildTree(IEnumerable<TreeBuildingRecord> records)
        => records
            .Sort()
            .CreateTrees()
            .TryGetRoot();

    private static IEnumerable<TreeBuildingRecord> Sort(this IEnumerable<TreeBuildingRecord> records)
    {
        var ordered = new SortedList<int, TreeBuildingRecord>();

        foreach (var record in records)
            ordered.Add(record.RecordId, record);

        records = ordered.Values;
        return records;
    }

    private static List<Tree> CreateTrees(this IEnumerable<TreeBuildingRecord> records)
    {
        var trees = new List<Tree>();
        var previousRecordId = -1;

        foreach (var record in records)
        {
            var tree = new Tree
            {
                Children = new List<Tree>(),
                Id = record.RecordId,
                ParentId = record.ParentId
            };
            trees.Add(tree);

            if (IsInvalidId(previousRecordId, tree))
                throw new ArgumentException("Invalid id");

            ++previousRecordId;
        }

        return trees;
    }

    private static bool IsInvalidId(int previousRecordId, Tree t)
        => (t.Id == 0 && t.ParentId != 0) ||
            (t.Id != 0 && t.ParentId >= t.Id) ||
            (t.Id != 0 && t.Id != previousRecordId + 1);

    private static Tree TryGetRoot(this List<Tree> trees)
    {
        if (!trees.Any())
            throw new ArgumentException("Trees is empty");

        for (var index = 1; index < trees.Count; index++)
        {
            var tree = trees.First(x => x.Id == index);
            var parent = trees.First(x => x.Id == tree.ParentId);
            parent.Children.Add(tree);
        }

        return trees.First(t => t.Id == 0);
    }
}