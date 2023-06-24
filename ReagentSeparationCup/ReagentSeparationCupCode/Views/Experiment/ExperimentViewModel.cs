using System.Collections.ObjectModel;
using Windows.Base;

namespace ReagentSeparationCupCode.Views
{
    internal class ExperimentViewModel : NotifyBase
    {
        public ExperimentViewModel()
        {
            ExperimentItems = new ObservableCollection<ExperimentItem>();
            for (int i = 0; i < 4; i++)
            {
                var experimentItem = new ExperimentItem();
                experimentItem.Index = i + 1;
                ExperimentItems.Add(experimentItem);
            }
        }

        public ObservableCollection<ExperimentItem> ExperimentItems { get; set; }
    }
}