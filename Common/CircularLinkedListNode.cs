namespace Common
{
    public static class CircularLinkedListNode
    {
        public static LinkedListNode<T>? NextCircular<T>(this LinkedListNode<T>? node, int count = 1)
        {
            if (node is null)
                return null;

            if (node.List is null)
                return null;

            if (count > 1)
            {
                return NextCircular(NextCircular(node), count - 1);
            }

            if (node == node.List.Last)
            {
                return node.List.First;
            }

            return node.Next;
        }
        public static LinkedListNode<T>? PreviousCircular<T>(this LinkedListNode<T>? node, int count = 1)
        {
            if (node is null)
                return null;

            if (node.List is null)
                return null;

            if (count > 1)
            {
                return PreviousCircular(PreviousCircular(node), count - 1);
            }

            if (node == node.List.First)
            {
                return node.List.Last;
            }

            return node.Previous;
        }


    }
}
