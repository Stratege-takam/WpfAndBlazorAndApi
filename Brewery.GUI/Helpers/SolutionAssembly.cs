using System.Linq;
using System.Reflection;

namespace Brewery.GUI.Helpers
{

    /// <summary>
    /// 
    /// </summary>
    public static class SolutionAssembly
    {
        /// <summary>
        /// 
        /// </summary>
        public static string Gui { get; set; } = "Brewery.GUI";

        /// <summary>
        /// 
        /// </summary>
        public static string ViewModel { get; set; } = "Brewery.VM";
        
        /// <summary>
        /// 
        /// </summary>
        public static string Bl { get; set; } = "Brewery.BL.Client";
        

        /// <summary>
        /// 
        /// </summary>
        public static string Core { get; set; } = "Elia.Core";

        /// <summary>
        /// 
        /// </summary>
        public static string Share { get; set; } = "Elia.Share.WPF";

        /// <summary>
        /// 
        /// </summary>
        public static Assembly[] GetAllAssemblies => new string[]
        {
            Core,
            Share,
            Bl,
            ViewModel,
            Gui
        }.Select(s => Assembly.Load(s)).ToArray();

    }
}
