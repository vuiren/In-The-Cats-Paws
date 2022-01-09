using System;
using Map_Generator_Project.Scripts.MonoBehaviours;

namespace Map_Generator_Project.Scripts.Data
{
    [Serializable]
    public class Corridor
    {
        public bool locked;
        public Cell cell1, cell2;
    }
}