using Helion4x.Core.Settlement.Installation;

namespace Helion4x.Core.Settlement.Projects
{
    public class InfrastructureProject : Project
    {
        public InfrastructureProject() : base(nameof(InfrastructureProject), 500)
        {
        }
        
        public override Installation.Installation Finish()
        {
            return new Infrastructure();
        }
    }
}