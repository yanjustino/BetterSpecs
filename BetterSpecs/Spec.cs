namespace BetterSpecs
{
    public abstract class Spec
    {
        public Subject Subject { get; } = new Subject();
        public It It { get; } = new It();
        public Context Context { get; } = new Context();
        public Describe Describe { get; } = new Describe();
        public Let Let { get; } = new Let();
    }
}
