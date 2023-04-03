using TMPro;
using UnityEngine;

namespace CodeBase.Logic.Timer
{
    public class Lap : MonoBehaviour
    {
        [SerializeField] private TMP_Text _number;
        [SerializeField] private TMP_Text _time;

        public void SetText(string number, string time)
        {
            _number.text = number;
            _time.text = time;
        }
    }
}