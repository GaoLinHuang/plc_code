using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Base;

namespace ReagentSeparationCupCode.Views
{
    internal class ExperimentViewModel:NotifyBase
    {
        public ExperimentViewModel()
        {
            ExperimentItems=new ObservableCollection<ExperimentItem>();
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
