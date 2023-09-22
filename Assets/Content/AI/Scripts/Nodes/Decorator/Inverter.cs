namespace BehaviorTree
{
    /// <summary>
    /// Represents a composite node, which acts like a NOT-logic gate:
    /// Evaluates successfully if its child node does not and vice versa.
    /// </summary>
    public class Inverter : Node
    {
        public Inverter(Node child) : base(child)
        {
        }

        /// <summary>
        /// Inverts the state of the child node.
        /// If it succeeds, set this node to failure.
        /// If it fails, set this node to success.
        /// If it is running, set this node to running.
        /// </summary>
        /// <returns>The NodeState of the Inverter.</returns>
        public override NodeState Evaluate()
        {
            Node child = children[0];
            NodeState childState = child.Evaluate();
            switch (childState)
            {
                case NodeState.FAILURE:
                {
                    state = NodeState.SUCCESS;
                    break;
                }
                case NodeState.SUCCESS:
                {
                    state = NodeState.FAILURE;
                    break;
                }
                case NodeState.RUNNING:
                {
                    state = NodeState.RUNNING;
                    break;
                }
            }

            return state;
        }
    }
}