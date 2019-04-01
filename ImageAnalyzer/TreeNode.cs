using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageAnalyzer
{
    public class TreeNode
    {
        private string localData;
        private int instanceCount;
        public TreeNode less;
        public TreeNode greater;

        public TreeNode()
        {
            localData = null;
            instanceCount = 0;
        }
        public void AddChild(string data) //Part of this seems broken
        {
            if (localData == null) //If this node was just created
            {
                this.localData = data;
                this.instanceCount = 1;
            }
            else
            {
                if (this.localData.CompareTo(data) > 0)
                {
                    if (less == null)
                    {
                        less = new TreeNode();
                    }

                    less.AddChild(data);
                }

                else if (this.localData.CompareTo(data) < 0)
                {
                    if (greater == null)
                    {
                        greater = new TreeNode();
                    }

                    greater.AddChild(data);
                }

                else
                {
                    instanceCount++;
                }
            }
        }

        public int GetInstanceCount()
        {
            return instanceCount;
        }

        public string GetLocalData()
        {
            return localData;
        }
    }
}
