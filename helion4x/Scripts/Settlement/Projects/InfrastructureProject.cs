using Helion4x.Scripts.Settlement.Installation;

namespace Helion4x.Scripts.Settlement.Projects
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