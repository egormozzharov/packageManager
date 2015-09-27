using PackageManager.Models;
using PackageManager.Tasks.Common;

namespace PackageManager.Tasks
{
	internal class ImportTask : BaseTask
	{
		//private readonly Import import;

		public ImportTask(Import import) : base(import.FailOnError)
		{
			//this.import = import;
		}

		protected override void ExecuteTask()
		{
			//TODO
		}
	}
}
