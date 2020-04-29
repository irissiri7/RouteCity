using System;

namespace ClassLibrary
{
    internal class Node<T>
    {
        internal T Value { get; set; }
        internal Node<T> RightChild { get; set; }
        internal Node<T> LeftChild { get; set; }
        internal Node<T> Parent { get; set; }
    }

    public class PriorityQueue<T> where T : IComparable<T>
    {
        private Node<T> root = null;
        private int count = 0;

        private Node<T> GetNode(int index, bool getParent)
        {
            // Figuring out which path to go in the binary tree. 
            // Ex: 9 is 1001 in binary.
            string indexInBinary = Convert.ToString(index, 2);
            
            // Below 1001 becomes 001 and that is the path we want to use to traverse the tree.
            string treePath = indexInBinary.Substring(1, indexInBinary.Length - 1);

            // If we want the parent of node 9 then only 00 is relevant.
            int destination = getParent ? treePath.Length - 1 : treePath.Length;
            
            // Traversing the tree
            // 0 means go to the LeftChild and 1 means go to RightChild. So the node at index 9
            // is left, left then right (001). 
            Node<T> current = root;
            for (int step = 0; step < destination; step++)
            {
                if (treePath[step] == '0')
                {
                    current = current.LeftChild;
                }
                else
                {
                    current = current.RightChild;
                }
            }

            return current;
        }

        /// <summary>
        /// Adds a new value to the priority queue
        /// </summary>
        /// <param name="value">The value to be added</param>
        public void Add(T value)
        {
            if (count == 0)
            {
                root = new Node<T> { Value = value };
            }
            else
            {
                // Getting the parent of the new node, not the new node itself, 
                // because "current = new Node<T>" won't work. The new node needs to
                // actually be set as the child of the parent.
                Node<T> parentToNewNode = GetNode(count + 1, true);

                // Even numbers always go to the right. 
                if ((count + 1) % 2 == 0)
                {
                    parentToNewNode.LeftChild = new Node<T> { Value = value };
                    parentToNewNode.LeftChild.Parent = parentToNewNode;
                    SortUp(parentToNewNode.LeftChild);
                }
                else
                {
                    parentToNewNode.RightChild = new Node<T> { Value = value };
                    parentToNewNode.RightChild.Parent = parentToNewNode;
                    SortUp(parentToNewNode.RightChild);
                }
            }

            count++;
        }

        /// <summary>
        /// Return the count of elements in the priority queue.
        /// </summary>
        /// <returns>The number of elements in the priority queue</returns>
        public int Count()
        {
            return count;
        }

        /// <summary>
        /// Returns the element with the highest priority in the queue.
        /// 
        /// This method should throw an InvalidOperationException if the queue is empty
        /// </summary>
        /// <returns>The element with the highest priority in the queue</returns>
        public T Peek()
        {
            if (count != 0)
            {
                return root.Value;
            }
            else
            {
                throw new InvalidOperationException("Cannot peek an empty list");
            }
        }

        /// <summary>
        /// Returns the element with the highest priority, and also removed it from the priority queue.
        /// 
        /// This method should throw an InvalidOperationException if the queue is empty
        /// </summary>
        /// <returns>The element with the highest priority</returns>
        public T Pop()
        {
            if (count != 0)
            {
                T value = root.Value;
                RemoveTop();
                return value;
            }
            else
            {
                throw new InvalidOperationException("Cannot pop an empty list");
            }
 
        }

        /// <summary>
        /// Removes the element at the front of the line in the list. 
        /// </summary>
        private void RemoveTop()
        {

            Node<T> parentToLastNode = GetNode(count, true);

            if (count == 1)
            {
                root = null;
            }
            // Go to the correct child, transfer it's value to root, nullify that child then sort
            // the new root value to where it's supposed to be. 
            else if (count % 2 == 0)
            {
                root.Value = parentToLastNode.LeftChild.Value;
                parentToLastNode.LeftChild = null;
                SortDown(root);
            }
            else
            {
                root.Value = parentToLastNode.RightChild.Value;
                parentToLastNode.RightChild = null;
                SortDown(root);
            }

            count--;
            
        }

        /// <summary>
        /// Gets element at a zero based index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="getParent"></param>
        /// <returns></returns>
        public T GetValueByIndex(int index)
        {
            Node<T> node = GetNode(index + 1, false);

            return node.Value;
        }

        /// <summary>
        /// Removes element at a zero based index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="minimumValue"></param>
        public void RemoveAt(int index)
        {
            Node<T> current = GetNode(index + 1, false);
            MoveToTop(current);
            RemoveTop();
        }

        /// <summary>
        /// Moves a specific element through the list to the top
        /// </summary>
        /// <param name="current"></param>
        private void MoveToTop(Node<T> current)
        {
            if (current.Parent == null)
            {
                return;
            }
            else
            {
                var tmp = current.Value;
                current.Value = current.Parent.Value;
                current.Parent.Value = tmp;
                MoveToTop(current.Parent);
            }
        }

        /// <summary>
        /// Recursive sorting method going from a certain position in the list and moves upward. 
        /// </summary>
        /// <param name="current"></param>
        private void SortUp(Node<T> current)
        {
            if (current.Parent == null)
            {
                return;
            }
            else if (current.Value.CompareTo(current.Parent.Value) == -1)
            {
                var tmp = current.Value;
                current.Value = current.Parent.Value;
                current.Parent.Value = tmp;
                SortUp(current.Parent);

            }
            else
            {
                SortUp(current.Parent);
            }
        }

        /// <summary>
        /// Recursive sorting method going from a certain position in the list and moves downward. 
        /// </summary>
        /// <param name="current"></param>
        private void SortDown(Node<T> current)
        {
            // If we are at the bottom of the list then return. 
            if (current.LeftChild == null && current.RightChild == null)
            {
                return;
            }
            // Either go to the only child or the child with the smallest value. 
            else if (current.RightChild == null || current.LeftChild.Value.CompareTo(current.RightChild.Value) == -1)
            {
                current = current.LeftChild;
            }
            else if (current.LeftChild == null || current.RightChild.Value.CompareTo(current.LeftChild.Value) == -1)
            {
                current = current.RightChild;
            }
            // If both children have the same value, then it doesn't matter what child it goes to. 
            else if (current.RightChild.Value.CompareTo(current.LeftChild.Value) == 0)
            {
                current = current.LeftChild;
            }

            // The current is not root, so we can start comparing.
            if (current.Value.CompareTo(current.Parent.Value) == -1)
            {
                var tmp = current.Parent.Value;
                current.Parent.Value = current.Value;
                current.Value = tmp;
            }

            SortDown(current);
        }
    }
}
