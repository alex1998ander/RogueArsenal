namespace BehaviorTree
{
    /// <summary>
    /// </summary>
    public class Succeeder : Node
    {
        public Succeeder(Node child) : base(child)
        {
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override NodeState Evaluate()
        {
            Node child = children[0];
            child.Evaluate();
            state = NodeState.SUCCESS;
            return state;
        }
    }
}