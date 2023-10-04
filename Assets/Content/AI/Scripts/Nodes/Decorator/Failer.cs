namespace BehaviorTree
{
    /// <summary>
    /// </summary>
    public class Failer : Node
    {
        public Failer(Node child) : base(child)
        {
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override NodeState Evaluate()
        {
            Node child = children[0];
            child.Evaluate();
            state = NodeState.FAILURE;
            return state;
        }
    }
}