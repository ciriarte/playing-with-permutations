using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scopely_challenge
{
    using Node = Tuple<Int32, String, String>;
    static
    class TreeExtensions
    {
        static
        public HashSet<Node> BuildTree(this String path, Func<String, String[]> strategy = null)
        {
            Contract.Requires(path != null, String.Format(_mask, "path"));
            if (strategy == null) {
                strategy = t => t.Split('|');
            }
            var levels = path.Split('/')
                             .Where(token => !String.IsNullOrEmpty(token))
                             .Select(token => strategy(token) )
                             .ToArray();
            var tree =
                levels.
                SelectMany((level, i) => {
                    var results =
                      level.Select(n => Tuple.Create(i, i > 0 ? levels[i - 1][0] : null, n));

                    IEnumerable<Node> nodes = null;
                    if (i > 0)
                        nodes = Expand(i, levels[i-1], level);

                    return nodes ?? results;
                 });

            return new HashSet<Node>(tree);
        }

        static
        public IEnumerable<Node> Insert(this HashSet<Node> tree, String path, Func<String, String[]> strategy = null)
        {
            Contract.Requires(tree != null, String.Format(_mask, "tree"));
            Contract.Requires(tree != null, String.Format(_mask, "path"));
            if (strategy == null) {
                strategy = t => t.Split('|');
            }

            var other = path.BuildTree(strategy);

            tree.UnionWith(other);

            return tree;
        }

        static
        public IEnumerable<String> Permutations(this IEnumerable<String> tokens)
        {
            Contract.Requires(tokens != null, String.Format(_mask, "tokens"));

            foreach (var token in tokens) {
                var set = Permutations(tokens.Where(t => t != token));
                foreach (var item in set)
                {
                    yield return String.Join("-", item);
                }
            }

            if (tokens.Any()) {
                yield return String.Join("-", tokens);
            }
        }

        static
        public String Collapse(this IEnumerable<Node> tree)
        {
            Contract.Requires(tree != null, String.Format(_mask, "tree"));

            var result = new StringBuilder();
            var orderedNodes =
            tree.Where(n => !n.Item3.Contains("-"));

            var deepest = tree.Max(n => n.Item1);

            for (int i = 0; i <= deepest; i++)
            {
                result.Append("/");
                var leaves = orderedNodes.Where(n => n.Item1 == i)
                                         .Select(n => n.Item3.Trim())
                                         .Distinct()
                                         .ToArray();
                result.Append(String.Join("|",leaves));
            }

            return result.ToString();
        }

        static
        public String FindFirstSynonym(this IEnumerable<Node> tree, String path)
        {
            Contract.Requires(tree != null, String.Format(_mask, "tree"));
            Contract.Requires(path != null, String.Format(_mask, "path"));
            Contract.Requires(path.Contains("|"), "Doesn't support combinatorials yet");

            var result = "Synonym not found";
            var other  = path.BuildTree();

            var depth = other.Max(n => n.Item1);
            var node  = other.First(n => n.Item1 == depth);

            var children = tree.Where(n => n.Item2 == node.Item3);
            var potentialSynonyms = tree.Where(n => n.Item3 == children.First().Item3);

            var maybeSynonyms = potentialSynonyms.Where(n => n.Item2 != node.Item3)
                                                 .Select(n => n);

            var childrenSet = new SortedSet<String>(children.Select(n => n.Item3));
            foreach (var item in maybeSynonyms)
            {
                var otherChildren = tree.Where(n => n.Item2 == item.Item2)
                                        .Select(o => o.Item3);
                var otherChildrenSet = new SortedSet<String>(otherChildren);

                if (otherChildrenSet.SequenceEqual(childrenSet))
                {
                    var miniTree = new SortedSet<Node>();
                    miniTree.Add(Tuple.Create(0, String.Empty, "home"));
                    miniTree.Add(Tuple.Create(item.Item1 - 1, "home", item.Item2));
                    result = miniTree.Collapse();
                    break;
                }
            }

            return result;
        }

        static
        public void Display(this IEnumerable<Node> tree)
        {
            Contract.Requires(tree != null, String.Format(_mask, "tree"));
            foreach (var node in tree)
            {
                Console.WriteLine("{0} => {1}:{2}", node.Item1, node.Item2, node.Item3);
            }
            Console.WriteLine();
        }

        static IEnumerable<Node> Expand(Int32 level, string[] parents, string[] children)
        {
            foreach (var parent in parents)
            {
                foreach (var child in children)
                {
                    yield return Tuple.Create(level, parent, child);
                }
            }
        }

        const String _mask = "{0} must not be null";
    }
}
