namespace Irydae.Services
{
    public class ModificationStatusService
    {
        private ModificationStatusService()
        {
            Dirty = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ModificationStatusService"/> is dirty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if dirty; otherwise, <c>false</c>.
        /// </value>
        public bool Dirty { get; set; }

        private static ModificationStatusService instance;

        public static ModificationStatusService Instance
        {
            get { return instance ?? (instance = new ModificationStatusService()); }
        }
    }
}