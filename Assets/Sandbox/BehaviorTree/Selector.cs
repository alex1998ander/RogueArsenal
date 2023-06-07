using System.Collections.Generic;

namespace BehaviorTree
{
    /// <summary>
    /// Represents a composite node, which acts like an OR-logic gate:
    /// Evaluates successfully if any child node does so too.
    /// </summary>
    public class Selector : Node
    {
        public Selector() : base()
        {
        }

        public Selector(List<Node> children) : base(children)
        {
        }

        /// <summary>
        /// Determines whether any child node evaluated successfully or is still running.
        /// If one succeeds, set this node to success.
        /// If one is running, set this node to running.
        /// If all fail, set this node to fail.
        /// </summary>
        /// <returns>The NodeState of the Selector.</returns>
        public override NodeState Evaluate()
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                    {
                        continue;
                    }
                    case NodeState.SUCCESS:
                    {
                        state = NodeState.SUCCESS;
                        return state;
                    }
                    case NodeState.RUNNING:
                    {
                        state = NodeState.RUNNING;
                        return state;
                    }
                    default:
                    {
                        continue;
                    }
                }
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}