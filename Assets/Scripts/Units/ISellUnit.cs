using UnityEngine.Events;

namespace Units
{
    public interface ISellUnit
    {
        public int Price { get; set; }
        public event UnityAction<int> OnBuy;
    }
}