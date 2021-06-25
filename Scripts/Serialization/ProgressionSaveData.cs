using System;

namespace PV3.Serialization
{
    [Serializable]
    public class ProgressionSaveData
    {
        public StageData StageData;

        public ProgressionSaveData()
        {
            StageData = new StageData();
        }
    }

    [Serializable]
    public class StageData
    {
        public int ChosenStage;
        public int HighestStageAvailable;

        public StageData(int chosenStage = 1, int highestStageAvailable = 1)
        {
            ChosenStage = chosenStage;
            HighestStageAvailable = highestStageAvailable;
        }
    }
}