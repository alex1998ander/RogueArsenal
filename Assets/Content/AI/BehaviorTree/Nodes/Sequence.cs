using System.Collections.Generic;

namespace BehaviorTree
{
    /// <summary>
    /// Represents a composite node, which acts like an AND-logic gate:
    /// Only evaluates successfully if all child nodes do so too.
    /// </summary>
    public class Sequence : Node
    {
        public Sequence(List<Node> children) : base(children)
        {
        }

        /// <summary>
        /// Determines whether all child nodes evaluated successfully.
        /// If even one fails, set this node to failed.
        /// If one is still running, set this node to running.
        /// </summary>
        /// <returns>The NodeState of the Sequence.</returns>
        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                    {
                        state = NodeState.FAILURE;
                        return state;
                    }
                    case NodeState.SUCCESS:
                    {
                        continue;
                    }
                    case NodeState.RUNNING:
                    {
                        anyChildIsRunning = true;
                        continue;
                    }
                    default:
                    {
                        state = NodeState.SUCCESS;
                        return state;
                    }
                }
            }

            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }
    }
}