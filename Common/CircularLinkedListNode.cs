namespace Common
{
    public static class CircularLinkedListNode
    {
        public static LinkedListNode<int> NextCircular(this LinkedListNode<int> node, int count = 1)
        {
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
        public static LinkedListNode<int> PreviousCircular(this LinkedListNode<int> node, int count = 1)
        {
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
