namespace Core{

    [System.Serializable]
    public class Data
    {
        public Data(bool b, float f){
            isCorrecting = b;
            percent =f;
        }

        public void SetData(Data data){
            isCorrecting =data.isCorrecting;
        }

        public bool isCorrecting;
        public float percent;
    }

    public enum FISH_TYPE{
        Mackerel = 0,
        Infected_Mackerel,
        Salmon,
        Infected_Salmon,
        Shark,
        Infected_Shark,
    }
};